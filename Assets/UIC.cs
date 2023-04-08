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

    bool backGroundMusicOn = true;

    GameObject audioManager;
    AudioSource stage1AudioSource;

    public GameObject musicOnOffButton;
    public Image musicOnOffButtonImage;
    public Sprite musicOnSprite;
    public Sprite musicOffSprite;

    public Slider musicSlider;

    // Start is called before the first frame update
    void Start()
    {
        menuPage.SetActive(false);

        audioManager = GameObject.Find("AudioManager");
        stage1AudioSource = audioManager.GetComponent<AudioSource>();

        //musicSlider.value = musicSlider.maxValue;
        musicSlider.value = stage1AudioSource.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
        {
            audioSource.clip = Resources.Load<AudioClip>("stage2배경음");
        }
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

    public void ToggleMusic()
    {
        if (backGroundMusicOn) //켜져있으면(defult)
        {
            backGroundMusicOn = false;
            stage1AudioSource.volume = 0f;
            musicOnOffButtonImage.sprite = musicOffSprite;
        }
        else
        {
            backGroundMusicOn = true;
            stage1AudioSource.volume = 1f;
            musicOnOffButtonImage.sprite = musicOnSprite;
        }
    }

    public void BackVolume(float volume)
    {
        stage1AudioSource.volume = volume;
    }
}
