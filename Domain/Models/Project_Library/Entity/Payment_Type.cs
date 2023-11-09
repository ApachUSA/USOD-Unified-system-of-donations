using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Payment_Type
	{
		public int Payment_Type_ID { get; set; }

		public required string Payment_Type_Name { get; set; }

		public required string Payment_Type_Image { get; set; }

		public List<Project_Payment>? Project_Payments { get; set; }
	}
}
