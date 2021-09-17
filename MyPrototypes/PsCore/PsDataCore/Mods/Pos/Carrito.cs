using System;
using System.Data;
using System.Linq;

namespace PsDataCore.Mods.Pos
{
    public class Carrito
    {

        public int CantidadDisponible(int productoId)
        {
            var db = new PosDbEntities();
            var producto = db.SERVICIOS.FirstOrDefault(p => p.ID == productoId);
            if (producto == null) return 0;
            {
                return producto.STOCK != null ? Convert.ToInt32(producto.STOCK.Value) : 0;
            }
        }

        public DataTable DevuelveCarritoArreglado(int id, int pCantidad, DataTable carrito)
        {
            DataTable table ;
            var cantidad = 0;
            if (carrito == null)
            {
                table = new DataTable();
                table.Columns.AddRange(new [] {
                    new DataColumn("ID"),
                    new DataColumn("Codigo"),
                    new DataColumn("Nombre"),
                    new DataColumn("Descripcion"),
                    new DataColumn("Precio"){DataType=Type.GetType("System.Decimal") },
                    new DataColumn("Cantidad"),
                    new DataColumn("Subtotal"){DataType=Type.GetType("System.Decimal")}
                });

            }
            else
            {
                table = carrito;

                var productoExiste = table.AsEnumerable().FirstOrDefault(r => r.Field<string>("ID") == id.ToString());
                if (productoExiste != null)
                {
                    cantidad = Convert.ToInt32(productoExiste["Cantidad"]);
                    productoExiste.Delete();
                }

            }
            cantidad += pCantidad;

            if ((CantidadDisponible(id) - cantidad) < 0)
            {
                return null;
            }

            var db = new PosDbEntities();
            var producto = db.SERVICIOS.FirstOrDefault(p => p.ID == id);
            if (producto == null) return null;
            {
                var subTotal = (decimal)(Convert.ToDouble(producto.PRECIO) * cantidad);
                table.Rows.Add(id, producto.CODIGO, producto.NOMBRE, producto.DESCRIPCION, producto.PRECIO, cantidad, subTotal);
            }

            return table;
        }

        public DataTable EliminaDelCarrito(int id, DataTable carrito)
        {
            var table  = carrito;

            var productoExiste = table.AsEnumerable().FirstOrDefault(r => r.Field<string>("ID") == id.ToString());
            if (productoExiste == null) return table;
            //cantidad = Convert.ToInt32(productoExiste["Cantidad"]);
            productoExiste.Delete();

            return table;
        }

    }
}
