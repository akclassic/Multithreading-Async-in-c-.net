ManualResetEventSlim manualResetEvent = new ManualResetEventSlim(false);

//manualResetEvent.Set();

//manualResetEvent.Reset();
Console.WriteLine("Press Enter to release all threads");

//Worker THreads
for(int i = 0; i < 3; i++)
{
    Thread thread = new Thread(Work);
    thread.Name = $"Worker Thread {i + 1}";
    thread.Start();
}

Console.ReadLine();

manualResetEvent.Set();

Console.ReadLine();

void Work()
{
    Console.WriteLine($"{Thread.CurrentThread.Name} is waiting for the signal");
    manualResetEvent.Wait();

    Thread.Sleep(2000); // Simulate work
    Console.WriteLine($"{Thread.CurrentThread.Name} has received the signal and released");
}

