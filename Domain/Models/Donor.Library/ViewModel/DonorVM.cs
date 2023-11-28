using Donor_Library.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor_Library.ViewModel
{
	public class DonorVM
	{
		public int Donor_ID { get; set; }

		public required string Surname { get; set; }

		public required string Name { get; set; }

		public string? Patronymic { get; set; }

		public required string Email { get; set; }

		public required string Username { get; set; }

		public string? Logo { get; set; }

		public List<Donor_Media>? Media { get; set; }
	}
}
