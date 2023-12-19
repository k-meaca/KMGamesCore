using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class Category
    {

        //----------PROPERTIES----------//

        [Key]
        [JsonPropertyName("CategoryId")]
        public int CategoryId { get; set; }

        [DisplayName("Category")]
        [StringLength(50,ErrorMessage = "{0} must be between {2} and {1} characters.",MinimumLength = 3)]
        [JsonPropertyName("Name")]
        public string Name { get; set; }

        [JsonIgnore]
        public virtual ICollection<Game>? Games { get; set; }

        [JsonIgnore]
        public virtual ICollection<GameCategory>? GameCategories { get; set; }

        [Timestamp]
        [JsonIgnore]
        public byte[]? RowVersion { get; set; }

        //----------CONSTRUCTOR----------//

        //public Category()
        //{
        //    GameCategories = new HashSet<GameCategory>();
        //}
    }
}
