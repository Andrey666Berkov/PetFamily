﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Volunteers;

public record ListRequisites
{
    public List<Requisite> Requisites { get; }
    private ListRequisites()
    {
    }
    
    public ListRequisites(IEnumerable<Requisite> requisites)
    {
        Requisites = requisites.ToList();
    }

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