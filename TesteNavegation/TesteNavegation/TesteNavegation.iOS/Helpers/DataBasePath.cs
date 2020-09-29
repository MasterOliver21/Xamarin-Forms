using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using TesteNavegation.Interface;
using TesteNavegation.iOS.Helpers;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(DataBasePath))]
namespace TesteNavegation.iOS.Helpers
{
    public class DataBasePath : IDbPath
    {
        public string GetDbPath()
        {
            string filename = "TesteNav.db3";
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
                Directory.CreateDirectory(libFolder);

            return Path.Combine(libFolder, filename);
        }
    }
    
}