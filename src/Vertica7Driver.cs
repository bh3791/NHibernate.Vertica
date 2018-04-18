using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Reflection;
using System.Data.Common;
using NHibernate.SqlTypes;

namespace NHibernate.Vertica
{
    public class Vertica7Driver : NHibernate.Driver.ReflectionBasedDriver
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Vertica7Driver"/> class.
        /// </summary>
        /// <exception cref="HibernateException">
        /// Thrown when the <c>Vertica.Data</c> assembly can not be loaded.
        /// </exception>
        public Vertica7Driver()
            : base(
                "Vertica",
                // "Vertica.Data", 
                Assembly.LoadWithPartialName("Vertica.Data").FullName, // needed in order to locate fully qualified name from GAC
                "Vertica.Data.VerticaClient.VerticaConnection",
                "Vertica.Data.VerticaClient.VerticaCommand")
        {
            
        }
        /*
        public override bool UseNamedPrefixInSql
        {
            get { return true; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return true; }
        }

        public override string NamedPrefix
        {
            get { return ":"; }
        }
        */
        // Vertica appears to use ? for prepared statements, not a prefixed identifier
        // https://my.vertica.com/docs/6.1.x/HTML/index.htm#11677.htm
        public override bool UseNamedPrefixInSql
        {
            get { return false; }
        }

        public override bool UseNamedPrefixInParameter
        {
            get { return false; }
        }

        public override string NamedPrefix
        {
            get { return String.Empty; }
        }

        public override bool SupportsMultipleOpenReaders
        {
            get { return false; }
        }

        protected override bool SupportsPreparingCommands
        {
            // NH-2267 Patrick Earl
            get { return true; }
        }

        public override NHibernate.Driver.IResultSetsCommand GetResultSetsCommand(NHibernate.Engine.ISessionImplementor session)
        {
            return new NHibernate.Driver.BasicResultSetsCommand(session);
        }

        public override bool SupportsMultipleQueries
        {
            get { return true; }
        }

        protected override void InitializeParameter(DbParameter dbParam, string name, SqlType sqlType)
        {
            base.InitializeParameter(dbParam, name, sqlType);

            // Since the .NET currency type has 4 decimal places, we use a decimal type in PostgreSQL instead of its native 2 decimal currency type.
            if (sqlType.DbType == DbType.Currency)
                dbParam.DbType = DbType.Decimal;
        }
    }
}
