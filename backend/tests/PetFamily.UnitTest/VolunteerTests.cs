
using FluentAssertions;
using PetFamily.Pet.Domain.Volunteers;
using PetFamily.Shared.SharedKernel.ValueObjects;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace PetFamily.UnitTest;

public class VolunteerTests
{
    [Fact]
    public void MoviePositionPet_ShouldNotMove_WhenPetAllReadyAtNewPosition()
    {
        //arrange
        //подготовка
        const int petCount = 6;
        
        var volunteer = CreateVolunteerWithPet(petCount);

        var p0 = volunteer.Pets[0];
        var p1 = volunteer.Pets[1];
        var p2 = volunteer.Pets[2];
        var p3 = volunteer.Pets[3];
        var p4 = volunteer.Pets[4];
        var p5 = volunteer.Pets[5];
        
       

        const int NEW_POSITION = 2;
        var newPosition=Position.Create(NEW_POSITION).Value;
        
        //act
        //вызов тестируемого метода
        var result = volunteer.MoviePositionPet(p1, newPosition);

        //assert
        ////проверка результата
        result.IsSuccess.Should().BeTrue();
        p0.Position.Should().Be(Position.Create(1).Value);
        p1.Position.Should().Be(Position.Create(2).Value);
        p2.Position.Should().Be(Position.Create(3).Value);
        p3.Position.Should().Be(Position.Create(4).Value);
        p4.Position.Should().Be(Position.Create(5).Value);
        p5.Position.Should().Be(Position.Create(6).Value);
    }

    [Fact]
    public void MoviePositionPet_ShouldMovieOtherPet_WhenNewPositionLower()
    {
        //arrange
        //подготовка
        const int petCount = 6;
        
        var volunteer = CreateVolunteerWithPet(petCount);

        var p0 = volunteer.Pets[0];
        var p1 = volunteer.Pets[1];
        var p2 = volunteer.Pets[2];
        var p3 = volunteer.Pets[3];
        var p4 = volunteer.Pets[4];
        var p5 = volunteer.Pets[5];
        
       

        const int NEW_POSITION = 2;
        var newPosition=Position.Create(NEW_POSITION).Value;
        
        //act
        //вызов тестируемого метода
        var result = volunteer.MoviePositionPet(p4, newPosition);

        //assert
        ////проверка результата
        result.IsSuccess.Should().BeTrue();
        p0.Position.Should().Be(Position.Create(1).Value);
        p1.Position.Should().Be(Position.Create(3).Value);
        p2.Position.Should().Be(Position.Create(4).Value);
        p3.Position.Should().Be(Position.Create(5).Value);
        p4.Position.Should().Be(Position.Create(2).Value);
        p5.Position.Should().Be(Position.Create(6).Value);
    }
    
    [Fact]
    public void MoviePositionPet_ShouldMovieOtherPet_WhenNewPositionGreater()
    {
        //arrange
        //подготовка
        const int petCount = 6;
        
        var volunteer = CreateVolunteerWithPet(petCount);

        var p0 = volunteer.Pets[0];
        var p1 = volunteer.Pets[1];
        var p2 = volunteer.Pets[2];
        var p3 = volunteer.Pets[3];
        var p4 = volunteer.Pets[4];
        var p5 = volunteer.Pets[5];
        
       

        const int NEW_POSITION = 5;
        var newPosition=Position.Create(NEW_POSITION).Value;
        
        //act
        //вызов тестируемого метода
        var result = volunteer.MoviePositionPet(p1, newPosition);

        //assert
        ////проверка результата
        result.IsSuccess.Should().BeTrue();
        p0.Position.Should().Be(Position.Create(1).Value);
        p1.Position.Should().Be(Position.Create(5).Value);
        p2.Position.Should().Be(Position.Create(2).Value);
        p3.Position.Should().Be(Position.Create(3).Value);
        p4.Position.Should().Be(Position.Create(4).Value);
        p5.Position.Should().Be(Position.Create(6).Value);
    }
    
    [Fact]
    public void MoviePositionPet_ShouldMovieOtherPet_WhenNewPositionInBegin()
    {
        //arrange
        //подготовка
        const int petCount = 6;
        
        var volunteer = CreateVolunteerWithPet(petCount);

        var p0 = volunteer.Pets[0];
        var p1 = volunteer.Pets[1];
        var p2 = volunteer.Pets[2];
        var p3 = volunteer.Pets[3];
        var p4 = volunteer.Pets[4];
        var p5 = volunteer.Pets[5];
        
       

        const int NEW_POSITION = 1;
        var newPosition=Position.Create(NEW_POSITION).Value;
        
        //act
        //вызов тестируемого метода
        var result = volunteer.MoviePositionPet(p4, newPosition);

        //assert
        ////проверка результата
        result.IsSuccess.Should().BeTrue();
        p0.Position.Should().Be(Position.Create(2).Value);
        p1.Position.Should().Be(Position.Create(3).Value);
        p2.Position.Should().Be(Position.Create(4).Value);
        p3.Position.Should().Be(Position.Create(5).Value);
        p4.Position.Should().Be(Position.Create(1).Value);
        p5.Position.Should().Be(Position.Create(6).Value);
    }
    
