using BilgeAdamBitirmeProjesi.Core.Entity;
using BilgeAdamBitirmeProjesi.Core.Map;
using BilgeAdamBitirmeProjesi.Model.Entities;
using BilgeAdamBitirmeProjesi.Model.Maps;
using BilgeAdamBitirmeProjesi.Model.Maps.Base;
using BilgeAdamBitirmeProjesi.Model.SeedData;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BilgeAdamBitirmeProjesi.Model.Context
{
    public class DataContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public DataContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) 
            :base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        //Jeneric bir yapı oluşturacağım.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            RegisterMapping(modelBuilder);

            //SeedData'ları tetikledim.
            modelBuilder.ApplyConfiguration(new UserSeedData());
        }

        private void RegisterMapping(ModelBuilder modelBuilder)
        {
            //EIntityBuilder bakıp kontrol edip yeni bir oluşum gerçeleşip çalışacak.
            var typeToRegisters = new List<Type>();
            var dataAssembly = Assembly.GetExecutingAssembly();

            typeToRegisters.AddRange(dataAssembly.DefinedTypes.Select(x => x.AsType()));
            foreach (var builderType in typeToRegisters.Where(x=> typeof(IEntityBuilder).IsAssignableFrom(x)))
            {
                if (builderType != null && builderType != typeof(IEntityBuilder))
                {
                    var builder = (IEntityBuilder)Activator.CreateInstance(builderType);
                    builder.Build(modelBuilder);
                }
            }
        }
        //Savechange dediğimde veritabında ki tüm değişiklikler burayı tetiklicek.SaveChangeAsync tetikledim.
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var modifiedEntities = ChangeTracker.Entries().Where(x => x.State == EntityState.Modified || x.State == EntityState.Added).ToList();
            string computerName = Environment.MachineName;
            string IPAdress = "94.54.234.138";
            DateTime date = DateTime.Now;
            foreach (var item in modifiedEntities)
            {
                //Var olan classın sadece coreentity vericem.
                CoreEntity entity = item.Entity as CoreEntity;
                if (item != null)
                {
                    switch (item.State)
                    {
                        case EntityState.Added:
                            entity.CreatedComputerName = computerName;
                            entity.CreatedIP = IPAdress;
                            entity.CreatedDate = date;
                            entity.CreatedUserID = GetUserId();
                            break;
                        case EntityState.Modified:
                            entity.ModifiedComputerName = computerName;
                            entity.ModifiedIP = IPAdress;
                            entity.ModifiedDate = date;
                            entity.ModifiedUserID = GetUserId();
                            break;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
        private Guid? GetUserId()
        {
            string userId = "";
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var claims = _httpContextAccessor.HttpContext.User.Claims.ToList();
                userId = claims?.FirstOrDefault(x => x.Type.Equals("jti", StringComparison.OrdinalIgnoreCase))?.Value;
            }
            if (userId != null)
                return Guid.Parse(userId);
            else
                return Guid.Empty;
        }
    }
}
