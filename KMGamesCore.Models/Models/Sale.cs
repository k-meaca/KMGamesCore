using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class Sale
    {
        [Key]
        public int SaleId { get; set; }

        public string PayPalId { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal Total { get; set; }

        public DateTime Date { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        public ICollection<SaleDetail> SalesDetails { get; set; }
    }
}
