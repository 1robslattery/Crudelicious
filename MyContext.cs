using Microsoft.EntityFrameworkCore;
 
namespace Crudelicious.Models
{
    public class MyContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public MyContext(DbContextOptions options) : base(options) { }

		// "dishes" table is represented by this DbSet "dish"
		// "DbSet<dish> is being referenced from public class truck w/n dish.cs
		public DbSet<dish> Dishes {get;set;}
    }
}