using System;
using System.Linq;

namespace PsDataCore.Mods.Pos
{
    public class WServicios
    {
        private int Id { get; set; }
        private string Codigo { get; set; }
        private string BarCode { get; set; }
        private string Nombre { get; set; }
        private string Descripcion { get; set; }
        //  private decimal PRECIO { get; set; }
        public string Notas { get; set; }
        // public decimal COSTO { get; set; }
        // public int STOCK { get; set; }
        public int Categoriaid { get; set; }
        //   public string CATEGORIA { get; set; }
        //public int TIPO_SERVICIOID { get; set; }
        //   public string TIPO_SERVICIO { get; set; }
        public string Imagen { get; set; }
        public int Autorid { get; set; }
        public int Sucursalid { get; set; }
        //private bool ValidaCampos()
        //{
        //    //if (txtStock.Text == "")
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('Debe digitar la cantidad disponible (Stock)!');", true);
        //    //    return false;
        //    //}
        //    //if (txtCosto.Text == "")
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('Debe digitar el costo (Precio) del Servicio/Producto!');", true);
        //    //    return false;
        //    //}
        //    //if (txtCosto.Text == "")
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('Debe digitar el Precio de Venta del Servicio/Producto!');", true);
        //    //    return false;
        //    //}
        //    decimal var = 0;
        //    //try
        //    //{                // Do not initialize this variable here.
        //    //    var = Convert.ToDecimal(txtPrecio.Text);
        //    //}
        //    //catch
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('El valor para la casilla precio de venta, debe ser numerico!');", true);
        //    //    return false;
        //    //}
        //    //try
        //    //{
        //    //    // Do not initialize this variable here.
        //    //    var = Convert.ToDecimal(txtCosto.Text);
        //    //}
        //    //catch
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('El valor para la casilla costo, debe ser numerico!');", true);
        //    //    return false;
        //    //}
        //    //try
        //    //{                // Do not initialize this variable here.
        //    //    var = Convert.ToDecimal(txtStock.Text);
        //    //}
        //    //catch
        //    //{
        //    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('El valor para la casilla disponibilidad, debe ser numerico!');", true);
        //    //    return false;
        //    //}
        //    if (txtNombre.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('Debe Digitar el Nombre del Producto!');", true);
        //        return false;
        //    }
        //    if (txtCodigo.Text == "")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "alert('Debe Digitar el Codigo del Producto!');", true);
        //        return false;
        //    }

        //    return true;

        //}
        private static bool ValidaCampos(string codigo, string nombre)
        {
            if (codigo == string.Empty)
            {
                return false;
            }
            return nombre != string.Empty;
            //if (nombre == string.Empty)
            //{
            //    return false;
            //}
            //return true;
        }

        private bool UpdateDatos(int pId)
        {
            try
            {
                using (var db = new PosDbEntities())
                {
                    var buscaObjeto = db.SERVICIOS.FirstOrDefault(c => c.ID == pId);
                    if (buscaObjeto == null) return false;
                    buscaObjeto.CODIGO = Codigo;// Convert.ToDateTime(txtfechanacimiento.Text);
                    buscaObjeto.NOMBRE = Nombre;
                    buscaObjeto.DESCRIPCION = Descripcion;
                    // buscaObjeto.PRECIO = Convert.ToDecimal( txtPrecio.Text);
                    buscaObjeto.NOTAS = Notas;
                    // buscaObjeto.COSTO = Convert.ToDecimal(txtCosto.Text);   
                    db.SaveChanges();
                    return true;
                }

            }
            catch (Exception)
            {

                return false;
            }


        }
        private bool InsertDatos(int pId)
        {
            //UploadFile(fuImagen);
            // int autor = Convert.ToInt32(Session["userAutor"]);
            //   int userID = Convert.ToInt32(Session["userId"]);

            //  int sucursal = Convert.ToInt32(Session["usrSucursal"]);
            //Creando los datos generales del socio en la tabla personas
            try
            {
                using (var db = new PosDbEntities())
                {
                    var datosObjeto = new SERVICIOS
                    {
                        CODIGO = Codigo,
                        BARCODE = BarCode,
                        CATEGORIAID = Categoriaid,
                        NOMBRE = Nombre,
                        DESCRIPCION = Descripcion,
                        NOTAS = Notas,
                        AUTORID = Autorid,
                        SUCURSALID = Sucursalid

                    };
                    // IMAGEN = fuImagen.FileName
                    //  PRECIO = Convert.ToDecimal(txtPrecio.Text),
                    //  
                    //   COSTO = Convert.ToDecimal(txtCosto.Text),
                    // STOCK = Convert.ToInt32(txtStock.Text),
                    db.SERVICIOS.Add(datosObjeto);
                    db.SaveChanges();
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }

        }
        public bool GuardarServicio(string id, string codigo, string barcode, string categoria, string nombre, string descripcion, string observaciones, string rutaimagen, int autor, int sucursal, int dec)

        {
            if (ValidaCampos(codigo, nombre) == false)
            {
                return false;
            }

            var categoriaId = Convert.ToInt32(categoria);
            if (descripcion == string.Empty)
            {
                descripcion = "";
            }
            if (observaciones == string.Empty)
            {
                observaciones = "";
            }

            Codigo = codigo;
            BarCode = barcode;
            Nombre = nombre;
            Descripcion = descripcion;
            Categoriaid = categoriaId;
            Notas = observaciones;
            Autorid = autor;
            Sucursalid = sucursal;

            if (dec == 2)
            {
                Id = Convert.ToInt32(id);
                return UpdateDatos(Id);
            }
            else
            {
                Id = 0;
                return InsertDatos(Id);
            }

        }
    }
}
