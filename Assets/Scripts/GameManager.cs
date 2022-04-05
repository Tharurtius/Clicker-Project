using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    public static float cash = 0; //cash
    public Text cashText, cardText, rentText, debtText;//for displaying text to player
    public float weekTimer = 0;//used to tell time
    public float rent, debt;//you have to pay this every week
    public static float costMult; //this goes up over time
    public static GameObject messageText;//used to display messages to player
    public GameObject messageBox;//gotta use this because static variables don't show up in the inspector
    public static bool coinTrigger = false;//messages coinmanager
                                           //a week = 8 seconds, rent + bills gets taken every week
    #endregion
    #region Main
    // Start is called before the first frame update
    void Start()
    {
        rent = 537;//actual average bill, not including transportation costs
        debt = 640;//implies player bought a card already
        costMult = 1;//used to calculate price increase due to interest rates, 1 week = 1 year
        messageText = messageBox;//why cant i see static variables in the inspector
        //update ui stuff
        rentText.text = "Weekly rent & bills" + System.Environment.NewLine + "$" + rent.ToString("F2");
        debtText.text = "Debt:" + System.Environment.NewLine + "$" + debt.ToString("F2");
    }
    // Update is called once per frame
    void Update()
    {
        cashText.text = "$" + cash.ToString("F2");//update cash display
        weekTimer += Time.deltaTime;//keeps track of time
        if (weekTimer >= 8) //once a week has passed
        {
            weekTimer -= 8;
            costMult *= 1.03f;//cost of everything goes up
            coinTrigger = true;//to tell coinmanager script when to fire
            PayRent();
        }
    }
    #endregion
    #region Functions
    public void DeliverFood()//work shitty delivery job for $2
    {
        cash += Random.Range(2f, 10f);
    }
    public void PayRent()//also used to pay debt
    {
        debt *= 1.03f;//compounding interest on debt, actual interest rate irl is ~6%
        cash -= rent;
        cash -= debt;
        debt = 0;
        if (cash < 0)//go into debt
        {
            debt += -cash;
            cash = 0;
        }
        rent = 537 * costMult;//update rent
        rentText.text = "Weekly rent & bills" + System.Environment.NewLine + "$" + rent.ToString("F2");//update text
        debtText.text = "Debt:" + System.Environment.NewLine + "$" + debt.ToString("F2");
        //if debt interest + rent is bigger than your income + cash, you lose
        if (rent + (debt*0.03) > CookieClickHandler.income + cash)
        {
            messageText.GetComponent<Text>().text = "You went Bankrupt" + System.Environment.NewLine + "Game Over!";
            messageText.SetActive(true);
            Time.timeScale = 0f;//stop game
        }
    }
    #endregion
    #region Coroutines
    public static IEnumerator ShowMessage(string message) //shows a message for 1 second, then disappears
    {
        messageText.GetComponent<Text>().text = message;
        messageText.SetActive(true);
        yield return new WaitForSeconds(1f);
        messageText.SetActive(false);
    }
    #endregion
}
