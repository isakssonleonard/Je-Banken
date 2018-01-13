using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Je_Banken
{
    public partial class Login : System.Web.UI.Page
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

            if (Master.FindControl("error") != null)
            {
                Databas.errorMessage = Master.FindControl("error") as HtmlGenericControl;
            }
        }

        protected void anstalld_Command(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            Session["test"] = login.whichTest(Convert.ToInt32((e.CommandArgument)));
            Session["user"] = e.CommandArgument;            
            Session["link"] = new HyperLink() {Text = "Tidgare prov", Enabled = true };
            Response.Redirect("Provstart.aspx");
        }

        protected void Administrator_Click(object sender, EventArgs e)
        {                       
            HyperLink link = Master.FindControl("passed_test_text") as HyperLink;
            Session["link"] = new HyperLink() { Text = "", Enabled = false };
            Response.Redirect("Administrator.aspx");
        }
    }
}