using KMGamesCore.Models.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace KMGamesCore.Web.ViewModel.GameVM
{
    public class GameVM
    {
        //----------PROPERTIES----------//

        //public Game Game { get; set; }

        public int GameId { get; set; }

        [DisplayName("Game")]
        [StringLength(100, ErrorMessage = "Game's title must be between {2} and {1} characters.", MinimumLength = 3)]
        public string Title { get; set; }

        [Required]
        [DisplayName("Actual Price")]
        [Range(1, 1000.00, ErrorMessage = "Price must be non-negative and less than 1000.")]
        public decimal ActualPrice { get; set; } = 1.00m;

        [StringLength(250, ErrorMessage = "Description must be between {2} and {1} characters.", MinimumLength = 3)]
        public string? Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        [Required]
        public DateTime Release { get; set; } = DateTime.Today;

        [Timestamp]
        public byte[]? RowVersion { get; set; }

        [DisplayName("Developer")]
        [Range(1, int.MaxValue, ErrorMessage = "At least one developer must be chosen")]
        public int DeveloperId { get; set; }

        //[ForeignKey("DeveloperId")]
        //public Developer Developer { get; set; }

        public string? Image { get; set; }

        [ValidateNever]
        public List<SelectListItem> Developers { get; set; }

        [ValidateNever]
        public List<SelectListItem> Categories { get; set; }

        [ValidateNever]
        [DisplayName("Player Types")]
        public List<SelectListItem> PlayerTypes { get; set; }

    }
}
