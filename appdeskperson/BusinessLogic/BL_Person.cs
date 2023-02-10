using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using appdeskperson.DataAccess;
using appdeskperson.BusinessLogic;

namespace appdeskperson.BusinessLogic
{
    public class BL_Person
    {
        public static DataTable ListPerson(string strPerson)
        {
            return DA_Person.ListPerson(strPerson);
        }

        public static bool InsertPerson(BE_Person Person)
        {
            return DA_Person.InsertPerson(Person);
        }
        public static bool UpdatePerson(BE_Person Person)
        {
            return DA_Person.UpdatePerson(Person);
        }

        public static bool DeletePerson(string idPerson)
        {
            return DA_Person.DeletePerson(idPerson);
        }

        public static string GenerateIdPerson()
        {
            return DA_Person.GenerateIdPerson();
        }
    }
}
