using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Modules.ValueObjects;

public record SpeciesBreed
{
    private SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }
    

    public static Result<SpeciesBreed> Create(SpeciesId speciesId, Guid breedId)
    {
        return Result
            .Success<SpeciesBreed>(new SpeciesBreed(speciesId, breedId));
    }
}