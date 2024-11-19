using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PetFamily.Pet.Domain.Volunteers;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace Petfamily.Pet.Infrastructure.Configurations.Write;

public class VolunteerConfiguration : IEntityTypeConfiguration<Volunteer>
{
    public void Configure(EntityTypeBuilder<Volunteer> builder)
    {
        builder.ToTable("volunteers");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id).HasConversion(id => id.Value,
            value => VolunteerId.Create(value));

        builder.ComplexProperty(c => c.FullName, b =>
        {
            b.IsRequired();

            b.Property(p => p.FirstName)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("first_name");

            b.Property(p => p.LastName)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("last_name");

            b.Property(p => p.MiddleName)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("middle_name");

            builder.Property<bool>("_isDeleted")
                .UsePropertyAccessMode(PropertyAccessMode.Field)
                .HasColumnName("is_deleted");
        });

        builder.Property(t => t.Description)
            .IsRequired()
            .HasMaxLength(ConstantsMy.MAX_HIGH_TEXT_LENGTH);

        builder.ComplexProperty(v => v.Email, em =>
        {
            em.IsRequired();
            em.Property(e => e.Emaill)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("email");
        });

        builder.ComplexProperty(v => v.PhoneNumber, em =>
        {
            em.IsRequired();
            em.Property(e => e.Phonenumber)
                .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH)
                .HasColumnName("phone_number");
        });

        builder.OwnsOne(p => p.RequisitesList, po =>
        {
            po.ToJson("requisites");
            po.OwnsMany(r => r.Requisites, r =>
            {
                r.Property(rq => rq.Title)
                    .IsRequired()
                    .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH);

                r.Property(rq => rq.Description)
                    .IsRequired()
                    .HasMaxLength(ConstantsMy.MAX_HIGH_TEXT_LENGTH);
            });
        });

        builder.OwnsOne(p => p.SocialNetworkList, po =>
        {
            po.ToJson("social_network");
            po.OwnsMany(s => s.SocialNetworks, s =>
            {
                s.Property(sn => sn.Link)
                    .IsRequired()
                    .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH);

                s.Property(sn => sn.Name)
                    .IsRequired()
                    .HasMaxLength(ConstantsMy.MAX_LOW_TEXT_LENGTH);
            });
        });

        builder.HasMany(v => v.Pets)
            .WithOne()
            .HasForeignKey("volunteer_id")
            .OnDelete(DeleteBehavior.Cascade);

        builder.Property<bool>("_isDeleted")
            .UsePropertyAccessMode(PropertyAccessMode.Field)
            .HasColumnName("is_deleted");
    }
}