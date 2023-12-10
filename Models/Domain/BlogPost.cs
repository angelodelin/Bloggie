using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace Bloggie.Models.Domain
{
    public class BlogPost
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Heading { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string PageTitle { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Content { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string ShortDescription { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string FeaturedImageUrl { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string UrlHandle { get; set; }
        public DateTime PublishedDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Author { get; set; }
        public bool? Visible { get; set; } = true;
        public ICollection<Tag> Tags { get; set; }
    }
}
