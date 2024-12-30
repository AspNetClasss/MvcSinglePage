using Microsoft.EntityFrameworkCore;
using System;

using SinglePage_Sample.Models.DomainModels.PersonAggregates;
using System.Collections.Generic;

namespace SinglePage.Sample01.Models
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Person> Person { get; set; }
    }
}
