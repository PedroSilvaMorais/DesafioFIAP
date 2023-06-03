using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FIAP.WebApp.Models;

namespace FIAP.WebApp.Data
{
    public class FIAPWebAppContext : DbContext
    {
        public FIAPWebAppContext (DbContextOptions<FIAPWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<FIAP.WebApp.Models.AlunoViewModel> AlunoViewModel { get; set; } = default!;

        public DbSet<FIAP.WebApp.Models.TurmaViewModel> TurmaViewModel { get; set; }
    }
}
