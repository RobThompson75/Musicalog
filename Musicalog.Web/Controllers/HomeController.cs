using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Musicalog.Web.Controllers
{
    public class HomeController : Controller
    {
        private Data.Context context;

        public HomeController()
        {
            // TODO : This would normally be done via DI
            this.context = new Data.Context();
        }

        public ActionResult Index(string sortOrder, int? page)
        {
            List<Models.AlbumViewModel> viewModel = new List<Models.AlbumViewModel>();
            List<Task> tasks = new List<Task>();

            tasks.Add(Task.Run(() =>
            {
                var albums = this.context.Albums.Include("Artist").Include("MediaTypes").Include("Label").ToList();
                var mediaTypes = this.context.MediaTypes.ToList();

                Dictionary<int, string> mediaTypeItems = new Dictionary<int, string>();

                foreach (var mediaType in mediaTypes)
                {
                    mediaTypeItems.Add(mediaType.Id, mediaType.Name);
                }

                foreach (var album in albums)
                {
                    foreach(var albumMediaType in album.MediaTypes)
                    {
                        Models.AlbumViewModel viewModelItem = new Models.AlbumViewModel()
                        {
                            Id = album.Id,
                            AlbumName = album.Name,
                            Stock = album.Stock,
                            Artist = album.Artist.Name, 
                            RecordLabel = album.Label.Name, 
                            MediaType = albumMediaType.Name,
                            MediaTypes = mediaTypeItems
                        };

                        viewModel.Add(viewModelItem);
                    }
                }
            }));

            int pageSize;

            if (!int.TryParse(System.Configuration.ConfigurationManager.AppSettings["PageSize"].ToString(), out pageSize))
            {
                // Default Page Size of 10
                pageSize = 10;
            }

            Task.WaitAll(tasks.ToArray());

            ViewBag.CurrentSort = sortOrder;
            ViewBag.AlbumSortParam = string.IsNullOrEmpty(sortOrder) ? "AlbumName_Desc" : string.Empty;
            ViewBag.ArtistSortParam = sortOrder == "Artist_Asc" ? "Artist_Desc" : "Artist_Asc";

            switch(sortOrder)
            {
                case "AlbumName_Desc":
                    viewModel = viewModel.OrderByDescending(vm => vm.AlbumName).ToList();
                    break;
                case "Artist_Asc":
                    viewModel = viewModel.OrderBy(vm => vm.Artist).ToList();

                    break;
                case "Artist_Desc":
                    viewModel = viewModel.OrderByDescending(vm => vm.Artist).ToList();

                    break;
                default:
                    viewModel = viewModel.OrderBy(vm => vm.AlbumName).ToList();

                    break;
            }

            return View(viewModel.ToPagedList(page ?? 1, pageSize));
        }

        [HttpGet]
        public ActionResult Create()
        {
            var mediaTypes = this.context.MediaTypes.ToList();

            Dictionary<int, string> mediaTypeItems = new Dictionary<int, string>();

            foreach (var mediaType in mediaTypes)
            {
                mediaTypeItems.Add(mediaType.Id, mediaType.Name);
            }

            Models.AlbumViewModel viewModelItem = new Models.AlbumViewModel() { MediaTypes = mediaTypeItems };

            return View(viewModelItem);
        }

        [HttpPost]
        public ActionResult Create(Models.AlbumViewModel viewModel)
        {
            UpdateAlbum(viewModel);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            var viewModelItem = this.SelectAlbumViewModel(id);

            return View(viewModelItem);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var viewModelItem = this.SelectAlbumViewModel(id);

            return View(viewModelItem);
        }

        [HttpPost]
        public ActionResult Edit(Models.AlbumViewModel viewModel)
        {
            UpdateAlbum(viewModel);

            return RedirectToAction("Index");
        }

        private Models.AlbumViewModel SelectAlbumViewModel(int id)
        {
            var album = this.context.Albums.Include("Artist").Include("MediaTypes").Include("Label").FirstOrDefault(a => a.Id == id);
            var mediaTypes = this.context.MediaTypes.ToList();

            Dictionary<int, string> mediaTypeItems = new Dictionary<int, string>();

            foreach (var mediaType in mediaTypes)
            {
                mediaTypeItems.Add(mediaType.Id, mediaType.Name);
            }


            return new Models.AlbumViewModel()
            {
                Id = album.Id,
                AlbumName = album.Name,
                Stock = album.Stock,
                Artist = album.Artist.Name,
                MediaType = album.MediaTypes.FirstOrDefault()?.Name,
                MediaTypes = mediaTypeItems
            };
        }

        private void UpdateAlbum(Models.AlbumViewModel viewModel)
        {
            var artist = this.context.Artists.FirstOrDefault(art => art.Name == viewModel.Artist);
            var recordLabel = this.context.RecordLabels.FirstOrDefault(rl => rl.Name == viewModel.RecordLabel);

            if (artist == null)
            {
                artist = new Data.Model.Artist();
                artist.Name = viewModel.Artist;
            }

            if (recordLabel == null)
            {
                recordLabel = new Data.Model.RecordLabel();
                recordLabel.Name = viewModel.RecordLabel;
            }

            var album = this.context.Albums.FirstOrDefault(a => a.Name == viewModel.AlbumName);
            var mediaType = this.context.MediaTypes.FirstOrDefault(mt => mt.Name == viewModel.MediaType);

            if (mediaType == null)
            {
                mediaType = new Data.Model.MediaType() { Name = viewModel.MediaType };
            }

            if (album == null)
            {
                album = new Data.Model.Album() { Name = viewModel.AlbumName, Stock = viewModel.Stock, Artist = artist, Label = recordLabel };
                album.MediaTypes.Add(mediaType);

                this.context.Albums.Add(album);
            }
            else
            {
                album.Label = recordLabel;
                album.Artist = artist;
                album.Stock = viewModel.Stock;

                if (!this.context.Albums.Any(a => a.Name == viewModel.AlbumName && a.MediaTypes.Any(mt => mt.Name == mediaType.Name)))
                {
                    album.MediaTypes.Add(mediaType);
                }
            }

            this.context.SaveChanges();
        }
    }
}