using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

[assembly: ExtensionApplication(typeof(PgAutoCAD.Plugin))]

namespace PgAutoCAD
{
    public class Plugin: IExtensionApplication
    {
        public void Initialize()
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nPgAutoCAD yüklendi...");
        }

        public void Terminate()
        {
            
        }
    }
}
