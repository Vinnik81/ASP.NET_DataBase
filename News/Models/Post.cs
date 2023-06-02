using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace News.Models
{
    public class Post
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="Where title?")]
        [MaxLength(100)]
        //[DataType(DataType.Password)]
        public string Title { get; set; }
        [Required]
        [MaxLength(20000)]
        public string Content { get; set; }
        public DateTime Date { get; set; }
        [Display(Name ="Post main image: ")]
        //[DataType(DataType.Upload)]
        public string ImageUrl { get; set; }
    }
}
