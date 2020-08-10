using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Common;
using System.Dynamic;
using ADOHelpersLib.ADOExtensions.Collections;
using System.Data;

namespace ADOHelpersLib.ADOExtensions.ADOExtensions
{
    public static class ADOHelperExtensions
    {
        public static List<ExpandoObject> ToExpandoArray<T>(this T source, string includedFields = null, string excludedFields = null) 
            where T: DbDataReader
        {
            if (source == null)
            {
                return Enumerable.Empty<ExpandoObject>().ToList(); 
            }
            List<ExpandoObject> result = new List<ExpandoObject>();
            List<string> ToInclude = includedFields == null ?
                new List<string>() : includedFields.Split(new string[]{","}, StringSplitOptions.RemoveEmptyEntries).ToList();
            ToInclude.ForEach(i => i = i.Trim());
            List<string> ToExclude = excludedFields == null ?
                new List<String>() : excludedFields.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ToExclude.ForEach(i => i = i.Trim());
            source.Select(r => r).ToList().ForEach(row =>
            {
                ExpandoObject tmpRow = new ExpandoObject();
                var DataColumn = (ICollection<KeyValuePair<string, object>>)tmpRow;
                for (int i = 0; i < row.FieldCount; i++)
                {
                    if (ToExclude.Contains(row.GetName(i), StringComparer.InvariantCultureIgnoreCase) && 
                        !ToInclude.Contains(row.GetName(i), StringComparer.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    else
                    {
                        if (ToInclude.Contains(row.GetName(i), StringComparer.InvariantCultureIgnoreCase) || ToInclude.Count == 0)
                        {
                            DataColumn.Add(new KeyValuePair<string, object>(row.GetName(i), row.GetValue(i)));
                        }    
                    }
                }
                result.Add(tmpRow);
            });

            return result;
        }

        public static List<ExpandoObject> ToExpandoArray(this DataTable source, string includedFields = null, string excludedFields = null)
        {
            if (source == null)
            {
                return Enumerable.Empty<ExpandoObject>().ToList();
            }
            List<ExpandoObject> result = new List<ExpandoObject>();
            List<string> ToInclude = includedFields == null ?
                new List<string>() : includedFields.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ToInclude.ForEach(i => i = i.Trim());
            List<string> ToExclude = excludedFields == null ?
                new List<String>() : excludedFields.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).ToList();
            ToExclude.ForEach(i => i = i.Trim());
            var columnNames = source.Columns.Cast<DataColumn>()
                                 .Select(x => x.ColumnName)
                                 .ToArray();

            source.AsEnumerable().ToList().ForEach(row =>
            {
                ExpandoObject tmpRow = new ExpandoObject();
                var DataColumn = (ICollection<KeyValuePair<string, object>>)tmpRow;
                for (int i = 0; i < columnNames.Length; i++)
                {
                    if (ToExclude.Contains(columnNames.ElementAt(i), StringComparer.InvariantCultureIgnoreCase) &&
                        !ToInclude.Contains(columnNames.ElementAt(i), StringComparer.InvariantCultureIgnoreCase))
                    {
                        continue;
                    }
                    else
                    {
                        if (ToInclude.Contains(columnNames.ElementAt(i), StringComparer.InvariantCultureIgnoreCase) || ToInclude.Count == 0)
                        {
                            DataColumn.Add(new KeyValuePair<string, object>(columnNames.ElementAt(i), row[columnNames.ElementAt(i)]));
                        }
                    }
                }
                result.Add(tmpRow);
            });

            return result;
        }

        //http://stackoverflow.com/questions/7794818/how-can-i-convert-a-datatable-into-a-dynamic-object
        public static IEnumerable<dynamic> AsDynamicEnumerable(this DataTable table)
        {
            // Validate argument here..

            return table.AsEnumerable().Select(row => new DynamicRow(row));
        }

        private sealed class DynamicRow : DynamicObject
        {
            private readonly DataRow _row;

            internal DynamicRow(DataRow row) { _row = row; }

            // Interprets a member-access as an indexer-access on the 
            // contained DataRow.
            public override bool TryGetMember(GetMemberBinder binder, out object result)
            {
                var retVal = _row.Table.Columns.Contains(binder.Name);
                result = retVal ? _row[binder.Name] : null;
                return retVal;
            }
        }

    }
}

