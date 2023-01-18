using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestranTeste.Models;

namespace GestranTeste.Data
{
    public class GestranTesteContext : DbContext
    {
        public GestranTesteContext (DbContextOptions<GestranTesteContext> options)
            : base(options)
        {
        }

        public DbSet<GestranTeste.Models.GestranProvider> GestranProvider { get; set; } = default!;

        public DbSet<GestranTeste.Models.Address> Address { get; set; }
    }
}
