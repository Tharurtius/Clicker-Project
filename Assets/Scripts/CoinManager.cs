using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinManager : MonoBehaviour
{
    #region Variables
    [Header("Coins")]//counts how many coins you have
    public int bitCount;
    public int bitConCount, ethCount;
    [Header("Prices")]//how much each coin costs, + cards price
    public float cardPrice;
    public float bitPrice, bitConPrice, ethPrice;
    [Header("Texts")]//reference to UI elements
    public GameObject buttonText;
    public Text bitPriceText, bitConPriceText, ethPriceText;
    public Text bitCountText, bitConCountText, ethCountText;
    [Header("Misc")]//other stuff
    public float time;//used for pricing coins
    public List<GameObject> cards;//list of cards
    public GameObject prefabCard;//graphics card prefab
    public GameObject cardPanel;//vertical layout
    #endregion
    #region Main
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        cardPrice = 640;//actual card price
        bitPrice = 47;//actual coin prices irl /1000 so player can afford it
        ethPrice = 3;
        bitConPrice = 0.5f;//bitconnect reference, price will crash in the future
        StartCoroutine(PriceWave());//this is used to determine price
        //initialize count text
        bitCountText.text = bitCount + " BTC";
        ethCountText.text = ethCount + " ETH";
        bitConCountText.text = bitConCount + " BCC";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.coinTrigger)
        {
            GameManager.coinTrigger = false;
            cardPrice = 640 * GameManager.costMult;//update card price
            if (cards.Count <= 10) //will be another message when there are enough cards
            {
                buttonText.GetComponentInChildren<Text>().text = "Buy Card - $" + cardPrice.ToString("F2");
            }
            for (int i = 0; i < cards.Count; i++)//actual code that gives coins
            {
                switch (cards[i].GetComponent<Dropdown>().value)
                {
                    case 0:
                        bitCount++;
                        break;
                    case 1:
                        ethCount++;
                        break;
                    case 2:
                        bitConCount++;
                        break;
                    default:
                        Debug.Log("Error!"); //should never trigger
                        break;
                }
            }
            //update text
            bitCountText.text = bitCount + " BTC";
            ethCountText.text = ethCount + " ETH";
            bitConCountText.text = bitConCount + " BCC";
        }
    }
#endregion
    #region Functions
    public float PriceChange(float offset) //how big of a price change happens
    {
        float change = Mathf.Sin((time) + offset) + Mathf.Sin((time * 2f) + offset) + Random.Range(-1.5f, 2f); //fancy wave + randomizer
        change *= Random.Range(1f, 5f); //magnitude of change
        return change;
    }
    public void BuyCard()
    {
        GameManager.cash -= cardPrice;
        cards.Add(Instantiate(prefabCard, cardPanel.transform));
        if (cards.Count == 11)//mining rig is full
        {
            buttonText.GetComponentInChildren<Text>().text = "No more room";
            if (buttonText.GetComponent<Button>().IsInteractable())
            {
                buttonText.GetComponent<Button>().interactable = false;
            }
        }
    }
    public void BuyCoin(string coin)//buy one coin at a time
    {
        switch (coin)
        {
            case "BTC":
                GameManager.cash -= bitPrice;
                bitCount++;
                bitCountText.text = bitCount + " BTC";//update text
                break;
            case "ETH":
                GameManager.cash -= ethPrice;
                ethCount++;
                ethCountText.text = ethCount + " ETH";
                break;
            case "BCC":
                GameManager.cash -= bitConPrice;
                bitConCount++;
                bitConCountText.text = bitConCount + " BCC";
                break;
            default:
                Debug.Log("Error!"); //should never trigger
                break;
        }
    }
    public void SellCoin(string coin)//sell all coins
    {
        switch (coin)
        {
            case "BTC":
                GameManager.cash += bitPrice * bitCount;
                bitCount = 0;
                bitCountText.text = bitCount + " BTC";
                break;
            case "ETH":
                GameManager.cash += ethPrice * ethCount;
                ethCount = 0;
                ethCountText.text = ethCount + " ETH";
                break;
            default:
                Debug.Log("Error!"); //should never trigger
                break;
        }
    }
#endregion
    #region Coroutines
    IEnumerator PriceWave()
    {
        while (true)
        {

            bitPrice = Mathf.Max(0, bitPrice + PriceChange(0)); //keeps price from reaching negative
            bitPriceText.text = "BitCoin Price:" + System.Environment.NewLine + "$" + bitPrice.ToString("F2"); //find text and change price to current price, F2 keeps float at 2 decimal places
            ethPrice = Mathf.Max(0, ethPrice + PriceChange(1));
            ethPriceText.text = "Ethereum Price:" + System.Environment.NewLine + "$" + ethPrice.ToString("F2");
            if (time < 60)//before 5 mins has passed, 300 seconds * 0.2 = 60
            {
                bitConPrice *= 1.1f;//is a scam so uses different maths
            }
            else//after 5 minutes
            {
                bitConPrice = 0;//tank scam coin price
            }
            bitConPriceText.text = "BitConnect Price:" + System.Environment.NewLine + "$" + bitConPrice.ToString("F2");
            time += 0.2f;//controls how fast the sine wave changes from positive to negative
            yield return new WaitForSeconds(1f);
        }
    }
    #endregion
}
