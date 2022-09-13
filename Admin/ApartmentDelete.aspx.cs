using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentDelete : System.Web.UI.Page
    {
        private readonly ApartmentRepository _apartmentRepository;
        public ApartmentDelete()
        {
            _apartmentRepository = new ApartmentRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            string qryStrId = Request.QueryString["id"];
            int? id = null;

            if (!string.IsNullOrEmpty(qryStrId))
            {
                id = int.Parse(qryStrId);
                var dbApartment = _apartmentRepository.GetApartment(id.Value);
                SetFormApartment(dbApartment);
            }
        }

        private void SetFormApartment(Apartment dbApartment)
        {
            lblApartmentOwner.Text = dbApartment.OwnerName;
            lblName.Text = dbApartment.Name;
            lblAddress.Text = dbApartment.Address;
            lblCity.Text = dbApartment.CityName;
        }

        protected void lbConfirmDelete_Click(object sender, EventArgs e)
        {
            string qryStrId = Request.QueryString["id"];
            var id = int.Parse(qryStrId);

            _apartmentRepository.DeleteApartment(id);

            Response.Redirect("ApartmentList.aspx");
        }

        protected void lbBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentList.aspx");
        }
    }
}