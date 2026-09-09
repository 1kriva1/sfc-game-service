using System.Reflection;

using AutoMapper;

namespace SFC.Game.Application.Common.Extensions;
public static class MappingExtensions
{
    public static IMappingExpression<TSource, TDestination> IgnoreAllNonExisting<TSource, TDestination>(this IMappingExpression<TSource, TDestination> expression)
    {
        ArgumentNullException.ThrowIfNull(expression);

        BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;

        Type sourceType = typeof(TSource);

        PropertyInfo[] destinationProperties = typeof(TDestination).GetProperties(flags);

        foreach (PropertyInfo property in destinationProperties)
        {
            if (sourceType.GetProperty(property.Name, flags) == null)
            {
                expression.ForMember(property.Name, opt => opt.Ignore());
            }
        }

        return expression;
    }
}