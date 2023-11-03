using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_Library.Entity
{
	public class Fund_Image
	{
		public int Fund_Image_ID { get; set; }

		public required string Image { get; set; }

		public int Fund_ID { get; set; }

		public Fund? Fund { get; set; }
	}
}
