using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Library.Entity
{
	public class Project_Fund
	{
		public int Project_Fund_ID { get; set; }

		public int Project_ID { get; set; }

		public Project? Project { get; set; }

		public int Fund_ID { get; set; }
	}
}
