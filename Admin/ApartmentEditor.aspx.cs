using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentEditor : System.Web.UI.Page
    {
        private readonly ApartmentOwnerRepository _apartmentOwnerRepository;
        private readonly StatusRepository _statusRepository;
        private readonly CityRepository _cityRepository;

        public ApartmentEditor()
        {
            _apartmentOwnerRepository = new ApartmentOwnerRepository();
            _statusRepository = new StatusRepository();
            _cityRepository = new CityRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RebindApartmentOwners();

                RebindCities();

                RebindStatuses();
            }
        }

        private void RebindApartmentOwners()
        {
            ddlApartmentOwner.DataSource = _apartmentOwnerRepository.GetApartmentOwners();
            ddlApartmentOwner.DataBind();
        }
        private void RebindCities()
        {
            ddlCity.DataSource = _cityRepository.GetCities();
            ddlCity.DataBind();
        }
        private void RebindStatuses()
        {
            ddlStatus.DataSource = _statusRepository.GetStatuses();
            ddlStatus.DataBind();
        }
    }
}