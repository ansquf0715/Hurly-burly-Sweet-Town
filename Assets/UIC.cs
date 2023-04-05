using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public GameObject bluePlusUI;
    public GameObject menuPage;

    bool menuPageIsOn = false;

    // Start is called before the first frame update
    void Start()
    {
        menuPage.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void clickBluePlus()
    {
        if (menuPageIsOn == false)
            menuPage.SetActive(true);
    }

    public void clickX()
    {
        menuPage.SetActive(false);
    }

    public void clickGoToMain()
    {
        SceneManager.LoadScene("Title");
    }

    public void clickRestart()
    {
        SceneManager.LoadScene("Stage1Cooking");
    }

    public void clickQuitGame()
    {
        Application.Quit();
    }
}
