
using System.Configuration;
using System.Data.Entity;

namespace BankMidterm.Models
{
    public class BankDatabase : DbContext
    {
        public BankDatabase()
            : base("name=BankDatabase")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public virtual DbSet<Client> Clients { get; set; }
        public virtual DbSet<Voucher> Vouchers { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<City> Cities { get; set; }


    }

}