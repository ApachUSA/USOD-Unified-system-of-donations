using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project_Card
	{
		public int Project_Card_ID { get; set; }

		public required string Project_Card_Title { get; set; }

		public required string Project_Card_Description { get; set; }

		public string? Project_Card_URL { get; set; }

		public int Project_ID { get; set; }

		public Project? Project { get; set; }

		public int Item_Tag_ID { get; set; }

		public Item_Tag? Item_Tag { get; set; }

		public List<Card_Image>? Card_Images { get; set; }

		public List<Card_Video>? Card_Videos { get; set; }

	}
}
