using System.Diagnostics;

namespace MyNetApp.Lists;

public class AccessOperator
{
    private int _itemCount;
    private List<int> _list = new();
    private SortedList<int, int> _sortedList = new();
    private Dictionary<int, int> _dictionary = new();
    private LinkedList<int> _linkedList = new();

    public AccessOperator(int itemCount)
    {
        _itemCount = itemCount;
        foreach (var i in Enumerable.Range(0, itemCount))
        {
            _list.Add(i);
            _sortedList[i] = i;
            _dictionary[i] = i;
            _linkedList.AddLast(i);
        }
    }

    public void Run()
    {
        Console.WriteLine("Access battle!");
        Parallel.Invoke(
            () => MeasureRetrieveItem(RetrieveFromList, "List"),
            () => MeasureRetrieveItem(RetrieveFromSortedList, "SortedList"),
            () => MeasureRetrieveItem(RetrieveFromDictionary, "Dictionary"),
            () => MeasureRetrieveItem(RetrieveFromLinkedList, "LinkedList")
        );
    }

    void MeasureRetrieveItem(RetrieveItem @operator, string colName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (int i in Enumerable.Range(0, _itemCount))
        {
            @operator((_itemCount - i) / 2);
        }

        stopwatch.Stop();
        Console.WriteLine($"Time required for accessing items to {colName}: {stopwatch.Elapsed.TotalMilliseconds}");
    }

    delegate int RetrieveItem(int index);

    int RetrieveFromList(int index)
    {
        return _list[index];
    }

    int RetrieveFromSortedList(int key)
    {
        return _sortedList[key];
    }

    int RetrieveFromDictionary(int key)
    {
        return _dictionary[key];
    }

    int RetrieveFromLinkedList(int value)
    {
        var nodeToReturn = 0;
        foreach (var node in _linkedList)
        {
            if (node.Equals(value))
            {
                nodeToReturn = value;
                break;
            }
        }

        return nodeToReturn;
    }
}