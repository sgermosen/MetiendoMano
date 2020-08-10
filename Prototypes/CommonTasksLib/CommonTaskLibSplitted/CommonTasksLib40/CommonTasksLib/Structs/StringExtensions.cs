using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI;

namespace CommonTasksLib.Structs
{
    public static class StringExtensions
    {
        /// <summary>
        /// Método extensión para mejorar el string.format, con la habilidad
        /// de aceptar los nombres de las propiedades de una clase, o de un 
        /// objeto anónimo como placeholders de reemplazo.
        /// </summary>
        /// <param name="format">Formato a utilizar.</param>
        /// <param name="source">Objeto utilizado para formatear.</param>
        /// <returns>Un objeto string formateado con el formato especificado.</returns>
        public static string FormatWith(this string format, object source)
        {
            return FormatWith(format, null, source);
        }

        /// <summary>
        /// Método extensión para mejorar el string.format, con la habilidad
        /// de aceptar los nombres de las propiedades de una clase, o de un 
        /// objeto anónimo como placeholders de reemplazo.
        /// </summary>
        /// <typeparam name="T">Tipo de datos del objeto usado para formatear.</typeparam>
        /// <param name="format">Formato a utilizar.</param>
        /// <param name="source">Objeto utilizado para formatear.</param>
        /// <returns>Un objeto string formateado con el formato especificado.</returns>
        public static string FormatWith<T>(this T source, string format)
            where T : class
        {
            return source.FormatWith(format, null);
        }

        /// <summary>
        /// Método extensión para mejorar el string.format, con la habilidad
        /// de aceptar los nombres de las propiedades de una clase, o de un 
        /// objeto anónimo como placeholders de reemplazo.
        /// </summary>
        /// <param name="format">Formato a utilizar.</param>
        /// <param name="provider">Objeto para controlar el proceso de formato.</param>
        /// <param name="source">Objeto utilizado para formatear.</param>
        /// <returns>Un objeto string formateado con el formato especificado.</returns>
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

        /// <summary>
        /// Método extensión para mejorar el string.format, con la habilidad
        /// de aceptar los nombres de las propiedades de una clase, o de un 
        /// objeto anónimo como placeholders de reemplazo.
        /// </summary>
        /// <typeparam name="T">Tipo de datos del objeto usado para formatear.</typeparam>
        /// <param name="source">Objeto utilizado para formatear.</param>
        /// <param name="format">Formato a utilizar.</param>
        /// <param name="provider">Objeto para controlar el proceso de formato.</param>
        /// <returns>Un objeto string formateado con el formato especificado.</returns>
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

        /// <summary>
        /// Método extensión utilizado para obtener una cantidad específica de 
        /// caracteres de una cadena desde el inicio de la misma.
        /// </summary>
        /// <param name="source">Cadena de la cual obtener los caracteres.</param>
        /// <param name="length">Cantidad de caracteres a extraer.</param>
        /// <returns>Un objeto string con la cantidad de caracteres obtenidos.</returns>
        public static string Left(this string source, int length)
        {
            source = (source ?? string.Empty);
            length = (length < 0) ? 0 : length;

            return source.Substring(0, Math.Min(length, source.Length));
        }

        /// <summary>
        /// Método extensión utilizado para obtener una cantidad específica de 
        /// caracteres de una cadena desde el final de la misma contando una cantidad
        /// específica.
        /// </summary>
        /// <param name="source">Cadena de la cual obtener los caracteres.</param>
        /// <param name="length">Cantidad de caracteres a extraer.</param>
        /// <returns>Un objeto string con la cantidad de caracteres obtenidos.</returns>
        public static string Right(this string source, int length)
        {
            source = (source ?? string.Empty);
            length = (length < 0) ? 0 : length;

            return (source.Length >= length) ? 
                source.Substring(source.Length - length, length) : source;
        }

        /// <summary>
        /// Método extensión utilizado para generar una contraseña aleatoria.
        /// </summary>
        /// <param name="length">Longitud de la contraseña a generar.</param>
        /// <returns>Un objeto String con la contraseña generada.</returns>
        public static string CreateRandomPassword(int length)
        {
            string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890#_";
            string randomPassword = "";
            Random randomizer = new Random();

            while (0 < length--)
            {
                randomPassword += validChars[randomizer.Next(validChars.Length)];
            }

            return randomPassword;
        }

        /// <summary>
        /// Método extensión utilizado para generar una contraseña aleatoria.
        /// </summary>
        /// <param name="length">Longitud de la contraseña a generar.</param>
        /// <param name="validChars">Conjunto de caracteres validos usados para generar la contraseña.</param>
        /// <returns>Un objeto String con la contraseña generada.</returns>
        public static string CreateRandomPassword(int length, string validChars)
        {
            string randomPassword = "";
            Random randomizer = new Random();

            while (0 < length--)
            {
                randomPassword += validChars[randomizer.Next(validChars.Length)];
            }

            return randomPassword;
        }

    }
}
