using System.IO;
using OpenDialogLibrary.Models.Interfaces;

namespace OpenDialogLibrary.Models.Implementation;

public class File : FMain
{
    public File(string path) : base(path)
    {
        var start = path.LastIndexOf('\\', path.Length - 1) + 1;
        var end = path.LastIndexOf('.', path.Length - 1);
        this.Name = path.Substring(start, end - start);
        this.Extension = path.Substring(end + 1, path.Length - end - 1);
        this.Size = (ulong)new FileInfo(path).Length;
    }
    
    public File(FileSystemInfo fi) : base(fi.FullName)
    {
        this.Name = fi.Name;
        this.Extension = fi.Extension;
        this.Size = (ulong)((FileInfo)fi).Length;
    }

    public string Extension
    {
        get => GetProperty<string>();
        set => SetProperty(value);
    }

    public ulong Size
    {
        get => GetProperty<ulong>();
        protected set => SetProperty(value, ulong.MinValue);
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
}