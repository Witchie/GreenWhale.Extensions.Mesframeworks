using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GreenWhale.Mesframeworks
{
    public interface IMesFramework
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
        Task<Result> PostAsync<T>(string workCenter, string employeeId, string orderId, string result, string deviceId, IEnumerable<T> testContent, string testDateTime, int pcbCount = 1, string workStationNumber = null) where T : ITestItem;
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
        Task<Result> PostAsync<T>(string workCenter, string employeeId, string orderId, string result, string deviceId, T[] testContent, DateTime testDateTime, int pcbCount = 1, string workStationNumber = null) where T : ITestItem;
    }
}