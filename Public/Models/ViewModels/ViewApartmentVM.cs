using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Public.Models.ViewModels
{
    public class ViewApartmentVM
    {
        public int ApartmentId { get; set; } 
        public string UserId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "E-mail address is required")]
        public string Email { get; set; } 
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage = "You need to specify arrival date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date), Required(ErrorMessage = "You need to specify return date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime EndDate { get; set; }

        public string Details { get; set; }

        public AptMVC Apartment { get; set; }
        public bool ShowReviewForm { get; set; }
    }
}