using System;
using System.Linq;
using System.Collections.Generic;
	
namespace MVC5Course.Models
{   
	public  class Table_1Repository : EFRepository<Table_1>, ITable_1Repository
	{

	}

	public  interface ITable_1Repository : IRepository<Table_1>
	{

	}
}