using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor_Library.Entity
{
	public class Donor_Media
	{
		public int Donor_Media_ID { get; set; }

		public string? Media_Url { get; set;}

		public int Donor_Media_Type_ID { get; set; }

		public Donor_Media_Type? Media_Type { get; set; }

		public int Donor_ID { get; set; }

		public Donor? Donor {  get; set; }
	}
}
