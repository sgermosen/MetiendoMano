using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Text.RegularExpressions;

namespace PsDataCore
{
    /// <summary>
    /// Clase utilitaria para el manejo de los parámetros.
    /// </summary>
    public static class DbTools
    {
        /// <summary>
        /// Crea un IDbDataParameter a partir de los datos introducidos.
        /// </summary>
        /// <param name="command">Commando al cual asociar el parámetro.</param>
        /// <param name="name">Nombre del parámetro.</param>
        /// <param name="value">Valor del parámetro.</param>
        /// <returns>Un Objeto IDbDataParameter con los datos del parámetro creado.</returns>
        public static IDbDataParameter ToConvertSqlParams(IDbCommand command, string name, object value)
        {
            var p = command.CreateParameter();
            p.ParameterName = name;
            p.Value = value;
            return p;
        }

        /// <summary>
        /// Método utilitario para obtener los nombres de los parámetros especificados
        /// en una sentencia SQL dada.
        /// </summary>
        /// <param name="query">Sentencia SQL.</param>
        /// <returns></returns>
        public static ArrayList getParameterNames(string query)
        {
            Regex pattern = new Regex(@"(?<!@)@\w+");
            ArrayList paramNames = new ArrayList();
            foreach (Match match in pattern.Matches(query))
            {
                paramNames.Add(match.Value);
            }

            return paramNames;
        }

        /// <summary>
        /// Método utilitario para obtener los nombres de los parámetros especificados
        /// en una sentencia SQL dada.
        /// </summary>
        /// <typeparam name="TCommand">Tipo asociado al comando que ejecutará la sentencia.</typeparam>
        /// <param name="query">Sentencia SQL a ejecutar.</param>
        /// <returns></returns>
        public static ArrayList getParameterNames<TCommand>(string query)
            where TCommand : DbCommand
        {
            ArrayList paramNames = new ArrayList();
            var type = typeof(TCommand).ToString();
            Regex pattern = new Regex(@"(?<!@)@\w+");

            if (type.IndexOf("Oracle", 0, type.Length, StringComparison.OrdinalIgnoreCase) != -1) // oracle provider
            {
                pattern = new Regex(":([A-Za-z0-9]+)(\\s*)");
                foreach (Match match in pattern.Matches(query))
                {
                    paramNames.Add(match.Value.Replace(':', ' ').Trim());
                }
            }
            else // other providers
            {
                foreach (Match match in pattern.Matches(query))
                {
                    paramNames.Add(match.Value);
                }
            }

            return paramNames;
        }

        /// <summary>
        /// Agrega un DbParameter al DbCommand asociado.
        /// </summary>
        /// <param name="comm">
        /// Objeto DbCommand.
        /// </param>
        /// <param name="paramName">
        /// El nombre del parámetro.
        /// </param>
        /// <param name="value">
        /// El valor del parámetro.
        /// </param>
        /// <remarks>
        /// </remarks>
        public static void AddWithValue<TCommand>
        (
            this TCommand comm, string paramName,
            object value,
            ParameterDirection direction = ParameterDirection.Input
        )
            where TCommand : DbCommand
        {
            var param = comm.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            param.Direction = direction;
            comm.Parameters.Add(param);
        }


        public static void AddParamWithVal<TCommand>
        (
            this TCommand comm, string paramName,
            object value,
            ParameterDirection direction = ParameterDirection.Input
        )
            where TCommand : DbCommand
        {
            var param = comm.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            param.Direction = direction;
            comm.Parameters.Add(param);
        }

    }
}
