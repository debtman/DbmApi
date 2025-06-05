using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace DbmShared.Dicts
{
    [Serializable]
    public class Client
    {
        public int Id { get; set; }  // Primary key
        public string OrgName { get; set; }
        public string ShortName { get; set; }        
    }
}
