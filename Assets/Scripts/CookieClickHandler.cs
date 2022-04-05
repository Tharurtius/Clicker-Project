using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CookieClickHandler : ButtonFunctions
{
    public static float income = 650;//minimum wage
    public static Text incomeText;//actual text, usable by every script
    public Text incomeBox;//unity makes me do this

    public void Start()
    {
        incomeText = incomeBox;//why are you forcing me to do this Unity?
        UpdateIncome();
    }
    public void Click()
    {
        GameManager.cash += income;
        //Debug.Log(GameManager.pokes);
        StartCoroutine(FillButton(gameObject, 8));//fancy button stuff
    }
    public static void UpdateIncome()//update income text
    {
        incomeText.text = "Weekly income:" + System.Environment.NewLine + "$" + income + " per week";
    }
}
