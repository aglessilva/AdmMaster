using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using WebAdministrator.Models;

namespace WebAdministrator.DAL
{
    public class DbCon : DbContext
    {
        // ESSE CONSTRUTOR, DESTINA-SE PARA AS MIGRATIONS DAS ENTIDADES,
        // SEM ESSE CONSTRUTOR, NÃO SERÁ POSSIVEL USAR OS COMANDOS Add-Migrations, Remove-Migration, Update-Database
        public DbCon(DbContextOptions<DbCon> option):base(option){}
        
        public DbCon():base(){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            if (!optionsBuilder.IsConfigured)
            {
                var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json");
                var config = builder.Build();
                string _urlBase = config.GetSection("ConnectionStrings:WAConnection").Value;

                optionsBuilder.UseOracle(_urlBase, x => x.MigrationsHistoryTable("__EFMigrationsHistory"));
            }
        }

        public virtual DbSet<Empresa> Empresa { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<Documento> Documento { get; set; }
        public virtual DbSet<Familia> Familia { get; set; }
        public virtual DbSet<Justificativa> Justificativa { get; set; }
        public virtual DbSet<Comarca> Comarca { get; set; }
        public virtual DbSet<Contrato> Contrato { get; set; }
        public virtual DbSet<Link> Link { get; set; }
        public virtual DbSet<MotivoAvaliacao> MotivoAvaliacao { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
        public virtual DbSet<Sentenca> Sentencas { get; set; }
        public virtual DbSet<DominioGenerico> DominioGenericos { get; set; } 
        public virtual DbSet<Questionamento> Questionamento { get; set; }
        public virtual DbSet<Advogado> Advogado { get; set; }
        public virtual DbSet<OrigemEnvolvida> OrigemEnvolvida { get; set; }
        public virtual DbSet<Empresa_Envolvida_Origem> Empresa_Envolvida { get; set; }
        public DbSet<CalendarioAjustavel> CalendarioAjustavel { get; set; }
    }
}
