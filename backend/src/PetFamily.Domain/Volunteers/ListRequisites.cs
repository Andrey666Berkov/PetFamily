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

    public List<Requisite> Requisites { get; }

    public static Result<ListRequisites, Error>
        Create(IEnumerable<Requisite> requisites)
    {
        var listRequisites = new ListRequisites(requisites);
        return listRequisites;
    }
    
    public  void Add(Requisite requisites)
    {
        Requisites.Add(requisites);
    }

    public static Result<ListRequisites, Error> Empty()
    {
        var listRequisites = new ListRequisites();
        return listRequisites;
    }
};