using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace Je_Banken
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {                              
                HyperLink link1 = Master.FindControl("links").FindControl("passed_test_text") as HyperLink;
                if (Session["link"] != null)
                {
                    link1.Text = (Session["link"] as HyperLink).Text;
                    link1.Enabled = (Session["link"] as HyperLink).Enabled;
                }
            }
        }
    }
}