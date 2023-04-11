using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ranking : MonoBehaviour
{
    public TextMeshProUGUI[] bestScoreText;

    // Start is called before the first frame update
    void Start()
    {

        PlayerPrefs.SetFloat("Money", 0);
        for (int i=0; i<5; i++)
        {
            bestScoreText[i].gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:N2}", PlayerPrefs.GetFloat(i + "BestScore"));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void restart()
    {
        GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum = 1;
        GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum = 0;
        SceneManager.LoadScene("Stage1");
    }

    public void quitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; //play모드를 false로.
#elif UNITY_WEBPLAYER
        Application.OpenURL("http://google.com"); //구글웹으로 전환
#else
        Application.Quit(); //어플리케이션 종료
#endif

    }
}
