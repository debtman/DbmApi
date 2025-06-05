using System.Diagnostics.Eventing.Reader;

namespace DbmApi.DataClasses
{
    public class ClassDebtInfo
    {
        public struct DebtStruct
        {
            public string DebtCode;
            public string Sum;
            public bool UseForCals;
        }

        public List<DebtStruct> Debt;
        public decimal DebtSum;
        public decimal CurrentDebtSum;

        public ClassDebtInfo() 
        { 
        }
    }
}
