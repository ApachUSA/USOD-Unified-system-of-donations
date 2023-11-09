using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project_Status
	{
		public int Project_Status_ID { get; set; }

		public required string Project_Status_Name { get; set; }

		public List<Project>? Projects { get; set; }
	}
}
