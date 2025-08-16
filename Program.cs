// A mutex is used to ensure that if we have a shared resource between multiple processes/instances
// of the same application, we can prevent race conditions using mutex.

string filePath = "shared_resource.txt";    

using (var mutex = new Mutex(false, $"GlobalFileMutex:{filePath}"))
{
    for(int i = 0; i < 10000; i++)
    {
        mutex.WaitOne(); // Wait until it is safe to enter
        try
        {
            int counter = ReadCounter(filePath);
            counter++;
            WriteCounter(filePath, counter);
        }
        finally
        {
            mutex.ReleaseMutex(); // Release the mutex so other processes can enter
        }
    }
}

int ReadCounter(string path)
{
    using (var stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Read, FileShare.ReadWrite))
    using (var reader = new StreamReader(stream))
    {
        string content = reader.ReadToEnd();
        return int.TryParse(content, out int counter) ? counter : 0;
    }
}

void WriteCounter(string path, int counter)
{
    using (var stream = new FileStream(path, FileMode.Create, FileAccess.Write, FileShare.None))
    using (var writer = new StreamWriter(stream))
    {
        writer.Write(counter);
    }
}