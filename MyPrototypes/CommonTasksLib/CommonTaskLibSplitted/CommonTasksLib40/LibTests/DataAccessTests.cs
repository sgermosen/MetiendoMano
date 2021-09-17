using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ADOHelpersLib.ADOExtensions;
using Oracle.ManagedDataAccess.Client;
using System.Data.SqlClient;
using ADOHelpersLib.ADOExtensions.Enums;

namespace LibTests
{
    [TestClass]
    public class DataAccessTests
    {
        [TestMethod]
        public void CheckOracleProvider()
        {
            GenericDAO<OracleCommand, OracleConnection, OracleDataAdapter> Dao = new GenericDAO<OracleCommand, OracleConnection, OracleDataAdapter>("", InstanceType.Oracle);
            Dao.FillCommand("select * from x", null);

            Assert.IsNotNull(Dao.Command, "Data Access Object Creation Failed With Oracle Provider");

        }

        [TestMethod]
        public void CheckSqlServerProvider()
        {
            GenericDAO<SqlCommand, SqlConnection, SqlDataAdapter> Dao = new GenericDAO<SqlCommand, SqlConnection, SqlDataAdapter>("", InstanceType.SqlServer);
            Dao.FillCommand("select * from x", null);

            Assert.IsNotNull(Dao.Command, "Data Access Object Creation Failed With Sql Server Provider");
        }
    }
}
