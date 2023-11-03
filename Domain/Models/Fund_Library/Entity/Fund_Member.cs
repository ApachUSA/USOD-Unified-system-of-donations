using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_Library.Entity
{
	public class Fund_Member
	{
		public int Fund_Member_ID { get; set; }

		public int Donor_ID { get; set; }

		public int Fund_ID { get; set; }

		public Fund? Fund { get; set; }

		public int Member_Role_ID { get; set; }

		public Member_Role? Member_Role { get; set; }
	}
}
