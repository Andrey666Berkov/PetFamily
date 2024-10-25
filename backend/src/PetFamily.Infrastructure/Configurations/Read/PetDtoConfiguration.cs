﻿using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Application.Dtos;

namespace PetFamily.Infrastructure.Configurations.Read;

public class PetDtoConfiguration : IEntityTypeConfiguration<PetDto>
{
    public void Configure(EntityTypeBuilder<PetDto> builder)
    {
        builder.ToTable("pets");

        builder.HasKey(x => x.Id);

        builder.Property(p => p.SpeciesId)
            .HasConversion(
                c => c,
                gui=>gui);

        builder.Property(p => p.BreedId)
            .HasConversion(
                c => c,
                gui=>gui);
            
        
        builder.Property(i => i.Files)
            .HasConversion(
                files => JsonSerializer.Serialize(string.Empty
                    , JsonSerializerOptions.Default),
                json => JsonSerializer
                    .Deserialize<PetFileDto[]>(json, JsonSerializerOptions.Default)!);
                    
        
        /*uilder.Property(i=>i.Files)
            .HasConversion( )
        
        builder.OwnsOne(p => p.Files, po =>
        {
            po.ToJson("files");
            
            po.OwnsMany(ph => ph.Values, pp =>
            {
                pp.Property(d => d.PathToStorage)
                    .IsRequired()
                    .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
            });
        });*/
    }
}