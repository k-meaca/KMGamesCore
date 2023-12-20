using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KMGamesCore.Models.Models
{
    public class CountryCode
    {
        [Key]
        public int CodeId { get; set; }

        public string Country { get; set; }

        public string Code { get; set; }

    }
}
