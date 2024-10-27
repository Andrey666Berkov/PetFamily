using CSharpFunctionalExtensions;
using PetFamily.Domain.IDs;

namespace PetFamily.Domain.Species;

public record SpeciesBreed
{
    private SpeciesBreed(Guid speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    public Guid SpeciesId { get; }
    public Guid BreedId { get; }
    

    public static Result<SpeciesBreed> Create(Guid speciesId, Guid breedId)
    {
        return Result
            .Success<SpeciesBreed>(new SpeciesBreed(speciesId, breedId));
    }
}