using Microsoft.EntityFrameworkCore;
using UsersManagerAPI.DomainClasses.Models;

namespace UsersManagerAPI.DataAccess
{
    public class UsersBDContext : DbContext
    {
        public UsersBDContext(DbContextOptions<UsersBDContext> options) : base(options)
        {

        }
        public DbSet<UserInfo> Users { get; set; }
        public DbSet<CompanyInfo> Companies { get; set; }
    }
}
