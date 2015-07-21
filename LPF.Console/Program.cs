using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LPF.Core;

namespace LPF.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            FixQueue<MyPoolItem> fixQueue = new FixQueue<MyPoolItem>(10);

            for (int i = 0; i < 100; i++)
            {
               // Console.WriteLine(string.Format("第【{0}】", i));


                fixQueue.EnQueue(new MyPoolItem()
                {
                    No = i + "",
                    Count = 0
                });
                PrintInfo(fixQueue);

            }
            

         //   Console.ReadLine();
        }

        public static void PrintInfo(FixQueue<MyPoolItem> fixQueue)
        {
            string str = string.Empty;
            fixQueue.ForEach(item =>
            {

                str += item.No + ",";

            });
            //Console.WriteLine(str);

        }
    }
}
