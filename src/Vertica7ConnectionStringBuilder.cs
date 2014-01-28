using System.Text;
using FluentNHibernate.Cfg.Db;

namespace NHibernate.Vertica
{
    public class Vertica7ConnectionStringBuilder : ConnectionStringBuilder
    {
        private string host;
        private int port;
        private string database;
        private string username;
        private string password;

        public Vertica7ConnectionStringBuilder Host(string host)
        {
            this.host = host;
            IsDirty = true;
            return this;
        }

        public Vertica7ConnectionStringBuilder Port(int port)
        {
            this.port = port;
            IsDirty = true;
            return this;
        }

        public Vertica7ConnectionStringBuilder Database(string database)
        {
            this.database = database;
            IsDirty = true;
            return this;
        }

        public Vertica7ConnectionStringBuilder Username(string username)
        {
            this.username = username;
            IsDirty = true;
            return this;
        }

        public Vertica7ConnectionStringBuilder Password(string password)
        {
            this.password = password;
            IsDirty = true;
            return this;
        }

        protected override string Create()
        {
            var connectionString = base.Create();

            if (!string.IsNullOrEmpty(connectionString))
                return connectionString;

            var sb = new StringBuilder();

            sb.AppendFormat("User Id={0};Password={1};Host={2};Port={3};Database={4};", username, password,
                host, port, database);

            return sb.ToString();
        }
    }
}
