using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SPJ_ProyectoMVC.Models;

namespace SPJAutomovilesApi.Data
{
    public class SPJAutomovilesApiContext : DbContext
    {
        public SPJAutomovilesApiContext (DbContextOptions<SPJAutomovilesApiContext> options)
            : base(options)
        {
        }

        public DbSet<SPJ_ProyectoMVC.Models.Catalogo> Catalogo { get; set; } = default!;
    }
}
