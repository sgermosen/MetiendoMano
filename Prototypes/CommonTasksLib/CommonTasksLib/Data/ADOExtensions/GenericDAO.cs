using CommonTasksLib.Data.ADOExtensions.Enums;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;

namespace CommonTasksLib.Data.ADOExtensions
{
    public class GenericDAO<TCommand, TConnection, TAdapter> : IDisposable
        where TCommand : DbCommand, new()
        where TConnection : DbConnection, new()
        where TAdapter : DbDataAdapter, new()
    {
        public TCommand Command { get; set; }
        public TConnection Connection { get; set; }
        public TAdapter Adapter { get; set; }
        public DbTransaction Transaction { get; set; }
        public string ConnString { get; set; }
        public DataSet Ds { get; set; }
        public InstanceType ContainerInstance { get; set; }
        private CommandType commandType;
        private bool isTransaction;

        public GenericDAO(string ConnString, InstanceType ContainerInstance = InstanceType.SqlServer)
        {
            this.ConnString = ConnString;
            this.ContainerInstance = ContainerInstance;
            Connection = (TConnection)Activator.CreateInstance(typeof(TConnection), ConnString);
            commandType = CommandType.Text;
        }

        /// <summary>
        /// Propiedad publica de solo lectura que especifica si para la
        /// conexión actual hacia la BD, se esta utilizando una transacción
        /// explícita.
        /// </summary>
        public bool IsTransaction
        {
            get { return isTransaction; }
        }

        /// <summary>
        /// Establece para el ámbito de conexión actual hacia la base de
        /// datos, se utilizará un manejo explícito de la transacción 
        /// asociada a las consultas
        /// </summary>
        public void BeginTransaction()
        {
            if (!isTransaction && Connection.State == ConnectionState.Open)
            {
                Transaction = Connection.BeginTransaction();
                isTransaction = true;
            }
        }

        /// <summary>
        /// Establece para el ámbito de conexión actual hacia la base de
        /// datos, se utilizará un manejo explícito de la transacción 
        /// asociada a las consultas, estableciendo el nivel de aislamiento
        /// y bloqueo de recursos de la BD a lo largo durante el transcurso
        /// de la transacción.
        /// </summary>
        /// <param name="level">Nivel de bloqueo de los recursos utilizados.</param>
        public void BeginTransaction(IsolationLevel level = IsolationLevel.ReadCommitted)
        {
            if (!isTransaction && Connection.State == ConnectionState.Open)
            {
                Transaction = Connection.BeginTransaction(level);
                isTransaction = true;
            }
        }

        /// <summary>
        /// Abre el objeto de conexión para realizar las consultas, si es 
        /// necesario o crea un nuevo ámbito de conexión.
        /// </summary>
        public void OpenConnection()
        {
            if (Connection == null)
            {
                Connection = (TConnection)Activator.CreateInstance(typeof(TConnection), ConnString);
                Connection.ConnectionString = ConnString;
                Connection.Open();
            }
            else if (Connection.State == ConnectionState.Closed)
            {
                Connection.Open();
            }
        }

        /// <summary>
        /// Cierra el objeto de conexión para las consultas, liberando
        /// los recursos asociados.
        /// </summary>
        public void CloseConnection()
        {
            if (Connection.State == ConnectionState.Open)
            {
                Connection.Close();
            }
        }

        /// <summary>
        /// Establece que la transacción actualmente manejada ha finalizado
        /// correctamente y escribe los cambios permanentementa a la BD.
        /// </summary>
        public void CommitTransaction()
        {
            if (isTransaction && Connection.State == ConnectionState.Open)
            {
                Transaction.Commit();
                isTransaction = false;
            }
        }

        /// <summary>
        /// Revierte los cambios realizados en la transacción actual.
        /// </summary>
        public void RollBackTransaction()
        {
            if (Connection.State == ConnectionState.Open && isTransaction)
            {
                Transaction.Rollback();
                isTransaction = false;
            }
        }

        /// <summary>
        /// Ejecuta una sentencia SQL usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">
        /// Propiedades de dirección de los parametros para la ejecución de la consulta,
        /// siguiendo la representación interna de la enumeracion ParameterDirection.
        /// Input = 1,
        /// Output = 2,
        /// InputOutput = 3,
        /// ReturnValue = 6 (The parameter represents a return value from an operation such
        ///     as a stored procedure, built-in function, or user-defined function.)
        /// 
        /// </param>
        /// <returns>El número de filas afectadas por dicha consulta.</returns>
        public int ExecuteNonQuery(string sqlCommand, Object[] values, Object[] paramDirs = null)
        {
            this.LoadCommandObj(sqlCommand, values, paramDirs);

            return Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos, en conjunción con los valores del procedimiento.</param>
        /// <param name="parameterNames">Nombres de los parametros para la consulta (separados por coma [,]).</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>El número de filas afectadas por dicha consulta.</returns>
        public int ExecuteNonQuery(string procedureName, Object[] values, String parameterNames, Object[] paramDirs = null)
        {
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);

            return Command.ExecuteNonQuery();
        }

        /// <summary>
        /// Ejecuta una sentencia SQL que devuelve un valor único, usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Objeto con el primer valor de la primera columna del resultado de la consulta.</returns>
        public Object ExecuteScalar(string sqlCommand, Object[] values, Object[] paramDirs = null)
        {
            Object returnVal = null;
            this.LoadCommandObj(sqlCommand, values, paramDirs);

            returnVal = Command.ExecuteScalar();

            return returnVal;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que devuelve un valor único, usando el objeto de conexión,
        /// el comando establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos, en conjunción con los valores del procedimiento.</param>
        /// <param name="parameterNames">Nombres de los parametros para la consulta (separados por coma [,]).</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Objeto con el primer valor de la primera columna del resultado de la consulta.</returns>
        public Object ExecuteScalar(string procedureName, Object[] values, String parameterNames, Object[] paramDirs = null)
        {
            Object returnVal = null;
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);
            returnVal = Command.ExecuteScalar();

            return returnVal;
        }

        /// <summary>
        /// Ejecuta una sentencia SQL que devuelve un objeto DbDataReader, usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Objeto DbDataReader con el resultado de la consulta.</returns>
        public DbDataReader ExecuteReader(string sqlCommand, object[] values, Object[] paramDirs = null)
        {
            this.LoadCommandObj(sqlCommand, values, paramDirs);

            return Command.ExecuteReader();
        }

        /// <summary>
        /// Ejecuta una sentencia SQL que devuelve un objeto Dataset, usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Objeto DataSet con el resultado de la consulta.</returns>
        public DataSet ExecuteQuery(string sqlCommand, object[] values, Object[] paramDirs = null)
        {
            DataSet ds = new DataSet("DataTable");
            Adapter = new TAdapter();
            Adapter.TableMappings.Add("Table", "DataTable");
            this.LoadCommandObj(sqlCommand, values, paramDirs);

            Adapter.SelectCommand = Command;
            Adapter.Fill(ds);

            return ds;
        }

        public DataSet ExecuteCursor(String dsName = "DataTable")
        {
            Adapter = new TAdapter();
            Adapter.TableMappings.Add("Table", dsName);
            DataSet ds = new DataSet("DataTable");
            Adapter.SelectCommand = Command;
            Adapter.Fill(ds);

            return ds;
        }


        /// <summary>
        /// Ejecuta un procedimiento almacenado que devuelve un objeto DataSet, usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos, en conjunción con los valores del procedimiento.</param>
        /// <param name="parameterNames">Nombres de los parametros para la consulta (separados por coma [,]).</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Objeto DataSet con el resultado de la consulta.</returns>
        public DataSet ExecuteQuery(string procedureName, Object[] values, string parameterNames, Object[] paramDirs = null)
        {
            DataSet ds = new DataSet("DataTable");
            Adapter = new TAdapter();
            Adapter.TableMappings.Add("Table", "DataTable");
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);

            Adapter.SelectCommand = Command;
            Adapter.Fill(ds);

            return ds;
        }

