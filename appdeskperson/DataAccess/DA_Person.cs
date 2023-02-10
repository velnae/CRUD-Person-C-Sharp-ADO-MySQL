using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using appdeskperson.BusinessLogic;

namespace appdeskperson.DataAccess
{
    public class DA_Person : DA_BaseClass
    {
        public static DataTable ListPerson(string strPerson)
        {
            string strSQL = "select * from tperson";
            strSQL += " where dni like '%" + strPerson + "%'";
            strSQL += " or firstName like '%" + strPerson + "%'";
            strSQL += " or surName like '%" + strPerson + "%'";
            strSQL += " order by idPerson desc; ";

            return GetDataTable(strSQL);
        }

        public static bool InsertPerson(BE_Person Person)
        {
            string strSQL = "insert into tperson(idPerson,dni,firstName,surName,birthDate) " +
                " values(";
            strSQL += "'" + Person.idPerson + "',";
            strSQL += "'" + Person.dni + "',";
            strSQL += "'" + Person.firstName + "',";
            strSQL += "'" + Person.surName + "',";
            strSQL += "'" + Person.birthDate.ToString("yyyy-MM-dd") + "'";
            strSQL += ");";

            return ExecTransaction(strSQL);
        }

        public static bool UpdatePerson(BE_Person Person)
        {
            string strSQL = "update tperson ";
            strSQL += "set dni='" + Person.dni + "',";
            strSQL += "firstName='" + Person.firstName+ "',";
            strSQL += "surName='" + Person.surName+ "',";
            strSQL += "birthDate='" + Person.birthDate.ToString("yyyy-MM-dd") + "'";
            strSQL += " where idPerson = '" + Person.idPerson + "';";

            return ExecTransaction(strSQL);
        }

        public static bool DeletePerson(string idPerson)
        {
            string strSQL = "delete from tperson";
            strSQL += " where idPerson= '" + idPerson + "';";

            return ExecTransaction(strSQL);
        }

        public static string GenerateIdPerson()
        {
            return System.Guid.NewGuid().ToString();
        }
    }
}
