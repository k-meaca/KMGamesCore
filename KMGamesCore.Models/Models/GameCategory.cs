using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class GameCategory
    {
        //----------PROPERTIES----------//

        [Required]
        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }

        public Category Category { get; set; }

        [Required]
        [ForeignKey("GameId")]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
