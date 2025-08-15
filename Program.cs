Queue<string?> requestsQueue = new Queue<string?>();

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
            Thread processeingThread = new Thread(() => ProcessInput(input));
            processeingThread.Start();
            Thread.Sleep(100);
        }
    }
}

static void ProcessInput(string? input)
{
    Thread.Sleep(1000);
    Console.WriteLine($"Processed input: {input}");
}