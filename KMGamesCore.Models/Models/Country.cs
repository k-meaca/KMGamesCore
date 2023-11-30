using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class Country
    {
        //----------PROPERTIES----------//

        [Key]
        public int CountryId { get; set; }

        [DisplayName("Country")]
        [StringLength(50, ErrorMessage = "{0} must be between {2} and {1} characters", MinimumLength = 3)]
        [JsonPropertyName("Country")]
        public string Name { get; set; }

        [Timestamp]
        [JsonIgnore]
        public byte[]? RowVersion { get; set; }

        //----------CONSTRUCTOR----------//
    }
}
