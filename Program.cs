using AutoResetEvent autoResetEvent = new AutoResetEvent(false); // use false for non-signaled initial state

string? userInput = null;

Console.WriteLine("Server is running. Type 'go' to proceed");

//Start the worker thread
for(int i = 0; i < 3; i++)
{
    Thread workerThread = new Thread(Worker);
    workerThread.Name = $"WorkerThread-{i+1}";
    workerThread.Start();
}

// Main thread receives user input and sends signals.
while (true)
{
    userInput = Console.ReadLine() ?? "";

    // Signal the worker thread to proceed if the input is "go"
    if (userInput?.ToLower() == "go")
    {
        autoResetEvent.Set(); // Signal the worker thread
    }
    else
    {
        Console.WriteLine("Type 'go' to signal the worker thread.");
    }
}

void Worker()
{
    while (true)
    {
        Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the signal");
        autoResetEvent.WaitOne(); // Wait for the signal from the main thread

        Console.WriteLine($"{Thread.CurrentThread.Name} received the signal and is proceeding");

        Thread.Sleep(1000); // Simulate work by sleeping for 1 second
    }
}