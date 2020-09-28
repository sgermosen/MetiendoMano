using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SGC_MVC.Models.ViewModels
{
    /// <summary>
    /// Clase que encapsula los parametros enviados por el plugin
    /// Datatable de jquery.
    /// </summary>
    public class JQueryDataTableParamModel
    {
        /// <summary>
        /// valor de sincronización utilizado por el plugin de
        /// jquery para las consultas y respuestas. Dicho valor
        /// debe ser devuelto en cada respuesta a las peticiones
        /// realizadas utilizando el plugin datatable.
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// Texto utilizado para el filtrado de datos
        /// </summary>
        public string sSearch { get; set; }

        /// <summary>
        /// Numero total de records que deben ser retornados
        /// de la consulta.
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// Primer record a mostrar, utilizado para la paginación.
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// Numero de columnas de la tabla.
        /// </summary>
        public int iColumns { get; set; }

        /// <summary>
        /// Numero de columnas utilizadas para el ordenamiento.
        /// </summary>
        public int iSortingCols { get; set; }

        /// <summary>
        /// Lista de todos los nombres de las columnas, delimitadas
        /// por coma.
        /// </summary>
        public string sColumns { get; set; }
    }
}