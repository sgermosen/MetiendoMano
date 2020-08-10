using System;
using System.Data;
using System.Linq;
using System.Collections;
using System.Data.Common;

namespace PsDataCore
{
    /// <summary>
    /// Clase genérica creada para la gestión de consultas a BD
    /// de los diferentes proveedores que extienden de las estructuras
    /// genéricas para el manejo de consultas a Sql Server, Oracle, MySql,
    /// tomando en cuenta la creación de parámetros adecuados para
    /// ejecutar dichas consultas, así como el manejo explícito de
    /// las transacciones realizadas.
    /// </summary>
    /// <typeparam name="TCommand">Clase de cual extiende el comando asociado.</typeparam>
    /// <typeparam name="TConnection">Clase de la cual extiende la conexión asociada.</typeparam>
    /// <typeparam name="TAdapter">Clase de la cual extiende el adaptador asociado.</typeparam>
    public class GenericDAO<TCommand, TConnection, TAdapter>
        where TCommand : DbCommand, new()
        where TConnection : DbConnection, new()
        where TAdapter : DbDataAdapter, new()
    {
        /// <summary>
        /// Objecto que encapsula el comando a ejecutar que extiende
        /// de DbCommand.
        /// </summary>
        public TCommand Command { get; set; }

        /// <summary>
        /// Valor interino de la propiedad ConnectionString que especifica
        /// la cadena de conexión utilizada para la conexión con la BD.
        /// </summary>
        private string connectionString;

        /// <summary>
        /// Objeto que almacena la instancia de una transacción asociada
        /// a la conexión a la BD.
        /// </summary>
        private DbTransaction transaction;

        /// <summary>
        /// Objeto de la instancia de la conexión a la BD.
        /// </summary>
        private TConnection connection;

        /// <summary>
        /// Objeto de la instancia de DbDataAdapter para las operaciones
        /// que requieran utilizar objetos que manejan datos en entornos
        /// desconectados.
        /// </summary>
        private TAdapter adapter;

        /// <summary>
        /// Objeto interino de tipo DataSet para usar como valor de retorno
        /// para las consultas que así lo requieran.
        /// </summary>
        private DataSet ds;

        /// <summary>
        /// Valor interino de la propiedad IsTransaction, que especifica
        /// si para la conexión que este activa se está haciendo un manejo
        /// explícito del ámbito de transacción.
        /// </summary>
        private bool isTransaction;

        /// <summary>
        /// Propidad privada para la inicialización por defecto del tipo de
        /// commando a ejecutar desde el objeto Command.
        /// </summary>
        private CommandType commandType;

        /// <summary>
        /// Constructor por defecto de la Clase que crea un objeto conexión
        /// y especifica el tipo de comando utilizado por defecto (Text).
        /// </summary>
        public GenericDAO()
        {
            connection = (TConnection)Activator.CreateInstance(typeof(TConnection), connectionString);
            commandType = CommandType.Text;
        }

        /// <summary>
        /// Constructor de la clase que inicializa el objeto de conexión
        /// con la cadena de conexión especificada como parámetro.
        /// </summary>
        /// <param name="connString"></param>
        public GenericDAO(String connString)
        {
            this.connectionString = connString;
            connection = (TConnection)Activator.CreateInstance(typeof(TConnection), connString);
        }

        /// <summary>
        /// Propiedad publica para establecer u obtener la cadena de
        /// conexión utilizada para las consultas a la BD.
        /// </summary>
        public String ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
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
            if (!isTransaction && connection.State == ConnectionState.Open)
            {
                transaction = connection.BeginTransaction();
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
        public void BeginTransaction(IsolationLevel level)
        {
            if (!isTransaction && connection.State == ConnectionState.Open)
            {
                transaction = connection.BeginTransaction(level);
                isTransaction = true;
            }
        }

        /// <summary>
        /// Abre el objeto de conexión para realizar las consultas, si es 
        /// necesario o crea un nuevo ámbito de conexión.
        /// </summary>
        public void OpenConnection()
        {
            if (connection == null)
            {
                connection = new TConnection();
                connection.ConnectionString = connectionString;
                connection.Open();
            }
            else if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
        }

        /// <summary>
        /// Cierra el objeto de conexión para las consultas, liberando
        /// los recursos asociados.
        /// </summary>
        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
                isTransaction = false;
            }
        }

