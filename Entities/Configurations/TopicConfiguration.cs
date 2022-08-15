using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace quizz.Entities.Configurations;

public class TopicConfiguration : EntityBaseConfiguration<Topic>
{
    public override void Configure(EntityTypeBuilder<Topic> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Name).HasMaxLength(255).IsFixedLength().IsRequired(true);
        builder.Property(p => p.NameHash).HasColumnType("char(64)").IsRequired(true);
        builder.Property(p => p.Description).HasColumnType("nvarchar(255)").IsRequired(true);
        builder.HasIndex(p => p.NameHash).IsUnique(true);
    }
}