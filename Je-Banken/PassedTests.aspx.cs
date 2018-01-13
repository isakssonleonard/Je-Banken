using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Xml.Linq;

namespace Je_Banken
{
    public partial class PassedTests : System.Web.UI.Page
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

            showPassedTests();
        }

        private void showPassedTests()
        {
            HtmlTableRow tablerow = new HtmlTableRow();
            HtmlTableCell cell1 = new HtmlTableCell();
            HtmlTableCell cell2 = new HtmlTableCell();

            bool run = false;
            string[] sql = { "SELECT top 1 fornamn, efternamn, datum, xmlstring, provtyp, godkand FROM person INNER JOIN prov ON person.person_id_pk = prov.person_id_fk  WHERE provtyp = 'ÅKU' AND person_id_pk = @add AND godkand = 'true' ORDER BY datum desc", "SELECT top 1 fornamn, efternamn, datum, xmlstring, provtyp FROM person INNER JOIN prov ON person.person_id_pk = prov.person_id_fk  WHERE provtyp = 'Licensieringtest' AND person_id_pk = @add AND godkand = 'true' ORDER BY datum desc" };

            foreach (string sqlquery in sql)
            {
                foreach (DataRow row in Databas.getData(sqlquery, new List<SqlParameter>() { new SqlParameter("@add", Convert.ToInt16(Session["user"]))}).Rows)
                {
                    if (!run)
                    {
                        run = true;
                        firstName.InnerText = row["fornamn"].ToString();
                        lastName.InnerText = row["efternamn"].ToString();
                        time.InnerText = Convert.ToDateTime(row["datum"]).ToShortDateString();
                        godkänd.InnerText = "Godkänd";
                        try
                        {
                            totalPoäng.InnerText = XElement.Parse(row["xmlstring"].ToString()).Attribute("totalpoäng").Value;
                            totalPoängEtik.InnerText = XElement.Parse(row["xmlstring"].ToString()).Attribute("poängetik").Value;
                            totalPoängEkonomi.InnerText = XElement.Parse(row["xmlstring"].ToString()).Attribute("poängekonomi").Value;
                            totalPoängProdukter.InnerText = XElement.Parse(row["xmlstring"].ToString()).Attribute("poängprodukter").Value;
                        }
                        catch (Exception ex)
                        {
                            if (Master.FindControl("error") as HtmlGenericControl != null)
                            {
                                HtmlGenericControl errormessage = Master.FindControl("error") as HtmlGenericControl;
                                errormessage.Visible = true;
                                errormessage.InnerText = ex.Message;
                            }
                        }
                       
                        provTyp.InnerText = row["provtyp"].ToString();

                        try
                        {
                            XDocument xDoc = XDocument.Parse(row["xmlstring"].ToString());
                            List<Questions> questions = new List<Questions>();

                            if (xDoc.Descendants("fraga").Count() == 15)
                            {
                                questions = Quiz.getQuestions("ÅKU");
                            }

                            else
                            {
                                questions = Quiz.getQuestions("Licensprov");
                            }

                            List<Anwer> anwers = Quiz.getAnwers(row["xmlstring"].ToString());

                            for (int i = 0; i < anwers.Count; i++)
                            {
                                if (questions[i].Type == "One")
                                {

                                    tidigare_prov_box.Controls.Add(new Label() { Text = "Fråga " + (i + 1).ToString() + ": " + anwers[i].Title, CssClass = "title" });
                                    tidigare_prov_box.Controls.Add(new Label() { Text = anwers[i].Subject, CssClass = "text" });

                                    bool runed = false;

                                    RadioButtonList radiolist = new RadioButtonList();
                                    radiolist.Attributes.Add("class", "background-color");
                                    radiolist.Enabled = false;
                                    tidigare_prov_box.Controls.Add(radiolist);
                                    radiolist.CssClass = "font";
                                    questions[i].Options.ForEach(x => radiolist.Items.Add(x.name));

                                    for (int j = 0; j < radiolist.Items.Count; j++)
                                    {
                                        if (radiolist.Items[j].Text == anwers[i].Anwers.Single())
                                        {
                                            radiolist.Items[j].Selected = true;
                                        }
                                    }

                                    foreach (var question in questions[i].Options)
                                    {
                                        if (question.name == anwers[i].Anwers.Single() && question.isture)
                                        {
                                            if (!runed)
                                            {
                                                Panel panel = new Panel();
                                                panel.Attributes.Add("class", "återkoppling-box");
                                                panel.Controls.Add(new Label() { Text = "Rätt", CssClass = "passtext" });
                                                panel.Controls.Add(new Label() { Text = "Korrekt svar", CssClass = "passtext" });
                                                panel.Controls.Add(new Label() { Text = "* " + anwers[i].Anwers.Single(), CssClass = "passtext" });
                                                tidigare_prov_box.Controls.Add(panel);
                                                runed = true;
                                            }
                                        }
                                    }

                                    if (!runed)
                                    {
                                        Panel panel = new Panel();
                                        panel.Attributes.Add("class", "återkoppling-box");
                                        panel.Controls.Add(new Label() { Text = "Fel", CssClass = "passtext" });
                                        panel.Controls.Add(new Label() { Text = "Korrekt svar", CssClass = "passtext" });
                                        panel.Controls.Add(new Label() { Text = "* " + questions[i].Options.Where(x => x.isture == true).Single().name, CssClass = "passtext" });
                                        tidigare_prov_box.Controls.Add(panel);
                                        runed = true;
                                    }                                   
                                }

                                else if (questions[i].Type == "Multi")
                                {
                                    CheckBoxList checkboxlist = new CheckBoxList();

                                    questions[i].Options.ForEach(x => checkboxlist.Items.Add(x.name));

                                    for (int j = 0; j < checkboxlist.Items.Count; j++)
                                    {
                                        if (anwers[i].Anwers.Exists(x => x == checkboxlist.Items[j].Text))
                                        {
                                            checkboxlist.Items[j].Selected = true;
                                        }                                     
                                    }

                                    checkboxlist.Enabled = false;
                                    tidigare_prov_box.Controls.Add(new Label() { Text = "Fråga " + (i + 1).ToString() + ": " + anwers[i].Title, CssClass = "title" });
                                    tidigare_prov_box.Controls.Add(new Label() { Text = anwers[i].Subject, CssClass = "text" });
                                    tidigare_prov_box.Controls.Add(checkboxlist);

                                    int counter = 0;
                                    bool wrong = true;

                                    foreach (string anwer in anwers[i].Anwers)
                                    {
                                        foreach (var question in questions[i].Options)
                                        {                                            
                                            if (question.name == anwer && question.isture)
                                            {
                                                counter++;
                                            }

                                            else if (question.name == anwer && question.isture == false)
                                            {
                                                wrong = false;
                                                break;
                                            }
                                        }
                                    }

                                    if (questions[i].Options.Count(x => x.isture == true) == counter && wrong)
                                    {

                                        Panel panel = new Panel();
                                        panel.Attributes.Add("class", "återkoppling-box");
                                        panel.Controls.Add(new Label() { Text = "Rätt", CssClass = "passtext" });
                                        panel.Controls.Add(new Label() { Text = "Korrekta svar", CssClass = "passtext" });
                                        
                                        foreach (Option option in questions[i].Options.Where(x => x.isture == true))
                                        {
                                            panel.Controls.Add(new Label() { Text = "* " + option.name, CssClass = "passtext" });
                                        }

                                        tidigare_prov_box.Controls.Add(panel);
                                    }

                                    else
                                    {
                                        Panel panel = new Panel();
                                        panel.Attributes.Add("class", "återkoppling-box");
                                        panel.Controls.Add(new Label() { Text = "Fel", CssClass = "passtext" });
                                        panel.Controls.Add(new Label() { Text = "Korrekta svar", CssClass = "passtext" });

                                        foreach (Option option in questions[i].Options.Where(x => x.isture == true))
                                        {
                                            panel.Controls.Add(new Label() { Text = "* " + option.name, CssClass = "passtext" });
                                        }

                                        tidigare_prov_box.Controls.Add(panel);
                                    }

                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            if (Master.FindControl("error") as HtmlGenericControl != null)
                            {
                                HtmlGenericControl errormessage = Master.FindControl("error") as HtmlGenericControl;
                                errormessage.Visible = true;
                                errormessage.InnerText = ex.Message;
                            }
                        }

                        break;
                    }
                }
            }

            if (run == false)
            {
                tidigare_prov_text.InnerText = "Inga prov att visa";
                table.Visible = false;
            }         
        }

        private void click_link(object sender, EventArgs e)
        {
            
        }
    }
}