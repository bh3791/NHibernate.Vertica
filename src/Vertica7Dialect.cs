using System.Data;
using System.Data.Common;
using NHibernate.Cfg;
using NHibernate.Dialect.Function;
using NHibernate.Dialect.Schema;
using NHibernate.SqlCommand;
using NHibernate.SqlTypes;
using NHibernate.Dialect;

namespace NHibernate.Vertica
{
    /// <summary>
    /// An SQL dialect for Vertica.
    /// </summary>
    /// <remarks>
    /// The VerticaDialect defaults the following configuration properties:
    /// <list type="table">
    ///	<listheader>
    ///		<term>Property</term>
    ///		<description>Default Value</description>
    ///	</listheader>
    ///	<item>
    ///		<term>connection.driver_class</term>
    ///		<description><see cref="NHibernate.Vertica.Vertica7Driver" /></description>
    ///	</item>
    /// </list>
    /// </remarks>
    public class Vertica7Dialect : NHibernate.Dialect.PostgreSQLDialect
    {
        public Vertica7Dialect()
            : base()
        {
            DefaultProperties[Environment.ConnectionDriver] = "NHibernate.Vertica.VerticaDriver";

            RegisterColumnType(DbType.Int32, "int");
        }

        public override bool SupportsIdentityColumns
        {
            get { return false; }
        }

        public override bool HasDataTypeInIdentityColumn
        {
            get { return true; }
        }

        /// <summary>
        /// Unlike PostgreSQL Vertica does not support the SERIES keyword.
        /// </summary>
        /// <returns>Empty string</returns>
        public override string GetIdentityColumnString(DbType type)
        {
            return string.Empty;
        }

        /// <summary>
        /// The sql syntax to insert a row without specifying any column in PostgreSQL is
        /// <c>INSERT INTO table DEFAULT VALUES;</c>
        /// </summary>
        public override string NoColumnsInsertString
        {
            get { return "default values"; }
        }

        /// <summary>
        /// PostgreSQL 8.1 and above defined the fuction <c>lastval()</c> that returns the
        /// value of the last sequence that <c>nextval()</c> was used on in the current session.
        /// Call <c>lastval()</c> if <c>nextval()</c> has not yet been called in the current
        /// session throw an exception.
        /// </summary>
        public override string IdentitySelectString
        {
            get { return "select currval('hibernate_sequence')"; }
        }

        public override string IdentityInsertString
        {
            get { return "nextval('hibernate_sequence')"; }
        }

        public override SqlString AppendIdentitySelectToInsert(SqlString insertSql)
        {
            return insertSql.Append("; " + IdentitySelectString);
        }

        public override bool SupportsInsertSelectIdentity
        {
            get { return true; }
        }

        /// <summary>
        /// override PostGresSQL behavior. Vertica does not support the RETURNING keyword
        /// </summary>
        /// <param name="insertString"></param>
        /// <param name="identifierColumnName"></param>
        /// <param name="parameterName"></param>
        /// <returns></returns>
        public override SqlString AddIdentifierOutParameterToInsert(SqlString insertString, string identifierColumnName, string parameterName)
        {
            return insertString;
        }

        public override InsertGeneratedIdentifierRetrievalMethod InsertGeneratedIdentifierRetrievalMethod
        {
            get { return InsertGeneratedIdentifierRetrievalMethod.ReturnValueParameter; }
        }
    }
}
