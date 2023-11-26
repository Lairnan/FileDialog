using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace OpenDialogLibrary.BehaviorsFiles;

internal static class IconConverter
{
    internal static async Task<byte[]> GetFileIcon(string path)
    {
        try
        {
            var fileIcon = Icon.ExtractAssociatedIcon(path);
            return fileIcon == null
                ? await GetDefaultImage()
                : await IconToBytes(fileIcon);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Ошибка: {ex.Message}");
            return await GetDefaultImage();
        }
    }

    private static async Task<byte[]> IconToBytes(Icon icon)
    {
        using var ms = new MemoryStream();

        try { icon.Save(ms); }
        finally { ms.Close(); await ms.DisposeAsync(); }
        return ms.ToArray();
    }

    private static async Task<byte[]> GetDefaultImage()
    {
        var data = await GetFromResource("Images/404.jpg");
        return data;
    }

    private static async Task<byte[]> GetFromPathUri(string path)
    {
        var uri = new Uri(path);
        var bitmapImage = new BitmapImage(uri);

        var encoder = new JpegBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
        using var ms = new MemoryStream();
        encoder.Save(ms);
        var data = ms.ToArray();
        await ms.DisposeAsync();
        
        return data;
    }

    internal static async Task<byte[]> GetFromResource(string path, string assembly = "")
    {
        if(string.IsNullOrWhiteSpace(assembly)) assembly = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name!;
        path = $"pack://application:,,,/{assembly};component/{path}";
        return await GetFromPathUri(path);
    }
}