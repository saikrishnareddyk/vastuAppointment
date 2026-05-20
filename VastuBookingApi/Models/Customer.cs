using System.ComponentModel.DataAnnotations;

namespace VastuBookingApi.Models
{
    public class  
    {
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; } = string.Empty;

        [Required]
        [MaxLength(10)]
        public string MobileNumber { get; set; } = string.Empty;

        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        public string Address { get; set; } = string.Empty;

        public string Place { get; set; } = string.Empty;

        public string InterestedServices { get; set; } = string.Empty;
    }
}