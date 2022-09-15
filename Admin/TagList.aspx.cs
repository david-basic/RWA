using DataLayer.Models;
using DataLayer.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Admin
{
    public partial class TagList : System.Web.UI.Page
    {

        private readonly TagRepository _tagRepository;
        public TagList()
        {
            _tagRepository = new TagRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            RebindTags();
        }

        private void RebindTags()
        {
            gvListaTagova.DataSource = _tagRepository.GetTags();
            gvListaTagova.DataBind();
        }
    }
}