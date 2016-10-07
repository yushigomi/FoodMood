using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMood.Web.Models.Domains
{
    public class Option
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Genre Genre { get; set; }
    }
}