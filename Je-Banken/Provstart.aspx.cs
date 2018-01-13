using System;
using System.Web.UI.WebControls;

namespace Je_Banken
{
    public partial class ValAvTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                title.Text = Session["test"].ToString();

                if (Session["test"].ToString() == "Du har klarat ÅKU provet så återkom om ett år")
                {
                    prov_knapp.Enabled = false;
                }

                HyperLink link1 = Master.FindControl("links").FindControl("passed_test_text") as HyperLink;
                if (Session["link"] != null)
                {
                    link1.Text = (Session["link"] as HyperLink).Text;
                    link1.Enabled = (Session["link"] as HyperLink).Enabled;
                }
            }
        }

        protected void prov_knapp_Click(object sender, EventArgs e)
        {
            Response.Redirect("prov.aspx");
        }
    }
}