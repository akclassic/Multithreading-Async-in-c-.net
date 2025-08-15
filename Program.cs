int[] arr = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

int SumSegment(int start, int end)
{
    int segmentSum = 0;
    for (int i = start; i < end; i++)
    {
        Thread.Sleep(100);
        segmentSum += arr[i];
    }

    return segmentSum;
}

int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;

var startTime = DateTime.Now;

int numOfThreads = 4;
int segmentLength = arr.Length / 4;

Thread[] threads = new Thread[numOfThreads];
threads[0] = new Thread(() => { sum1 = SumSegment(0, segmentLength); });
threads[1] = new Thread(() => { sum1 = SumSegment(segmentLength, 2 * segmentLength); });
threads[2] = new Thread(() => { sum1 = SumSegment(2 * segmentLength, 3 * segmentLength); });
threads[3] = new Thread(() => { sum1 = SumSegment(3 * segmentLength, arr.Length); });

foreach (var thread in threads) { thread.Start(); }

// when threads perform a calculation that produces a result, Join() can be useful to wait
// for all worker threads to complete therir calculations before processing or aggregating the results
// in the main thread
foreach (var thread in threads) { thread.Join(); }

var endTime = DateTime.Now;

var timeSpan = endTime - startTime;

Console.WriteLine($"The sum is {sum1 + sum2 + sum3 + sum4}");
Console.WriteLine($"The time it takes: {timeSpan.TotalMilliseconds}");

Console.ReadLine();