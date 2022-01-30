using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LABA1
{
    public class prepod:super_slave
    {
       public string lessons;

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

        public string Lessons
        {
            get { return lessons; }
            set { lessons = value; }
        }

    }
}
