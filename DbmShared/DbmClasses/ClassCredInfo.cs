namespace DbmApi.DataClasses
{
    public class ClassCredInfo
    {
        private string contractNum;
        private DateTime contractDate;
        private decimal credSum;

        public ClassDebtInfo DebtInfo { get; set; }
        
    }
}
