using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public int whoTurn; // 0 = x, 1 = o
    public int turnCount; // number of turn played
    public GameObject[] turnIcons; // displays whos turn it is
    public Sprite[] playIcons; // 0 = x icon, 1 = o icon
    public Button[] tictactoeSpaces; // playable space for our game
    public int[] markedSpaces; // ID which spaces is marked by player
    public Text winnerText; // Hold winner text's text
    public GameObject[] winningLine; // Hold all the visual line for winning
    public GameObject winnerPanel;
    public int xPlayerScore;
    public int oPlayerScore;
    public Text xPlayerScoreText;
    public Text oPlayerScoreText;
    public Button xPlayerButton;
    public Button oPlayerButton;

    // Start is called before the first frame update
    void Start()
    {
        GameSetup();
    }

    void Update()
    {

    }

    // Update is called once per frame
    void GameSetup()
    {
        whoTurn = 0;
        turnCount = 0;
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i<tictactoeSpaces.Length; i++)
        {
            tictactoeSpaces[i].interactable = true;
            tictactoeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0;i<markedSpaces.Length;i++)
        {
            markedSpaces[i] = -100; 
        }
    }

    public void TicTacToeButton(int WhichNumber)
    {
        xPlayerButton.interactable = false;
        oPlayerButton.interactable = false;
        tictactoeSpaces[WhichNumber].image.sprite = playIcons[whoTurn];
        tictactoeSpaces[WhichNumber].interactable = false;

        markedSpaces[WhichNumber] = whoTurn + 1;
        turnCount++;
        if (turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == false)
            {
                Draw();
                Invoke("Rematch", 1.5f);
            }
        }
        
        if (whoTurn == 0)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
    }

    bool WinnerCheck()
    {
        int s1 = markedSpaces[0] + markedSpaces[1] + markedSpaces[2];
        int s2 = markedSpaces[3] + markedSpaces[4] + markedSpaces[5];
        int s3 = markedSpaces[6] + markedSpaces[7] + markedSpaces[8];
        int s4 = markedSpaces[0] + markedSpaces[3] + markedSpaces[6];
        int s5 = markedSpaces[1] + markedSpaces[4] + markedSpaces[7];
        int s6 = markedSpaces[2] + markedSpaces[5] + markedSpaces[8];
        int s7 = markedSpaces[0] + markedSpaces[4] + markedSpaces[8];
        int s8 = markedSpaces[2] + markedSpaces[4] + markedSpaces[6];
        var solutions = new int[] { s1, s2, s3, s4, s5, s6, s7, s8 };
        for (int i = 0; i < solutions.Length; i++)
        {
            if (solutions[i] == 3 * (whoTurn + 1))
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn)
    {
        winnerPanel.gameObject.SetActive(true);
        if (whoTurn == 0)
        {
            xPlayerScore++;
            xPlayerScoreText.text = xPlayerScore.ToString();
            winnerText.text = "Player X Wins!";
        }
        else if (whoTurn == 1)
        {
            oPlayerScore++;
            oPlayerScoreText.text = oPlayerScore.ToString();
            winnerText.text = "Player O Wins!";
        }
        winningLine[indexIn].SetActive(true);
        Invoke("Rematch",1.5f);
    }

    public void Rematch()
    {
        GameSetup();
        for (int i = 0; i < winningLine.Length ; i++)
        {
            winningLine[i].SetActive(false);
        }
        winnerPanel.SetActive(false);
        xPlayerButton.interactable = true;
        oPlayerButton.interactable = true;
    }

    public void Restart()
    {
        Rematch();
        xPlayerScore = 0;
        oPlayerScore = 0;
        xPlayerScoreText.text = "0";
        oPlayerScoreText.text = "0";
    }

    public void SwitchPlayer(int whichPlayer)
    {
        if (whichPlayer == 0)
        {
            whoTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (whichPlayer == 1)
        {
            whoTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void Draw()
    {
        winnerPanel.SetActive(true);
        winnerText.text = "DRAW";
    }
}
