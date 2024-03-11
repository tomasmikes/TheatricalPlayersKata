﻿using System;

namespace TheatricalPlayersRefactoringKata;

public class PlayTypeCalculator
{
    public int CalculateByType(string type, int audience)
    {
        switch (type)
        {
            case "tragedy":
                return CalculateTragedy(audience);
            case "comedy":
                return CalculateComedy(audience);
            default:
                throw new Exception("unknown type: " + type);
        }
    }

    private int CalculateTragedy(int audience)
    {
        var price = 40000;
        if (audience > 30)
        {
            price += 1000 * (audience - 30);
        }

        return price;
    }

    private int CalculateComedy(int audience)
    {
        var price = 30000;
        if (audience > 20)
        {
            price += 10000 + 500 * (audience - 20);
        }

        price += 300 * audience;

        return price;
    }

}