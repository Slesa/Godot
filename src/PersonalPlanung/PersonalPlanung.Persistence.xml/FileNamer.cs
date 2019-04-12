using System.IO;

namespace PersonalPlanung.Persistence.xml
{
    public static class FileNamer
    {
        const string UsrDir = "usr";

        public static string GetFilenameFor(string listName)
        {
            var edd = new DirectoryInfo(UsrDir);
            if( !edd.Exists)
                edd.Create();
            var path = Path.Combine(UsrDir, listName);
            if (!path.EndsWith(".xml")) path += ".xml";
            return path;
        }
    }
}