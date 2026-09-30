
using Microsoft.EntityFrameworkCore;
using AcquaWS.Models;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.Internal;
using AcquaWS.Response;

namespace AcquaWS
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<EmpleadosModel> Empleados { get; set; }
        public DbSet<ClientesModel> Clientes { get; set; }
        public DbSet<SRClientesSectoresModel> Servicios_recurrentes_clientes_sectores { get; set; }
        public DbSet<PeriodoModel> Servicios_recurrentes_lectura {  get; set; }
        public DbSet<ConfigModel> Config { get; set; }
        public DbSet<UpdateApp> updateVersionApp { get; set; }
        public DbSet<ConsumoResponse> ConsumoResponse { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ConsumoResponse>().HasNoKey();
        }
        public DbSet<SRClientesModel> Servicios_recurrentes_clientes {  get; set; }
        public DbSet<SRTarifasModel> Servicios_recurrentes_tarifas { get; set; }
        public DbSet<SRClientesSectoresZonasModel> Servicios_recurrentes_clientes_sectores_zonas { get; set; }
        public DbSet<SRLecturaPrefacturaModel> Servicios_recurrentes_lectura_prefactura {  get; set; }
        public DbSet<SRClientesColbar> Servicios_recurrentes_clientes_colbarr { get; set; }
        public DbSet<SRClientesServicios> Servicios_recurrentes_clientes_servicios { get; set; }
    }
}
