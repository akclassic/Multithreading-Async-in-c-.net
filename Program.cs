int counter = 0;

object counterLock = new object();

Thread thread1 = new Thread(IncrementCounter);
Thread thread2 = new Thread(IncrementCounter);

thread1.Start();
thread2.Start();

thread1.Join();
thread2.Join();

Console.WriteLine($"Final value of the Counter: {counter}");

void IncrementCounter()
{
    for (int i = 0; i < 1000; i++)
    {
        lock (counterLock) // A lock is used to ensure that only one thread can access the counter at a time
        {
            counter++;
        }
    }
}   