    [Fact]
    public void MoviePositionPet_ShouldMovieOtherPet_WhenNewPositionInEndn()
    {
        //arrange
        //подготовка
        const int petCount = 6;
        
        var volunteer = CreateVolunteerWithPet(petCount);

        var p0 = volunteer.Pets[0];
        var p1 = volunteer.Pets[1];
        var p2 = volunteer.Pets[2];
        var p3 = volunteer.Pets[3];
        var p4 = volunteer.Pets[4];
        var p5 = volunteer.Pets[5];
        
       

        const int NEW_POSITION = 6;
        var newPosition=Position.Create(NEW_POSITION).Value;
        
        //act
        //вызов тестируемого метода
        var result = volunteer.MoviePositionPet(p1, newPosition);

        //assert
        ////проверка результата
        result.IsSuccess.Should().BeTrue();
        p0.Position.Should().Be(Position.Create(1).Value);
        p1.Position.Should().Be(Position.Create(6).Value);
        p2.Position.Should().Be(Position.Create(2).Value);
        p3.Position.Should().Be(Position.Create(3).Value);
        p4.Position.Should().Be(Position.Create(4).Value);
        p5.Position.Should().Be(Position.Create(5).Value);
    }
    
    
    [Fact]
    public void MoviePositionPet_ShouldMovieOtherPet_WhenNewPositionInEndOfBegin()
    {
        //arrange
        //подготовка
        const int petCount = 6;
        
        var volunteer = CreateVolunteerWithPet(petCount);

        var p0 = volunteer.Pets[0];
        var p1 = volunteer.Pets[1];
        var p2 = volunteer.Pets[2];
        var p3 = volunteer.Pets[3];
        var p4 = volunteer.Pets[4];
        var p5 = volunteer.Pets[5];
        
       

        const int NEW_POSITION = 1;
        var newPosition=Position.Create(NEW_POSITION).Value;
        
        //act
        //вызов тестируемого метода
        var result = volunteer.MoviePositionPet(p5, newPosition);

        //assert
        ////проверка результата
        result.IsSuccess.Should().BeTrue();
        p0.Position.Should().Be(Position.Create(2).Value);
        p1.Position.Should().Be(Position.Create(3).Value);
        p2.Position.Should().Be(Position.Create(4).Value);
        p3.Position.Should().Be(Position.Create(5).Value);
        p4.Position.Should().Be(Position.Create(6).Value);
        p5.Position.Should().Be(Position.Create(1).Value);
    }
    
    // /////////////////////////
    private Volunteer CreateVolunteerWithPet(int petCount)
    {
        var initial = Initials.Create("bob", "Fedorovich", "Popov").Value;
        var email=Email.Create("bob@gmail.com").Value;
        var description = "description";
        var numberPhone=PhoneNumber.Create("555-555-5555").Value;
        int experience = 10;
        var requisite = Requisite.Create("alfa","herovds").Value;
        var requisiteList = ListRequisites.Create([requisite]).Value;
        var socialNetwork = SocialNetwork.Create("VK","her").Value;
        var socialNetworkList = ListSocialNetwork.Create([socialNetwork]).Value;
        var volunteerId=VolunteerId.CreateNew();

        var address = Address.Create("city", "street", "ggg").Value;
        var pets = Enumerable.Range(1, petCount).Select(_ =>
            new Pet.Domain.Volunteers.Pet(
                PetId.CreateNewPetId(),
                    "NameA",
                    "description",
                    PetType.Cat,
                    "green",
                    "HelfBasd",
                    address,
                    22,
                    33,
                    5555555,
                    false,
                    requisite,
                    SpeciesBreed.Create(SpeciesId.CreateNew().Value, Guid.NewGuid()).Value,
                    null
                )).ToList();

        int i = 1;
        foreach (var pet in pets)
        {
            pet.SetPosition(Position.Create(i++).Value);
        }
        
        var volunteer=Volunteer.Create(volunteerId, initial, email, description, 
            numberPhone, experience, requisiteList, socialNetworkList).Value;
        
        volunteer.AddPet(pets);

        return volunteer;
    }
    
    private Pet.Domain.Volunteers.Pet CreatePet()
    {
        var requisite = Requisite.Create("alfa","herovds").Value;
        var requisiteList = ListRequisites.Create([requisite]).Value;
        
        var address = Address.Create("city", "street", "ggg").Value;
        
        var pet = new Pet.Domain.Volunteers.Pet(
            PetId.CreateNewPetId(),
            "NewPaTY",
            "description",
            PetType.Cat,
            "green",
            "HelfBasd",
            address,
            22,
            33,
            5555555,
            false,
            requisite,
            SpeciesBreed.Create(SpeciesId.CreateNew().Value, Guid.NewGuid()).Value,
            null
        );

        return pet;
    }
}