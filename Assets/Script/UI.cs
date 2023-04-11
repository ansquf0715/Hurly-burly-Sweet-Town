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
        if (menuPage != null)
            menuPage.SetActive(false);

        if (GameObject.Find("AudioManager") != null)
        {
            audioManager = GameObject.Find("AudioManager");
            stage1AudioSource = audioManager.GetComponent<AudioSource>();
        }


        musicSlider.value = musicSlider.maxValue;
        //musicSlider.value = stage1AudioSource.volume;
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
        //SceneManager.LoadScene("Title");
        PlayerPrefs.SetInt("heartNum", 3);
        PlayerPrefs.Save();

        setGameNum();

        if (audioManager != null)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().checkTitle();
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().replay();

        }
        SceneManager.LoadScene("Title");
        Time.timeScale = 1;

    }

    public void clickRestart()
    {
        //SceneManager.LoadScene("Stage1Cooking");

        PlayerPrefs.SetInt("heartNum", 3);
        PlayerPrefs.Save();

        setGameNum();
        if (audioManager != null)
        {
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().checkTitle();
            GameObject.Find("AudioManager").GetComponent<AudioManagerScript>().replay();
        }
        SceneManager.LoadScene("Stage1");
        Time.timeScale = 1;

    }

    public void clickQuitGame()
    {
        //Application.Quit();
        PlayerPrefs.SetInt("heartNum", 3);
        PlayerPrefs.Save();

        setGameNum();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //play��带 false��.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //���������� ��ȯ
#else
        Application.Quit(); //���ø����̼� ����
#endif

        Time.timeScale = 1;

    }

    public void ToggleMusic()
    {
        if (backGroundMusicOn) //����������(defult)
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
        stage1AudioSource.volume = musicSlider.value;
    }

    void setGameNum()
    {
        GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum = 1;
        GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum = 0;

        PlayerPrefs.SetFloat("Money", 0);
    }
}
