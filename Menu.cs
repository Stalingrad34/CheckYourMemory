using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
   public void ToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
    public void StartGame()
    {
        SceneManager.LoadScene("Game");

    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChoosePlayCards(int number)
    {
        if (number == 1)
        {
            GamePlay.numberOfCards = 6;
            
        }

        if (number == 2)
        {
            GamePlay.numberOfCards = 12;
            
        }

        if (number == 3)
        {
            GamePlay.numberOfCards = 18;
            
        }

        

    }
}
