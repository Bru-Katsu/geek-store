
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStore.Core.Models
{
    public abstract class Validable
    {
        public Validable()
        {
            ValidationResult = new ValidationResult();
        }

        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }

        public virtual bool IsValid() => throw new NotImplementedException("Recurso de validação não está implementado!");
    }
}
