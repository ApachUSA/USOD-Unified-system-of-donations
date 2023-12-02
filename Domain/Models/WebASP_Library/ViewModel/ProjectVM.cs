using Fund_Library.Entity;
using Project_Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP_Library.ViewModel
{
	public class ProjectVM
	{
		public required Project Project { get; set; }

		public Fund? FundOwner { get; set; }

		public List<Fund>? FundCoop { get; set; }
	}
}
