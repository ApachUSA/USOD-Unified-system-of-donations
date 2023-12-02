using Donor_Library.Entity;
using Donor_Library.ViewModel;
using Fund_Library.Entity;
using Project_Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP_Library.ViewModel
{
	public class FundVM
	{
		public List<DonorVM>? Fund_Members { get; set; }

		public required Fund Fund { get; set; }

		public List<ProjectVM>? Projects { get; set; }
	}
}
