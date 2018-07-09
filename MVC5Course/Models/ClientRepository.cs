using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class ClientRepository : EFRepository<Client>, IClientRepository
	{
        public Client Find(int id)
        {
            return this.All().FirstOrDefault(p => p.ClientId == id);

        }
	}

	public  interface IClientRepository : IRepository<Client>
	{

	}
}