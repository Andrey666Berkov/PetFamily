using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Pet.Domain.Volunteers.Species;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace Petfamily.Pet.Infrastructure.Configurations.Write;

public class BreedConfiguration:IEntityTypeConfiguration<Breed>
{
    public void Configure(EntityTypeBuilder<Breed> builder)
    {
        builder.ToTable("breed");
        
        builder.HasKey(s => s.Id);
        
        builder.Property(s => s.Id).
            HasConversion(id => id.Value, 
                value => BreedId.Create(value));

        builder.Property(t => t.Name)
            .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
            .IsRequired();
        
        builder.Property(t => t.Description)
            .HasMaxLength(ConstantsMy.MAX_HIGH_TEXT_LENGTH)
            .IsRequired();
    }
}