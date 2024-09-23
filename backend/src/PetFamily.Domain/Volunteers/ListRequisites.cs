using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record ListRequisites
{
    private ListRequisites()
    {
    }

    public ListRequisites(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }

    public IReadOnlyList<Requisite> Requisites { get; } = [];

    public static Result<ListRequisites, Error> 
        Create(IEnumerable<Requisite>? requisites)
    {
        if (requisites is not null)
        {
            var listRequisites = new ListRequisites(requisites);
            return listRequisites;
        }

        return ListRequisites.Empty();
    }
    
    public static Result<ListRequisites, Error> Empty()
    {
        var listRequisites = new ListRequisites();
        return listRequisites;
    }
};