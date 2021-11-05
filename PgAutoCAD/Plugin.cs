using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Autodesk.AutoCAD.Runtime;

[assembly: ExtensionApplication(typeof(PgAutoCAD.Plugin))]

namespace PgAutoCAD
{
    public class Plugin: IExtensionApplication
    {
        public void Initialize()
        {
            
        }

        public void Terminate()
        {
            
        }
    }
}
