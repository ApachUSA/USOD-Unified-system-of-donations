using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project_Report_Image
	{
		public int Project_Report_Image_ID { get; set; }

		public required string Project_Report_Image_URL { get; set; }

		public int Project_Report_ID { get; set; }

		public Project_Report? Project_Report {  get; set; }
	}
}
