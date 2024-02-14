using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DBL
{
    public abstract class DB
    {

        private const string MySqlConnSTR = @"server=localhost;
                                    user id=root;
                                    password=josh17rog;
                                    persistsecurityinfo=True;
                                    database=mystore";

        protected static DbConnection conn;
        protected DbCommand cmd;
        protected DbDataReader reader;

        protected DB()
        {
            if (conn == null)
            {

                conn = new MySqlConnection(MySqlConnSTR);
                cmd = new MySqlCommand();

            }
            else
            {

                cmd = new MySqlCommand();

            }
            cmd.Connection = conn;
            reader = null;
        }
    }
}