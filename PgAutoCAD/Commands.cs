using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Runtime;

//[assembly: CommandClass(typeof(PgAutoCAD.Commands))]

namespace PgAutoCAD
{
    public class Commands
    {
        [CommandMethod("KomutGrubu", "IlkKomutum", "IlkKomutYerel", CommandFlags.Modal)]
        public void IlkKomut() 
        {
            Editor ed = Application.DocumentManager.MdiActiveDocument.Editor;
            ed.WriteMessage("\nİlk komut oluşturuldu");
        }

        [CommandMethod("MajorVersiyon")]
        public void AcadMajorVersiyon()
        {
            var majorVersion = Autodesk.AutoCAD.ApplicationServices.Application.Version.Major;
            Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument.Editor.WriteMessage($"\nMajor versiyon: {majorVersion}");
        }
        //[CommandMethod("ShowBlockTableRecord")]
        //public void ShowBlockTableRecordCommand()
        //{
        //    Bolum4.ShowBlockTableRecords();
        //}
    }
}