        /// <summary>
        /// Ejecuta una sentencia SQL que devuelve un objeto Dataset, usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos estableciendo usando el nombre de tabla especificado.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="dsName">Nombre de tabla para el DataSet.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Un Objeto DataSet con el resultado de la consulta.</returns>
        public DataSet ExecuteNamedQuery(string sqlCommand, Object[] values, string dsName, Object[] paramDirs = null)
        {
            Adapter = new TAdapter();
            Adapter.TableMappings.Add("Table", dsName);
            Ds = new DataSet(dsName);
            this.LoadCommandObj(sqlCommand, values, paramDirs);
            Adapter.SelectCommand = Command;
            Adapter.Fill(Ds, dsName);

            return Ds;
        }

        /// <summary>
        /// Ejecuta un procedimiento almacenado que devuelve un objeto DataSet, usando el objeto de conexión, el comando
        /// establecido como parámetro, y los valores suplidos.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos, en conjunción con los valores del procedimiento.</param>
        /// <param name="dataSetName">Nombre de tabla para el DataSet.</param>
        /// <param name="parameterNames">Nombres de los parametros para la consulta (separados por coma [,]).</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Objeto DataSet con el resultado de la consulta.</returns>
        public DataSet ExecuteNamedQuery(string procedureName, Object[] values, string dataSetName, string parameterNames, Object[] paramDirs = null)
        {
            Adapter = new TAdapter();
            Adapter.TableMappings.Add("Table", dataSetName);
            Ds = new DataSet(dataSetName);
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);
            Adapter.SelectCommand = Command;
            Adapter.Fill(Ds, dataSetName);

