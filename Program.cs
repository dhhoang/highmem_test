using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

class Program
{
    private static readonly int ProcId = Process.GetCurrentProcess().Id;

    public static async Task<int> Main(string[] args)
    {
        if (args.Length != 2)
        {
            Console.Error.WriteLine("Usage: ./highmem_test <file> <size>");
            return 1;
        }

        using var fileHandle = File.OpenHandle(args[0], FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
        var nread = 0;
        var total = 0;
        var toRead = long.Parse(args[1]);

        var buf = new byte[1024];
        while (total < toRead && (nread = await RandomAccess.ReadAsync(fileHandle, buf, 0)) > 0)
        {
            total += nread;
        }

        using var p = Process.Start("/bin/sh", $"cat /proc/ProcId/status | grep RssAnon");
        p.Start();
        string stdoutContent = p.StandardOutput.ReadToEnd();
        Console.WriteLine(stdoutContent);
        Console.ReadLine();
        return 0;
    }
}
