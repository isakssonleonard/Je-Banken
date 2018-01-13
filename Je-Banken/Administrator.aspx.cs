using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Xml.Linq;
using System.Web.UI.HtmlControls;

namespace Je_Banken
{
    public partial class Admin : System.Web.UI.Page
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

        protected void admin_knapp1_Click(object sender, EventArgs e)
        {
            GridView.DataSource = null;
            DataTable table = new DataTable();

            table.Columns.Add("Förnamn");
            table.Columns.Add("Efternamn");
            table.Columns.Add("Provtyp");
           
            bool pass = false;

            foreach (Person person in Person.getPersons())
            {
                foreach (Test test in person.tests)
                {
                    if (test.testtyp == "Licensieringtest" && test.passed)
                    {
                        table.Rows.Add(person.firstName, person.lastName, "ÅKU");
                        pass = true;
                    }

                    else if(test.testtyp == "ÅKU" && test.passed)
                    {
                        table.Rows.RemoveAt(table.Rows.Count - 1);
                        pass = true;
                        break;
                    }
                }

                if (!pass)
                {
                    table.Rows.Add(person.firstName, person.lastName, "Licensieringtest");
                }

                pass = false;                
            }

            GridView.DataSource = table;
            GridView.DataBind();
        }

        protected void admin_knapp2_Click(object sender, EventArgs e)
        {
            GridView.DataSource = null;
            DataTable table = new DataTable();

            table.Columns.Add("Förnamn");
            table.Columns.Add("Efternamn");
            table.Columns.Add("Datum");
            table.Columns.Add("Provtyp");

            List<Person> persons = Person.getPersons();

            bool run1 = false;
            bool run2 = false;

            foreach (Person person in persons)
            {
                run1 = false;
                run2 = false;

                foreach (Test test in person.tests)
                {
                    if (test.testtyp == "Licensieringtest")
                    {
                        if (!run1)
                        {
                            Test temptest = person.tests.OrderByDescending(x => x.time).Where(y => y.testtyp == "Licensieringtest").First();

                            table.Rows.Add(persons.Where(x => x.personID == temptest.personFK).Select(x => x.firstName).Single(),
                            persons.Where(x => x.personID == temptest.personFK).Select(x => x.lastName).Single(),
                            temptest.time, temptest.testtyp);

                            run1 = true;
                        }
                      
                    }

                    else if (test.testtyp == "ÅKU")
                    {
                        if (!run2)
                        {
                            Test temptest = person.tests.OrderByDescending(x => x.time).Where(y => y.testtyp == "ÅKU").First();

                            table.Rows.Add(persons.Where(x => x.personID == temptest.personFK).Select(x => x.firstName).Single(),
                            persons.Where(x => x.personID == temptest.personFK).Select(x => x.lastName).Single(),
                            temptest.time, temptest.testtyp);

                            run2 = true;
                        }                        
                    }
                }
            }

            GridView.DataSource = table;
            GridView.DataBind();
        }

