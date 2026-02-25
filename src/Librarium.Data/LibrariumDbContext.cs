using Microsoft.EntityFrameworkCore;
using Librarium.Data.Entities;

namespace Librarium.Data;

public class LibrariumDbContext : DbContext
{
    public LibrariumDbContext(DbContextOptions<LibrariumDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books => Set<Book>();
    public DbSet<Member> Members => Set<Member>();
    public DbSet<Loan> Loans => Set<Loan>();
    public DbSet<Author> Authors => Set<Author>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>()
            .Property(b => b.Title)
            .IsRequired()
            .HasMaxLength(200);

        modelBuilder.Entity<Book>()
            .Property(b => b.ISBN)
            .IsRequired()
            .HasMaxLength(20);
        
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity(j => j.ToTable("BookAuthors"));

        modelBuilder.Entity<Member>()
            .HasIndex(m => m.Email)
            .IsUnique(); 

        modelBuilder.Entity<Member>()
            .Property(m => m.PhoneNumber)
            .HasMaxLength(20)
            .IsRequired();

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Book)
            .WithMany(b => b.Loans)
            .HasForeignKey(l => l.BookId);

        modelBuilder.Entity<Loan>()
            .HasOne(l => l.Member)
            .WithMany(m => m.Loans)
            .HasForeignKey(l => l.MemberId);
    }
}