using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project_Payment
	{
		public int Project_Payment_ID { get; set; }

		public required string Payment_URL { get; set; }

		public int Payment_Type_ID { get; set;}

		public Payment_Type? Payment_Type { get; set;}
	}
}
