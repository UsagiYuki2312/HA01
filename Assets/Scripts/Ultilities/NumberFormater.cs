using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class NumberFormater
{
    public static string[] TextFormat = { " ", " K", " M", " B", " T", " a"," b"," c"," d"," e"," f"," g"," h"," i"," j"," k"," l", " m",
    " n"," o"," p"," q"," r"," s", " t"," u"," v"," w","x"," y"," z"," aa"," ab"," ac"," ad"," ae"," af"," ag"," ah"," ai"," aj"," ak"," al"," am"," an"," ao"," ap"," aq"," ar"," as"," at"," au"," av"," aw"," ax"," ay"," az"};
    // takes a double and returns a simplified string representation
    public static string Format(this int number)
    {
        string txt = "";
        if (number <= 0) return "0";

        if (number < 1000)
        {
            txt = number.ToString();
        }
        else
        {
            int a = (int)System.Math.Log10(number);
            int b = a / 3;
            if (b == 0)
                return number.ToString();
            if (b == 1)
                txt = (number / 1000).ToString() + " K";
            else if (b < TextFormat.Length)
                txt = (number / System.Math.Pow(10, b * 3)).ToString() + TextFormat[b];
            else
             if (number < 1000)
                txt = number.ToString();
            string[] tmp = txt.Split(' ');
            string[] tmp2 = tmp[0].Split('.');
            if (tmp2.Length > 1)
            {
                if (tmp2[1] == "00")
                    return tmp2[0] + " " + tmp[1];
            }
        }
        return txt;

    }
}
