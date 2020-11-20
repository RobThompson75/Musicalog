namespace Musicalog.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Musicalog.Data.Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(Musicalog.Data.Context context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.

            if (!context.MediaTypes.Any(mt => mt.Name == "Vinyl"))
                context.MediaTypes.AddOrUpdate(new Model.MediaType() { Name = "Vinyl" });

            if (!context.MediaTypes.Any(mt => mt.Name == "CD"))
                context.MediaTypes.AddOrUpdate(new Model.MediaType() { Name = "CD" });

            context.SaveChanges();
        }
    }
}
