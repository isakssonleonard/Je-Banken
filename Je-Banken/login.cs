using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System;

namespace Je_Banken
{
    public class login
    {
        public static string whichTest(int id)
        {
            bool licensieringtestPassed = false;
            bool åkutestPassed = false;

            foreach (DataRow row in Databas.getData("SELECT provtyp, godkand FROM prov INNER JOIN person ON person_id_pk = prov.person_id_fk WHERE person.person_id_pk = @add", new List<SqlParameter>() { new SqlParameter("@add", id)}).Rows)
            {
                if (row["provtyp"].ToString() == "Licensieringtest" && Convert.ToBoolean(row["godkand"]) == true)
                {
                    licensieringtestPassed = true;
                }

                else if (row["provtyp"].ToString() == "ÅKU" && Convert.ToBoolean(row["godkand"]) == true)
                {
                    åkutestPassed = true;
                }
            }

            if (licensieringtestPassed && åkutestPassed)
            {
                return "Du har klarat ÅKU provet så återkom om ett år";
            }

            foreach (DataRow row in Databas.getData("SELECT provtyp, godkand FROM prov INNER JOIN person ON person_id_pk = prov.person_id_fk WHERE person.person_id_pk = @add", new List<SqlParameter>() { new SqlParameter("@add", id) }).Rows)
            {
                if (row["provtyp"].ToString() == "Licensieringtest" && Convert.ToBoolean(row["godkand"]) == true) 
                {
                    return "ÅKU";
                }
            }
            return "Licensieringtest";
        }
    }
}