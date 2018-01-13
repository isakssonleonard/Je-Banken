using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Je_Banken
{
    public partial class Prov : System.Web.UI.Page
    {        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["clicked"] = false;

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

            generateQuestions(Quiz.getQuestions(Session["test"].ToString()));
        }

        private void generateQuestions(List<Questions> lista)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.ID = "section";
            div.Attributes.Add("class", "quizz-text");

            Panel panel = new Panel();
            panel.ID = "panel";
                                                   
            int id = 1;

            foreach (Questions question in lista)
            {
                if (question.Type == "One")
                {
                    Label title = new Label();
                    title.Text = "Fråga " + id.ToString() + ": " + question.Title;
                    title.Attributes.Add("class", "title");
                                       
                    Label subject = new Label();
                    subject.Text = "Ämne: " + question.Subject;
                    subject.Attributes.Add("class", "text");
                                       
                    RadioButtonList radiolist = new RadioButtonList();                    
                    radiolist.ID = id.ToString();
                    radiolist.Attributes.Add("class", "list-style");

                    foreach (var option in question.Options)
                    {
                        radiolist.Items.Add(option.name);
                    }
                   
                    panel.Controls.Add(title);
                    panel.Controls.Add(subject);
                    if (question.Picture != "")
                    {
                        Image image = new Image();
                        image.Attributes.Add("class", "picture");
                        image.ImageUrl = "Pictures/" + question.Picture;
                        panel.Controls.Add(image);
                    }
                    panel.Controls.Add(radiolist);
                    div.Controls.Add(panel);
                }

                else
                {
                    Label title = new Label();
                    title.Text = "Fråga " + id.ToString() + ": " + question.Title;
                    
                    Label subject = new Label();                    
                    subject.Text = "Ämne: " + question.Subject;
                    subject.Attributes.Add("class", "text");

                    CheckBoxList checkboxlist = new CheckBoxList();
                    checkboxlist.ID = id.ToString();
                    checkboxlist.Attributes.Add("class", "list-style");

                    foreach (var option in question.Options)
                    {
                        checkboxlist.Items.Add(option.name);
                    }

                    panel.Controls.Add(title);
                    panel.Controls.Add(subject);
                    panel.Controls.Add(checkboxlist);
                    div.Controls.Add(panel);
                }
                id++;
            }
            prov_div.Controls.Add(div);           
        }

        private void Feedback(Feedback result)
        {
            HtmlGenericControl div = new HtmlGenericControl("div");
            div.Attributes.Add("class", "feedback");

            if (result.passed)
            {
                div.Controls.Add(new Label() { Text = "Du är godkänd", CssClass = "pass" });
            }

            else
            {
                div.Controls.Add(new Label() { Text = "Du är underkänd", CssClass = "pass" });
              
            }

            div.Controls.Add(new Label() { Text = "Total poäng " + result.totalPoints, CssClass = "passtext text-fix" });
            div.Controls.Add(new Label() { Text = "Total poäng inom delområdet Ekonomi " + result.pointsEkonomi, CssClass = "passtext text-fix" });
            div.Controls.Add(new Label() { Text = "Total poäng inom delområdet Etik och regelverk " + result.pointsEtik, CssClass = "passtext text-fix" });
            div.Controls.Add(new Label() { Text = "Total poäng inom delområdet Produkter " + result.pointsProdukter, CssClass = "passtext text-fix" });


            submit_button.Text = "OK";
            submit_button.Width = 150;

            prov_div.Controls.Add(div);            
        }

        private bool validation()
        {
            foreach (Control control in Page.Controls)
            {
                foreach (Control childcontrol in prov_div.FindControl("panel").Controls)
                {
                    if (childcontrol is RadioButtonList)
                    {
                        if (((RadioButtonList)childcontrol).SelectedIndex == -1)
                        {
                            return true;
                        }
                    }

                    else if (childcontrol is CheckBoxList)
                    {
                        if ((((CheckBoxList)childcontrol).SelectedIndex == -1))
                        {
                            return true;
                        }
                    }

                }
            }

            return false;
        }

        protected void submit_button_Click(object sender, EventArgs e)
        {

            if (Convert.ToBoolean(ViewState["clicked"]))
            {
                Response.Redirect("Default.aspx");
                return;
            }

            else if (validation())
            {
                info.Visible = true;
                return;
            }

            info.Visible = false;
            List<Anwer> anwers = new List<Anwer>();
            int counter = 0;
            Anwer anwer = new Anwer();

            foreach (Control control in Page.Controls)
            {
                foreach (Control childcontrol in prov_div.FindControl("section").Controls)
                {
                    if (childcontrol is Panel)
                    {
                        foreach (var grandchildcontrol in (childcontrol).Controls)
                        {
                            if (3 == counter)
                            {
                                anwers.Add(anwer);
                                anwer = new Anwer();
                                counter = 0;
                            }

                            if (anwer.Title == "")
                            {
                                anwer.Title = ((Label)grandchildcontrol).Text;
                                counter++;
                            }

                            else if (anwer.Subject == "")
                            {
                                anwer.Subject = ((Label)grandchildcontrol).Text;
                                counter++;
                            }

                            else if (grandchildcontrol is CheckBoxList)
                            {
                                anwer.Type = "Multi";
                                foreach (ListItem item in (((CheckBoxList)grandchildcontrol).Items))
                                {
                                    if (item.Selected)
                                    {
                                        anwer.Anwers.Add(item.Value);
                                    }
                                }
                                counter++;
                            }

                            else if (grandchildcontrol is RadioButtonList)
                            {
                                anwer.Type = "One";
                                anwer.Anwers.Add((((RadioButtonList)grandchildcontrol).SelectedValue));
                                counter++;
                            }                                                                                                            
                        }                        
                    }
                }
            }

            anwers.Add(anwer);
            clearForm();
            ViewState["clicked"] = true;                                              
            Feedback feedback = Quiz.correctTest(Session["test"].ToString(), anwers);
                      
            Databas.dataToDB("INSERT INTO prov(person_id_fk, xmlstring, datum, godkand, provtyp) VALUES(@id, @xmlstring, @datum, @godkand, @provtyp);", new List<SqlParameter>()
            {
                new SqlParameter("@id", Convert.ToInt16(Session["user"])),
                new SqlParameter("@datum", DateTime.Now),
                new SqlParameter("@godkand", feedback.passed),
                new SqlParameter("@xmlstring", Quiz.createXmlString(anwers)),
                new SqlParameter("@provtyp", Session["test"].ToString())
            });
            Feedback(feedback);
        }

        private void clearForm()
        {
            foreach (Control control in Page.Controls)
            {
                foreach (Control childcontrol in prov_div.FindControl("section").Controls)
                {
                    if (childcontrol is Panel)
                    {
                        (((Panel)childcontrol)).Controls.Clear();
                    }
                }
            }

        }

    }
}


