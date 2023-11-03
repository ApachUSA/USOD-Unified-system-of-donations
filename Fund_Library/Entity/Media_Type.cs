using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_Library.Entity
{
	public class Media_Type
	{
		public int Media_Type_ID { get; set; }

		public required string Media_Type_Name { get; set; }

		public required string Media_Type_Image { get; set; }

		public List<Fund_Media>? Fund_Medias { get; set; }
	}
}
