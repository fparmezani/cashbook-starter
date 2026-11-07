using CashBook.Ledger.Domain.Categories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBook.Ledger.Infrastructure.Persistence.Configurations;

internal sealed class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    private const int TypeMaxLength = 20;

    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder.ToTable("ledger_categories");

        builder.HasKey(category => category.Id);
        builder.Property(category => category.Id).ValueGeneratedNever();

        builder.Property(category => category.Name)
            .HasMaxLength(Category.NameMaxLength)
            .UseCollation("NOCASE")
            .IsRequired();
        builder.HasIndex(category => category.Name).IsUnique();

        // Enum como string (rule: dotnet / persistence).
        builder.Property(category => category.Type)
            .HasConversion<string>()
            .HasMaxLength(TypeMaxLength)
            .IsRequired();
    }
}
