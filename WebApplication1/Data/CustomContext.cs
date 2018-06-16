using System;
using System.Collections.Generic;
using System.Linq;
using log4net;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Entities;
using WebApplication1.Helpers;

namespace WebApplication1.Data
{
    public class CustomContext : IdentityDbContext<IdentityUser>
    {
        protected readonly ILog Log = CustomLogger.Log();

        public DbSet<Audit> Audit { get; set; }
        public DbSet<People> People { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder
            .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=test;Integrated Security=True");

        public override int SaveChanges()
        {
            try
            {
                return AddAudit();
            }
            catch (Exception e)
            {
                Log.Error("Database Exception "+e);
                return -1;
            }
        }

        private int AddAudit() {
            var modifiedEntities = this.ChangeTracker.Entries().Where(p => p.State == EntityState.Modified).ToList();
            var audits=new List<Audit>();
            foreach (var change in modifiedEntities)
            {
                audits.AddRange(from prop in change.OriginalValues.Properties
                    let originalValue = change.GetDatabaseValues().GetValue<object>(prop).ToString()
                    let currentValue = change.CurrentValues[prop]?.ToString()
                    where originalValue != currentValue
                    select new Audit
                    {
                        Old = originalValue,
                        New = currentValue,
                        ColumnName = prop.Name,
                        TableName = change.Metadata.Relational().TableName,
                        UserId = 1 //todo auth
                    });
            }

            foreach (var audit in audits)
            {
                Audit.Add(audit);
            }

            return base.SaveChanges();
        }
    }
}
