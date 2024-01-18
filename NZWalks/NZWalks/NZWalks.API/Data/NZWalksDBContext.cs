using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDBContext : DbContext
    {
        public NZWalksDBContext(DbContextOptions<NZWalksDBContext> dbContextOptions): base(dbContextOptions)
        {
            
        }

        public DbSet<Difficulty> Difficulties { get; set; }

        public DbSet<Region> Regions { get; set; }

        public DbSet<Walk> Walks { get; set; }

        public DbSet<Image> Images { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Seed difficulties table
            //Easy , Medium , Hard
            var difficulties = new List<Difficulty>()
            {
                new Difficulty
                {
                    Id = Guid.Parse("460c85b4-6cd2-4807-98cd-29b58b865686"),
                    Name = "Easy"
                },
                new Difficulty
                {
                    Id = Guid.Parse("71f69bb3-9acd-47c9-b4ad-64f30615aec3"),
                    Name = "Medium"
                },
                new Difficulty
                {
                    Id = Guid.Parse("9e05d345-4857-4ebc-81a6-9089d1f03c0f"),
                    Name = "Hard"
                }
            };
            //Seed difficulties to the DB
            modelBuilder.Entity<Difficulty>().HasData(difficulties);

            //Seed data for regions

            var regions = new List<Region>()
            {
               new Region()
                {
                    Id = Guid.Parse("7e1ff06e-7cf7-40e4-b6f3-6c9a11ec6540"),
                    Code = "AUK",
                    Name = "Aukland",
                    RegionImageUrl = "AKL.png"

                },
               new Region()
                {
                    Id = Guid.Parse("2e59b289-3d9a-4b29-9c7c-b7f111a5e21b"),
                    Code = "WLG",
                    Name = "Wellington",
                    RegionImageUrl = "WLG.png"

                },
               new Region()
                {
                    Id = Guid.Parse("d3964401-5fdc-4aa9-9cfd-70a37a513a5b"),
                    Code = "NLS",
                    Name = "Nelson",
                    RegionImageUrl = "NLS.png"

                },
               new Region()
                {
                    Id = Guid.Parse("4807e3d9-0472-4ec2-b7ae-bdfa567b9769"),
                    Code = "Bay of Plenty",
                    Name = "BOP",
                    RegionImageUrl = "BOP.png"

                },
               new Region()
                {
                    Id = Guid.Parse("fa85a572-b6ed-48fd-afa9-41438656e220"),
                    Code = "Southland",
                    Name = "STL",
                    RegionImageUrl = null

                }
            };
        
            modelBuilder.Entity<Region>().HasData(regions);
        
        }


    }
}
