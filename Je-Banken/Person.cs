using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

namespace Je_Banken
{
    public class Person
    {
        public int personID { get; set; } = 0;
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public List<Test> tests { get; set; } = new List<Test>();

        public static List<Person> getPersons()
        {
            List<Person> persons = new List<Person>();

            foreach (DataRow row in Databas.getData("SELECT person_id_pk, fornamn, efternamn FROM person WHERE person.roll_id_fk = 1", null).Rows)
            {
                persons.Add(new Person() {  personID = Convert.ToInt16(row["person_id_pk"]), firstName = row["fornamn"].ToString(), lastName = row["efternamn"].ToString() });
            }


            foreach (Test test in Test.getTests())
            {
                foreach (Person person in persons)
                {
                    if (person.personID == test.personFK)
                    {
                        person.tests.Add(test);
                    }
                }
            }

            return persons;
        }
    }
}