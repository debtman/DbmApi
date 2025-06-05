using DbmApi.FbData;
using DbmShared.Dicts;
using System.Collections.Generic;
using System.Data;
using static DbmApi.FbData.CommonData;

namespace DbmApi.Models
{
    public class DictsModel
    {
        public List<Tuple<string, string>> DataInfo;

        public DictsModel(bool loadAdmin)
        {
            DataInfo = new List<Tuple<string, string>>();
            DataInfo.Add(new Tuple<string, string>("select * from asp_getactions", "ACTIONS"));
            DataInfo.Add(new Tuple<string, string>("select * from asp_getstatuslist", "STATUSES"));
            DataInfo.Add(new Tuple<string, string>("select * from asp_getsources", "SOURCES"));
            DataInfo.Add(new Tuple<string, string>("select * from asp_getprovince", "PROVINCES"));
            DataInfo.Add(new Tuple<string, string>("select * from asp_getregions", "REGIONS"));

            if (loadAdmin == true)
            {
                DataInfo.Add(new Tuple<string, string>("select * from asp_getusersforsplit", "USERS_FOR_SPLIT"));
                DataInfo.Add(new Tuple<string, string>("select * from asp_get_deps_list", "DEPARTMENTS"));
            }
        }

        public MetaDict FillDicts(DbmDataSet p_ds)
        {
            MetaDict dict = new MetaDict();

            if (p_ds.dmDataSet.Tables.Count > 0)
            {
                if (p_ds.dmDataSet.Tables.Contains("SOURCES"))
                {
                    List<Client> clients = new List<Client>();
                    foreach (DataRow row in p_ds.dmDataSet.Tables["SOURCES"].Rows)
                    {
                        clients.Add(new Client
                        {
                            Id = row.Field<int>("SRCID"),
                            ShortName = row.Field<string>("SRC_SHORT") ?? string.Empty,
                            OrgName = row.Field<string>("SRC_FULL") ?? string.Empty                            
                        });
                    }

                    if (clients.Count > 0)
                    {
                        dict.ClientsDict.FillClients(clients);
                    }
                }                                
            }

            return dict;
        }

        

}
}
