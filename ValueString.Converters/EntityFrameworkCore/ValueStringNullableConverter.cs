// (c) Yury Malich, 2024-2025
// MIT License

#if !NETFRAMEWORK
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ValueStringType.Converters.EntityFrameworkCore;

/// <summary>
/// EF Core value converters for ValueString.to nullable string values
/// </summary>
/// <example>
/// <code>
/// protected override void OnModelCreating(ModelBuilder modelBuilder)
/// {
///     modelBuilder.Entity<Model>(entity =>
///     {
///         entity.Property(x => x.NullableName).HasConversion(new ValueStringNullableConverter());
///     });
/// }
/// </code>
/// </example>
public class ValueStringNullableConverter : ValueConverter<ValueString, string?>
{
    public ValueStringNullableConverter()
        : base(v => v.NullIfEmpty(), v => new ValueString(v))
    {
    }
}
#endif