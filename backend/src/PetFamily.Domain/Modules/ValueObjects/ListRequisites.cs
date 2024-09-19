using CSharpFunctionalExtensions;

namespace PetFamily.Domain.Modules.ValueObjects;

public record ListRequisites
{
    private ListRequisites()
    {
    }
    public List<Requisite> Requisites { get; } = [];

    public static  Result<ListRequisites> Create(Requisite requisites)
    {
        ListRequisites listRequisites = new ListRequisites();
        listRequisites.Requisites.Add(requisites);
        return listRequisites;
    }
};

    
