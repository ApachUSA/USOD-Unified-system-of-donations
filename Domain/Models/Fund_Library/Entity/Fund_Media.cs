using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fund_Library.Entity
{
	public class Fund_Media
	{
		public int Fund_Media_ID { get; set; }

		public required string Fund_Media_URL { get; set; }

		public int Media_Type_ID { get; set; }

		public Media_Type? Media_Type { get; set; }

		public int Fund_ID { get; set; }

		public Fund? Fund {  get; set; }

	}
}
