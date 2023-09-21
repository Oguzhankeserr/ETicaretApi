using ETicaretAPI.Domain.Entities;
using ETicaretAPI.Domain.Entities.Commen;
using ETicaretAPI.Domain.Entities.Identity;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Persistence.Contexts
{
    public class ETicaretAPIDbContext : IdentityDbContext<AppUser,AppRole, string>
    {
        public ETicaretAPIDbContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Product>Products { get; set; }
        public DbSet<Order>Orders { get; set; }
        public DbSet<Customer>Customers { get; set; }
        public DbSet<Domain.Entities.File>Files { get; set; }
        public DbSet<ProductImageFile>productImageFiles { get; set; }
        public DbSet<InvoiceFile>ınvoiceFiles { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker: Entityler üzerinden yapılan değişiklerin ya da yeni eklenen veririn yakalanmasını sağlayan propertydir.
            //Update operasyonlarında Track edilen verileri yakalayıp elde etememizi sağlar.

            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach(var data in datas)
            {
               _ = data.State switch
               {
                   EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
                   EntityState.Modified => data.Entity.UpdateDate = DateTime.UtcNow,
                   _=> DateTime.UtcNow
               };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
