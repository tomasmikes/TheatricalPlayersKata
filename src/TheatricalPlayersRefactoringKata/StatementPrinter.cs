using System;
using System.Collections.Generic;
using System.Globalization;

namespace TheatricalPlayersRefactoringKata;

public class StatementPrinter : IPrinter
{
    readonly CultureInfo _cultureInfo = new CultureInfo("en-US");
    public string Print(Invoice invoice, Dictionary<string, Play> plays)
    {
        var totalAmount = 0;
        var volumeCredits = 0;
        var result = GetFormatStatement(invoice.Customer);

        foreach(var perf in invoice.Performances) 
        {
            var play = plays[perf.PlayId];
            var price = CalculatePrice(perf, play);
            volumeCredits += GetVolumeCredits(perf.Audience, play.Type);

            // print line for this order
            result += GetFormatSeats(play.Name, price, perf.Audience);

            totalAmount += price;
        }

        result += GetFormatAmountOwned(totalAmount);
        result += GetFormatEarnedCredits(volumeCredits);
        return result;
    }

    private int GetVolumeCredits(int audience, string playType)
    {
        // add volume credits
        var volumeCredits = Math.Max(audience - 30, 0);
        // add extra credit for every ten comedy attendees
        if ("comedy" == playType)
        {
            volumeCredits += (int)Math.Floor((decimal)audience / 5);
        }
        return volumeCredits;
    }

    private string GetFormatStatement(string invoiceCustomer) 
        => string.Format("Statement for {0}\n", invoiceCustomer);

    private string GetFormatSeats(string playName, int price, int perfAudience)
        => String.Format(_cultureInfo, "  {0}: {1:C} ({2} seats)\n", playName, Convert.ToDecimal(price / 100), perfAudience);

    private string GetFormatEarnedCredits(int volumeCredits) 
        => String.Format("You earned {0} credits\n", volumeCredits);

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

    private string GetFormatAmountOwned(int totalAmount) => String.Format(_cultureInfo, "Amount owed is {0:C}\n", Convert.ToDecimal(totalAmount / 100));
}