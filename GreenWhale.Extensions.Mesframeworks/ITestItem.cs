namespace GreenWhale.Mesframeworks
{
    public interface ITestItem
    {
        /// <summary>
        /// 测试项名称
        /// </summary>
        string ItemName { get; set; }
        /// <summary>
        /// 测试项值
        /// </summary>
        string ItemValue { get; set; }
        /// <summary>
        /// 测试单位
        /// </summary>
        string ItemUnit { get; set; }
        /// <summary>
        /// 测试条件
        /// </summary>
        string Condation { get; set; }
        /// <summary>
        /// 规格
        /// </summary>
        string Standard { get; set; }
    }
}
