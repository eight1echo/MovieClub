namespace MovieClub.Web.Common.Validation;

public class DateInTheFuture : ValidationAttribute
{
    public override string FormatErrorMessage(string name)
    {
        return "Date must be in the future.";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        ArgumentNullException.ThrowIfNull(value);

        var date = (DateTime)value;

        if (date < DateTime.Now)
        {
            return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
        }

        return ValidationResult.Success;
    }
}