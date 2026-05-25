using Microsoft.EntityFrameworkCore;
using PROJETO.MEU.Models;

namespace PROJETO.MEU.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<Ingrediente> Ingredientes { get; set; } = null!;
    public DbSet<RestricaoAlimentar> RestricoesAlimentares { get; set; } = null!;
    public DbSet<Refeicao> Refeicoes { get; set; } = null!;
    public DbSet<Estudante> Estudantes { get; set; } = null!;
    public DbSet<Cardapio> Cardapios { get; set; } = null!;
    public DbSet<ItemCardapio> ItemCardapios { get; set; } = null!;
    public DbSet<RegistroConsumo> RegistrosConsumo { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Refeicao - Ingrediente (muitos para muitos)
        modelBuilder.Entity<Refeicao>()
            .HasMany(r => r.Ingredientes)
            .WithMany(i => i.Refeicoes);

        // Refeicao - RestricaoAlimentar (muitos para muitos)
        modelBuilder.Entity<Refeicao>()
            .HasMany(r => r.Restricoes)
            .WithMany(ra => ra.Refeicoes);

        // Estudante - RestricaoAlimentar (muitos para muitos)
        modelBuilder.Entity<Estudante>()
            .HasMany(e => e.RestricoesDieteticas)
            .WithMany();

        // ItemCardapio - Cardapio (um para muitos)
        modelBuilder.Entity<ItemCardapio>()
            .HasOne(ic => ic.Cardapio)
            .WithMany(c => c.Itens)
            .HasForeignKey(ic => ic.CardapioId)
            .OnDelete(DeleteBehavior.Cascade);

        // ItemCardapio - Refeicao (um para muitos)
        modelBuilder.Entity<ItemCardapio>()
            .HasOne(ic => ic.Refeicao)
            .WithMany(r => r.ItemCardapios)
            .HasForeignKey(ic => ic.RefeicaoId)
            .OnDelete(DeleteBehavior.Restrict);

        // RegistroConsumo - Estudante (um para muitos)
        modelBuilder.Entity<RegistroConsumo>()
            .HasOne(rc => rc.Estudante)
            .WithMany(e => e.Consumos)
            .HasForeignKey(rc => rc.EstudanteId)
            .OnDelete(DeleteBehavior.Cascade);

        // RegistroConsumo - Refeicao (um para muitos)
        modelBuilder.Entity<RegistroConsumo>()
            .HasOne(rc => rc.Refeicao)
            .WithMany(r => r.Consumos)
            .HasForeignKey(rc => rc.RefeicaoId)
            .OnDelete(DeleteBehavior.Restrict);

        // Unique constraint para Estudante.Matricula
        modelBuilder.Entity<Estudante>()
            .HasIndex(e => e.Matricula)
            .IsUnique();

        // Unique constraint para RestricaoAlimentar.Nome
        modelBuilder.Entity<RestricaoAlimentar>()
            .HasIndex(ra => ra.Nome)
            .IsUnique();

        // Unique constraint para Ingrediente.Nome
        modelBuilder.Entity<Ingrediente>()
            .HasIndex(i => i.Nome)
            .IsUnique();
    }
}
