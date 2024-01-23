using System.IO;
using System.IO.MemoryMappedFiles;

namespace MetricsVisualizer.Services.IO.SharedMemory;

public static class MemoryMappedFileHelper
{
    public static void WaitForMemoryMappedFile(string mapName, CancellationToken cancellationToken = default)
    {
        const int delayInMilliseconds = 500;

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                using (MemoryMappedFile.OpenExisting(mapName))
                    break;
            }
            catch (FileNotFoundException)
            {
                Thread.Sleep(delayInMilliseconds);
            }
        }
    }
}