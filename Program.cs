//Airplane seat booking system

List<int> availableSeats = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
List<int> bookedSeats = new List<int>();
int bookingRequests = 0;
Queue<int> cancellationRequests = new Queue<int>();

Console.WriteLine("Welcome to the Airplane Seat Booking System!");

object lockObject = new object();

Thread monitoringThread = new Thread(MonitoringRequests);
monitoringThread.Start();

while (true)
{
    Console.WriteLine("Press 'b' to book a seat or 'c' to cancel a booking. Type 'exit' to quit.");
    string? input = Console.ReadLine();
    if (input?.ToLower() == "b")
    {
        bookingRequests+= 1;
    }
    else if (input?.ToLower() == "c")
    { 
        Console.WriteLine("Enter seat number to cancel booking:");
        int seatNumber = int.Parse(Console.ReadLine() ?? "0");
        cancellationRequests.Enqueue(seatNumber);
    }
    else if (input?.ToLower() == "exit")
    {
        Console.WriteLine("Exiting the booking system. Goodbye!");
        break;
    }
    else
    {
        Console.WriteLine("Invalid input, please try again.");
    }
}

void MonitoringRequests()
{
    while(true)
    {
        if (bookingRequests > 0)
        {
            Thread processBookingRequestThread = new Thread(ProcessBookingOrCancellationRequest);
            processBookingRequestThread.Start();
            Thread.Sleep(1000); // Simulate processing time
        }

        if (cancellationRequests.Count > 0)
        {
            Thread cancellationRequestThread = new Thread(ProcessBookingOrCancellationRequest);
            cancellationRequestThread.Start();
            Thread.Sleep(1000); // Simulate processing time
        }
    }
}

void ProcessBookingOrCancellationRequest()
{
    lock (lockObject)
    {
        if (bookingRequests > 0)
        {
            if (availableSeats.Count > 0)
            {
                int seatNumber = availableSeats[0];
                availableSeats.RemoveAt(0);
                bookedSeats.Add(seatNumber);
                bookingRequests -= 1;
                Console.WriteLine($"Seat {seatNumber} has been booked successfully.");
                Thread.Sleep(1000); // Simulate processing time
            }
            else
            {
                Console.WriteLine("No seats available for booking.");
            }
        }

        if (cancellationRequests.Count > 0)
        {
            int seatNumberToCancel = cancellationRequests.Dequeue();
            if (bookedSeats.Contains(seatNumberToCancel))
            {
                bookedSeats.Remove(seatNumberToCancel);
                availableSeats.Add(seatNumberToCancel);
                Console.WriteLine($"Booking for seat {seatNumberToCancel} has been cancelled successfully.");
                Thread.Sleep(1000); // Simulate processing time
            }
            else
            {
                Console.WriteLine($"Seat {seatNumberToCancel} was not booked, cannot cancel.");
            }
        }
    }
}

