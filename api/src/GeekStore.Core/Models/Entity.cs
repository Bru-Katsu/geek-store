using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GeekStore.Core.Models
{
    public abstract class Entity<TEntity> : Validable<TEntity> where TEntity : class
    {
        [Key]
        [Column("id")]
        public Guid Id { get; protected set; }
    }
}
