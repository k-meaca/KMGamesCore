using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class Category
    {

        //----------PROPERTIES----------//

        [Key]
        public int CategoryId { get; set; }

        [DisplayName("Category")]
        [StringLength(50,ErrorMessage = "{0} must be between {2} and {1} characters.",MinimumLength = 3)]
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }
        public virtual ICollection<GameCategory> GameCategories { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        //----------CONSTRUCTOR----------//

        //public Category()
        //{
        //    GameCategories = new HashSet<GameCategory>();
        //}
    }
}
