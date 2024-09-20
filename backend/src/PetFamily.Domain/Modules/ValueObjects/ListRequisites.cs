using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

public record ListRequisites
{
    private ListRequisites()
    {
    }
    
    public  ListRequisites(List<Requisite> requisites)
    {
        Requisites = requisites;
    }
    public IReadOnlyList<Requisite> Requisites { get; } = [];

    public static  Result<ListRequisites,Error> Create(Requisite requisites)
    {
        ListRequisites listRequisites = new ListRequisites();
        listRequisites.Requisites.Append(requisites);
        return listRequisites;
    }
};

    