        protected void admin_knapp3_Click(object sender, EventArgs e)
        {
            GridView.DataSource = null;
            DataTable table = new DataTable();

            table.Columns.Add("Förnamn");
            table.Columns.Add("Efternamn");
            table.Columns.Add("Provresultat");
            table.Columns.Add("Etik");
            table.Columns.Add("Ekonomi");
            table.Columns.Add("Produkter");
            table.Columns.Add("Provtyp");

            List<Person> persons = Person.getPersons();

            bool run1 = false;
            bool run2 = false;

            foreach (Person person in persons)
            {
                run1 = false;
                run2 = false;

                foreach (Test test in person.tests)
                {
                    if (test.testtyp == "Licensieringtest")
                    {
                        if (!run1)
                        {
                            Test temptest = person.tests.OrderByDescending(x => x.time).Where(y => y.testtyp == "Licensieringtest").First();

                            table.Rows.Add(persons.Where(x => x.personID == temptest.personFK).Select(x => x.firstName).Single(),
                            persons.Where(x => x.personID == temptest.personFK).Select(x => x.lastName).Single(),
                            XElement.Parse(temptest.xmlString).Attribute("totalpoäng").Value + "/" + Quiz.getQuestions(temptest.testtyp).Count,
                            XElement.Parse(temptest.xmlString).Attribute("poängetik").Value + "/" + Quiz.getQuestions(temptest.testtyp).Where(x => x.Subject == "Etik och regelverk").Count(),
                            XElement.Parse(temptest.xmlString).Attribute("poängekonomi").Value + "/" + Quiz.getQuestions(temptest.testtyp).Where(x => x.Subject == "Ekonomi").Count(),
                            XElement.Parse(temptest.xmlString).Attribute("poängprodukter").Value + "/" + Quiz.getQuestions(temptest.testtyp).Where(x => x.Subject == "Produkter").Count(),
                            temptest.testtyp);

                            run1 = true;
                        }
                    }

                    else if (test.testtyp == "ÅKU")
                    {
                        if (!run2)
                        {
                            Test temptest = person.tests.OrderByDescending(x => x.time).Where(y => y.testtyp == "ÅKU").First();

                            table.Rows.Add(persons.Where(x => x.personID == temptest.personFK).Select(x => x.firstName).Single(),
                            persons.Where(x => x.personID == temptest.personFK).Select(x => x.lastName).Single(),
                            XElement.Parse(temptest.xmlString).Attribute("totalpoäng").Value + "/" + Quiz.getQuestions(temptest.testtyp).Count,
                            XElement.Parse(temptest.xmlString).Attribute("poängetik").Value + "/" + Quiz.getQuestions(temptest.testtyp).Where(x => x.Subject == "Etik och regelverk").Count(),
                            XElement.Parse(temptest.xmlString).Attribute("poängekonomi").Value + "/" + Quiz.getQuestions(temptest.testtyp).Where(x => x.Subject == "Ekonomi").Count(),
                            XElement.Parse(temptest.xmlString).Attribute("poängprodukter").Value + "/" + Quiz.getQuestions(temptest.testtyp).Where(x => x.Subject == "Produkter").Count(),
                            temptest.testtyp);

                            run2 = true;
                        }
                    }                   
                }
            }

            GridView.DataSource = table;
            GridView.DataBind();
        }

        protected void admin_knapp4_Click(object sender, EventArgs e)
        {
            GridView.DataSource = null;
            DataTable table = new DataTable();

            table.Columns.Add("Förnamn");
            table.Columns.Add("Efternamn");
            table.Columns.Add("Resultat");
            table.Columns.Add("Provtyp");

            List<Person> persons = Person.getPersons();

            bool run1 = false;
            bool run2 = false;

            foreach (Person person in persons)
            {
                run1 = false;
                run2 = false;
                
                foreach (Test test in person.tests)
                {                   
                    if (test.testtyp == "Licensieringtest")
                    {

                        Test temptest = person.tests.OrderByDescending(x => x.time).Where(y => y.testtyp == "Licensieringtest").First();

                        string passtext = "";

                        if (temptest.passed)
                        {
                            passtext = "Godkänd";
                        }

                        else
                        {
                            passtext = "Underkänd";
                        }

                        if (!run1)
                        {
                            table.Rows.Add(persons.Where(x => x.personID == temptest.personFK).Select(x => x.firstName).Single(),
                            persons.Where(x => x.personID == temptest.personFK).Select(x => x.lastName).Single(),
                            passtext, temptest.testtyp);

                            run1 = true;
                        }
                    }

                    else if (test.testtyp == "ÅKU")
                    {
                        Test temptest = person.tests.OrderByDescending(x => x.time).Where(y => y.testtyp == "ÅKU").First();

                        string passtext = "";

                        if (temptest.passed)
                        {
                            passtext = "Godkänd";
                        }

                        else
                        {
                            passtext = "Underkänd";
                        }

                        if (!run2)
                        {
                            table.Rows.Add(persons.Where(x => x.personID == temptest.personFK).Select(x => x.firstName).Single(),
                            persons.Where(x => x.personID == temptest.personFK).Select(x => x.lastName).Single(),
                            passtext, temptest.testtyp);

                            run2 = true;
                        }
                    }
                }
            }

            GridView.DataSource = table;
            GridView.DataBind();
        }
    }
}