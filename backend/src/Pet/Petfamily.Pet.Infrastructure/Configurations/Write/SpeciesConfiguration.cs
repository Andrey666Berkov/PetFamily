using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Pet.Domain.Volunteers.Species;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace Petfamily.Pet.Infrastructure.Configurations.Write;

public class SpeciesConfiguration:IEntityTypeConfiguration<Species>
{
    public void Configure(EntityTypeBuilder<Species> builder)
    {
        builder.ToTable("species");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id).
            HasConversion(id => id.Value, 
                value => SpeciesId.Create(value));

        builder.Property(t => t.Name)
            .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(ConstantsMy.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();

        builder.HasMany(c => c.Breeds)
            .WithOne()
            .HasForeignKey("species_id");
    }
}