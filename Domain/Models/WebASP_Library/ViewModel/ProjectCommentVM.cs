using Donor_Library.Entity;
using Donor_Library.ViewModel;
using RealTime_Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebASP_Library.ViewModel
{
	public class ProjectCommentVM
	{
		public DonorVM? Donor { get; set; }

		public required Project_Comment Project_Comment { get; set; }
	}
}
