using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Runtime;

namespace PgAutoCAD
{
    public class BlockTables
    {
        [CommandMethod("ShowBlockTableRecords")]
        public void ShowBlockTableRecords()
        {
            // 1. Autocad aktif belgesine erişim  
            Document doc = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument;
            Database db1 = Autodesk.AutoCAD.ApplicationServices.Core.Application.DocumentManager.MdiActiveDocument.Database;
            Database db2 = Autodesk.AutoCAD.DatabaseServices.HostApplicationServices.WorkingDatabase;
            // 2. İşlem yığının başlatılması  
            using (Transaction transaction = doc.TransactionManager.StartTransaction())
            {
                // 3. Belge veritabanının okuma amaçlı açılması  
                BlockTable blockTable = (BlockTable)doc.Database.BlockTableId.GetObject(OpenMode.ForRead);
                // 4. Blok tablo kayıtlarını okuma amaçlı açılması  
                foreach (var blockTableRecordId in blockTable)
                {
                    BlockTableRecord blockTableRecord = 
                        (BlockTableRecord)transaction.GetObject(blockTableRecordId, OpenMode.ForRead);
                    // 5. Blok tablo kayıt adının komut satırını yazdırılması
                    doc.Editor.WriteMessage($"\n{blockTableRecord.Name}");
                }
            }
        }

        [CommandMethod("NumberOfInsertBtrsInModelSpace")]
        public static void NumberOfInsertBtrsInModelSpace()
        {
            // 1. Autocad aktif belgesine erişim  
            Document doc = Application.DocumentManager.MdiActiveDocument;
            // 2. Autocad aktif veritabanına erişim  
            Database db = doc.Database;
            // 3. İşlem yığının başlatılması
            using (Transaction tr = db.TransactionManager.StartTransaction())
            {
                // 4. Blok tablosunun okuma amaçlı açılması
                BlockTable blkTable = (BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead);
                // 5. ModelSpace blok tablosu kaydına okuma amaçlı erişim
                BlockTableRecord btRecord =
                    (BlockTableRecord)tr.GetObject(blkTable[BlockTableRecord.ModelSpace], OpenMode.ForRead);

                int objectCount = 0;
                // 6. Tek tek ModelSpace kayıtlarına erşim
                foreach (ObjectId objectId in btRecord)
                {
                    if(objectId.ObjectClass.DxfName == "INSERT")
                        objectCount++;
                }
                doc.Editor.WriteMessage($"\nÇizimdeki blok referans sayısı: {objectCount}");
                tr.Commit();
            }
        }
    }
}
