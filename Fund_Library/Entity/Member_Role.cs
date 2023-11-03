using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_Library.Entity
{
	public class Member_Role
	{
		public int Member_Role_ID { get; set; }

		public required string Member_Role_Name { get; set; }

		public List<Fund_Member>? Fund_Members { get; set; }
	}
}
