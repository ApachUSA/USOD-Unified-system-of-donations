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

        public string Surname { get; set; }

        public string Name { get; set; }

        public string Patronymic { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public string Logo { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public int Donor_Role_ID { get; set; }

        public Donor_Role? Donor_Role { get; set; }
    }
}
