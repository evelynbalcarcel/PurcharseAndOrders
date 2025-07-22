// Data/ApplicationDbContext.cs
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity; // Para IdentityUser
using Microsoft.EntityFrameworkCore;

namespace BlazorPurchaseOrders.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // Aquí puedes agregar tus propios DbSet para tablas personalizadas si las tienes
        // public DbSet<MiEntidad> MisEntidades { get; set; }
    }
}