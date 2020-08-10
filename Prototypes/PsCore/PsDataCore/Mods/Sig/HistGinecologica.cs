using System;
using System.Data.SqlClient;

namespace PsDataCore.Mods.Sig
{
    public class HistGinecologica
    {
        //public hGinecologica()
        //{
        //}
        public int GetLastExp(int pacienteId, string autor)
        {

            //new SqlParameter("@consultaID", 0),
            //         new SqlParameter("@fecha", DateTime.Today),
            //       new SqlParameter("@observaciones", ""),
            //         new SqlParameter("@menarquia", ""),
            //         new SqlParameter("@pubarquia", ""),
            //      new SqlParameter("@telarquia", ""),
            //         new SqlParameter("@fum", DateTime.Today),
            //         new SqlParameter("@patronmens", ""),
            //     new SqlParameter("@dismenorrea", ""),
            //         new SqlParameter("@primerarelacion", ""),
            //         new SqlParameter("@relsemana", ""),
            //           new SqlParameter("@nocompaneros", ""),
            //         new SqlParameter("@tiempocasada", ""),
            //         new SqlParameter("@usoactual", ""),
            //           new SqlParameter("@usopasado", ""),
            //         new SqlParameter("@oral", ""),
            //         new SqlParameter("@div", ""),
            //         new SqlParameter("@debarrera", ""),
            //         new SqlParameter("@norplat", ""),
            //         new SqlParameter("@gestas", ""),
            //         new SqlParameter("@partos", ""),
            //         new SqlParameter("@cesareas", ""),
            //             new SqlParameter("@abortos", ""),
            //                 new SqlParameter("@hijosvivos", ""),
            //                     new SqlParameter("@hijosprematuros", ""),
            //                         new SqlParameter("@natimuertos", ""),
            //                             new SqlParameter("@fecultimoparto", ""),
            //                                 new SqlParameter("@forceps", ""),
            //                                     new SqlParameter("@nohijosprem", 0),
            //                     new SqlParameter("@reslab", ""),
            //                     new SqlParameter("@exp", 0),
            SqlDataCore sql = new SqlDataCore();
            try
            {
                var ds = sql.GetDataBySP("WHGINECOLOGICA",
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
