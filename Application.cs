using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices;
using ACAD_CustomDataManager;

namespace ACAD_NewDrawingSectionSelect
{
    public class Application
    {
        private const string dept = "department";
        private const string sect = "section";

        Dictionary<string, List<string>> Departments = new Dictionary<string, List<string>>();

        public Application()
        {
            CustomDataManager CDManager = new CustomDataManager();

            Departments = CDManager.Local.GetDictionary("ProjectSections");
        }

        public CurrentSectionDepartment ShowForm(CurrentSectionDepartment sectDept = null)
        {
            SectionSelectForm form = new SectionSelectForm(Departments, sectDept);

            System.Windows.Forms.DialogResult res = form.ShowDialog();

            if (res == System.Windows.Forms.DialogResult.OK)
            {
                return form.result;
            }

            return null;
        }

        /// <summary>
        /// Returns the object contains current section and department from DWG if these data contains in DWG, else returns NULL.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public CurrentSectionDepartment GetSectionDepartment(Document doc)
        {
            CustomDataManager CDManager = new CustomDataManager();

            string curDept = CDManager.DWG.GetStringValue(doc, dept);

            string curSect = CDManager.DWG.GetStringValue(doc, sect);

            if (curDept == null) return null;

            if (curSect == null) return null;

            CurrentSectionDepartment curSectDept = new CurrentSectionDepartment(curDept, curSect);

            return curSectDept;
        }

        public void WriteSectionToDWG(Document doc, CurrentSectionDepartment sectDept = null)
        {
            CurrentSectionDepartment selResult = ShowForm(sectDept);

            if (selResult == null) return;
            
            CustomDataManager CDManager = new CustomDataManager();

            CDManager.DWG.SetStringValue(doc, dept, selResult.department);
            CDManager.DWG.SetStringValue(doc, sect, selResult.section);
        }
    }
}
