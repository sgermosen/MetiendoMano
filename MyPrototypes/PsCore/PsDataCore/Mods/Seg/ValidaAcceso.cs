using System;
using System.Linq;

namespace PsDataCore.Mods.Seg
{
    public class ValidaAcceso
    {
        public bool ValidaAccesoOpcion(int userid, string opcion, int autor)
        {
            var db = new SegDbEntities();

            var query = db.ListRolesUsuario(userid);
            var lst = query.ToList();

            foreach (var items in lst)
            {
                {
                    var query2 = db.GetOpcionesRol(items.ROLID);
                    var lst2 = query2.ToList();
                    if (lst2.Any(record => record.link == opcion))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //public bool ValidaAccesoOpcion(Int32 userid, string opcion, int autor)
        //{
        //    var db = new SegDbEntities();

        //    List<ListRolesUsuario_Result> lst = new List<ListRolesUsuario_Result>();
        //    var query = db.ListRolesUsuario(userid);
        //    lst = query.ToList();

        //    foreach (var items in lst)
        //    {
        //        {
        //            List<GetOpcionesRol_Result> lst2 = new List<GetOpcionesRol_Result>();
        //            var query2 = db.GetOpcionesRol(items.ROLID);
        //            lst2 = query2.ToList();
        //            foreach (var record in lst2)
        //            {
        //                if (record.link == opcion)
        //                { return true; }
        //            }
        //        }
        //    }
        //    return false;
        //}

        //public bool ValidaSession(int userid, int autor)
        //{
        //    if (userid == 0)
        //    {
        //        return false;
        //    }
        //    if (userid.ToString().Length == 0)
        //    {
        //        return false;
        //    }

        //    int id = Convert.ToInt32(userid);
        //    using (var db = new SegDbEntities())
        //    {
        //        var user = db.USUARIOSSEG.FirstOrDefault(u => u.ID == id && u.AUTORID == autor);
        //        if (user != null)
        //        {
        //            return true;
        //        }
        //        else
        //        {
        //            return false;
        //        }
        //    }
        //}
        public bool ValidaSession(int userid, int autor)
        {
            if (userid == 0)
            {
                return false;
            }
            if (userid.ToString().Length == 0)
            {
                return false;
            }

            var id = Convert.ToInt32(userid);
            using (var db = new SegDbEntities())
            {
                var user = db.USUARIOSSEG.FirstOrDefault(u => u.ID == id && u.AUTORID == autor);
                return user != null;
            }
        }

        public bool ValidaAccesoApp(string username, string password)
        {
            using (var db = new SegDbEntities())
            {
                var user = db.USUARIOSSEG.FirstOrDefault(u => u.USERNAME == username && u.PASSWORD == password && u.ESTATUS);
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }

        public USUARIOSSEG GetDatosUsuario(string username, string password)
        {
            using (var db = new SegDbEntities())
            {
                var user = db.USUARIOSSEG.FirstOrDefault(u => u.USERNAME == username && u.PASSWORD == password && u.ESTATUS);
                //  DataSet ds = new DataSet();
                //  ds = user;// obj.getXmlData();// get the multiple table in dataset.
                return user;
                //Employee objEmp = new Employee();// create the object of class Employee 
                //List<Employee> empList = new List<Employee>();
                //int table = Convert.ToInt32(ds.Tables.Count);// count the number of table in dataset
                //for (int i = 1; i < table; i++)// set the table value in list one by one
                //{
                //    foreach (DataRow dr in ds.Tables[i].Rows)
                //    {
                //        empList.Add(new Employee { Title1 = Convert.ToString(dr["Title"]), Hosting1 = Convert.ToString(dr["Hosting"]), Startdate1 = Convert.ToString(dr["Startdate"]), ExpDate1 = Convert.ToString(dr["ExpDate"]) });
                //    }
                //}
                //dataGridView1.DataSource = empList;
                // DataTable   dt = new DataTable();
                //   dt.add
                //    return user;
            }
        }

        public bool ValidaAccesoAppwAutor(string username, string password, string autor)
        {
            using (var db = new SegDbEntities())
            {
                var user = db.USUARIOSSEG.FirstOrDefault(u => u.USERNAME == username && u.PASSWORD == password && u.AUTOR == autor && u.ESTATUS  );
                if (user != null)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
