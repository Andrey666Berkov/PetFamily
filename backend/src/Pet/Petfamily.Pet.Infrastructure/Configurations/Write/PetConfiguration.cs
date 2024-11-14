using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Shared.Core.Dtos;
using PetFamily.Shared.Core.Extensions;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace Petfamily.Pet.Infrastructure.Configurations.Write;

public class PetConfiguration :IEntityTypeConfiguration<PetFamily.Pet.Domain.Volunteers.Pet>
{
    public void Configure(EntityTypeBuilder<PetFamily.Pet.Domain.Volunteers.Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(id => id, //конвертируй из PetID(в Pet) в Guid(d Db)
            value => PetId.Create(value.Value)); // а обратно из db в Pet bиз Guid в PetId

        builder.Property(t => t.NickName)
            .IsRequired()
            .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(ConstantsMy.MAX_HIGH_TEXT_LENGTH);

        builder.OwnsOne(p => p.RequisiteList, lb =>
        {
            lb.ToJson("requisites");

            lb.OwnsMany(r => r.Requisites, rb =>
            {
                rb.Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH);

                rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(ConstantsMy.MAX_HIGH_TEXT_LENGTH);
            });
        });

        builder.ComplexProperty(s => s.Position, lb =>
        {
            lb.Property(l => l.Value)
                .IsRequired()
                .HasColumnName("position");
        });

        builder.OwnsOne(c => c.SpeciesBreed, tb =>
            {
                tb.Property(c => c.BreedId)
                    .IsRequired()
                    .HasColumnName("breed_id");

                tb.Property(c => c.SpeciesId)
                    .IsRequired()
                    .HasColumnName("species_id");

                /*tb.Property(c => c.SpeciesId)
                    .HasConversion(sguid => sguid.Value,
                        v => SpeciesId.Create(v))
                    .HasColumnName("species_id");*/
            }
        );
        
        builder.Property(i => i.Files)
            .ValueObjectCollection(
                c => new PetFileDto() { PathToStorage = c.FilePath.FullPath },
                dto => new PetFile(FilePath.CreateOfString(dto.PathToStorage).Value, false))
            .HasColumnName("files");

        /*builder.OwnsOne(p => p.Files, po =>
        {
            po.ToJson("photos");

            po.OwnsMany(ph => ph.Values, pp =>
            {
                pp.Property(d => d.FilePathToStorage)
                    .HasConversion(
                        c => c.FullPath,
                        value => FilePath.CreateOfString(value).Value)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });
        });*/
        /*builder.ComplexProperty(p => p.SpeciesBreed, po =>
        {
            po.Property(sp => sp.SpeciesId)
                .HasConversion(c => c.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("species_id");
            po.Property(sp => sp.BreedId)
                .IsRequired()
                .HasColumnName("breed_id");
        });*/

        builder.ComplexProperty(c => c.Address, b =>
        {
            b.IsRequired();

            b.Property(p => p.City)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("city");

            b.Property(p => p.Country)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("country");

            b.Property(p => p.Street)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("street");
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
        
        builder.Property(c=>c.PetType)
            .HasConversion(c=>c.ToString(),
                v=>(PetType)Enum.Parse(typeof(PetType), v));
    }
}