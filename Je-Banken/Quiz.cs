using System;
using System.Data;
using System.Xml.Linq;
using System.Linq;
using System.Web;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data.SqlClient;

namespace Je_Banken
{
    public static class Quiz
    {
        private static int pointsEtik = 0;
        private static int pointsProdukter = 0;
        private static int pointsEkonomi = 0;
        private static int totalPoints = 0;

        public static List<Questions> getQuestions(string test)
        {
            pointsEtik = 0;
            pointsProdukter = 0;
            pointsEkonomi = 0;
            totalPoints = 0;

            switch (test)
            {
                case "ÅKU":
                    test = "XML/åku.xml";
                    break; 
                default:
                    test = "XML/licensieringtest.xml";
                    break;
            }

            string path = System.Web.HttpContext.Current.Server.MapPath(test);
            List<Questions> questions = new List<Questions>();

            try
            {
                XDocument xDoc = XDocument.Load(path);
                foreach (var node in xDoc.Descendants("question"))
                {
                    Questions question = new Questions();
                    question.Title = node.Attribute("title").Value.Trim();
                    question.Subject = node.Attribute("subject").Value.Trim();
                    question.Type = node.Attribute("type").Value.Trim();
                    question.Picture = node.Attribute("picture").Value.Trim();

                    bool a = Convert.ToBoolean(xDoc.Descendants("question").Where(x => x.Attribute("title").Value == question.Title).Descendants("option1").Attributes("istrue").Single().Value);

                    question.Options.Add(new Option()
                    {
                        name = node.Element("option1").Value.Trim(),
                        isture = Convert.ToBoolean(xDoc.Descendants("question").Where(x => x.Attribute("title").Value == question.Title).Descendants("option1").Attributes("istrue").Single().Value)
                    });
                    question.Options.Add(new Option()
                    {
                        name = node.Element("option2").Value.Trim(),
                        isture = Convert.ToBoolean(xDoc.Descendants("question").Where(x => x.Attribute("title").Value == question.Title).Descendants("option2").Attributes("istrue").Single().Value)
                    });
                    question.Options.Add(new Option()
                    {
                        name = node.Element("option3").Value.Trim(),
                        isture = Convert.ToBoolean(xDoc.Descendants("question").Where(x => x.Attribute("title").Value == question.Title).Descendants("option3").Attributes("istrue").Single().Value)
                    });
                    question.Options.Add(new Option()
                    {
                        name = node.Element("option4").Value.Trim(),
                        isture = Convert.ToBoolean(xDoc.Descendants("question").Where(x => x.Attribute("title").Value == question.Title).Descendants("option4").Attributes("istrue").Single().Value)
                    });

                    questions.Add(question);
                }
            }
            catch (Exception ex)
            {
                HtmlGenericControl errormessage = Databas.errorMessage;
                errormessage.Visible = true;
                errormessage.InnerText = ex.Message;
            }

            return questions;
        }
        
        public static Feedback correctTest(string test, List<Anwer> anwers)
        {
            List<Questions> questions = getQuestions(test);

            for (int i = 0; i < anwers.Count; i++)
            {
                if (questions[i].Type == "One")
                {
                    foreach (var question in questions[i].Options)
                    {
                        if (question.name == anwers[i].Anwers.Single() && question.isture)
                        {
                            totalPoints++;
                            categoryPoints(anwers[i]);
                        }
                    }
                }

                else if (questions[i].Type == "Multi")
                {
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
                        totalPoints++;
                        categoryPoints(anwers[i]);
                    }
                }
            }

            if (totalPoints >= Math.Round(questions.Count * 0.70))
            {
                if (pointsEtik >= Math.Round(questions.Count(x => x.Subject == "Etik och regelverk") * 0.60) && pointsProdukter >= Math.Round(questions.Count(x => x.Subject == "Produkter") * 0.60) && pointsEkonomi >= Math.Round(questions.Count(x => x.Subject == "Ekonomi") * 0.60))
                {
                    return new Feedback() { totalPoints = totalPoints, pointsEkonomi = pointsEkonomi, pointsEtik = pointsEtik, pointsProdukter = pointsProdukter, passed = true };
                }
            }

            return new Feedback() { totalPoints = totalPoints, pointsEkonomi = pointsEkonomi, pointsEtik = pointsEtik, pointsProdukter = pointsProdukter, passed = false };            
        }

        private static void categoryPoints(Anwer anwer)
        {

            if (anwer.Subject.Contains("Produkter"))
            {
                pointsProdukter++;
            }

            else if(anwer.Subject.Contains("Ekonomi"))
            {
                pointsEkonomi++;
            }

            else
            {
                pointsEtik++;
            }
        }

        public static string createXmlString(List<Anwer> anwers)
        {
            using (StringWriter str = new StringWriter())
            using (XmlTextWriter xml = new XmlTextWriter(str))
            {
                xml.WriteStartDocument();
                xml.WriteStartElement("fragor");
                xml.WriteAttributeString("totalpoäng", totalPoints.ToString());
                xml.WriteAttributeString("poängetik", pointsEtik.ToString());
                xml.WriteAttributeString("poängekonomi", pointsEkonomi.ToString());
                xml.WriteAttributeString("poängprodukter", pointsProdukter.ToString());
               
                foreach (Anwer anwer in anwers)
                {
                    xml.WriteStartElement("fraga");

                    xml.WriteAttributeString("Title", anwer.Title.Substring(anwer.Title.IndexOf(":") + 1).Trim());
             
                    xml.WriteAttributeString("Ämne", anwer.Subject.Substring(anwer.Subject.IndexOf(":") + 1).Trim());
                    
                    xml.WriteAttributeString("Typ", anwer.Type);
                    
                    foreach (string anweruser in anwer.Anwers)
                    {
                        xml.WriteStartElement("Val");
                        xml.WriteString(anweruser);
                        xml.WriteEndElement();
                    }

                    xml.WriteEndElement();
                }

                xml.WriteEndElement();
                return str.ToString();
            }

        }

        public static List<Anwer> getAnwers(string xmlstring)
        {
            try
            {
                XDocument xDoc = XDocument.Parse(xmlstring);

                List<Anwer> anwers = new List<Anwer>();

                foreach (var node in xDoc.Descendants("fraga"))
                {
                    Anwer anwer = new Anwer();
                    anwer.Title = node.Attribute("Title").Value;
                    anwer.Subject = node.Attribute("Ämne").Value;
                    anwer.Type = node.Attribute("Typ").Value;
                    
                    foreach (XElement val in xDoc.Descendants("fraga").Where(x => x.Attribute("Title").Value == anwer.Title).Elements("Val"))
                    {
                        anwer.Anwers.Add(val.Value);
                    }

                    anwers.Add(anwer);
                }

                return anwers;
            }
            catch (Exception ex)
            {
                HtmlGenericControl errormessage = Databas.errorMessage;
                errormessage.Visible = true;
                errormessage.InnerText = ex.Message;
            }

            return new List<Anwer>();
        }
    }
}