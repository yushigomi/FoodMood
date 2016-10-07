using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FoodMood.Web.Models.Requests
{
    public class OptionUpdateRequest: OptionAddRequest
    {
        public int Id { get; set; }
    }
}