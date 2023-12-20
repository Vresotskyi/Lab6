using System;
using System.IO;

public delegate void FileMonitorDelegate(string filePath);

public class FileMonitor
{
    public event FileMonitorDelegate OnFileCreated;
    public event FileMonitorDelegate OnFileDeleted;
    public event FileMonitorDelegate OnFileModified;
    public event FileMonitorDelegate OnFileRenamed;

    private FileSystemWatcher watcher;

    public FileMonitor(string path)
    {
        watcher = new FileSystemWatcher();
        watcher.Path = path;

        // Включення відслідковування різних подій файлів
        watcher.Created += (sender, e) => OnFileCreated?.Invoke(e.FullPath);
        watcher.Deleted += (sender, e) => OnFileDeleted?.Invoke(e.FullPath);
        watcher.Changed += (sender, e) => OnFileModified?.Invoke(e.FullPath);
        watcher.Renamed += (sender, e) => OnFileRenamed?.Invoke(e.FullPath);

        watcher.EnableRaisingEvents = true;
    }
}
class Program
{
    static void Main(string[] args)
    {
        string directoryPath = "D:\\Lab6"; 

        FileMonitor fileMonitor = new FileMonitor(directoryPath);

        // Додавання обробників подій через лямбда-вирази
        fileMonitor.OnFileCreated += (filePath) => Console.WriteLine($"Створено файл {filePath}");
        fileMonitor.OnFileDeleted += (filePath) => Console.WriteLine($"Видалено файл {filePath}");
        fileMonitor.OnFileModified += (filePath) => Console.WriteLine($"Змінено файл {filePath}");
        fileMonitor.OnFileRenamed += (filePath) => Console.WriteLine($"Перейменовано файл {filePath}");

   

        Console.ReadLine(); // Щоб консоль не закривалаь одразу
    }
}
