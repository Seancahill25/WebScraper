namespace WebScraper.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class stock_portfolioEntities1 : DbContext
    {
        public stock_portfolioEntities1()
            : base("name=stock_portfolioEntities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<stock> stocks { get; set; }
    }
}
