using System.ComponentModel.DataAnnotations;

namespace Projektor.Core.Worklogs.Payloads
{
  public abstract class SaveWorklogPayload : IValidatableObject
  {
    public string? Description { get; set; }
    public DateTime EndedAt { get; set; }
    public DateTime StartedAt { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
      var results = new List<ValidationResult>(capacity: 1);

      if (StartedAt >= EndedAt)
      {
        results.Add(new ValidationResult(
          "The end date must be later than the start date.",
          new[] { nameof(EndedAt) }
        ));
      }

      return results;
    }
  }
}
