using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dropship : ButtonFunctions
{
    public Text buttonText;
    public void Purchase()//purchase website
    {
        GameManager.cash -= 5000f;//minimum reccomended price for a website designer
        StartCoroutine(DropShip());
        buttonText.text = "Dropshipping Site" + System.Environment.NewLine + "~$50 per week";
        GetComponent<Button>().interactable = false;
    }
    IEnumerator DropShip()
    {
        while (true)
        {
            GameManager.cash += Random.Range(0f, 25f);
            yield return new WaitForSeconds(2f);//pay roughly every quarter week, average is $50 per week
        }
    }
}
