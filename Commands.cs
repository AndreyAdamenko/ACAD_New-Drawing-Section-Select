using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.ApplicationServices;

namespace ACAD_NewDrawingSectionSelect
{
    public class Commands
    {
        [CommandMethod("AddSection")]
        public void AddSection()
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;

            Application app = new Application();

            CurrentSectionDepartment curSectDept = app.GetSectionDepartment(doc);

            app.WriteSectionToDWG(doc, curSectDept);
        }
    }
}
