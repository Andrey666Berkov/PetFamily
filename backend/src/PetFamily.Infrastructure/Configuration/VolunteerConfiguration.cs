using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Domain.IDs;
using PetFamily.Domain.Shared;
using PetFamily.Domain.Volunteers;

namespace PetFamily.Infrastructure.Configuration;

public class VolunteerConfiguration:IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");
        
        builder.HasKey(x => x.Id);
        
        builder.Property(x => x.Id).
            HasConversion(id => id.Value, 
                value => VolunteerId.Create(value));
        
        builder.Property(t=>t.FirstName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(t=>t.LastName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(t=>t.MiddleName)
            .IsRequired()
            .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
        
        builder.Property(t=>t.Description)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
        
        builder.Property(t=>t.Email)
            .IsRequired()
            .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id");
       
        builder.OwnsOne(p => p.RequisitesList, po =>
         {
             po.ToJson("requisites");
             po.OwnsMany(r => r.Requisites, r =>
             {
                 r.Property(rq=>rq.Title)
                     .IsRequired()
                     .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
                 
                 r.Property(rq=>rq.Description)
                     .IsRequired()
                     .HasMaxLength(Constants.MAX_HIGH_TEXT_LENGTH);
             });
         });

         builder.OwnsOne(p => p.SocialNetworkList, po =>
         {
             po.ToJson("social_network");
             po.OwnsMany(s => s.SocialNetworks, s =>
             {
                 s.Property(sn => sn.Link)
                     .IsRequired()
                     .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);

                 s.Property(sn => sn.Name)
                     .IsRequired()
                     .HasMaxLength(Constants.MAX_LOW_TEXT_LENGTH);
             });
         });
    }
}