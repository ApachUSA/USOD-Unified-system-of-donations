using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project_Report
	{
		public int Project_Report_ID { get; set; }

		public string? Project_Report_Text { get; set; }

		public required string Project_Report_File_URL { get; set; }

		public int Project_ID { get; set; }

		public Project? Project { get; set; }

		public List<Project_Report_Image>? Images { get; set; }

	}
}
