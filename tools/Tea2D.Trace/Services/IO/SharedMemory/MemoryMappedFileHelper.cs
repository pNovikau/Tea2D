using System.IO;
using System.IO.MemoryMappedFiles;

namespace Tea2D.Trace.Services.IO.SharedMemory;

public static class MemoryMappedFileHelper
{
    public static bool WaitForMemoryMappedFile(string mapName, CancellationToken cancellationToken = default)
    {
        const int delayInMilliseconds = 500;

        while (cancellationToken.IsCancellationRequested is false)
        {
            try
            {
                using (MemoryMappedFile.OpenExisting(mapName))
                    return true;
            }
            catch (FileNotFoundException)
            {
                Thread.Sleep(delayInMilliseconds);
            }
        }

        return false;
    }
}