using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealTime_Library
{
	public class Project_Comment
	{
		public int Comment_ID { get; set; }

		public required string Comment_Message { get; set; }

		public int Project_ID { get; set; }

		public int Donor_ID { get; set; }

		public DateTime Comment_Date { get; set; }
	}
}
