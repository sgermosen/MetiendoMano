using System.Collections.Generic;

namespace Common
{
    public class AnexGRID
    {
        public string columna { get; set; }
        public string columna_orden { get; set; }
        public int limite { get; set; }
        public int pagina { get; set; }

        public List<AnexGRIDFiltro> filtros { get;set; }
        public List<AnexGRIDParametro> parametros { get;set; }

        private AnexGRIDResponde aresponde = new AnexGRIDResponde();

        public void Inicializar() 
        {
            /* Cantidad de registros por página */
            pagina = pagina - 1;
        
            /* Desde que número de fila va a paginar */
            if(pagina > 0) pagina = pagina * limite;
        
            /* Filtros */
            if(filtros == null)
                filtros = new List<AnexGRIDFiltro>();
        
            /* Parametros adicionales */
            if(parametros == null)
                parametros = new List<AnexGRIDParametro>();
        }

        public void SetData(dynamic data, int total) 
        {
            aresponde = new AnexGRIDResponde
            {
                data = data,
                total = total
            };
        }

        public AnexGRIDResponde responde()
        {
            return aresponde;
        }
    }

    public class AnexGRIDResponde
    {
        public int total { get; set; }
        public dynamic data { get; set; }
    }

    public class AnexGRIDFiltro
    {
        public string columna { get; set; }
        public string valor { get; set; }
    }

    public class AnexGRIDParametro
    {
        public string clave { get; set; }
        public string valor { get; set; }
    }
}
