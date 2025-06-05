using System;
using System.Collections.Generic;
using System.Text;

namespace DbmShared.Dicts
{
    [Serializable]
    public class MetaDict
    {
        public Clients ClientsDict { get; set;}
        public MetaDict() 
        { 
            ClientsDict = new Clients();
        }

    }
}
