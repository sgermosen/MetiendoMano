using System;
//using System.Data;
using System.Data.SqlClient;

namespace PsDataCore.Mods.Gen
{
    public class WGeneric
    {
        public int GeneraCodigo(int autor, string tabla)
        {
            var sql = new SqlDataCore();
            try
            {
                var ds = sql.GetDataBySP("GEN.GENERACODIGO",
                    new SqlParameter("@table", tabla),
                    new SqlParameter("@autor", autor));

                return ds.Tables[0].Rows.Count == 0 ? 1 : Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                //if (ds.Tables[0].Rows.Count == 0)
                //{
                //    return 1;
                //}
                //else
                //{
                //    return Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
                //}
            }
            catch (Exception)
            {
                return 1;
            }


        }
    }
}
