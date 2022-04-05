using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFunctions : MonoBehaviour
{
    //empty and fill button, also sets button inactive while not filled
    public IEnumerator FillButton(GameObject button, float x)//x = how many seconds button is deactivated for
    {
        button.GetComponent<Image>().fillAmount = 0;
        button.GetComponent<Button>().interactable = false;//deactivate button
        while (button.GetComponent<Image>().fillAmount < 1)
        {
            //Debug.Log(button.fillAmount);
            button.GetComponent<Image>().fillAmount += (Time.deltaTime / x);//slowly fill button over time
            yield return null;
        }
        button.GetComponent<Button>().interactable = true;//reactivate button
    }
}
