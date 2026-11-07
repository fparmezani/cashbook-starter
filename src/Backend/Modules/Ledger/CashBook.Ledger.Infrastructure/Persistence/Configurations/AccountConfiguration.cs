using CashBook.Ledger.Domain.Accounts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashBook.Ledger.Infrastructure.Persistence.Configurations;

internal sealed class AccountConfiguration : IEntityTypeConfiguration<Account>
{
    public void Configure(EntityTypeBuilder<Account> builder)
    {
        builder.ToTable("ledger_accounts");

        builder.HasKey(account => account.Id);
        builder.Property(account => account.Id).ValueGeneratedNever();

        // NOCASE: "Carteira" e "carteira" contam como o mesmo nome, no índice e nas consultas.
        builder.Property(account => account.Name)
            .HasMaxLength(Account.NameMaxLength)
            .UseCollation("NOCASE")
            .IsRequired();
        builder.HasIndex(account => account.Name).IsUnique();

        builder.Property(account => account.IsActive).IsRequired();
    }
}
