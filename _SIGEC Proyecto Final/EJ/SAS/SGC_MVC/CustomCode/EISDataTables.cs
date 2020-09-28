using SGC_MVC.Models;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace SGC_MVC.CustomCode
{
    public class EISDataTables
    {
        public int iDisplayStart { get; set; }
        public int iDisplayLength { get; set; }
        public int iColumns { get; set; }
        public string sSearch { get; set; }
        public string rawColums { get; set; }
        public List<string> colums { get; set; }
        public bool bEscapeRegex { get; set; }
        public int iSortingCols { get; set; }
        public string sEcho { get; set; }
        public List<bool> bSortable { get; set; }
        public List<bool> bSearchable { get; set; }
        public List<string> sSearchColumns { get; set; }
        public List<int> iSortCol { get; set; }
        public List<string> sSortDir { get; set; }
        public List<bool> bEscapeRegexColumns { get; set; }
        public string buttonsOptions { get; set; }
        public bool companyRules { get; set; }
        public string tableName { get; set; }
        public string secundaryWhere { get; set; }
        public string[] ffColums { get; set; }
        public string[] ffvalues { get; set; }
        
        public EISDataTables()
        {
            bSortable = new List<bool>();
            bSearchable = new List<bool>();
            sSearchColumns = new List<string>();
            iSortCol = new List<int>();
            sSortDir = new List<string>();
            bEscapeRegexColumns = new List<bool>();
        }

        public void BindModel(HttpRequestBase r)
        {
            var request = r.Params;

            iDisplayStart = Convert.ToInt32(request["iDisplayStart"]);
            iDisplayLength = Convert.ToInt32(request["iDisplayLength"]);

            ffColums = request["ffColumns"].Split(',');
            ffvalues = request["ffValues"].Split('&');

            iColumns = Convert.ToInt32(request["iColumns"]);
            sSearch = request["sSearch"];
            bEscapeRegex = Convert.ToBoolean(request["bEscapeRegex"]);
            iSortingCols = Convert.ToInt32(request["iSortingCols"]);
            sEcho = request["sEcho"];
            rawColums = request["colums"] + ",ID";
            colums = (this.rawColums).Split(',').ToList<string>();
            buttonsOptions = request["options"];
            tableName = request["tableName"];
            secundaryWhere = request["wherePlus"];

            for (int i = 0; i < this.iColumns; i++)
            {
                this.bSortable.Add(Convert.ToBoolean(request["bSortable_" + i]));
                this.bSearchable.Add(Convert.ToBoolean(request["bSearchable_" + i]));
                this.sSearchColumns.Add(request["sSearch_" + i]);
                this.bEscapeRegexColumns.Add(Convert.ToBoolean(request["bEscapeRegex_" + i]));
                this.iSortCol.Add(Convert.ToInt32(request["iSortCol_" + i]));
                this.sSortDir.Add(request["sSortDir_" + i]);
            }

            
        }
    }

    public class QueryBuilder
    {
        public string fullQuery { get; set; }
        public string searchText { get; set; }
        public string WhereConditions { get; set; }
        public string OrderBy { get; set; }
        public EISDataTables datatables { get; set; }
        public QueryBuilder(EISDataTables datatable)
        {
            datatables = datatable;
        }
        public void FilterPagingSortingSearch(int? companyID)
        {
            if (datatables.iSortCol[0] != (datatables.colums.Count() - 1))
                this.OrderBy = string.Format("ORDER BY {0} {1}", datatables.colums[datatables.iSortCol[0]], datatables.sSortDir[0]);

            if (datatables.sSearch != "")
            {
                WhereConditions = "WHERE (";
                foreach (string column in datatables.colums)
                {
                    if (column != "ID")
                        WhereConditions += string.Format("{0} LIKE '%{1}%' OR ", column, datatables.sSearch);
                }

                WhereConditions += "**";
                WhereConditions = WhereConditions.Replace("OR **", "") + string.Format(") AND ({0})",datatables.secundaryWhere);
            }
            else
            {
                if (string.IsNullOrEmpty(WhereConditions))
                    WhereConditions += string.Format("WHERE ", companyID);

                int index = 0; bool hasFilter = false;
                foreach (string column in datatables.ffColums)
                {
                    string filterColumName = column.Split('(')[0];
                    string patern = column.Split('(')[1].Replace(")", "");
                    patern = patern.Replace("value", datatables.ffvalues[index]);

                    if (datatables.ffvalues[index] != ""){
                        WhereConditions += string.Format("{0} {1} AND ", filterColumName,patern);
                        hasFilter = true;
                    }
                    index++;
                }

                WhereConditions += "**";
                WhereConditions = WhereConditions.Replace("AND **", "").Replace("**", "")
                    + string.Format(" {1} ({0})", datatables.secundaryWhere, (hasFilter)?"AND":"");
            }
            if (datatables.companyRules && companyID != null)
            {
                if (!string.IsNullOrEmpty(WhereConditions))
                    WhereConditions += string.Format(" AND (companyID={0})", companyID);
                else
                    WhereConditions += string.Format("WHERE (companyID={0})", companyID);
            }

            fullQuery = string.Format("select {0} from {1} {2} {3}", datatables.rawColums, datatables.tableName, WhereConditions, OrderBy);
        }
            
    }

    public class jsonBuilder
    {
        public EISDataTables datatables { get; set; }
        public jsonBuilder(EISDataTables datatable)
        {
            datatables = datatable;
        }

        public string getDataJson(SASContext db, QueryBuilder query, ButtonTemplates buttons)
        {
            IEnumerable<object[]> result;

            using (var cmd = db.Database.Connection.CreateCommand())
            {
                db.Database.Connection.Open();
                cmd.CommandText = query.fullQuery;
                using (var reader = cmd.ExecuteReader())
                    result = ReadDataReader(reader).ToList();
                db.Database.Connection.Close();
            }

            var filteredResult = result.Skip(datatables.iDisplayStart).Take(datatables.iDisplayLength).ToList();

            int iTotalRecords = result.Count();
            int iTotalDisplayRecords = result.ToList().Skip(0).Count();

            string json = string.Format("{2}\"iTotalRecords\": {0},\"iTotalDisplayRecords\": {1},  "
                + "\"sEcho\":{3},"
                + "\"aaData\":["
                , iTotalRecords, iTotalDisplayRecords, "{", datatables.sEcho, datatables.iDisplayStart);

            for (int h = 0; h < filteredResult.Count(); h++)
            {
                json += "[";
                for (int i = 0; i < datatables.colums.Count() - 1; i++)
                {
                    json += "\"" + filteredResult[h][i] + "\",";
                }
                json += "\"";
                foreach (string template in buttons.ButtonMasks)
                {
                    json += template.Replace("#id", filteredResult[h][datatables.colums.Count() - 1].ToString());
                }
                json += "\",],**";
                json = json.Replace(",],**", "],");
            }

            json += "]}**";
            json = json.Replace(",]}**", "]}");
            json = json.Replace("}**", "}");
            return json;
        }

        public IEnumerable<object[]> ReadDataReader(DbDataReader reader)
        {
            while (reader.Read())
            {
                var values = new List<object>();
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    values.Add(reader.GetValue(i));
                }
                yield return values.ToArray();
            }
        }
    }

    public class ButtonTemplates
    {
        public List<string> ButtonMasks { get; set; }

        public ButtonTemplates()
        {
            ButtonMasks = new List<string>();
        }

        public  void fillTemplates(string optionsParams)
        {
            string[] options = optionsParams.Split(';');
            foreach (string option in options)
            {
                string button = option.Split('(')[0];
                string controller = option.Split('(')[1].Split(',')[0];
                string action = option.Split('(')[1].Split(',')[1].Replace(")", "");
                string template = string.Format("<a class=\\\"{0}_link button_link\\\" href=\\\"{1}/{2}/{3}/#id\\\">{0}</a>"
                    , button, System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath, controller, action);
                ButtonMasks.Add(template);
            }
        }
    }
}