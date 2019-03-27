using System;
using System.Collections.Generic;

namespace DatLoader
{
    class DataList
    {
        public string Name { get; set; }
        public string Filename { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            foreach(var dataList in GetDataLists())
                ReadList(dataList);

            Console.ReadKey();
        }

        static IEnumerable<DataList> GetDataLists()
        {
            yield return new DataList { Name = "Währungen", Filename = @"data\currency.dat" };
            yield return new DataList { Name = "Artikel", Filename = @"data\articles.dat" };
            yield return new DataList { Name = "Warengruppen", Filename = @"data\families.dat" };
            yield return new DataList { Name = "Zahlarten", Filename = @"data\payform.dat" };
            yield return new DataList { Name = "Kellner", Filename = @"data\waiters.dat" };
            yield return new DataList { Name = "Terminals", Filename = @"data\terminals.dat" };
            yield return new DataList { Name = "Verkaufsart", Filename = @"data\vatrate.dat" };
        }

        static void ReadList(DataList dataList)
        {
            Console.WriteLine($"======================= {dataList.Name} ======================= ");

            var data = new TValueList();
            data.load(dataList.Filename);
            foreach (var id in data.Keys)
            {
                Console.WriteLine("-----------------------");
                var element = data[id];
                foreach (var key in element.Keys)
                    Console.WriteLine($"- {id}, {key}: {element.GetValue(key)}");
            }
        }
    }
}
