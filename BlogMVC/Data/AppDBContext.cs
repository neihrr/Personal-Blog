using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;



namespace BlogMVC.Data
{

    public class AppDBContext : IdentityDbContext

    {

       
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }
        public DbSet<Content> Contents {get; set;}
    }
}
