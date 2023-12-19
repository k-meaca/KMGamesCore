using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class ShoppingCart
    {
        public int ShoppingCartId { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }


        //public ICollection<Game> Games { get; set; }

        public ICollection<GameInCart> GamesInCart { get; set; }

    }
}
