using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configuration;

public class PetConfiguration:IEntityTypeConfiguration<Pet>
{
    public void Configure(EntityTypeBuilder<Pet> builder)
    {
        builder.ToTable("pets");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).
            HasConversion(id => id.Value, //конвертируй из PetID(в Pet) в Guid(d Db)
                value => PetId.Create(value));// а обратно из db в Pet bиз Guid в PetId
        
        builder.Property(t=>t.NickName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(t=>t.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        
        /*builder.Property(t=>t.Breed)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        
        builder.Property(t=>t.Color)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        
        builder.Property(t=>t.InfoHelth)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);*/
        
        builder.OwnsOne(p => p.RequisiteList, lb=>
        {
            lb.ToJson("requisites");
            
            lb.OwnsMany(r => r.Requisites , rb =>
            {
               
               rb.Property(r=>r.Title)
                   .IsRequired()
                   .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
               
               rb.Property(r=>r.Description)
                   .IsRequired()
                   .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
            });
        });
        
        builder.OwnsOne(p => p.PhotosList, po =>
        {
            po.ToJson("photos");
            po.OwnsMany(ph => ph.Photos, pp =>
            {
                pp.OwnsOne(pr => pr.PhotoPathToStorage, pk =>
                {
                    pk.Property(p => p.FullPath)
                        .IsRequired()
                        .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

                });
                /*pp.Property(ps => ps.PhotoPathToStorage)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);*/

                pp.Property(ps => ps.IsFavorite)
                    .IsRequired();

            });
        });

        builder.ComplexProperty(p => p.SpeciesBreed, po =>
        {
          po.Property(sp=>sp.SpeciesId)
                .HasConversion(c=>c.Value,
                    value=>SpeciesId.Create(value))
                .HasColumnName("species_id");
          po.Property(sp=>sp.BreedId)
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