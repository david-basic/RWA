using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Public.Models.ViewModels
{
    public class StarsVM
    {
        public int Stars { get; set; }
        public int TotalReviews { get; set; }
        public bool ShowTotalReviews { get; set; }
    }
}