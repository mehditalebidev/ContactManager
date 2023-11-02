using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactManager.Domain.Entities
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Salutation { get; set; }

        [Required]
        [MaxLength(50)]
        public string Firstname { get; set; }

        [Required]
        [MaxLength(50)]
        public string Lastname { get; set; }

        [MaxLength(150)]
        public string? DisplayName { 
            get { return string.IsNullOrEmpty(displayName) ? $"{Salutation} {Firstname} {Lastname}".Trim() : displayName; }
            set { displayName = value; }
        }
        private string? displayName;

        public DateTime? Birthdate { get; set; }

        [Required]
        public DateTime CreationTimestamp { get; set; }= DateTime.UtcNow;

        [Required] public DateTime LastChangeTimestamp { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public bool NotifyHasBirthdaySoon 
        {
            get
            {
                if (!Birthdate.HasValue)
                    return false;

                var today = DateTime.UtcNow.Date;
                var upcomingDate = today.AddDays(14);
                var thisYearBirthday = new DateTime(today.Year, Birthdate.Value.Month, Birthdate.Value.Day);

                if (thisYearBirthday < today)
                {
                    thisYearBirthday = new DateTime(today.Year + 1, Birthdate.Value.Month, Birthdate.Value.Day);
                }
                return thisYearBirthday >= today && thisYearBirthday <= upcomingDate;
            }
        }
        
        
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        public string? PhoneNumber { get; set; }

    }
}
