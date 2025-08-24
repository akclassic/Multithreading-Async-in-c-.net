Queue<string?> requestsQueue = new Queue<string?>();

using SemaphoreSlim semaphore = new SemaphoreSlim(3,3);

//2. Start the requests queue monitoring thread
Thread monitoringThread = new Thread(MonitorQueue);
monitoringThread.Start();   

//1. Enqueue the requests
Console.WriteLine("Server is running. Type 'exit' to stop.");

while (true)
{
   string? input = Console.ReadLine();  
    if(input?.ToLower() == "exit")
    {
        break;
    }

    requestsQueue.Enqueue(input);
}

void MonitorQueue()
{
    while (true)
    {
        if(requestsQueue.Count > 0)
        {
            string? input = requestsQueue.Dequeue();
            semaphore.Wait();
            Thread processeingThread = new Thread(() => ProcessInput(input));
            processeingThread.Start();
            Thread.Sleep(100);
        }
    }
}

void ProcessInput(string? input)
{
    try
    {
        Thread.Sleep(1000);
        Console.WriteLine($"Processed input: {input}");
    }
    finally
    {
        var prevCount = semaphore.Release();
    }
}