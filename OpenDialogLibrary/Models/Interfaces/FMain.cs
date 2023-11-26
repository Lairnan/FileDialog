using System.IO;
using MvvmBuilder.Notifies;
using OpenDialogLibrary.BehaviorsFiles;
using OpenDialogLibrary.Models.Implementation;

namespace OpenDialogLibrary.Models.Interfaces;

public abstract class FMain : NotifyBase
{
    protected FMain(string path)
    {
        this.Path = path;
        this.FType = (System.IO.File.GetAttributes(path)
                      & FileAttributes.Directory) != 0
            ? FMainType.Directory
            : FMainType.File;
        LoadAsync(path);
        Load(path);
    }

    private async void LoadAsync(string path)
    {
        this.Image = await IconConverter.GetFileIcon(path);
    }

    private void Load(string path)
    {
        if (string.IsNullOrWhiteSpace(path)) throw new ArgumentNullException(path, "Path can't be empty");
        FileSystemInfo info;
        
        if ((System.IO.File.GetAttributes(path) & FileAttributes.Directory) != 0) info = new DirectoryInfo(path);
        else info = new FileInfo(path);
        
        this.CreationTime = info.CreationTime;
        this.LastModifiedTime = info.LastWriteTime;
        this.LastAccessTime = info.LastAccessTime;
        this.Exists = info.Exists;
    }
        
    private FMain LoadMainDir()
    {
        var end = this.Path.LastIndexOf('\\', this.Path.Length - 1);
        return end < 4 ? null! : new Folder(this.Path[..end]);
    }
    
    public string Path
    {
        get => GetProperty<string>(string.Empty);
        protected set => SetProperty(value);
    }
    
    public string Name
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }
    
    public byte[]? Image
    {
        get => GetProperty<byte[]?>();
        protected set => SetProperty(value);
    }

    public DateTime CreationTime
    {
        get => GetProperty<DateTime>();
        private set => SetProperty(value, DateTime.Now);
    }

    public DateTime LastModifiedTime
    {
        get => GetProperty<DateTime>();
        protected set => SetProperty(value, DateTime.Now);
    }

    public DateTime LastAccessTime
    {
        get => GetProperty<DateTime>();
        protected set => SetProperty(value, DateTime.Now);
    }

    public bool Exists
    {
        get => GetProperty<bool>();
        protected set => SetProperty(value, false);
    }
    
    public FMainType FType
    {
        get => GetProperty<FMainType>();
        init => SetProperty(value);
    }

    private FMain? _mainDir;
    public FMain MainDir => _mainDir ??= LoadMainDir();

    public abstract void Create();
    public abstract void Delete();

    public abstract void Move(string dest);
    public abstract void Copy(string dest);
}

public enum FMainType
{
    File,
    Directory
}