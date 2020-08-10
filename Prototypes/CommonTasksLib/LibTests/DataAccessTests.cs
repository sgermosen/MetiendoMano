using CommonTasksLib.Data.ADOExtensions;
using CommonTasksLib.Data.ADOExtensions.Enums;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;

namespace LibTests
{
    [TestClass]
    public class DataAccessTests
    {
        [TestMethod]
        public void CheckOracleProvider()
        {
            try
            {
                GenericDAO<OracleCommand, OracleConnection, OracleDataAdapter> Dao = new GenericDAO<OracleCommand, OracleConnection, OracleDataAdapter>("", InstanceType.Oracle);
                Dao.FillCommand("select * from x", null);

                Assert.IsNotNull(Dao.Command, "Data Access Object Creation Failed With Oracle Provider");
            }
            catch
            {
                Assert.IsNotNull(null, "Data Access Object Creation Failed With Oracle Provider");
            }
        }

        [TestMethod]
        public void CheckSqlServerProvider()
        {

            try
            {
                GenericDAO<SqlCommand, SqlConnection, SqlDataAdapter> Dao = new GenericDAO<SqlCommand, SqlConnection, SqlDataAdapter>("", InstanceType.SqlServer);
                Dao.FillCommand("select * from x", null);

                Assert.IsNotNull(Dao.Command, "Data Access Object Creation Failed With Sql Server Provider");
            }
            catch
            {
                Assert.IsNotNull(null, "Data Access Object Creation Failed With Sql Server Provider");
            }
        }
    }
}
