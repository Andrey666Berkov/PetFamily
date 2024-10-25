﻿namespace PetFamily.Application.PetManagment.Queries.GetVolunteerWhithPagination;

public class PageList<T>
{
    public IReadOnlyList<T> Items { get; init; } = [];
    public int TotalCount { get; init; }
    public int PageSize { get; init; }
    public int Page { get; init; }
    public bool HasNextPage => Page *PageSize< TotalCount;
    public bool HasPreviousPage => Page > 1;
}