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
                    new TestItem{ItemName="电压测试",Condation=">0V 且 <3.8 V",ItemUnit="V",ItemValue="3.4",Standard="VOLTAGE" },
                    new TestItem{ItemName="电流测试",Condation=">0 uA 且 <3.8 uA",ItemUnit="uA",ItemValue="11.6", },
                    new TestItem{ItemName="液晶测试",Condation="全显",ItemUnit=string.Empty,ItemValue="通过", },
                    new TestItem{ItemName="按键测试",Condation="按键接触良好",ItemUnit=string.Empty,ItemValue="通过", }
                };
                for (int i = 0; i < 20; i++)
                {
                    var result = await client.PostAsync("1", "DIP8-TEST1-1", "002348", "LSD3HMF0365-13EA", "OK", i.ToString().PadLeft(12, '0'), item, "2020/12/08 17:37:07");
                }
            }


    }
}
