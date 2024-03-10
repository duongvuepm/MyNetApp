using System.Diagnostics;

namespace MyNetApp.Lists
{
    public class AddItemOperator(int itemCount)
    {

        public void Run()
        {
            Console.WriteLine("Adding battle!");

            Parallel.Invoke(
                () => MeasureAddItem(ListAddItem, itemCount, "List"),
                () => MeasureAddItem(SortedListAddItem, itemCount, "SortedList"),
                () => MeasureAddItem(DictionaryAddItem, itemCount, "Dictionary"));
        }

        delegate object AddItem(int amount);

        void MeasureAddItem(AddItem addItem, int amount, string colName)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            addItem(amount);
            stopwatch.Stop();
            Console.WriteLine($"Time required for adding items to {colName}: {stopwatch.Elapsed.Milliseconds}");
        }

        List<int> ListAddItem(int amount)
        {
            Random rnd = new();
            List<int> list = new List<int>();
            for (int i = 0; i <= amount; i++)
            {
                int number = rnd.Next(0, itemCount);
                list.Add(number);
            }

            return list;
        }

        SortedList<int, int> SortedListAddItem(int amount)
        {
            Random rnd = new();
            SortedList<int, int> sortedList = new SortedList<int, int>();
            for (int i = 0; i <= amount; i++)
            {
                int number = rnd.Next(0, itemCount);
                sortedList[number] = number;
            }

            return sortedList;
        }

        Dictionary<int, int> DictionaryAddItem(int amount)
        {
            Random rnd = new();
            Dictionary<int, int> dictionary = new Dictionary<int, int>();
            for (int i = 0; i <= amount; i++)
            {
                int number = rnd.Next(0, itemCount);
                dictionary[number] = number;
            }

            return dictionary;
        }
    }
}