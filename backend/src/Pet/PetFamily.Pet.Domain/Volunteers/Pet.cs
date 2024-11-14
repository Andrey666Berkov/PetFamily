using CSharpFunctionalExtensions;
using PetFamily.Shared.Core.Dtos;
using PetFamily.Shared.SharedKernel;
using PetFamily.Shared.SharedKernel.ValueObjects;
using PetFamily.Shared.SharedKernel.ValueObjects.IDs;

namespace PetFamily.Pet.Domain.Volunteers;

public class Pet :EntityMy<PetId>, ISoftDeletable
{
    /// //////////////////////////////////////
    //property
    private bool _isDeleted = false;

    private readonly List<PetFile> _files = [];

    public string NickName { get; private set; } = default!;
    public PetType PetType { get; private set; }
    public Position Position { get; private set; }
    public string Description { get; private set; } = default!;
    public string? Color { get; private set; }
    public string? InfoHelth { get; private set; }
    public Address Address { get; private set; }
    public double? Weight { get; private set; }
    public int? Height { get; private set; }
    public int? NumberPhoneOwner { get; private set; }
    public SpeciesBreed SpeciesBreed { get; private set; }
    public bool IsCastrated { get; private set; } = false;
    public DateOnly? BirthDate { get; private set; }
    public bool IsVaccinated { get; private set; } = false;
    public StatusHelper StatusHelper { get; private set; }
    public ListRequisites RequisiteList { get; private set; }
    public DateTime CreatedOn => DateTime.Now;
    public IReadOnlyList<PetFile> Files => _files;

    /// //////////////////////////
    //constructor
    public Pet(PetId id) : base(id)
    {
    }

    public Pet(PetId id,
        string nickName,
        string description,
        PetType petType,
        string color,
        string infoHealth,
        Address address,
        double weight,
        int height,
        int numberPhoneOwner,
        bool isCastrated,
        Requisite requisite,
        SpeciesBreed speciesBreed,
        List<PetFile>? petPhotos
    ) : base(id)
    {
        NickName = nickName;
        Description = description;
        PetType = petType;
        Color = color;
        InfoHelth = infoHealth;
        Address = address;
        Weight = weight;
        Height = height;
        NumberPhoneOwner = numberPhoneOwner;
        IsCastrated = isCastrated;
        SpeciesBreed = speciesBreed;
        AddRequisites(requisite);
        _files = petPhotos ?? new List<PetFile>();
    }
    //methods
    
    public UnitResult<ErrorMy> MoviePositionForward()
    {
        var newPosition = Position.Forward();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<ErrorMy>();
    }
    
    public UnitResult<ErrorMy> MoviePositionBack()
    {
        var newPosition = Position.Back();
        if (newPosition.IsFailure)
            return newPosition.Error;

        Position = newPosition.Value;

        return Result.Success<ErrorMy>();
    }
    
    public void UpdateFiles(List<PetFile> photosList)
    {
        _files.Clear();
        _files.AddRange(photosList);
    }

    public void AddRequisites(Requisite requisite)
    {
        if (RequisiteList == null)
        {
            IEnumerable<Requisite> requisites = new List<Requisite>();
            RequisiteList = ListRequisites.Create(requisites.Append(requisite)).Value;
        }

        RequisiteList.Requisites.Add(requisite);
    }

    public void Delete()
    {
        if (_isDeleted == false)
        {
            _isDeleted = true;
        }
    }
    
    public void SetPosition(Position position)
    {
        Position = position;
    }
    public void MoviePosition(Position position)
    {
        Position = position;
    }

    public void Restore()
    {
        if (_isDeleted == true)
            _isDeleted = false;
    }
}