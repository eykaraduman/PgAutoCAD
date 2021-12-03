using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;

namespace PgAutoCAD
{
    /// <summary>
    /// AutoCAD çalışma ortamı aktif nesnelerine kolay erişim sağlar.
    /// </summary>
    public static class Active
    {
        /// <summary>
        /// Aktif Document nesnesini döndürür.
        /// </summary>
        public static Document Document => Application.DocumentManager.MdiActiveDocument;

        /// <summary>
        ///  Aktif Editor nesnesini döndürür.
        /// </summary>
        public static Editor Editor => Document.Editor;


        /// <summary>
        ///  Aktif Database nesnesini döndürür.
        /// </summary>
        public static Database Database => Document.Database;

        /// <summary>
        /// Aktif editörü kullanarak komut satırına mesaj yazdırır.
        /// </summary>
        /// <param name="message">Yazdırılacak mesaj.</param>
        public static void WriteMessage(string message)
        {
            Editor.WriteMessage(message);
        }

        /// <summary>
        /// Aktif editörü kullanarak komut satırına
        /// String.Format biçimleyicisiyle mesaj yazdırır.
        /// </summary>
        /// <param name="message">Biçimlendiricileri içeren mesaj.</param>
        /// <param name="parameter">Biçimlendirme dizesinde değiştirilecek değişkenler.</param>
        public static void WriteMessage(string message, params object[] parameter)
        {
            Editor.WriteMessage(message, parameter);
        }
    }
}
