using System;

namespace SWNUniverseGenerator.CreationTools;

public class RomanNumerals
{
    public static string ToRoman(int number)
    {
        switch (number)
        {
            case < 0:
            case > 3999:
                throw new ArgumentOutOfRangeException("Number not in range");
            case < 1:
                return string.Empty;
            case >= 1000:
                return "M" + ToRoman(number - 1000);
            case >= 900:
                return "CM" + ToRoman(number - 900);
            case >= 500:
                return "D" + ToRoman(number - 500);
            case >= 400:
                return "CD" + ToRoman(number - 400);
            case >= 100:
                return "C" + ToRoman(number - 100);
            case >= 90:
                return "XC" + ToRoman(number - 90);
            case >= 50:
                return "L" + ToRoman(number - 50);
            case >= 40:
                return "XL" + ToRoman(number - 40);
            case >= 10:
                return "X" + ToRoman(number - 10);
            case >= 9:
                return "IX" + ToRoman(number - 9);
            case >= 5:
                return "V" + ToRoman(number - 5);
            case >= 4:
                return "IV" + ToRoman(number - 4);
            case >= 1:
                return "I" + ToRoman(number - 1);
        }
    }
}