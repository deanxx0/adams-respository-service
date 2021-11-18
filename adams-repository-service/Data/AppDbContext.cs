using adams_repository_service.Models;
using Microsoft.EntityFrameworkCore;
using NAVIAIServices.RepositoryService.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace adams_repository_service.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<ProjectInfo> ProjectInfos { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            this.Database.EnsureCreated();
        }
    }
}
