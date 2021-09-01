using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNetCoreExam.Models
{
    public class StreamingPlatform
    {
        public int ID { get; set; }

        [DisplayName("Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The name can not be empty"), MinLength(length: 3, ErrorMessage = "The name must be at least 3 symbols"), MaxLength(length: 20, ErrorMessage = "The name must be at most 20 symbols")]
        public string Name { get; set; }
        
        [DisplayName("Description")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "The description can not be empty"), MinLength(length: 3, ErrorMessage = "The description must be at least 3 symbols"), MaxLength(length: 600, ErrorMessage = "The description must be at most 20 symbols")]
        public string Description { get; set; }

        [DisplayName("Date added")]
        public DateTime Date { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }

        [NotMapped]
        [DisplayName("Upload File")]
        [Required(ErrorMessage = "Please select a file.")]
        public IFormFile ImageFile { get; set; }
    }
}
