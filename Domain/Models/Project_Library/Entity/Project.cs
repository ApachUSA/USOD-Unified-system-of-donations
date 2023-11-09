using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project
	{
		public int Project_ID { get; set; }

		public required string Project_Name { get; set; }

		public string? Project_Description { get; set;}

		public double Project_Goal {  get; set;}

		public double Project_Current_Sum { get; set; }

		public int Project_Status_ID { get; set; }

		public Project_Status? Project_Status { get; set; }

		public int Project_Report_ID { get; set; }

		public Project_Report? Project_Report { get; set; }

		public List<Project_Payment>? Project_Payments { get; set; }

		public List<Project_Card>? Project_Cards { get; set; }


	}
}
