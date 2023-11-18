using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    
    public class PlayerGame
    {
        //----------PROPERTIES-----------//

        [Required]
        [ForeignKey("GameId")]
        public int GameId { get; set; }

        public Game Game { get; set; }

        [Required]
        [ForeignKey("PlayerTypeId")]
        public int PlayerTypeId { get; set; }

        public PlayerType PlayerType { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }
    }
}
