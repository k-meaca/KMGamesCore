﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class Game
    {
        public int GameId { get; set; }

        [DisplayName("Game")]
        [StringLength(100, ErrorMessage = "Game's title must be between {2} and {1} characters.", MinimumLength = 3)]
        [JsonPropertyName("Game")]
        public string Title { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:C}")]
        [DisplayName("Actual Price")]
        [Range(1, 1000,ErrorMessage = "Price must be non-negative and less than 1000.")]
        [JsonPropertyName("Price")]
        public decimal ActualPrice { get; set; }

        [JsonIgnore]
        [StringLength(250, ErrorMessage = "Description must be between {2} and {1} characters.", MinimumLength = 3)]
        public string? Description { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dddd/MM/yyyy}")]
        public DateTime Release { get; set; }

        [JsonIgnore]
        [Timestamp]
        public byte[]? RowVersion { get; set; }

        [DisplayName("Developer")]
        [Range(1,int.MaxValue,ErrorMessage = "At least one developer must be chosen")]
        public int DeveloperId { get; set; }

        [ForeignKey("DeveloperId")]
        public Developer Developer { get; set; }

        public string? Image { get; set; }

        [JsonIgnore]
        public virtual ICollection<Category> Categories { get; set; } = new List<Category>();

        [JsonIgnore]
        [DisplayName("Game Categories")]
        public virtual ICollection<GameCategory> GameCategories { get; set; }

        [JsonIgnore]
        public ICollection<PlayerType> PlayerTypes { get; set; } = new List<PlayerType>();

        [JsonIgnore]
        [DisplayName("Player Types")]
        public ICollection<PlayerGame> PlayersGames { get; set; }

        [JsonIgnore]
        public ICollection<GameInCart> GamesInCart { get; set; }

        [JsonIgnore]
        public ICollection<SaleDetail> SalesDetails { get; set; }

        [JsonIgnore]
        public ICollection<PurchasedGame>? PurchasedGames { get; set; }

        //----------CONSTRUCTOR----------//

        //public Game()
        //{
        //    GameCategories = new HashSet<GameCategory>();
        //    PlayersGame = new HashSet<PlayerGame>();
        //}
    }
}
