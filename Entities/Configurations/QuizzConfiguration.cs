using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace quizz.Entities.Configurations;

public class QuizConfiguration : EntityBaseConfiguration<Quizz>
{
    public override void Configure(EntityTypeBuilder<Quizz> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Title).HasMaxLength(255).IsFixedLength().IsRequired(true);
        builder.Property(b => b.PassWord).HasColumnType("char(255)").IsRequired(true);
        builder.Property(p => p.PassWordHash).HasColumnType("char(64)").IsRequired(true);
        builder.Property(p => p.Description).HasColumnType("nvarchar(255)").IsRequired(true);
        builder.Property(b => b.StartTime).HasColumnType("datetime2").IsRequired();
        builder.Property(b => b.EndTime).HasColumnType("datetime2").IsRequired();
        builder.HasIndex(p => p.PassWordHash).IsUnique(true);
    }
}