using UnityEngine;

namespace InterestingTrait.code;

public class CallBack
{
    public static float temp1;
    public static float temp2;
    public static void TSOTW_ADD(string pCurrentValue)
    {
       bool bol = float.TryParse(pCurrentValue,out temp1);
       if (bol)
       {
           Globals.TsotwAdd = temp1;
       }
       else
       {
           Globals.TsotwAdd = 1000.0f;
           Debug.Log("数值格式不正确,请检查");
       }
    }public static void TSOTW_INIT(string pCurrentValue)
    {
       bool bol = float.TryParse(pCurrentValue,out temp2);
       if (bol)
       {
           Globals.Tsotw = temp2;
       }
       else
       {
           Globals.Tsotw = 10000.0f;
           Debug.Log("数值格式不正确,请检查");
       }
    }
}