            return Ds;
        }

        /// <summary>
        /// Carga los parámetros de la consulta sin hacer ninguna ejecución contra la base de datos.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Un Objeto DbCommand con los parametros cargados, y su respectiva sentencia SQL.</returns>
        public DbCommand FillCommand(string sqlCommand, Object[] values, Object[] paramDirs = null)
        {
            this.LoadCommandObj(sqlCommand, values, paramDirs);

            return Command;
        }

        /// <summary>
        /// Carga los parámetros de un procedimiento almacenado a la BD, sin hacer ninguna ejecución contra el objeto de conexión.
        /// </summary>
        /// <param name="procedureName">Nombre del procedimiento.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos, en conjunción con los valores del procedimiento.</param>
        /// <param name="parameterNames">Nombres de los parametros para la consulta (separados por coma [,]).</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Un Objeto DbCommand con los parametros cargados.</returns>
        public DbCommand FillCommand(string procedureName, Object[] values, String parameterNames, Object[] paramDirs = null)
        {
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);

            return Command;
        }

        /// <summary>
        /// Carga los parámetros de la consulta sin hacer ninguna ejecución contra la base de datos y no retorna ningún valor.<br />
        /// Este método sirve de base para la parametrización de los datos de todos los demás métodos disponibles de forma 
        /// pública desde el Helper.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <param name="isProcedure">Especifica si se va a ejecutar un procedimiento almacenado.</param>
        /// <param name="parameters">Nombres de los parametros para la consulta (separados por coma [,]).</param>
        protected void LoadCommandObj(String sqlCommand, Object[] values, Object[] paramDirs = null, bool isProcedure = false, string parameters = null)
        {
            Command = new TCommand();
            Command.Connection = Connection;
            if (isTransaction) { Command.Transaction = Transaction; }
            Command.CommandText = sqlCommand;
            ArrayList names = new ArrayList();

            if (isProcedure)
            {
                string[] prms = parameters.Split(',');
                names.AddRange(prms);
                Command.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                names = GetParameterNames(sqlCommand);
                Command.CommandType = commandType;
            }

            int index = 0;
            if (values != null)
            {
                foreach (Object val in values)
                {
                    if (paramDirs == null)
                    {
                        if (names.Count == 0)
                        {
                            AddWithValue("", val);
                        }
                        else
                        {
                            AddWithValue(names[index].ToString(), val);
                        }

                    }
                    else
                    {
                        if (paramDirs.Contains(names[index]))
                        {
                            AddWithValue(names[index].ToString(), val, ParameterDirection.Output);
                        }
                        else if (!paramDirs.Contains(names[index]) && paramDirs.Length < values.Length)
                        {
                            AddWithValue(names[index].ToString(), val);
                        }
                        else
                        {
                            AddWithValue(
                                names[index].ToString(), val,
                                (ParameterDirection)Enum.Parse(typeof(ParameterDirection),
                                paramDirs[index].ToString()));
                        }
                    }

                    index++;
                }
            }
        }

        /// <summary>
        /// Método utilitario para obtener los nombres de los parámetros especificados
        /// en una sentencia SQL dada.
        /// </summary>
        /// <typeparam name="T">Tipo asociado al comando que ejecutará la sentencia.</typeparam>
        /// <param name="query">Sentencia SQL a ejecutar.</param>
        /// <returns>ArrayList con los nombres de los parametros.</returns>
        public virtual ArrayList GetParameterNames(string query)
        {
            ArrayList paramNames = new ArrayList();
            switch (ContainerInstance)
            {
                case InstanceType.SqlServer:
                case InstanceType.MySql:
                    {
                        Regex pattern = new Regex(@"(?<!@)@\w+");
                        foreach (Match match in pattern.Matches(query))
                        {
                            paramNames.Add(match.Value);
                        }
                    }
                    break;
                case InstanceType.Oracle:
                    {
                        Regex pattern = new Regex(":([A-Za-z0-9]+)(\\s*)");
                        foreach (Match match in pattern.Matches(query))
                        {
                            paramNames.Add(match.Value.Replace(':', ' ').Trim());
                        }
                    }
                    break;
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
        public virtual void AddWithValue
            (
                string paramName,
                object value,
                ParameterDirection direction = ParameterDirection.Input
            )
        {
            var param = Command.CreateParameter();
            param.ParameterName = FormatParameter(paramName);
            param.Value = value;
            param.Direction = direction;
            Command.Parameters.Add(param);
        }

        public virtual string FormatParameter(string paramName)
        {

            return (ContainerInstance != InstanceType.Oracle) ?
                "@" + paramName : paramName;
        }

        public void Dispose()
        {
            Connection.Dispose();
        }
    }
}
