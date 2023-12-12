using OpenDialogLibrary.Models.Implementation;
using OpenDialogLibrary.Views;

namespace OpenDialogLibrary;

public partial class OpenDialog
{
    private static OpenDialog? instance;
    internal static OpenDialog? Instance
    {
        get => instance ?? null;
        private set => instance = value;
    }

    public DialogType DialogType { get; private set; }

    public bool MultiSelect { get; set; }
    public string FileName { get; internal set; } = string.Empty;
    public string[]? FileNames { get; internal set; } = null;
    public string StartDirectory { get; set; } = System.IO.Directory.GetCurrentDirectory();
    public string Filter { get; set; } = string.Empty;
    
    /*
     * Ниже приведен пример строки фильтра:
     * Text files (*.txt)|*.txt|All files (*.*)|*.*
     * Можно добавить несколько шаблонов фильтров в фильтр, разделив типы файлов точкой с запятой, например:
     * Image Files(*.BMP;*.JPG;*.GIF)|*.BMP;*.JPG;*.GIF|All files (*.*)|*.*
     *
     *
     * "All Files (*.*)|*.*" +
    "|All Pictures (*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico)" +
        "|*.emf;*.wmf;*.jpg;*.jpeg;*.jfif;*.jpe;*.png;*.bmp;*.dib;*.rle;*.gif;*.emz;*.wmz;*.tif;*.tiff;*.svg;*.ico" +
    "|Windows Enhanced Metafile (*.emf)|*.emf" +
    "|Windows Metafile (*.wmf)|*.wmf" +
    "|JPEG File Interchange Format (*.jpg;*.jpeg;*.jfif;*.jpe)|*.jpg;*.jpeg;*.jfif;*.jpe" +
    "|Portable Network Graphics (*.png)|*.png" +
    "|Bitmap Image File (*.bmp;*.dib;*.rle)|*.bmp;*.dib;*.rle" +
    "|Compressed Windows Enhanced Metafile (*.emz)|*.emz" +
    "|Compressed Windows MetaFile (*.wmz)|*.wmz" +
    "|Tag Image File Format (*.tif;*.tiff)|*.tif;*.tiff" +
    "|Scalable Vector Graphics (*.svg)|*.svg" +
    "|Icon (*.ico)|*.ico";
     */

    public OpenDialog(DialogType dialogType, bool multiSelect = false)
    {
        this.DialogType = dialogType;
        this.MultiSelect = multiSelect;
        Instance = this;
    }

    public OpenDialog(DialogType dialogType, string startDirectory, bool multiSelect) : this(dialogType, multiSelect)
    {
        this.StartDirectory = startDirectory;
    }

    public OpenDialog(DialogType dialogType, string filter, string startDirectory, bool multiSelect) : this(dialogType, multiSelect)
    {
        this.StartDirectory = startDirectory;
        this.Filter = filter;
    }

    public bool ShowDialog()
    {
        if (Instance == null) return false;
        IoC.Initialize();
        var mainWindow = new MainWindow();
        var res = mainWindow.ShowDialog();
        Instance = null;
        IoC.Deinitialize();
        return res ?? false;
    }

    public bool ShowDialog(string startDirectory)
    {
        if (string.IsNullOrWhiteSpace(startDirectory)) return false;
        if (!System.IO.Path.Exists(startDirectory)) return false;

        this.StartDirectory = startDirectory;
        return ShowDialog();
    }
}

public enum DialogType
{
    SaveFile,
    OpenFile,
    OpenDirectory
}