namespace PetFamily.Domain.Modules;

public record SpeciesBreed
{
    
    private SpeciesBreed()
    {
    }
    private SpeciesBreed(SpeciesId speciesId, Guid breedId)
    {
        SpeciesId = speciesId;
        BreedId = breedId;
    }
    public SpeciesId SpeciesId { get; }
    public Guid BreedId { get; }
    

    public static Result<SpeciesBreed> Create(SpeciesId speciesId, Guid breedId)
    {
        return Result<SpeciesBreed>
            .Success(new SpeciesBreed(speciesId, breedId));
    }
}