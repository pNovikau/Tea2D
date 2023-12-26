using System.IO.MemoryMappedFiles;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;

namespace Tea2D.Metrics.IO.SharedMemory;

public class PipeListener
{
    private readonly string _name;

    private readonly CancellationTokenSource _cancellationTokenSource;
    private readonly Task _workerTask;

    public PipeListener(ReadOnlySpan<char> name)
    {
        _name = name.ToString();

        _cancellationTokenSource = new CancellationTokenSource();
        _workerTask = new Task(Listen, _cancellationTokenSource.Token, TaskCreationOptions.LongRunning);
    }

    public void Start()
    {
        
    }

    public async Task Listen()
    {
        
    }
}

public class MemoryMappedFileUtils
{
    private const int ERROR_FILE_NOT_FOUND = 2;
    
    [SupportedOSPlatform("windows")]
    public static void WaitRead(string mapName)
    {
        while (true)
        {
            var handle = Interop.OpenFileMapping((int)MemoryMappedFileRights.Read, false, mapName);
        
            var lastError = Marshal.GetLastPInvokeError();

            if (handle.IsInvalid is false)
            {
                handle.Dispose();

                return;
            }

            handle.Dispose();

            if (lastError != ERROR_FILE_NOT_FOUND)
                throw new Exception();

            Thread.Sleep(1000);
        }
    }
}