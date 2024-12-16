// (c) Yury Malich, 2024-2025
// MIT License

#if !NETFRAMEWORK
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ValueStringType.Converters.EntityFrameworkCore;

/// <summary>
/// EF Core value converters for ValueString.to non-nullable string values
/// </summary>
/// <example>
/// <code>
/// protected override void OnModelCreating(ModelBuilder modelBuilder)
/// {
///     modelBuilder.Entity<Model>(entity =>
///     {
///         entity.Property(x => x.Name).HasConversion(new ValueStringConverter());
///     });
/// }
/// </code>
/// </example>
public class ValueStringConverter : ValueConverter<ValueString, string>
{
    public ValueStringConverter()
        : base(v => v.Value, v => new ValueString(v))
    {
    }
}
#endif