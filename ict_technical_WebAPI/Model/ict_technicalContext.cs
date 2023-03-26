using ict_technical_WebAPI.Model.Domain;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Drawing2D;

namespace ict_technical_WebAPI.Model
{
    public class ict_technicalContext : DbContext
    {
        public ict_technicalContext()
        {
        }

        public ict_technicalContext(DbContextOptions<ict_technicalContext> options)
            : base(options)
        {

        }
        public virtual DbSet<product_info> product_info { get; set; }
        public virtual DbSet<campaign_info> campaign_info { get; set; }
        public virtual DbSet<campaign_products> campaign_product { get; set; }
        public virtual DbSet<product_price> product_price { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<product_info>().OwnsOne(x => x.auto_id);
        }
        }
}
