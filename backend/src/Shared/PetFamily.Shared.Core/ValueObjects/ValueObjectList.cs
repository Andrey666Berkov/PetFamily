﻿using System.Collections;

namespace PetFamily.Shared.Core.ValueObjects;

public class ValueObjectList<T> : IReadOnlyList<T>
{
  public IReadOnlyList<T> Values { get; } = null!;
    public T this[int index] => Values[index];
    public int Count => Values.Count;
    
    public ValueObjectList()
    {
        
    }

    public ValueObjectList(IEnumerable<T> list)
    {
        Values =new List<T>(list).AsReadOnly();
    }

    public IEnumerator<T> GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return Values.GetEnumerator();
    }

    public static implicit operator ValueObjectList<T>(List<T> list) =>
        new(list);
    
    public static implicit operator List<T>(ValueObjectList<T> list) =>
        list.Values.ToList();
 
}