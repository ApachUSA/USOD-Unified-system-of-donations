using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Card_Image
	{
		public int Card_Image_ID { get; set; }

		public required string Card_Image_URL { get; set; }

		public int Project_Card_ID { get; set; }

		public Project_Card? Project_Card { get; set; }
	}
}
