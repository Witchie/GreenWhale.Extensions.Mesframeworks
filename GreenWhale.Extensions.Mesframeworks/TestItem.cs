namespace GreenWhale.Mesframeworks
{
    public class TestItem : ITestItem
    {
        private string standard;
        private string itemValue;
        private string itemUnit;
        private string condation;

        public TestItem(string itemName, string standard, string itemValue, string itemUnit="")
        {
            ItemName = itemName;
            ItemValue = itemValue;
            ItemUnit = itemUnit;
            Condation = standard;
            Standard = standard;
        }

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
}
