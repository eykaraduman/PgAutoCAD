using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;

namespace PgAutoCAD
{
    public class Transactions
    {
        [CommandMethod("TransactionWithoutUsing")]
        public void TransactionSampleWithoutUsingKeyword()
        {
            // 1. Autocad aktif belgesine erişim  
            Document doc = Application.DocumentManager.MdiActiveDocument;
            // 2. Aktif belgeye ait işlem yığın yöneticisine erişim  
            Autodesk.AutoCAD.DatabaseServices.TransactionManager transactionManager =
                Application.DocumentManager.MdiActiveDocument.TransactionManager;
            // 3. İşlem yığının başlatılması  
            Transaction transaction = transactionManager.StartTransaction();
            // 4. Belge veritabanının/blok tablosunun okuma amaçlı açılması  
            BlockTable blockTable = (BlockTable)doc.Database.BlockTableId.GetObject(OpenMode.ForRead);
            // 5. Model uzayının (modelspace) yazma amaçlı açılması  
            BlockTableRecord blockTableRecord =
                (BlockTableRecord)transactionManager.
                    GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);
            // 6. Autocad daire nesnesinin oluşturulması  
            Circle circle = new Circle();
            circle.Center = new Point3d(0.0, 0.0, 0.0);
            circle.Radius = 10.0;
            // 7. Model uzayı blok tablo kaydına daire nesnesinin eklenmesi  
            ObjectId circleId = blockTableRecord.AppendEntity(circle);
            // 8. Daire nesnesinin işlem yığınına eklenmesi  
            transaction.AddNewlyCreatedDBObject(circle, true);
            // 9. İşlem yığınının onaylanması  
            transaction.Commit();
            // 10. Sırasıyla işlem yığını ve işlem yığın yöneticisinin sonlandırılması  
            transaction.Dispose();
            transactionManager.Dispose();
        }

        [CommandMethod("TransactionWithUsing")]
        public void TransactionSampleWithUsingKeyword()
        {
            // 1. Autocad aktif belgesine erişim  
            Document doc = Application.DocumentManager.MdiActiveDocument;
            // 2. İşlem yığının başlatılması  
            Transaction transaction =
                Application.DocumentManager.MdiActiveDocument.TransactionManager.StartTransaction();
            using (transaction)
            {
                // 3. Belge veritabanının/blok tablosunun okuma amaçlı açılması 
                BlockTable blockTable = (BlockTable)doc.Database.BlockTableId.GetObject(OpenMode.ForRead);
                // 4. Model uzayının (modelspace) yazma amaçlı açılması  
                BlockTableRecord blockTableRecord =
                    (BlockTableRecord)Application.DocumentManager.MdiActiveDocument.
                        TransactionManager.GetObject(blockTable[BlockTableRecord.ModelSpace],
                            OpenMode.ForWrite);
                // 5. Autocad daire nesnesinin oluşturulması  
                Circle circle = new Circle();
                circle.Center = new Point3d(0.0, 0.0, 0.0);
                circle.Radius = 10.0;
                // 8. Model uzayı blok tablo kaydına daire nesnesinin eklenmesi  
                ObjectId circleId = blockTableRecord.AppendEntity(circle);
                // 9. Daire nesnesinin işlem yığınına eklenmesi  
                transaction.AddNewlyCreatedDBObject(circle, true);
                // 10. İşlem yığınının onaylanması  
                transaction.Commit();
            }
        }

        public void CreateLine(Point3d startPoint, Point3d endPoint)
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            using (Transaction transaction = doc.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)doc.Database.BlockTableId.GetObject(OpenMode.ForRead);
                BlockTableRecord blockTableRecord =
                    (BlockTableRecord)Application.DocumentManager.MdiActiveDocument.
                        TransactionManager.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                Line ln = new Line(startPoint, endPoint);
                ln.SetDatabaseDefaults();
                blockTableRecord.AppendEntity(ln);
                transaction.AddNewlyCreatedDBObject(ln, true);
                transaction.Commit();
            }
        }

        [CommandMethod("CreatLines1")]
        public void CreateLines1()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            DateTime starTime = DateTime.Now;
            for (int i = 0; i < 5000; i++)
            {
                CreateLine(new Point3d(0.0, i * 100, 0.0), new Point3d(100.0, i * 100, 0.0));
            }

            TimeSpan elapsedTime = DateTime.Now.Subtract(starTime);
            doc.Editor.WriteMessage($"\nCreateLines1() yordamının çalışmasında geçen zaman: " +
                                    $"{elapsedTime.TotalMilliseconds}");
        }

        [CommandMethod("CreatLines2")]
        public void CreateLines2()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;

            DateTime starTime = DateTime.Now;
            using (Transaction transaction = doc.TransactionManager.StartTransaction())
            {
                BlockTable blockTable = (BlockTable)doc.Database.BlockTableId.GetObject(OpenMode.ForRead);
                BlockTableRecord blockTableRecord =
                    (BlockTableRecord)Application.DocumentManager.MdiActiveDocument.
                        TransactionManager.GetObject(blockTable[BlockTableRecord.ModelSpace],  OpenMode.ForWrite);

                for (int i = 0; i < 5000; i++)
                {
                    Line ln = new Line(new Point3d(0.0, i * 100, 0.0), new Point3d(100.0, i * 100, 0.0));
                    ln.SetDatabaseDefaults();
                    blockTableRecord.AppendEntity(ln);
                    transaction.AddNewlyCreatedDBObject(ln, true);
                }
                transaction.Commit();
            }

            TimeSpan elapsedTime = DateTime.Now.Subtract(starTime);
            doc.Editor.WriteMessage($"\nCreateLines2() yordamının çalışmasında geçen zaman: " +
                                    $"{elapsedTime.TotalMilliseconds}");
        }

        public void CreateLine(Document doc, Transaction transaction, Point3d startPoint, Point3d endPoint)
        {
            BlockTable blockTable = (BlockTable)doc.Database.BlockTableId.GetObject(OpenMode.ForRead);
            BlockTableRecord blockTableRecord =
                (BlockTableRecord)doc.TransactionManager.GetObject(blockTable[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

            Line ln = new Line(startPoint, endPoint);
            ln.SetDatabaseDefaults();
            blockTableRecord.AppendEntity(ln);
            transaction.AddNewlyCreatedDBObject(ln, true);
        }

        [CommandMethod("CreatLines3")]
        public void CreateLines3()
        {
            Document doc = Application.DocumentManager.MdiActiveDocument;
            using (Transaction transaction = doc.TransactionManager.StartTransaction())
            {
                for (int i = 0; i < 5000; i++)
                {
                    CreateLine(doc, transaction, 
                        new Point3d(0.0, i * 100, 0.0), new Point3d(100.0, i * 100, 0.0));
                }
                transaction.Commit();
            }
        }
    }
}
