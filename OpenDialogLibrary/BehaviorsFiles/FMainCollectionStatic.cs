using System.Collections.ObjectModel;
using OpenDialogLibrary.Models.Implementation;
using OpenDialogLibrary.Models.Interfaces;

namespace OpenDialogLibrary.BehaviorsFiles;

public static class FMainCollectionStatic
{
    public static async void AddFMains(this ObservableCollection<FMain> collection, string path)
    {
        await foreach (var folder in GetFolders(path))
            collection.Add(folder);

        await foreach (var file in GetFiles(path))
            collection.Add(file);

    }

    private static async IAsyncEnumerable<Folder> GetFolders(string path)
    {
        var dirs = System.IO.Directory.GetDirectories(path);
        foreach (var dir in dirs)
            yield return await Task.Run(() => new Folder(dir));
    }

    private static async IAsyncEnumerable<File> GetFiles(string path)
    {
        var files = System.IO.Directory.GetFiles(path);
        foreach (var file in files)
            yield return await Task.Run(() => new File(file));
    }
}