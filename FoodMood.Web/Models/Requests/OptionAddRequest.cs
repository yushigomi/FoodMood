using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMood.Web.Models.Requests
{
    public class OptionAddRequest
    {
        public string Name { get; set; }
        public int? GenreId { get; set; }

    }
}