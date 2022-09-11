using DataLayer.Repositories;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentList : System.Web.UI.Page
    {
        private readonly ApartmentRepository _apartmentRepository;
        public ApartmentList()
        {
            _apartmentRepository = new ApartmentRepository();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            gvListaApartmana.DataSource = _apartmentRepository.GetApartments();
            gvListaApartmana.DataBind();
        }
    }
}