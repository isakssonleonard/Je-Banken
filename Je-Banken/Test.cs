using System;
using System.Data;
using System.Collections.Generic;

namespace Je_Banken
{
    public class Test
    {
        public int provID { get; set; } = 0;
        public int personFK { get; set; } = 0;
        public string xmlString { get; set; } = "";
        public DateTime time { get; set; }
        public string testtyp { get; set; } = "";
        public bool passed { get; set; }

        public static List<Test> getTests()
        {
            List<Test> tests = new List<Test>();
            
            foreach (DataRow row in Databas.getData("SELECT prov_id_pk, person_id_fk, xmlstring, datum, godkand, provtyp FROM prov", null).Rows)
            {
                tests.Add(new Test() { provID = Convert.ToInt16(row["prov_id_pk"]),
                    personFK = Convert.ToInt16(row["person_id_fk"]),
                    xmlString = row["xmlstring"].ToString(),
                    time = Convert.ToDateTime(row["datum"]),
                    passed = Convert.ToBoolean(row["godkand"]),
                    testtyp = row["provtyp"].ToString() });
            }

            return tests;
        }
    }
}