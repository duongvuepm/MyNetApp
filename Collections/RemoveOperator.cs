using System.Diagnostics;

namespace MyNetApp.Lists
{
    public class RemoveOperator
    {
        private int _itemCount;
        private List<int> _list = new();
        private SortedList<int, int> _sortedList = new();
        private Dictionary<int, int> _dictionary = new();

        public RemoveOperator(int itemCount)
        {
            _itemCount = itemCount;
            foreach (var i in Enumerable.Range(1, _itemCount))
            {
                _sortedList[i] = i;
                _dictionary[i] = i;
                _list.Add(i);
            }
        }

        public void Run()
        {
            Console.WriteLine("Remove battle!");

            Parallel.Invoke(
                () => MeasureRetrieveItem(ListRemoveItem, "List"),
                () => MeasureRetrieveItem(SortedListRemoveItem, "SortedList"),
                () => MeasureRetrieveItem(DictionaryRemoveItem, "Dictionary")
            );
        }

        void MeasureRetrieveItem(RemoveItem @operator, string colName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            foreach (int i in Enumerable.Range(0, _itemCount))
            {
                @operator((_itemCount - i) / 2);
            }

            stopwatch.Stop();
            Console.WriteLine($"Time required for removing items to {colName}: {stopwatch.Elapsed.TotalMilliseconds}");
        }

        delegate void RemoveItem(int index);

        void ListRemoveItem(int index)
        {
            _list.RemoveAt(index);
        }

        void SortedListRemoveItem(int index)
        {
            _sortedList.RemoveAt(index);
        }

        void DictionaryRemoveItem(int item)
        {
            _dictionary.Remove(item);
        }
    }
}