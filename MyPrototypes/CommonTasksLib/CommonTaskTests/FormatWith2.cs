using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;

namespace CommonTaskTests
{
    public static class FormatWith2
    {
        public static string FormatWith(this string format, object source)
        {
            return FormatWith(format, null, source);
        }

        public static string FormatWith<T>(this T source, string format)
            where T: class
        {
            return source.FormatWith(format, null);
        }

        public static string FormatWith(this string format, IFormatProvider provider, object source)
        {
            if (format == null)
                throw new ArgumentNullException("format");

            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<format>:[^}]+)?(?<end>\})+",
              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            List<object> values = new List<object>();
            string rewrittenFormat = r.Replace(format, delegate(Match m)
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];

                values.Add((propertyGroup.Value == "0")
                  ? source
                  : DataBinder.Eval(source, propertyGroup.Value));

                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                  + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }

        public static string FormatWith<T>(this T source, string format, IFormatProvider provider)
            where T : class
        {
            if (format == null)
                throw new ArgumentNullException("format");

            Regex r = new Regex(@"(?<start>\{)+(?<property>[\w\.\[\]]+)(?<method>\(\))*(?<format>:[^}]+)?(?<end>\})+",
              RegexOptions.Compiled | RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);

            List<object> values = new List<object>();
            string rewrittenFormat = r.Replace(format, delegate(Match m)
            {
                Group startGroup = m.Groups["start"];
                Group propertyGroup = m.Groups["property"];
                Group formatGroup = m.Groups["format"];
                Group endGroup = m.Groups["end"];
                Group methodGroup = m.Groups["method"];

                if ((methodGroup.Value == "()"))
                {
                    bool isInnerObject = propertyGroup.Value.IndexOf('.') != -1;
                    var method = !isInnerObject ? propertyGroup.Value : propertyGroup.Value.Split('.')[1];
                    Type t = typeof(T);
                    var lastDotPosition = propertyGroup.Value.LastIndexOf('.');
                    var objectMember = isInnerObject ?
                        DataBinder.Eval(source, propertyGroup.Value.Substring(0, lastDotPosition)) :
                        source;
                    Object s = t.InvokeMember(method,
                        BindingFlags.DeclaredOnly |
                        BindingFlags.Public | BindingFlags.NonPublic |
                        BindingFlags.Instance | BindingFlags.InvokeMethod, null, objectMember, null);
                    values.Add(s);
                }
                else
                {
                    values.Add((propertyGroup.Value == "0")
                      ? source
                      : DataBinder.Eval(source, propertyGroup.Value));
                }
                return new string('{', startGroup.Captures.Count) + (values.Count - 1) + formatGroup.Value
                  + new string('}', endGroup.Captures.Count);
            });

            return string.Format(provider, rewrittenFormat, values.ToArray());
        }
    }
}
