using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class PlayerType
    {
        [Key]
        public int PlayerTypeId { get; set; }

        [Required]
        [DisplayName("Player Type")]
        [StringLength(100, ErrorMessage = "Type must be between {2} and {1} characters", MinimumLength = 3)]
        public string Type { get; set; }

        public ICollection<Game> Games { get; set; }
        public ICollection<PlayerGame> PlayersGames { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        //----------CONSTRUCTOR----------//

        //public PlayerType()
        //{
        //    PlayersGames = new HashSet<PlayerGame>();
        //}
    }
}
