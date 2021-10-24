using System.ComponentModel.DataAnnotations;

namespace Pinterest.Models
{
    public class Image
    {
        public int ImageId { get; set; }
        [Required]
        public string URL { get; set; }
        [Required]
        public string Height { get; set; }
        [Required]
        public string Width { get; set; }
    }
}