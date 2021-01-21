using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using System.Xml.Linq;
using System.Net.Http;

namespace GreenWhale.Mesframeworks
{
    /// <summary>
    /// MES框架
    /// </summary>
    public class MesFramework : IMesFramework
    {
        /// <summary>
        /// 推送单个记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workStationNumber">测试站SN</param>
        /// <param name="workCenter">工作中心</param>
        /// <param name="employeeId">员工编号</param>
        /// <param name="orderId">制令单号</param>
        /// <param name="result">测试结果</param>
        /// <param name="deviceId">产品序列号</param>
        /// <param name="pcbCount">PCB个数</param>
        /// <param name="testContent">测试结果内容</param>
        /// <param name="testDateTime">生产测试时间</param>
        public Task<Result> PostAsync<T>(string workCenter, string employeeId, string orderId, string result, string deviceId, T[] testContent, DateTime testDateTime, int pcbCount = 1, string workStationNumber = null) where T : ITestItem
        {
            return this.PostAsync(workCenter, employeeId, orderId, result, deviceId, testContent, testDateTime.ToString("yyyy/MM/dd HH:mm:ss"), pcbCount, workStationNumber);
        }
        /// <summary>
        /// 推送单个记录
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="workStationNumber">测试站SN</param>
        /// <param name="workCenter">工作中心</param>
        /// <param name="employeeId">员工编号</param>
        /// <param name="orderId">制令单号</param>
        /// <param name="result">测试结果</param>
        /// <param name="deviceId">产品序列号</param>
        /// <param name="pcbCount">PCB个数</param>
        /// <param name="testContent">测试结果内容</param>
        /// <param name="testDateTime">生产测试时间</param>
        public async Task<Result> PostAsync<T>(string workCenter, string employeeId, string orderId, string result, string deviceId, IEnumerable<T> testContent, string testDateTime, int pcbCount = 1, string workStationNumber = null) where T : ITestItem
        {
            var res = testContent.Select(p => $"{p.ItemName}:{p.ItemValue}:{p.ItemUnit}:{p.Condation}:{p.Standard}");
            var content = string.Join("|", res);
            if (workStationNumber == null)
            {
                workStationNumber = System.Environment.MachineName;
            }
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var req = new Uri($"http://10.16.10.61:9061/MesFrameWork.asmx/TEST_AOI?" +
                        $"M_DEVICE_SN={workStationNumber}" +
                        $"&M_WORKSTATION_SN={workCenter}" +
                        $"&M_EMP_NO={employeeId}" +
                        $"&M_MO={orderId}" +
                        $"&M_TEST_RESULT={result}" +
                        $"&M_SN={deviceId}" +
                        $"&M_EC_ALL=null" +
                        $"&M_EQ_ALL=" +
                        $"&M_POSITION=" +
                        $"&M_PCB_NUM={pcbCount}" +
                        $"&M_ITEMVALUE={content}" +
                        $"&M_TEST_TIME={testDateTime}" +
                        $"&M_NEW_SN=");
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
#if NET461 || NET451 || NETSTANDARD
                                var temp = text.Split("~~~~".ToCharArray());
                                var message = temp.FirstOrDefault();
                                var count = message.Split(':');
#else
                                var temp = text.Split("~~~~");
                                var message = temp.FirstOrDefault();
                                var count = message.Split(":");
#endif
                                if (count[0] == "0")
                                {
                                    return new Result(true, count[1]);
                                }
                                else
                                {
                                    return new Result(false, message);
                                }
                            }
                            else
                            {
                                return new Result(false, "解析XML文件失败");
                            }
                        }
                        else
                        {
                            return new Result(false, "解析XML文件失败");
                        }
                    }
                    else
                    {
                        return new Result(false, await respond.Content.ReadAsStringAsync());
                    }
                }

            }
            catch (Exception err)
            {
                return new Result(false, err.Message);

            }
        }

    }
}
