using Microsoft.EntityFrameworkCore;
using UniLibraryData.Models;

namespace UniLibraryData
{
    public class UniLibraryContext : DbContext
    {
        public UniLibraryContext(DbContextOptions options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Checkout> Checkouts { get; set; }
        public DbSet<CheckoutHistory> CheckoutHistories { get; set; }
        public DbSet<LibraryBook> LibraryBooks { get; set; }
        public DbSet<LibraryBranch> LibraryBranches { get; set; }
        public DbSet<BranchHours> BranchHours { get; set; }
        public DbSet<IdCard> IdCards { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<Reserve> Reserves { get; set; }




    }
}
