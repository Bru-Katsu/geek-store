using FluentValidation.Results;

namespace GeekStore.Core.Extensions
{
    public static class ValidationResultExtensions
    {
        public static ValidationResult Merge(this ValidationResult target, ValidationResult source)
        {
            var allErrors = target.Errors.Concat(source.Errors);
            return new ValidationResult(allErrors);
        }
    }
}
