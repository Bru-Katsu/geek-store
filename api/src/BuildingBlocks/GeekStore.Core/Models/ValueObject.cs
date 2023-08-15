using FluentValidation;
using FluentValidation.Results;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStore.Core.Models
{
    public abstract class ValueObject
    {
        [NotMapped]
        public ValidationResult ValidationResult { get; protected set; }
        public virtual ValidationResult Validate() => throw new NotImplementedException();
    }
}
