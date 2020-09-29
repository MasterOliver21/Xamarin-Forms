using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using TesteNavegation.Droid.Helpers;
using TesteNavegation.Interface;

[assembly: Xamarin.Forms.Dependency(typeof(DataBasePath))]
namespace TesteNavegation.Droid.Helpers
{
    public class DataBasePath : IDbPath
    {
        public string GetDbPath()
        {
            return Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "TesteNav.db3");
        }
    }
}