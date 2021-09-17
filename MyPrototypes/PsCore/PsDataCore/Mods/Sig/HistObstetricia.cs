using System;
using System.Data.SqlClient;

namespace PsDataCore.Mods.Sig
{
   public class HistObstetricia
    {

        public int GetLastExp(int pacienteId, string autor)
        {
            var sql = new SqlDataCore();
            try
            {
                var ds = sql.GetDataBySP("WHOBSTETRICIA",
                    new SqlParameter("@dec", 5),
                    new SqlParameter("@PACIENTEID", pacienteId),
                    new SqlParameter("@autor", autor));

                return ds.Tables[0].Rows.Count == 0 ? 0 : Convert.ToInt32(ds.Tables[0].Rows[0][0].ToString());
            }
            catch (Exception)
            {
                return 0;
            }


        }
    }
}
