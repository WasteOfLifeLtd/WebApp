using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SportsStore.Domain.Entities.IdentityEntities;

namespace SportsStore.Domain.Entities
{
    public class Order
    {
        public enum OrderStatus
        {
            Delivered, Confirmed, Whatever
        }

        [Required]
        public int Id { get; set; }

        public OrderStatus Status { get; set; }

        public virtual ICollection<Book> Books { get; set; }

        public string CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public virtual AppUser AppUser { get; set; }
    }
}
