using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata
{
    public class StatementPrinter
    {
        readonly CultureInfo _cultureInfo = new CultureInfo("en-US");
        public string Print(Invoice invoice, Dictionary<string, Play> plays)
        {
            var totalAmount = 0;
            var volumeCredits = 0;
            var result = string.Format("Statement for {0}\n", invoice.Customer);

            foreach(var perf in invoice.Performances) 
            {
                var play = plays[perf.PlayID];
                var price = CalculatePrice(perf, play);
                // add volume credits
                volumeCredits += Math.Max(perf.Audience - 30, 0);
                // add extra credit for every ten comedy attendees
                if ("comedy" == play.Type) volumeCredits += (int)Math.Floor((decimal)perf.Audience / 5);

                // print line for this order
                result += String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", play.Name, Convert.ToDecimal(price / 100), perf.Audience);
                totalAmount += price;
            }
            result += String.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
            result += String.Format("You earned {0} credits\n", volumeCredits);
            return result;
        }

        private int CalculatePrice(Performance perf, Play play)
        {
            var price = 0;
            switch (play.Type) 
            {
                case "tragedy":
                    price = 40000;
                    if (perf.Audience > 30) {
                        price += 1000 * (perf.Audience - 30);
                    }
                    break;
                case "comedy":
                    price = 30000;
                    if (perf.Audience > 20) {
                        price += 10000 + 500 * (perf.Audience - 20);
                    }
                    price += 300 * perf.Audience;
                    break;
                default:
                    throw new Exception("unknown type: " + play.Type);
            }

            return price;
        }

        private string GetFormatAmountOwned()
        {
            return "";
        }
        
    }
}
