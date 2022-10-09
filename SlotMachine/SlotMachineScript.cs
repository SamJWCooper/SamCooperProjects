using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotMachineScript : MonoBehaviour
{
    bool isSpinning;

    public bool slotHandlePressed;
    public bool increaseBetPressed;
    public bool increaseBetX10Pressed;

    public int rowOneNum;
    public int rowTwoNum;
    public int rowThreeNum;
    int rowCount;

    Text rowOneText;
    Text rowTwoText;
    Text rowThreeText;

    Text creditText;
    Text betAmountText;

    public static int creditAmount = 1000;
    public static int betAmount;

    void Start()
    {
        creditText = GameObject.Find("Credit").GetComponent<UnityEngine.UI.Text>();
        betAmountText = GameObject.Find("BetAmount").GetComponent<UnityEngine.UI.Text>();


        rowOneText = GameObject.Find("Row1").GetComponent<UnityEngine.UI.Text>();
        rowTwoText = GameObject.Find("Row2").GetComponent<UnityEngine.UI.Text>();
        rowThreeText = GameObject.Find("Row3").GetComponent<UnityEngine.UI.Text>();
    }

    void Update()
    {
        UpdateTextOnScreen();
        CheckForEndOfSpin();
        Spin();
    }

    // intractable buttons
    void OnMouseUp()
    {
        if (slotHandlePressed && isSpinning == false && betAmount > 0)
        {
            isSpinning = true;
            StartCoroutine(Row2());
        }

        if (increaseBetPressed && isSpinning == false && creditAmount > 0)
        {
            creditAmount = creditAmount - 10;
            betAmount = betAmount + 10;
        }

        if (increaseBetX10Pressed && isSpinning == false && creditAmount >= 100)
        {
            creditAmount = creditAmount - 100;
            betAmount = betAmount + 100;
        }
    }

    // randomize spin numbers
    IEnumerator Row2()
    {
        Debug.Log("CORUITINE START");
        do
        {
            yield return new WaitForSeconds(0.2f);
            Debug.Log("rowCount" + rowCount );
            rowCount = rowCount + 1;

        } while (rowCount < 14);
    }

    // select final numbers
    void Spin()
    {
        // row 3
        if (rowCount < 14 && isSpinning == true)
        {
            rowThreeText.text = Random.Range(1, 9).ToString();
            int.TryParse(rowThreeText.text, out rowThreeNum);
        }

        // row 2
        if (rowCount < 10 && isSpinning == true)
        {
            rowTwoText.text = Random.Range(1, 9).ToString();
            int.TryParse(rowTwoText.text, out rowTwoNum);
        }

        // row 1
        if (rowCount < 6 && isSpinning == true) 
        {
            rowOneText.text = Random.Range(1, 9).ToString();
            int.TryParse(rowOneText.text, out rowOneNum);
        }
    }

    // reset rowcount for next spin
    void CheckForEndOfSpin()
    {
        if(rowCount == 14)
        {
            rowCount = 0;
            isSpinning = false;
            CheckForWin();
        }
    }

    // check numbers for win
    void CheckForWin()
    {
        Debug.Log("CHECKED NUMBERS");

        if (rowOneNum == rowTwoNum || rowTwoNum == rowThreeNum || rowOneNum == rowThreeNum)
        {
            creditAmount = betAmount * 2 + creditAmount;
            betAmount = 0;
        }

        else if (rowOneNum == rowTwoNum && rowTwoNum == rowThreeNum)
        {
            creditAmount = betAmount * 10 + creditAmount;

            betAmount = 0;
        }

        else
        {
            betAmount = 0;
        }
    }

    // keep credit and bet text updated
    void UpdateTextOnScreen()
    {
        betAmountText.text = betAmount.ToString();
        creditText.text = creditAmount.ToString();

    }
}

       


   






