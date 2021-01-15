using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Net.Http;
using System.Xml.Linq;

namespace GreenWhale.Mesframeworks
{
    public class TestItem : ITestItem
    {
        private string standard;
        private string itemValue;
        private string itemUnit;
        private string condation;

        public string ItemName { get; set; }
        public string ItemValue { get { return itemValue == string.Empty ? "NA" : itemValue; } set => itemValue = value; }
        public string ItemUnit { get { return itemUnit == string.Empty ? "NA" : itemUnit; } set => itemUnit = value; }
        public string Condation { get { return condation == string.Empty ? "NA" : condation; } set => condation = value; }
        public string Standard
        {
            get
            {
                if (standard == null)
                {
                    return ItemValue == string.Empty ? "NA" : ItemValue;
                }
                return standard;
            }
            set => standard = value;
        }
    }
    /// <summary>
    /// MES框架
    /// </summary>
    public class MesFramework
    {

        /// <summary>
        /// 推送单个记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workStationNumber">测试站编号</param>
        /// <param name="workCenter">工作中心</param>
        /// <param name="employeeId">员工编号</param>
        /// <param name="orderId">制令单号</param>
        /// <param name="result">测试结果</param>
        /// <param name="deviceId">产品序列号</param>
        /// <param name="pcbCount">PCB个数</param>
        /// <param name="testContent">测试结果内容</param>
        /// <param name="testDateTime">生产测试时间</param>
        public async Task<(bool result,string message)> PostAsync<T>(string workStationNumber, string workCenter, string employeeId, string orderId, string result, string deviceId, IEnumerable<T> testContent, string testDateTime, int pcbCount = 1) where T: ITestItem
        {
            var res= testContent.Select(p => $"{p.ItemName}:{p.ItemValue}:{p.ItemUnit}:{p.Condation}:{p.Standard}");
            var content=  string.Join("|", res);
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var req = new Uri($"http://10.16.10.61:9061/MesFrameWork.asmx/TEST_AOI?" + $"M_DEVICE_SN={workStationNumber}&M_WORKSTATION_SN={workCenter}&M_EMP_NO={employeeId}&M_MO={orderId}&M_TEST_RESULT={result}&M_SN={deviceId}&M_EC_ALL=null&M_EQ_ALL=&M_POSITION=&M_PCB_NUM={pcbCount}&M_ITEMVALUE={content}&M_TEST_TIME={testDateTime}&M_NEW_SN=");
                    var respond = await client.GetAsync(req);
                    if (respond.IsSuccessStatusCode)
                    {
                        var str = await respond.Content.ReadAsStringAsync();
                        var doc = XDocument.Parse(str);
                        if (doc != null)
                        {
                            if (doc.FirstNode is XElement element)
                            {
                                var text = element.Value;
                                var temp = text.Split("~~~~");
                                var message = temp.FirstOrDefault();
                                var count= message.Split(":");
                                if (count[0]=="0")
                                {
                                    return (true, count[1]);
                                }
                                else
                                {
                                    return (false, count[2]);
                                }
                            }
                            else
                            {
                                return (false, "解析XML文件失败");
                            }
                        }
                        else
                        {
                            return (false, "解析XML文件失败");
                        }
                    }
                    else
                    {
                        return (false, await respond.Content.ReadAsStringAsync());
                    }
                }

            }
            catch (Exception err)
            {
                return (false, err.Message);

            }
        }

    }
}
