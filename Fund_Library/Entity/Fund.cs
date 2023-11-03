using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_Library.Entity
{
	public class Fund
	{
		public int Fund_ID { get; set; }	

		public required string Fund_Name { get; set; }

		public string? Fund_Logo { get; set; }

		public string? Fund_Info { get; set; }

		public string? Fund_Poster { get; set; }

		public List<Fund_Media>? Fund_Medias { get; set; }

		public List<Fund_Image>? Fund_Images { get; set; }

		public List<Fund_Member>? Fund_Members { get; set; }

		
	}
}
