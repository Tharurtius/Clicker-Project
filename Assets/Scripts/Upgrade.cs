using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Upgrade : ButtonFunctions
{
    public void Promotion()
    {
        if (Random.Range(0f, 100f) > 80)//20% chance of promotion
        {
            CookieClickHandler.income += 80;//extra $2 an hour
            CookieClickHandler.UpdateIncome();//update income text
            StartCoroutine(GameManager.ShowMessage("You got Promoted"));
        }
        else
        {
            //Debug.Log("Can't afford");
            StartCoroutine(GameManager.ShowMessage("You didn't get Promoted"));
        }
        StartCoroutine(FillButton(gameObject, 32f));//disables button for 32 seconds
    }
}
