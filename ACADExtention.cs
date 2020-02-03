using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;

namespace ACAD_NewDrawingSectionSelect
{
    public class ACADExtention : IExtensionApplication
    {
        public void Initialize()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentCreated += DocumentManager_DocumentCreated;
        }

        private void DocumentManager_DocumentCreated(object sender, DocumentCollectionEventArgs e)
        {
            Document doc = e.Document;

            var f = doc.IsNamedDrawing; 

            Application app = new Application();

            CurrentSectionDepartment curSectDept = app.GetSectionDepartment(doc);

            if (curSectDept == null || curSectDept.department == "" || curSectDept.section == "")
            {
                //If data contains in DWG not full or not specified, then call method

                app.WriteSectionToDWG(doc);
            }
        }

        public void Terminate()
        {
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.DocumentCreated -= DocumentManager_DocumentCreated;
        }
    }
}
