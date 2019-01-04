using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MukSoft.Core.Domain
{
    // This can easily be modified to be BaseEntity<T> and public T Id to support different key types.
    // Using non-generic integer types for simplicity and to ease caching logic
    public class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid? Id { get; set; }
        public bool Active { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime LastUpdated { get; set; }
        public string EnteredBy { get; set; }

        protected BaseEntity()
        {
            InitializeBaseEntityState();
        }

        protected void InitializeBaseEntityState()
        {
            Id = default(Guid?);
            DateCreated = DateTime.Now;
            LastUpdated = DateTime.Now;
            Active = true;
        }
    }
}
