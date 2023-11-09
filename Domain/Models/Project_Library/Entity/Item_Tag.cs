using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Item_Tag
	{
		public int Item_Tag_ID { get; set; }

		public required string Item_Tag_Name { get; set; }

		public required string Item_Tag_URL { get; set; }

		public List<Project_Card>? Cards { get; set; }

	}
}
