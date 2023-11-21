using System.IO;
using OpenDialogLibrary.BehaviorsFiles;
using OpenDialogLibrary.Models.Interfaces;

namespace OpenDialogLibrary.Models.Implementation;

public class Folder : FMain
{
    public Folder(string path) : base(path)
    {
        Load(path);
    }

    private async void Load(string path)
    {
        this.FilesInFolders = (uint)new DirectoryInfo(path).GetFiles().Length;
        this.Image = await IconConverter.GetFromResource("/Images/folder.png");
        var start = path.LastIndexOf('\\', path.Length - 1) + 1;
        this.Name = path.Substring(start, path.Length - start);
    }

    public uint FilesInFolders
    {
        get => GetProperty<uint>();
        set => SetProperty(value, uint.MinValue);
    }
    
    public override void Create()
    {
    }

    public override void Delete()
    {
    }

    public override void Move(string dest)
    {
    }

    public override void Copy(string dest)
    {
    }

    private static IEnumerable<File> GetFiles(IEnumerable<string> files)
    {
        return files.Select(filePath => new File(filePath));
    }

    public IEnumerable<File> GetFiles()
    {
        var files = Directory.GetFiles(this.Path);
        return GetFiles(files);
    }

    public IEnumerable<File> GetFiles(Predicate<string> filter)
    {
        var files = Directory.GetFiles(this.Path).ToList();
        
        foreach (var filePath in files)
        {
            var start = filePath.LastIndexOf('\\', filePath.Length) + 1;
            var fileName = filePath.Substring(start, filePath.Length - start);
            if (!filter(fileName)) files.Remove(filePath);
        }

        return GetFiles(files);
    }

    private static IEnumerable<Folder> GetDirectories(IEnumerable<string> folders)
    {
        return folders.Select(folderPath => new Folder(folderPath));
    }

    public IEnumerable<Folder> GetDirectories()
    {
        var folders = Directory.GetDirectories(this.Path);
        return GetDirectories(folders);
    }

    public IEnumerable<Folder> GetDirectories(Predicate<string> filter)
    {
        var folders = Directory.GetDirectories(this.Path).ToList();
        
        foreach (var folderPath in folders)
        {
            var start = folderPath.LastIndexOf('\\', folderPath.Length) + 1;
            var folderName = folderPath.Substring(start, folderPath.Length - start);
            if (!filter(folderName)) folders.Remove(folderPath);
        }

        return GetDirectories(folders);
    }
}