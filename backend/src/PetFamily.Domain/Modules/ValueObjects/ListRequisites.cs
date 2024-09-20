﻿using CSharpFunctionalExtensions;
using PetFamily.Domain.Shared;

namespace PetFamily.Domain.Modules.ValueObjects;

public record ListRequisites
{
    private ListRequisites()
    {
    }
    
    private  ListRequisites(List<Requisite> requisites)
    {
        Requisites = requisites;
    }
    public IReadOnlyList<Requisite> Requisites { get; } = [];

    public static  Result<ListRequisites,Error> Create(List<Requisite>? requisites)
    {
        if (requisites is not null)
        {
            var listRequisites=new ListRequisites(requisites);
            return listRequisites;
        }
        return Errors.General.ValueIsInavalid();
    }
};

    