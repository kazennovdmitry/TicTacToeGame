using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable] //внутренние классы для хранения переменных
public class Player // класс служит для отображения того, чей ход
{
    public Image panel;
    public Text text;
}

[System.Serializable] //внутренние классы для хранения переменных
public class PlayerColor //этот класс содержит константы (panelColor и textColor), инициализируемые в Unity
{
    public Color panelColor;
    public Color textColor;
}



public class GameController : MonoBehaviour {

    public Text[] buttonList; //кнопки 0-8
    private string playerSide; //X или О

    public GameObject gameOverPanel;
    public Text gameOverText;
    public GameObject restartButton;
    public GameObject newGameButton;
    public GameObject quitButton;

    private int moveCount; //счетчик шагов - проверка на ничью

    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor; 
    public PlayerColor inactivePlayerColor;


    void Awake()
    {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        newGameButton.SetActive(true);
        SetBoardInteractable(false);
        SetGameControllerReferenceOnButtons();
    }

    public void NewGame()
    {
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        newGameButton.SetActive(false);
        SetBoardInteractable(true);
        playerSide = "X";
        moveCount = 0;
        setPlayerColors(playerX, playerO);
    }

    public void QuitGame()
    {
        SetBoardInteractable(true);
        Application.Quit();
    }

    void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void EndTurn()
    {
        moveCount++;
        // реализация с циклами не имеет преимуществ по производительности
        if (buttonList[0].text == playerSide && buttonList[1].text == playerSide && buttonList[2].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[3].text == playerSide && buttonList[4].text == playerSide && buttonList[5].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[6].text == playerSide && buttonList[7].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[0].text == playerSide && buttonList[3].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[1].text == playerSide && buttonList[4].text == playerSide && buttonList[7].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[2].text == playerSide && buttonList[5].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[0].text == playerSide && buttonList[4].text == playerSide && buttonList[8].text == playerSide)
        {
            GameOver(playerSide);
        }

        else if (buttonList[2].text == playerSide && buttonList[4].text == playerSide && buttonList[6].text == playerSide)
        {
            GameOver(playerSide);
        }
        else if (moveCount >= 9)
        {
            GameOver("draw");
        }
        else ChangeSides();
    }

    void setPlayerColors(Player newPlayer, Player oldPlayer) 
        //поля двух объектов Player не меняются местами, а приравниваются к полям классов-хранилищ цветов
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);

        if (winningPlayer.Equals("draw"))
        {
            setGameOverText("Ничья!");
        }
        else setGameOverText(winningPlayer + " победил!");
        
        gameOverPanel.SetActive(true);
        restartButton.SetActive(true);
    }

    void ChangeSides()
    {
        playerSide = playerSide.Equals("X") ? "O" : "X";
        //цвета меняются уже после фактической смены игоков
        if (playerSide.Equals("X"))
        {
            setPlayerColors(playerX, playerO);
        }
        else
        {
            setPlayerColors(playerO, playerX);
        }
    }

    void setGameOverText(string value)
    {
        gameOverText.text = value;
    }

    public void RestartGame()
    {
        NewGame(); 
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
        
    }

    void SetBoardInteractable(bool toggle) //true активирует игровое поле
    {
        for(int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

}
