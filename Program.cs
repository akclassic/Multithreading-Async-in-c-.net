void WriteThreadName()
{
    for(int i = 0; i < 100; i++)
    {
        Console.WriteLine(Thread.CurrentThread.Name);
    }
}

Thread thread1 = new Thread(WriteThreadName);
Thread thread2 = new Thread(WriteThreadName);

thread1.Name = "Thread 1";
thread2.Name = "Thread 2";
Thread.CurrentThread.Name = "Main Thread";

// Setting priority
thread1.Priority = ThreadPriority.Highest;
thread2.Priority = ThreadPriority.Normal;
Thread.CurrentThread.Priority = ThreadPriority.Lowest;

thread1.Start();
thread2.Start();
WriteThreadName(); // executes on the main thread

Console.ReadLine();