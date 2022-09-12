using DataLayer.Repositories;
using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class ApartmentEditor : System.Web.UI.Page
    {
        private readonly ApartmentOwnerRepository _apartmentOwnerRepository;
        private readonly ApartmentRepository _apartmentRepository;
        private readonly StatusRepository _statusRepository;
        private readonly CityRepository _cityRepository;
        private readonly TagRepository _tagRepository;

        public ApartmentEditor()
        {
            _apartmentOwnerRepository = new ApartmentOwnerRepository();
            _apartmentRepository = new ApartmentRepository();
            _statusRepository = new StatusRepository();
            _cityRepository = new CityRepository();
            _tagRepository = new TagRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string qryStrId = Request.QueryString["id"];
                int? id = null;
                
                if (!string.IsNullOrEmpty(qryStrId))
                {
                    id = int.Parse(qryStrId);
                }

                if (id.HasValue)
                {
                    var dbApartment = _apartmentRepository.GetApartment(id.Value);
                    dbApartment.Tags = _apartmentRepository.GetApartmentTags(id.Value);
                    SetExistingApartment(dbApartment);
                }

                RebindApartmentOwners();
                RebindCities();
                RebindStatuses();
                RebindTags();
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
        private void RebindTags()
        {
            ddlTags.DataSource = _tagRepository.GetTags();
            ddlTags.DataBind();
        }

        protected void ddlTags_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlTags.SelectedIndex == 0)
            {
                return;
            }

            var tags = GetRepeaterTags();
            var newTag = GetSelectedTag();

            if (tags.Any(x => x.Id == newTag.Id))
            {
                return;
            }

            tags.Add(newTag);

            repTags.DataSource = tags;
            repTags.DataBind();
        }

        private List<Tag> GetRepeaterTags()
        {
            var repTagsItems = repTags.Items;
            var tags = new List<Tag>();

            foreach (RepeaterItem item in repTagsItems)
            {
                var tag = new Tag
                {
                    Id = int.Parse((item.FindControl("hidTagId") as HiddenField).Value),
                    Name = (item.FindControl("txtTagName") as Label).Text
                };
                tags.Add(tag);
            }
            return tags;
        }

        private Tag GetSelectedTag()
        {
            var selectedValue = ddlTags.SelectedItem.Value;

            var newTag = new Tag
            {
                Id = int.Parse(ddlTags.SelectedItem.Value),
                Name = ddlTags.SelectedItem.Text
            };

            return newTag;
        }

        protected void btnDeleteTag_Click(object sender, EventArgs e)
        {
            var tags = GetRepeaterTags();

            var lbSender = (LinkButton)sender;
            var parentItem = (RepeaterItem)lbSender.Parent;
            var hidTagId = (HiddenField)parentItem.FindControl("hidTagId");
            var tagId = int.Parse(hidTagId.Value);
            var existingTag = tags.FirstOrDefault(x => x.Id == tagId);
            tags.Remove(existingTag);

            repTags.DataSource = tags;
            repTags.DataBind();
        }

        protected void lblSave_Click(object sender, EventArgs e)
        {
            var isNewApartment = (Request.QueryString["id"] == null);

            if (isNewApartment)
            {
                var apartment = GetFormApartment();
                _apartmentRepository.CreateApartment(apartment);
            }
            else
            {
                var apartment = GetFormApartment();
                apartment.Id = int.Parse(Request.QueryString["id"]);
                _apartmentRepository.UpdateApartment(apartment);
            }

            Response.Redirect("ApartmentList.aspx");
        }

        private Apartment GetFormApartment()
        {
            int statusId = int.Parse(ddlStatus.SelectedValue);

            int? cityId = int.Parse(ddlCity.SelectedValue);
            if (cityId == 0)
            {
                cityId = null;
            }

            int ownerId = int.Parse(ddlApartmentOwner.SelectedValue);
            decimal price = decimal.Parse(tbPrice.Text);

            int? maxAdults = null;
            if (!string.IsNullOrEmpty(tbMaxAdults.Text))
            {
                maxAdults = int.Parse(tbMaxAdults.Text);
            }

            int? maxChildren = null;
            if (!string.IsNullOrEmpty(tbMaxChildren.Text))
            {
                maxChildren = int.Parse(tbMaxChildren.Text);
            }

            int? totalRooms = null;
            if (!string.IsNullOrEmpty(tbTotalRooms.Text))
            {
                totalRooms = int.Parse(tbTotalRooms.Text);
            }

            int? beachDistance = null;
            if (!string.IsNullOrEmpty(tbBeachDistance.Text))
            {
                beachDistance = int.Parse(tbBeachDistance.Text);
            }

            return new Apartment
            {
                Guid = Guid.NewGuid(),
                OwnerId = ownerId,
                TypeId = 999, // hardcoded
                StatusId = statusId,
                CityId = cityId,
                Address = tbAddress.Text,
                Name = tbName.Text,
                Price = price,
                MaxAdults = maxAdults,
                MaxChildren = maxChildren,
                TotalRooms = totalRooms,
                BeachDistance = beachDistance,
                Tags = GetRepeaterTags()
            };
        }

        private void SetExistingApartment(Apartment apartment)
        {
            ddlStatus.SelectedValue = apartment.StatusId.ToString();
            ddlApartmentOwner.SelectedValue = apartment.OwnerId.ToString();
            tbName.Text = apartment.Name;
            tbAddress.Text = apartment.Address;
            ddlCity.SelectedValue = apartment.CityId.ToString();
            tbPrice.Text = apartment.Price.ToString();
            tbMaxAdults.Text = apartment.MaxAdults?.ToString();
            tbMaxChildren.Text = apartment.MaxChildren?.ToString();
            tbTotalRooms.Text = apartment.TotalRooms?.ToString();
            tbBeachDistance.Text = apartment.BeachDistance?.ToString();

            repTags.DataSource = apartment.Tags;
            repTags.DataBind();
        }

        protected void lbReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("ApartmentList.aspx");
        }
    }
}