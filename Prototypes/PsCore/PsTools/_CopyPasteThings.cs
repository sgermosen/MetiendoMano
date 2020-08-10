namespace PsTools
{
    public class CopyPasteThings
    {
        #region Imprimir Contenido de un div
        //<a class="btn btn-primary" id="BtnPrintA4">
        //    <span class="fa fa-print"></span> Imprimir en A4
        //</a>


        #endregion

        #region Secciones con Estilo

        //<section class="MySection">
        //    <h4>Detalles de la Factura</h4>
        //</section>

        #endregion
        #region Checkbox Razor
        //<div class="form-group">
        //    @Html.LabelFor(model => model.Undefined,  new { @class = "control-label col-md-2" })
        //<div class="col-md-10">
        //    <div class="checkbox">
        //        @Html.EditorFor(model => model.Undefined)
        //        @Html.ValidationMessageFor(model => model.Undefined, "", new { @class = "text-danger" })
        //    </div>
        //</div>
        //</div>
        #endregion

        #region Crear una lista que sera consumida en la vista desde un viewBag
        //ViewBag.StatusId = _db.Status.Where(p => p.Table == "ALL")
        //.ToSelectListItems(p => p.Name, p => p.StatusId.ToString(), l=>l.StatusId== myElement.StatusId); //el primer corresponde al textfield y lue
        //value to display, clave o id (debe ser to string), un lamda con el valor seleccionado
        #endregion

        #region Transferir a los fines de convertir de un objeto a otro

        //private static Person ToPeople(PatientView view)
        //{
        //    if (view == null) throw new ArgumentNullException(nameof(view));

        //    var m = new Person(); //el tipo que vamos a devolver
        //    view.Transfer(ref m);
        //    return m;
        //}
        //private static AppointmentView ToView(Report rview, Appointment view)
        //{
        //    if (rview == null) throw new ArgumentNullException(nameof(rview));
        //    if (view == null) throw new ArgumentNullException(nameof(view));

        //    var m = new AppointmentView(); //el tipo que vamos a devolver
        //    rview.Transfer(ref m);
        //    view.Transfer(ref m);

        //    return m;

        //}

        #endregion

        #region Radiobuttons html

        // <div>

        //<input type = "radio" id="option1" name="Sells" value="1" checked="checked">
        //<label for="option1">Ventas Del Dia, Caja   &nbsp; &nbsp;</label>

        //<input type = "radio" id="option2" name="Sells" value="2">
        //<label for="option2">Ventas Del Dia, Sucursal  &nbsp; &nbsp;</label>

        //<input type = "radio" id="option3" name="Sells" value="3">
        //<label for="option3">Ventas Del Dia, Negocio  &nbsp; &nbsp;</label>

        //<input type = "radio" id="option4" name="Sells" value="4">
        //<label for="option4">Ventas Por Rango de Fecha  &nbsp; &nbsp;</label>

        //</div>

        #endregion

        #region Pasando parametros multiples desde Jquery y redireccionando al controller
        //Armar la url base
        // var url = '@Url.Action("Index", "Sales",new {area="Pos"})'; 
        //redireccionar y concatenar, nota: poner /? antes del primer parametro, 
        //jquery ya convirtio a string con el val.trim, 
        //no puede haver espacios en blanco en el armado de la url
        //  window.location.href = url + '/?id=0,error=""&searchTipe=4&dateFrom=' + $('#FromDate').val().trim() + '&dateTo=' + $('#ToDate').val().trim() + '&select=' + $('#selectOption').val();
        #endregion

        #region Intentar atrapar los errores de entity para poder entenderlos mas facil
        //public string CreateTryForEntityErrors()
        //{
        //    try
        //    {
        //        //realizar el evento
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        var message = string.Empty;
        //        foreach (var eve in e.EntityValidationErrors)
        //        {

        //            //Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //            //    eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            message = string.Format("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);

        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                //Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                //    ve.PropertyName, ve.ErrorMessage);
        //                message += string.Format("\n- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        return (message);
        //    }
        //    return "";
        //}
        #endregion

        #region Desabilitar y habilitar elementos con jquery

        // $('#BtnSearch').attr('disabled', 'disabled');
        //  $('#BtnSearch').removeAttr('disabled');

        #endregion


    }
}