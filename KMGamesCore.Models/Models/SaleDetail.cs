using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class SaleDetail
    {
        public int SaleId { get; set; }

        [ForeignKey("SaleId")]
        public Sale Sale { get; set; }
        
        public int GameId { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }

        public decimal GamePrice { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

    }
}
