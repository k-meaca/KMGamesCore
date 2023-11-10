using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
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
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [DisplayName("Game")]
        [StringLength(100,ErrorMessage="Game's title must be between {2} and {1} characters.",MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [Range(0,1000)]
        [DisplayFormat(DataFormatString = "{0:C}")]
        public decimal ActualPrice { get; set; }

        [ValidateNever]
        [StringLength(250,ErrorMessage ="Description must be between {2} and {1} characters.",MinimumLength = 3)]
        public string Description { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime Release { get; set; }

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        [Required]
        [DisplayName("Developer")]
        public int DeveloperId { get; set; }

        [ForeignKey("DeveloperId")]
        public Developer Developer { get; set; }

        public string Image { get; set; }
    }
}
