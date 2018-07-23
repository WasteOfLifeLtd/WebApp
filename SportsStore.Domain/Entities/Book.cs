using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Domain.Entities
{
    public class Book
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        [Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        [StringLength(17, MinimumLength = 17)]
        [Index(IsUnique = true)]
        public string ISBN { get; set; }

        public string Description { get; set; }

        public int? NumberOfPages { get; set; }

        public int? NumberInStock { get; set; }

        public byte[] CoverImage { get; set; }

        public string ImageMimeType { get; set; }

        public virtual Category Category { get; set; }

        public virtual ICollection<Author> Authors { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}
