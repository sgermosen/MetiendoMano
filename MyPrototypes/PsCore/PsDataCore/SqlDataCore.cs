using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text.RegularExpressions;

namespace PsDataCore
{
    public class SqlDataCore
    {
        private readonly SqlConnection _connection;
        private SqlCommand _command;
        private DataSet _ds;
        private SqlDataAdapter _da;

        public SqlCommand Command { get; set; }
        public SqlTransaction Transaction { get; set; }
        private bool isTransaction;
        public CommandType commandType { get; set; }
        public DataSet Ds { get; set; }
        public SqlDataCore()
        {
            //iniciando el constructor con el tipo que usare mas amenudo
            commandType = CommandType.StoredProcedure;
            var cs = ConfigurationManager.ConnectionStrings["PsConnection"];

            //string _str = cs == null ? Decifrar(ConfigurationManager.ConnectionStrings["cadenaConexionProd"].ConnectionString) : cs.ConnectionString;
            string _str = cs == null ? (ConfigurationManager.ConnectionStrings["PsConnectionProd"].ConnectionString) : cs.ConnectionString;
            this._connection = new SqlConnection(_str);
        }

        public static void VerifyConexion()
        {
            var cs = ConfigurationManager.ConnectionStrings["PsConnection"];
            //string _str = cs == null ? Decifrar(ConfigurationManager.ConnectionStrings["cadenaConexionProd"].ConnectionString) : cs.ConnectionString;
            string _str = cs == null ? (ConfigurationManager.ConnectionStrings["PsConnectionProd"].ConnectionString) : cs.ConnectionString;
            var _connection = new SqlConnection(_str);
        }

        public void OpenConnection()
        {
            this._connection.Open();
        }


        public DataSet GetDataBySP(string spName, params SqlParameter[] spParams)
        {

            this._ds = new DataSet();

            IniCommandAddParam(spName, spParams);

            this._da = new SqlDataAdapter(this._command);
            try
            {
                this.OpenConnection();
                this._da.Fill(this._ds);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } finally
            {
                this.CloseConnection();
            }

            return this._ds;
        }

        private void IniCommandAddParam(string spName, params SqlParameter[] spParams)
        {
            this._command = new SqlCommand(spName, this._connection);
            this._command.CommandType = CommandType.StoredProcedure;

            foreach (var param in spParams)
            {
                this._command.Parameters.Add(param);
            }
        }


        public void ExecuteCommandScalar(string spName, params SqlParameter[] spParams)
        {
            IniCommandAddParam(spName, spParams);
            try
            {
                this.OpenConnection();
                this._command.ExecuteScalar();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            } finally
            {
                this.CloseConnection();
            }

        }

        protected void Dispose()
        {
            this._ds.Dispose();
            this._da.Dispose();
            this._command.Dispose();
            if (this._connection.State != ConnectionState.Closed)
            {
                this.CloseConnection();
            }
        }

        public void CloseConnection()
        {
            this._connection.Close();
        }

        //private static string Decifrar(string pCifrado)
        //{
        //    var textConverter = new ASCIIEncoding();
        //    var myRijndael = new System.Security.Cryptography.RijndaelManaged();
        //    byte[] fromEncrypt = null;
        //    byte[] encrypted = Convert.FromBase64String(pCifrado.Replace(";", "ON").Replace(",", "on").Replace(".", "+"));
        //    byte[] key =
        //    {
        //        12,
        //        215,
        //        88,
        //        34,
        //        53,
        //        68,
        //        100,
        //        183,
        //        95,
        //        132,
        //        56,
        //        74,
        //        46,
        //        27,
        //        39,
        //        62
        //    };
        //    byte[] IV =
        //    {
        //        88,
        //        234,
        //        153,
        //        46,
        //        27,
        //        39,
        //        12,
        //        215,
        //        62,
        //        68,
        //        10,
        //        183,
        //        44,
        //        132,
        //        56,
        //        64
        //    };
        //    System.Security.Cryptography.ICryptoTransform decryptor = myRijndael.CreateDecryptor(key, IV);
        //    var msDecrypt = new System.IO.MemoryStream(encrypted);
        //    var csDecrypt =
        //        new System.Security.Cryptography.CryptoStream(msDecrypt, decryptor,
        //            System.Security.Cryptography.CryptoStreamMode.Read);

        //    fromEncrypt = new byte[encrypted.Length + 1];
        //    csDecrypt.Read(fromEncrypt, 0, fromEncrypt.Length);
        //    return textConverter.GetString(fromEncrypt);
        //}






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
            if (!isTransaction && _connection.State == ConnectionState.Open)
            {
                Transaction = _connection.BeginTransaction();
                isTransaction = true;
            }
        }
        public void RollBackTransaction()
        {
            isTransaction = false;
            Transaction.Rollback();

        }
        public void CommitTransaction()
        {
            Transaction.Commit();
            isTransaction = false;
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
            if (!isTransaction && _connection.State == ConnectionState.Open)
            {
                Transaction = _connection.BeginTransaction(level);
                isTransaction = true;
            }
        }

        /// <summary>
        /// Abre el objeto de conexión para realizar las consultas, si es 
        /// necesario o crea un nuevo ámbito de conexión.
        /// </summary>
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
        private void LoadCommandObj(String sqlCommand, Object[] values, Object[] paramDirs = null, bool isProcedure = false, string parameters = null)
        {
            Command = new SqlCommand();
            Command.Connection = _connection;
            if (isTransaction) { Command.Transaction = Transaction; }
            Command.CommandText = sqlCommand;
            ArrayList names = new ArrayList();

            if (isProcedure)
            {
                string[] prms = parameters.Split(',').Select(r => r.Trim()).ToArray();
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
        public ArrayList GetParameterNames(string query)
        {
            ArrayList paramNames = new ArrayList();

            Regex pattern = new Regex(":([A-Za-z0-9]+)(\\s*)");
            foreach (Match match in pattern.Matches(query))
            {
                paramNames.Add(match.Value.Replace(':', ' ').Trim());
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
        public void AddWithValue
        (
            string paramName,
            object value,
            ParameterDirection direction = ParameterDirection.Input
        )
        {
            var param = Command.CreateParameter();
            param.ParameterName = paramName;
            param.Value = value;
            param.Direction = direction;
            Command.Parameters.Add(param);
        }

        public DataSet ExecuteCursor(String dsName = "DataTable")
        {
            _da = new SqlDataAdapter();
            _da.TableMappings.Add("Table", dsName);
            DataSet ds = new DataSet("DataTable");
            _da.SelectCommand = Command;
            _da.Fill(ds);

            return ds;
        }

        //public DataSet ReturnDsFromCursor(string procedureName, Object[] values, String parameterNames, Object[] paramDirs = null, String parameterOut = "")
        //{
        //    //OpenConnection();
        //    //this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);
        //    ////return Command;
        //    //Command.Parameters[parameterOut].SqlDbType = SqlDbType..RefCursor;
        //    //Ds = ExecuteCursor();
        //    //CloseConnection();
        //    //return Ds;
        //}

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

        public DataSet ReturnDsFromCursor(string procedureName, Object[] values, String parameterNames, Object[] paramDirs = null, String parameterOut = "")
        {
            OpenConnection();
            this.LoadCommandObj(procedureName, values, paramDirs, true, parameterNames);
            //return Command;
            //Command.Parameters[parameterOut].SqlDbType = SqlDbType.RefCursor;
            Ds = ExecuteCursor();
            CloseConnection();
            return Ds;
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
    }
}
