using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.Modules;

namespace PetFamily.Infrastructure.Configuration;

public class PetConfiguration:IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("Pets");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).
            HasConversion(id => id.Value, 
                value => PetId.Create(value));
        builder.Property(t=>t.NickName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        builder.Property(t=>t.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        builder.Property(t=>t.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        builder.OwnsOne(p => p.Requisites, po =>
        {
            po.ToJson();
            po.OwnsMany(r => r.Requisites, r =>
            {
                r.Property(rq=>rq.Title).IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);;
            });
        });
        builder.OwnsOne(p => p.Photos, po =>
        {
            po.ToJson();
            po.OwnsMany(ph => ph.Photos, pp =>
            {
                pp.Property(ps => ps.PathToStorage).IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });
        });

    }
}