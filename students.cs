using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA1
{
    public class students:super_slave
    {
        private string ticket;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string Group
        {
            get { return group; }
            set { group = value; }
        }

        public string Ticket
        {
            get { return ticket; }
            set { ticket = value; }
        }

    }
}
