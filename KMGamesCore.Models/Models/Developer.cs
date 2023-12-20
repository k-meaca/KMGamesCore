using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Newtonsoft.Json;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace KMGamesCore.Models.Models
{
    public class Developer
    {

        //----------PROPERTIES----------//

        [Key]
        public int DeveloperId { get; set; }

        [DisplayName("Developer")]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters.", MinimumLength = 3)]

        [JsonPropertyName("Developer")]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        [DisplayName("Country")]
        public int CountryId { get; set; }

        [ForeignKey("CountryId")]
        [ValidateNever]
        public Country? Country { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "You must select a country.")]
        [DisplayName("City")]
        public int CityId { get; set; }

        [ForeignKey("CityId")]
        [ValidateNever]
        public City? City { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