        /// <summary>
        /// Establece que la transacción actualmente manejada ha finalizado
        /// correctamente y escribe los cambios permanentementa a la BD.
        /// </summary>
        public void CommitTransaction()
        {
            if (isTransaction && connection.State == ConnectionState.Open)
            {
                transaction.Commit();
                isTransaction = false;
            }
        }

        /// <summary>
        /// Revierte los cambios realizados en la transacción actual.
        /// </summary>
        public void RollBackTransaction()
        {
            if (connection.State == ConnectionState.Open && isTransaction)
            {
                transaction.Rollback();
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
            adapter = new TAdapter();
            adapter.TableMappings.Add("Table", "DataTable");
            this.LoadCommandObj(sqlCommand, values, paramDirs);

            adapter.SelectCommand = Command;
            adapter.Fill(ds);

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
            adapter = new TAdapter();
            adapter.TableMappings.Add("Table", "DataTable");
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);

            adapter.SelectCommand = Command;
            adapter.Fill(ds);

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
            adapter = new TAdapter();
            adapter.TableMappings.Add("Table", dsName);
            ds = new DataSet(dsName);
            this.LoadCommandObj(sqlCommand, values, paramDirs);
            adapter.SelectCommand = Command;
            adapter.Fill(ds, dsName);

            return ds;
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
            adapter = new TAdapter();
            adapter.TableMappings.Add("Table", dataSetName);
            ds = new DataSet(dataSetName);
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);
            adapter.SelectCommand = Command;
            adapter.Fill(ds, dataSetName);

            return ds;
        }

        /// <summary>
        /// Carga los parámetros de la consulta sin hacer ninguna ejecución contra la base de datos.
        /// </summary>
        /// <param name="sqlCommand">Comando parametrizado a ejecutar en la BD.</param>
        /// <param name="values">Valores actuales para los parámetros establecidos.</param>
        /// <param name="paramDirs">Dirección de los parametros suplidos.</param>
        /// <returns>Un Objeto DbCommand con los parametros cargados, y su respectiva sentencia SQL.</returns>
        public DbCommand FillCommand(String sqlCommand, Object[] values, Object[] paramDirs = null)
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
        private void LoadCommandObj(String sqlCommand, Object[] values, Object[] paramDirs = null, bool isProcedure = false, string parameters = null)
        {
            Command = new TCommand();
            Command.Connection = connection;
            Command.CommandText = sqlCommand;
            ArrayList names = new ArrayList();
            if (isProcedure)
            {
                names.AddRange(parameters.Split(','));
                Command.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                names = DbTools.getParameterNames<TCommand>(sqlCommand);
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
                            Command.AddWithValue("", val);
                        }
                        else
                        {
                            Command.AddWithValue(names[index].ToString(), val);
                        }

                    }
                    else
                    {
                        if (paramDirs.Contains(names[index]))
                        {
                            Command.AddWithValue(names[index].ToString(), val, ParameterDirection.Output);
                        }
                        else if (!paramDirs.Contains(names[index]) && paramDirs.Length < values.Length)
                        {
                            Command.AddWithValue(names[index].ToString(), val);
                        }
                        else
                        {
                            Command.AddWithValue(
                                names[index].ToString(), val,
                                (ParameterDirection)Enum.Parse(typeof(ParameterDirection),
                                    paramDirs[index].ToString()));
                        }
                    }

                    index++;
                }
            }
        }

    }
}
