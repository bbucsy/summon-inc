using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public GameObject howToPage1;
    public GameObject howToPage2;
    
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void ShowHowTo()
    {
        howToPage1.SetActive(true);
    }

    public void ShowNextPage()
    {
        howToPage2.SetActive(true);
    }
    
    public void CloseHowTo()
    {
        howToPage1.SetActive(false);
        howToPage2.SetActive(false);
    }
}
