using System.Diagnostics;

namespace MyNetApp.StackQueue;

public class StackQueueBenchmark(int itemCount)
{
    private Stack<int> _stack = new();
    private Queue<int> _queue = new();

    public void Run()
    {
        Console.WriteLine("In battle!!");
        Parallel.Invoke(
            () => MeasureIn(InStack, "Stack"),
            () => MeasureIn(EnQueue, "Queue")
        );

        Console.WriteLine("Out battle!!");
        Parallel.Invoke(
            () => MeasureOut(OutStack, "Stack"),
            () => MeasureOut(DeQueue, "Queue")
        );
    }

    void MeasureIn(In inOp, string colName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (int i in Enumerable.Range(0, itemCount))
        {
            inOp(i);
        }

        stopwatch.Stop();
        Console.WriteLine($"Time required for in operator to {colName}: {stopwatch.Elapsed.TotalMilliseconds}");
    }

    void MeasureOut(Out outOp, string colName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (int _ in Enumerable.Range(0, itemCount))
        {
            outOp();
        }

        stopwatch.Stop();
        Console.WriteLine($"Time required for out operator to {colName}: {stopwatch.Elapsed.TotalMilliseconds}");
    }

    delegate void In(int value);

    delegate void Out();

    void InStack(int value)
    {
        _stack.Push(value);
    }

    void EnQueue(int value)
    {
        _queue.Enqueue(value);
    }

    void OutStack()
    {
        _stack.Pop();
    }

    void DeQueue()
    {
        _queue.Dequeue();
    }
}