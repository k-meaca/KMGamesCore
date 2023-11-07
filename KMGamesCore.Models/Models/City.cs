using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KMGamesCore.Models.Models
{
    public class City
    {
        //----------PROPERTIES----------//

        [Key]
        public int CityId { get; set; }

        [DisplayName("City")]
        [StringLength(50, ErrorMessage = "{0} name must be between {2} and {1} characters", MinimumLength = 2)]
        public string Name { get; set; }


        [Required]
        [DisplayName("Country")]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        public int CountryId { get; set; }

        [ForeignKey(nameof(CountryId))]
        [ValidateNever]
        public Country Country { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

    }
}
