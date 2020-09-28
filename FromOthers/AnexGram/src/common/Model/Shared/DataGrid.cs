using System.Collections.Generic;

namespace Model.Shared
{
    /// <summary>
    /// Esta clase sería algo más robusto que lo tenemos actualmente como ApiFilter
    /// </summary>
    public class DataGrid
    {
        public string SortBy { get; set; }
        public bool Descending { get; set; }
        public int RowsPerPage { get; set; }
        public int Page { get; set; }
        public Dictionary<string, string> Filter { get; set; }

        private DataGridResponse DataResponse = new DataGridResponse();

        public void Initialize() 
        {
            /* Cantidad de registros por página */
            Page = Page - 1;
        
            /* Desde que número de fila va a paginar */
            if(Page > 0) Page = Page * RowsPerPage;
        }

        public void SetData(dynamic data, int total) 
        {
            DataResponse = new DataGridResponse
            {
                Data = data,
                Total = total
            };
        }

        public DataGridResponse Response()
        {
            return DataResponse;
        }
    }

    public class DataGridResponse
    {
        public int Total { get; set; }
        public dynamic Data { get; set; }
    }
}
