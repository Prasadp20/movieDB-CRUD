using System.Data.Common;
using System.Data.SqlClient;

namespace CRUDonMovieDB.Repository
{
    public class BaseAsyncRepository
    {
        private string SqlWriterConnectionString;
        private string SqlReaderConnectionString;
        private string databaseType;

        public BaseAsyncRepository(IConfiguration _con)
        {
            SqlWriterConnectionString = _con.GetSection("DBInfo:WriterConnectionString").Value;
            SqlReaderConnectionString = _con.GetSection("DBInfo:ReaderConnectionString").Value;
            databaseType = _con.GetSection("DBInfo:DbType").Value;
        }

        internal DbConnection WriterConnectionString
        {
            get
            {
                switch (databaseType)
                {
                    case "SqlServer":
                        return new System.Data.SqlClient.SqlConnection(SqlWriterConnectionString);
                    default:
                        return new SqlConnection(SqlWriterConnectionString);
                }
            }
        }

        internal DbConnection ReaderConnectionString
        {
            get
            {
                switch (databaseType)
                {
                    case "SqlServer":
                        return new System.Data.SqlClient.SqlConnection(SqlReaderConnectionString);
                    default:
                        return new SqlConnection(SqlReaderConnectionString);
                }
            }
        }
    }
}
