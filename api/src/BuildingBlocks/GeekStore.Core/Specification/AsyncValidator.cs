using FluentValidation.Results;

namespace GeekStore.Core.Specification
{
    public abstract class AsyncValidator<T> where T : class
    {
        private readonly Dictionary<IAsyncSpecification<T>, string> _specifications = new();

        protected void Add(IAsyncSpecification<T> specification, string message)
        {
            _specifications.Add(specification, message);
        }

        public async Task<ValidationResult> ValidateAsync(T instance)
        {
            var errors = new List<ValidationFailure>();

            foreach (var spec in _specifications)
            {
                if (!await spec.Key.IsSatisfiedBy(instance))
                    errors.Add(new ValidationFailure("Erro", spec.Value));
            };

            return new ValidationResult(errors);
        }
    }
}
