using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;   

namespace ConsoleApp2
{

    class player
    {
       public string name;
        public string country;
    }
    class order
    {
        public int orderId;
        public string itemName;
        public DateTime date;
        public int quantity;
    }
    class item
    { 
        public string itemName;
        public int price;
    }
    class Program
    {
        public IEnumerable<int> ques1(IEnumerable<int> arr)
        {
            var query = from num in arr
                        where num * num * num > 100 && num * num * num < 1000
                        select num*num*num;
            return query; 
        }
        public IEnumerable<IEnumerable<player>> ques2(List<player> arr)
        {
          List<List<player>> comb=new List<List<player>>();
            int len = arr.Count();
            for(int i=0;i<len/2;i++)
            {
                for(int j=len/2;j<len;j++)
                {
                    if (arr[i].country != arr[j].country)
                    {
                    List<player> list = new List<player>();
                        list.Add(arr[i]); list.Add(arr[j]);
                        comb.Add(list);
                    }
                }
            }
            return comb;
         }
        public IEnumerable<order> ques3(IEnumerable<order> arr)
        {
            var query = arr.GroupBy(o => o.date)
                .Select(group =>
                new
                {
                    date = group.Key,
                    order = group.OrderBy(x => x.quantity)
                })
                  .OrderByDescending(group => group.date);
            List<order> list = new List<order>();
            foreach (var group in query)
            {
                foreach (var o in group.order)
                {
                    
                    list.Add(o);
                }
            }
            return list;
        }
        public IEnumerable<order> ques4(IEnumerable<order> arr)
        {
            var query = arr.GroupBy(o => o.date.Month)
                .Select(group =>
                new
                {
                    month = group.Key,
                    order = group.OrderByDescending(x => x.date)
                });
            List<order> list = new List<order>();
            foreach (var group in query)
            {
                foreach (var o in group.order)
                {
                    list.Add(o);
                }
            }
            return list;
        }
        public void ques5(IEnumerable<order> arr1, IEnumerable<item> arr2)
        {
            var query = arr1.Join(// outer sequence 
                       arr2,  // inner sequence 
                       o => o.itemName,    // outerKeySelector
                       i => i.itemName,  // innerKeySelector
                       (o, i) => new  // result selector
                       {
                           orderId = o.orderId,
                           itemName = o.itemName,
                           date=o.date,
                           TotalPrice=o.quantity*i.price
                       });
            var query2 =  query.GroupBy(o => o.date.Month)
                .Select(group =>
                new
                {
                    month = group.Key,
                    order = group.OrderByDescending(x => x.date)
                });

        }
        public void ques7(IEnumerable<order> o)
        {
            bool greaterThanZero = true;
            foreach(var item in o)
            {
                if (item.quantity <= 0)
                {
                    greaterThanZero = false; break;
                }
            }
            var query = o.OrderByDescending(x => x.quantity).ToList();
            string highestOrder = query[0].itemName;
            bool orderBeforeJan = false;
            DateTime d = new DateTime(DateTime.Now.Year, 1, 1);
            foreach (var item in o)
            {
                if (item.date < d)
                {
                    orderBeforeJan = true; break;
                }
            }
        }
        public void ques9(IEnumerable<order> o, int[] num)
        {
            int c = 0;
            foreach (var number in  num)
            {
                if (number%2==0)
                {
                    c++;
                    Console.WriteLine(number);
                }
            }
            Console.WriteLine($"number of even numbers {c}");
            var query = o.GroupBy(o => o.itemName)
                .Select(group =>
                new
                {
                    itemName = group.Key,
                    quantity
                    = group.Sum(x => x.quantity)
                });
            var query2=query.OrderByDescending(x => x.quantity).ToList();
            Console.WriteLine($"maximum orders are of item {query2[0].itemName}");

        }
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
