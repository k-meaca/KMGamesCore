using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class GameInCart
    {
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public int ShoppingCartId { get; set; }

        [ForeignKey("ShoppingCartId")]
        public ShoppingCart ShoppingCart { get; set; }
    }
}
