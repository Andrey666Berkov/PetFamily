using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace PetFamily.Application.Extensions;

public static class EfCoreFluentApiExtensions
{
    public static PropertyBuilder<IReadOnlyList<TValueObject>> ValueObjectCollection<TValueObject, TDto>(
        this PropertyBuilder<IReadOnlyList<TValueObject>> builder, Func<TValueObject, TDto> toDtoSelector,
        Func<TDto, TValueObject> toValueObjectSelector)
    {
        return builder.HasConversion(
                valueObjects => SerializeValueObjectsCollection(valueObjects, toDtoSelector),
                json => DeserializeDtoObject(json, toValueObjectSelector),
                GeneratedCompare<TValueObject>())
            .HasColumnType("jsonb");
    }

    private static string SerializeValueObjectsCollection<TValueObject, TDto>(IReadOnlyList<TValueObject> valueObjects,
        Func<TValueObject, TDto> selector)
    {
        var dtos = valueObjects.Select(selector);
        return JsonSerializer.Serialize(dtos, JsonSerializerOptions.Default);
    }

    private static IReadOnlyList<TValueObject> DeserializeDtoObject<TValueObject,TDtos>(string json, Func<TDtos, TValueObject> selector)
    {
        var dtos = JsonSerializer
            .Deserialize<IEnumerable<TDtos>>(json, JsonSerializerOptions.Default) ?? [];
            

        return dtos.Select(selector).ToList();
    }
    
    private static  ValueComparer<IReadOnlyList<TValueObject>> GeneratedCompare<TValueObject>()
    {
        return new ValueComparer<IReadOnlyList<TValueObject>>(
            (c1, c2) => c1!.SequenceEqual(c2!),
            c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
            c => (IReadOnlyList<TValueObject>)c.ToList());

    }
}