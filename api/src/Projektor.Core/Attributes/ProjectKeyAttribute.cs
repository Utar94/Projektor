using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Attributes
{
  internal class ProjectKeyAttribute : ValidationAttribute
  {
    public override bool IsValid(object? value)
    {
      if (value == null)
      {
        return true;
      }

      return value is string key && key.All(c => char.IsLetterOrDigit(c));
    }
  }
}
