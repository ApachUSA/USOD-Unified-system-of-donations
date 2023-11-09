using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Card_Video
	{
		public int Card_Video_ID { get; set; }

		public required string Card_Video_URL { get; set; }

		public int Project_Card_ID { get; set; }

		public Project_Card? Project_Card { get; set; }
	}
}
