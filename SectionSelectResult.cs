using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ACAD_NewDrawingSectionSelect
{
    public class CurrentSectionDepartment
    {       
        public string department = "";

        public string section = "";

        public CurrentSectionDepartment()
        { }

        public CurrentSectionDepartment(string departament, string section)
        {
            this.department = departament;
            this.section = section;
        }
    }
}
