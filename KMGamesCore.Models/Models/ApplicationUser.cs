using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMGamesCore.Models.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public string? StreetAddress { get; set; }


        [Required]
        [Range(1,int.MaxValue,ErrorMessage ="Country must be selected.")]
        [DisplayName("Country")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        public Country Country { get; set; }

        [Required]
        [Range(1,int.MaxValue,ErrorMessage = "City must be selected.")]
        [DisplayName("City")]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        public City City { get; set; }

        public string? ZipCode { get; set; }

        public ICollection<PurchasedGame>? PurchasedGames { get; set; }
    }
}
