using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Attributes
{
  internal class AliasAttribute : ValidationAttribute
  {
    public override bool IsValid(object? value)
    {
      if (value == null)
      {
        return true;
      }

      return value is string alias && alias.All(c => char.IsLetterOrDigit(c));
    }
  }
}
