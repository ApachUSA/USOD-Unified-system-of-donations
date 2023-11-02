using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Donor_Library.Entity
{
    public class Donor_Role
    {
        public int Donor_Role_ID { get; set; }

        public string Donor_Role_Name { get; set; }

        public List<Donor>? Donors { get; set; }
    }
}
