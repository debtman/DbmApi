using DbmShared.Dicts;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace DbmShared
{
    [Serializable]    
    public class Clients
    {
        public List<Client> ClientsList { get; set; } = new List<Client>();
        public Clients() { }
        public void FillClients(List<Client> p_clients)
        {            
            ClientsList.Clear();
            ClientsList.AddRange(p_clients);
        }
               
        public Client? GetClientById(int id)
        {
            var client = ClientsList.Find(c => c.Id == id);
            return client; 
        }

        public Client? GetClientByShortName(string shortName)
        {
            var client = ClientsList.Find(c => c.ShortName.Equals(shortName, StringComparison.OrdinalIgnoreCase));
            return client;
        }
        public List<Client> GetAllClients()
        {
            return ClientsList;
        }
    }
}
