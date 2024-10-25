using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configurations.Write;

public class PetConfiguration : IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(id => id.Value, //конвертируй из PetID(в Pet) в Guid(d Db)
            value => PetId.Create(value)); // а обратно из db в Pet bиз Guid в PetId

        builder.Property(t => t.NickName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

        builder.OwnsOne(p => p.RequisiteList, lb =>
        {
            lb.ToJson("requisites");

            lb.OwnsMany(r => r.Requisites, rb =>
            {
                rb.Property(r => r.Title)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                rb.Property(r => r.Description)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });

        builder.ComplexProperty(s => s.Position, lb =>
        {
            lb.Property(l => l.Value)
                .IsRequired()
                .HasColumnName("position");
        });

        builder.Property(i => i.Files)
            .HasConversion(
                files => JsonSerializer.Serialize(
                    files
                        .Select(c => new PetFileDto()
                        {
                            PathToStorage = c.FilePath.FullPath
                        })
                    , JsonSerializerOptions.Default),
                json => JsonSerializer
                    .Deserialize<List<PetFileDto>>(json, JsonSerializerOptions.Default)!.Select(dto =>
                        new PetFile(FilePath.CreateOfString(dto.PathToStorage).Value, false))
                    .ToList(),
                new ValueComparer<IReadOnlyList<PetFile>>(
                    (c1, c2) => c1.SequenceEqual(c2),
                    c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                    c => (IReadOnlyList<PetFile>)c.ToList()));

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

        builder.ComplexProperty(p => p.SpeciesBreed, po =>
        {
            po.Property(sp => sp.SpeciesId)
                .HasConversion(c => c.Value,
                    value => SpeciesId.Create(value))
                .HasColumnName("species_id");
            po.Property(sp => sp.BreedId)
                .IsRequired()
                .HasColumnName("breed_id");
        });

        builder.ComplexProperty(c => c.Address, b =>
        {
            b.IsRequired();

            b.Property(p => p.City)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("city");

            b.Property(p => p.Country)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("country");

            b.Property(p => p.Street)
                .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("street");
        });

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}