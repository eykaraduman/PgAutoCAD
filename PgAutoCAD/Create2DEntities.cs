using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.ApplicationServices.Core;
using Application = Autodesk.AutoCAD.ApplicationServices.Core.Application;


//using TransactionManager = Autodesk.AutoCAD.ApplicationServices.TransactionManager;

namespace PgAutoCAD
{
    public class Create2DEntities
    {
        public void Pontd3dUsage()
        {
            Vector3d vec = Vector3d.XAxis;
            Point3d pntA = new Point3d(1.0, 0.0, 0.0);
            Point3d pntB = new Point3d(4.0, 0.0, 0.0);
            Line line = new Line(pntA, pntB);
        }

        [CommandMethod("DrawLine1")]
        public void DrawLine1()
        {
            // Aktif doküman ve veritabanı erişim
            Document doc = Active.Document;
            Database db = Active.Database;

            // İşlem yığınının başlatılması
            using (Transaction tr = doc.TransactionManager.StartTransaction())
            {
                // Blok tablosunun okuma amaçlı ve
                // ModelSpace blok tablosu kaydının ise yazma amaçlı açılması 
                BlockTable bt = (BlockTable)db.BlockTableId.GetObject(OpenMode.ForRead);
                BlockTableRecord btr = (BlockTableRecord)doc.TransactionManager.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite);

                // Çizgi nesnesinin oluşturulması
                Point3d startPnt = new Point3d(1.0, 0.0, 0.0);
                Point3d endPnt = new Point3d(4.0, 0.0, 0.0);
                Line line= new Line(startPnt, endPnt);

                // Çizginin blok tablo kaydına eklenmesi
                //btr.UpgradeOpen();
                btr.AppendEntity(line);
                // Çizginin işlme yığınına eklenmesi
                tr.AddNewlyCreatedDBObject(line, true);
                // İşlem yığınının onaylanması
                tr.Commit();
            } // İşlem yığınının sonlandırılması
        }
    }
}

