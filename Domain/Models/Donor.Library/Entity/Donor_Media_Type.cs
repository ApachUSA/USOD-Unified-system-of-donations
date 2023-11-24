using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor_Library.Entity
{
	public class Donor_Media_Type
	{
		public int Donor_Media_Type_ID { get; set; }

		public required string Donor_Media_Type_Name { get; set; }

		public required string Donor_Media_Type_Image { get; set; }

		public List<Donor_Media>? Donor_Medias { get; set; }
	}
}
