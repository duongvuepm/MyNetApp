using System.Diagnostics;

namespace MyNetApp.Lists
{
    public class AddItemOperator
    {
        static Random _rnd = new();
        static int itemsCount = 1000000;

        public void Run()
        {
            MeasureAddItem(ListAddItem, itemsCount, "List");
            MeasureAddItem(SortedListAddItem, itemsCount, "SortedList");
            MeasureAddItem(DictionaryAddItem, itemsCount, "Dictionary");
        }

        public delegate object AddItem(int amount);

        public void MeasureAddItem(AddItem addItem, int amount, string colName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            addItem(amount);
            stopwatch.Stop();
            Console.WriteLine($"Time required for adding items to {colName}: {stopwatch.Elapsed}");
        }

        public List<int> ListAddItem(int amount)
        {
            List<int> list = new List<int>();
            for (int i = 0; i < amount; i++)
            {
                int number = _rnd.Next(1, itemsCount);
                list.Add(number);
            }
            return list;
        }

        public SortedList<int, int> SortedListAddItem(int amount)
        {
            SortedList<int, int> sortedList = new SortedList<int, int>();
            for (int i = 0; i < amount; i++)
            {
                int number = _rnd.Next(1, itemsCount);
                sortedList[number] = number;
            }
            return sortedList;
        }

        public Dictionary<int, int> DictionaryAddItem(int amount)
        {
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i < amount; i++)
            {
                int number = _rnd.Next(1, itemsCount);
                dictionary[number] = number;
            }
            return dictionary;
        }
    }
}
