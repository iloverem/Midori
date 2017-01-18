using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace MidoriBot.Common
{
    public class Dates
    {
        Dictionary<string, string> Months = new Dictionary<string, string>();
        string CurrentMonth;
        string Output;
        public void Setup()
        {
            Months.Add("01", "January");
            Months.Add("02", "Febuary");
            Months.Add("03", "March");
            Months.Add("04", "April");
            Months.Add("05", "May");
            Months.Add("06", "June");
            Months.Add("07", "July");
            Months.Add("08", "August");
            Months.Add("09", "September");
            Months.Add("10", "October");
            Months.Add("11", "November");
            Months.Add("12", "December");
        }
        public string GetDate(DateTimeOffset Input)
        {
            bool TryGetMonth = Months.TryGetValue(Input.Month.ToString(), out CurrentMonth);
            if (TryGetMonth)
            {
                Output = Input.Day + " " + CurrentMonth + " " + Input.Year.ToString();
            }
            return Output;
        }
    }
}
