using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStore.Core.Models
{
    public abstract class Entity : Validable
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; protected set; }
    }
}
