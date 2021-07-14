using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public GameObject[] turnIcons;
    public Sprite[] SpriteIcons;
    public Button[] TickTacToeSpaces;
    public int[] markedSpaces;
    public GameObject[] winnerLine;
    public GameObject winnerPanel;
    public Text winnerText;
    public Text PlayersScoreText;
    public Button crossButton;
    public Button zeroButton;
    public GameObject selectionPanel;
    public GameObject complexityPanel;
    public Image tieImage;
    public bool AIEasy;
    public bool AIHard;

    private bool moveRobot;
    private int whoseTurn; // Чья очередь
    private int turnCount;
    private int xPlayerScore;
    private int oPlayerScore;
    void Start()
    {
        GameSetup();
    }

    void GameSetup() 
    {
        whoseTurn = 0;
        turnCount = 0;
        moveRobot = false;
        winnerPanel.SetActive(false);
        if (AIEasy || AIHard)
        {
            complexityPanel.SetActive(true);
        }
        else 
        {
            complexityPanel = null;
        }
        selectionPanel.SetActive(true);
        tieImage.gameObject.SetActive(false);
        turnIcons[0].SetActive(true);
        turnIcons[1].SetActive(false);
        for (int i = 0; i < TickTacToeSpaces.Length; i++)
        {
            TickTacToeSpaces[i].interactable = true;
            TickTacToeSpaces[i].GetComponent<Image>().sprite = null;
        }
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            markedSpaces[i] = -10;
        }
        PlayersScoreText.text = $"{xPlayerScore} : {oPlayerScore}";
    }
    public int RandomPosition()
    {
        int countRand = 0;
        for (int i = 0; i < markedSpaces.Length; i++)
        {
            countRand = Random.Range(0, markedSpaces.Length);
            if (markedSpaces[countRand] == -10)
            {
                return countRand;
            }
        }
        return RandomPosition();
    }
    public int AiHardPosition() 
    {
        if (markedSpaces[4] == -10)
        {
            return 4;
        }
        else if (markedSpaces[6] == -10)
        {
            return 6;
        }
        else if (markedSpaces[8] == -10)
        {
            return 8;
        }
        else if (markedSpaces[0] == -10)
        {
            return 0;
        }
        else if (markedSpaces[7] == -10)
        {
            return 7;
        }
        return RandomPosition();
    }
    public void MoveEasyRobot() 
    {
        int rand = RandomPosition();
        if (moveRobot)
        {
            try
            {
                TickTacToeSpaces[rand].image.sprite = SpriteIcons[whoseTurn];
            }
            catch (System.Exception)
            {
                Debug.Log($"rand: {rand} и whoseTurn:{whoseTurn}");
                throw;
            }
            TickTacToeSpaces[rand].interactable = false;
            markedSpaces[rand] = whoseTurn + 1;
            turnCount++;
            moveRobot = false;
            WhoseTurnMethod();
        }
    }
    public void MoveHardRobot()
    {
        int isMove = AiHardPosition();
        if (moveRobot)
        {
            try
            {
                TickTacToeSpaces[isMove].image.sprite = SpriteIcons[whoseTurn];
            }
            catch (System.Exception)
            {
                Debug.Log($"isTurn: {isMove} и whoseTurn:{whoseTurn}");
                throw;
            }
            TickTacToeSpaces[isMove].interactable = false;
            markedSpaces[isMove] = whoseTurn + 1;
            turnCount++;
            moveRobot = false;
            WhoseTurnMethod();
        }
    }
    public void TickTacToeButton(int WhichNumber) 
    {
        TickTacToeSpaces[WhichNumber].image.sprite = SpriteIcons[whoseTurn];
        TickTacToeSpaces[WhichNumber].interactable = false;
        markedSpaces[WhichNumber] = whoseTurn + 1;
        turnCount++;
        WhoseTurnMethod();
        if (AIEasy) 
        {
            if (WinnerCheck() == false)
            {
                moveRobot = true;
                MoveEasyRobot();
            }
        } 
        else if(AIHard) 
        {
            if (WinnerCheck() == false)
            {
                moveRobot = true;
                MoveHardRobot();
            }
        }
    }

    public void WhoseTurnMethod() 
    {
        if (turnCount > 4)
        {
            bool isWinner = WinnerCheck();
            if (turnCount == 9 && isWinner == false)
            {
                Tie();
            }
        }
        if (whoseTurn == 0)
        {
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
        else
        {
            whoseTurn = 0;
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
            if (solutions[i] == 3 * (whoseTurn + 1)) 
            {
                WinnerDisplay(i);
                return true;
            }
        }
        return false;
    }

    void WinnerDisplay(int indexIn) 
    {
        winnerPanel.SetActive(true);
        moveRobot = false;
        if (whoseTurn == 0)
        {
            xPlayerScore++;
            PlayersScoreText.text = $"{xPlayerScore} : {oPlayerScore}";
            winnerText.text = "Победили крестики!";
        }
        else if (whoseTurn == 1) 
        {
            oPlayerScore++;
            PlayersScoreText.text = $"{xPlayerScore} : {oPlayerScore}";
            winnerText.text = "Победили нолики!";
        }

        winnerLine[indexIn].SetActive(true);

        for (int i = 0; i < TickTacToeSpaces.Length; i++)
        {
            TickTacToeSpaces[i].interactable = false;
        }
    }

    public void Repeat() 
    {
        GameSetup();
        for (int i = 0; i < winnerLine.Length; i++)
        {
            winnerLine[i].SetActive(false);
        }
    }

    public void Restart() 
    {
        xPlayerScore = 0;
        oPlayerScore = 0;
        Repeat();
    }

    public void SwitchPlayer(int whichPlayer) 
    {
        if (whichPlayer == 0)
        {
            selectionPanel.SetActive(false);
            whoseTurn = 0;
            turnIcons[0].SetActive(true);
            turnIcons[1].SetActive(false);
        }
        else if (whichPlayer == 1) 
        {
            selectionPanel.SetActive(false);
            whoseTurn = 1;
            turnIcons[0].SetActive(false);
            turnIcons[1].SetActive(true);
        }
    }

    void Tie() 
    {
        winnerPanel.SetActive(true);
        tieImage.gameObject.SetActive(true);
        winnerText.text = "Ничья!";
    }

    public void EasyLevel() 
    {
        complexityPanel.SetActive(false);
        AIEasy = true;
        AIHard = false;
    }
    public void HardLevel() 
    {
        complexityPanel.SetActive(false);
        AIEasy = false;
        AIHard = true;
    }


    //public bool IsWinner() 
    //{
    //    return (markedSpaces[0] == markedSpaces[1] && markedSpaces[1] == markedSpaces[2]) ||
    //           (markedSpaces[3] == markedSpaces[4] && markedSpaces[4] == markedSpaces[5]) ||
    //           (markedSpaces[6] == markedSpaces[7] && markedSpaces[7] == markedSpaces[8]) ||

    //           (markedSpaces[0] == markedSpaces[3] && markedSpaces[3] == markedSpaces[6]) ||
    //           (markedSpaces[1] == markedSpaces[4] && markedSpaces[4] == markedSpaces[7]) ||
    //           (markedSpaces[2] == markedSpaces[5] && markedSpaces[5] == markedSpaces[8]) ||

    //           (markedSpaces[0] == markedSpaces[4] && markedSpaces[4] == markedSpaces[8]) ||
    //           (markedSpaces[2] == markedSpaces[4] && markedSpaces[4] == markedSpaces[6]);
    //}
}
