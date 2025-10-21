using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Data;
using System.IO; 
namespace Core
{
public class EvaluateMath
{
    public double Evaluate(string expression)
    {
        try
        {
            DataTable dt = new DataTable();
            var result = dt.Compute(expression, "");
            return Convert.ToDouble(result);
        }
        catch
        {
            throw new Exception("Matematiksel ifade çözülemedi: " + expression);
        }
    }
}

}
