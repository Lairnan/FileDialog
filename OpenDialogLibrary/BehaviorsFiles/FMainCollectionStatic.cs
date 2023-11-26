using System.Collections.ObjectModel;
using System.IO;
using OpenDialogLibrary.Models.Implementation;
using OpenDialogLibrary.Models.Interfaces;
using File = OpenDialogLibrary.Models.Implementation.File;

namespace OpenDialogLibrary.BehaviorsFiles;

public static class FMainCollectionStatic
{
    public static void GetDefaultFMains(this ObservableCollection<FMain> collection, string path)
    {
        foreach (var fileSystemPath in Directory.GetFileSystemEntries(path))
        {
            if((System.IO.File.GetAttributes(fileSystemPath) & FileAttributes.Directory) != 0)
                collection.Add(new Folder(fileSystemPath));
            else collection.Add(new File(fileSystemPath));
        }
    }
}