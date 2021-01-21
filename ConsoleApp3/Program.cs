using GreenWhale.Mesframeworks;

using System;
using System.Threading.Tasks;

namespace ConsoleApp3
{
    class Program
    {      static async Task Main(string[] args)
            {
            var client = new MesFramework();
            var item = new TestItem[]
            {
                new TestItem("电压测试","大于0小于3.8","1.2"),
                new TestItem("电流测试","大于0小于3.8","2.3"),
                new TestItem("液晶测试","全显","4.5"),
                new TestItem("按键测试","按键接触良好","OK")
            };
            var result = await client.PostAsync("DIP8-TEST1-1", "002348", "TEST666602", "OK", "33W66660205015", item, "2020/12/08 17:37:07");
            if (result.IsSuccess==true)
            {
                Console.WriteLine("成功\t"+result.Message);
            }
            else
            {
                Console.WriteLine("失败\t" + result.Message);
            }
            Console.ReadLine();
        }


    }
}
