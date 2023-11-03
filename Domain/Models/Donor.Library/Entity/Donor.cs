using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor_Library.Entity
{
    public class Donor
    {
        public int Donor_ID { get; set; }

        public required string Surname { get; set; }

        public required string Name { get; set; }

        public string? Patronymic { get; set; }

        public required string Email { get; set; }

        public required string Username { get; set; }

        public string? Logo { get; set; }

        public required string Login { get; set; }

        public required string Password { get; set; }

        public int Donor_Role_ID { get; set; }

        public Donor_Role? Donor_Role { get; set; }
    }
}
