using System.Data;

namespace DbmApi.FbData
{
    [Serializable]
    public static class CommonData
    {
        public struct DbmDataTable
        {
            public string? message;
            public DataTable dmtable;
            public DbmDataTable()
            {
                dmtable = new DataTable();
            }
        }

        public class DbmDataSet
        {
            public string? message;
            public DataSet dmDataSet;
            public DbmDataSet()
            {
                dmDataSet = new DataSet();
            }
        }

        public struct DbmString
        {
            public string message;
            public string dmstring;
        }

        public struct DbmInt
        {
            public string message;
            public int dmint;
        }

        public struct PayInfo
        {
            public DateTime PayDate;
            public decimal PaySum;
        }
    }
}
