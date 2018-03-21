using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure.Annotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AspIdentityConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var username = "rich@rich.com";
            var password = "Password123!";
            var userStore = new CustomUserStore(new CustomUserDbContext());
            var userManager = new UserManager<CustomUser, int>(userStore);

            var creationResult = userManager.Create(new CustomUser{UserName = username}, password);

            Console.WriteLine($"Created: {creationResult.Succeeded}");

            var user = userManager.FindByName(username);
            //var claimResult = userManager.AddClaim(user.Id, new Claim("given_name", "Rich"));
            //Console.WriteLine($"Claim: {claimResult.Succeeded}");

            var isMatch = userManager.CheckPassword(user, password);
            Console.WriteLine($"match: {isMatch}");

            Console.ReadKey();
        }
    }

    public class CustomUser : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PasswordHash { get; set; }
    }

    public class CustomUserDbContext : DbContext
    {
        public CustomUserDbContext() : base("DefaultConnection")
        {
            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var user = modelBuilder.Entity<CustomUser>();
            user.ToTable("Users");
            user.HasKey(x => x.Id);
            user.Property(u => u.Id).IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            user.Property(u => u.UserName).IsRequired().HasMaxLength(256).HasColumnAnnotation("Index",
                new IndexAnnotation(new IndexAttribute("UserNameIndex") {IsUnique = true}));

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CustomUser> Users { get; set; }
    }

    public class CustomUserStore : IUserPasswordStore<CustomUser, int>
    {
        private readonly CustomUserDbContext _context;

        public CustomUserStore(CustomUserDbContext context)
        {
            _context = context;
        }
        public void Dispose()
        {
            _context.Dispose();
        }

        public Task CreateAsync(CustomUser user)
        {
            _context.Users.Add(user);
            return _context.SaveChangesAsync();
        }

        public Task UpdateAsync(CustomUser user)
        {
            _context.Users.Attach(user);
            return _context.SaveChangesAsync();
        }

        public Task DeleteAsync(CustomUser user)
        {
            _context.Users.Remove(user);
            return _context.SaveChangesAsync();
        }

        public Task<CustomUser> FindByIdAsync(int userId)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        }

        public Task<CustomUser> FindByNameAsync(string userName)
        {
            return _context.Users.FirstOrDefaultAsync(u => u.UserName == userName);
        }

        public Task SetPasswordHashAsync(CustomUser user, string passwordHash)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(CustomUser user)
        {
            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(CustomUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }
    }
}
