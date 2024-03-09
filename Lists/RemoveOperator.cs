using System.Diagnostics;
using Microsoft.VisualBasic;

namespace MyNetApp.Lists
{
    public class RemoveOperator
    {
        static AddItemOperator _addItemOperator = new();
        static int _itemsCount = 1000000;
        static List<int> _list = _addItemOperator.ListAddItem(_itemsCount);
        static SortedList<int, int> _sortedList = _addItemOperator.SortedListAddItem(_itemsCount);

        public void Run()
        {
            List<RemoveItem> operators = new()
            {
                ListRemoveItem,
                SortedListRemoveItem
            };
            foreach (RemoveItem @operator in operators)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                @operator(10);
                stopwatch.Stop();
                Console.WriteLine($"Time required for removing items from {@operators.GetType()}: {stopwatch.Elapsed}");
            }
        }

        public delegate void RemoveItem(int index);

        public void ListRemoveItem(int index)
        {
            _list.RemoveAt(index);
        }

        public void SortedListRemoveItem(int index)
        {
            _sortedList.RemoveAt(index);
        }
    }
}
