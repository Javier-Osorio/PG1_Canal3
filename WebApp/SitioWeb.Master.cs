using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class SitioWeb : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["logueado"] == null)
            {
                Response.Redirect(ResolveUrl("~/WebLogIn.aspx"));
            }
            else
            {
                lblPrueba.Text = Session["logueado"].ToString();
            }           
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Session.Abandon();

            Response.Redirect(ResolveUrl("~/WebLogIn.aspx"));
        }
    }
}