using System;
using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Books
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public int Edition { get; set; }
        public int Price { get; set; }
        public int count { get; set; }
    }
}