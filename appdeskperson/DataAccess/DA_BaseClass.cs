using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using MySql.Data.MySqlClient;

namespace appdeskperson.DataAccess
{
       
    public class DA_BaseClass
    {
        public static string strCadenaConexion = "server=localhost;uid=root;" +
                "pwd=;database=dbstorage";

        // devuelve un datatable a partir de una sentencia select
        public static DataTable GetDataTable(string strSQL)
        {
            try
            {
                using (MySqlConnection cn = new MySqlConnection(strCadenaConexion))
                {
                    cn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(strSQL, cn))
                    {
                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            DataTable dt = new DataTable();
                            dt.Load(dr);
                            return dt;
                        }
                    }

                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        //Insert, udpate , delete
        public static bool ExecTransaction(string strSQL)
        {
            bool resultado = false;

            try
            {
                using (MySqlConnection cn = new MySqlConnection(strCadenaConexion))
                {
                    cn.Open();
                    using (MySqlTransaction trx = cn.BeginTransaction())
                    {
                        try
                        {
                            using (MySqlCommand cmd = new MySqlCommand(strSQL, cn))
                            {
                                cmd.Transaction = trx;
                                cmd.ExecuteNonQuery();
                            }
                            trx.Commit();
                            resultado = true;
                        }
                        catch (Exception)
                        {
                            trx.Rollback();
                            resultado = false;
                            throw;
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return resultado;
        }
    }

}
