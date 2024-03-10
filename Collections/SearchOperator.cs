using System.Diagnostics;

namespace MyNetApp.Lists;

public class SearchOperator
{
    private int _itemCount;
    private List<int> _list = new();
    private SortedList<int, int> _sortedList = new();
    private Dictionary<int, int> _dictionary = new();

    public SearchOperator(int itemCount)
    {
        _itemCount = itemCount;
        foreach (var i in Enumerable.Range(0, itemCount))
        {
            _list.Add(i);
            _sortedList[i] = i;
            _dictionary[i] = i;
        }
    }

    public void Run()
    {
        Console.WriteLine("Search battle!");
        Parallel.Invoke(
            () => MeasureContainItem(ListContainItem, "List"),
            () => MeasureContainItem(SortedListContainItem, "SortedList"),
            () => MeasureContainItem(DictionaryContainItem, "Dictionary")
        );
    }

    void MeasureContainItem(ContainItem @operator, string colName)
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();
        foreach (var i in Enumerable.Range(_itemCount / 2, _itemCount))
        {
            @operator(i);
        }

        stopwatch.Stop();
        Console.WriteLine($"Time required for adding items to {colName}: {stopwatch.Elapsed.TotalMilliseconds}");
    }

    delegate bool ContainItem(int value);

    bool ListContainItem(int value)
    {
        return _list.Contains(value);
    }

    bool SortedListContainItem(int value)
    {
        return _sortedList.ContainsKey(value);
    }

    bool DictionaryContainItem(int value)
    {
        return _dictionary.ContainsValue(value);
    }
}