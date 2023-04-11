using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameNum : MonoBehaviour
{

    public int StageNum = 1;
    public int OrderNum = 0;

    GameObject firstHeart;
    GameObject secondHeart;
    GameObject thirdHeart;

    public GameObject moneyText;

    List<Dictionary<string, object>> orders;

    Scene scene;

    public static GameNum instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        orders = CSVReader.Read("order");

        PlayerPrefs.SetInt("heartNum", 3);
        PlayerPrefs.Save();
    }

    // Update is called once per frame
    void Update()
    {
        if(moneyText != null)
        {
            getMoney();
        }
        scene = SceneManager.GetActiveScene();
        if (scene.name == "Stage1")
        {
            moneyText = GameObject.Find("Canvas").gameObject.transform.Find("Money").gameObject.transform.Find("moneyText").gameObject;

            firstHeart = GameObject.Find("Canvas").gameObject.transform.Find("firstHeart").gameObject;
            secondHeart = GameObject.Find("Canvas").gameObject.transform.Find("secondHeart").gameObject;
            thirdHeart = GameObject.Find("Canvas").gameObject.transform.Find("thirdHeart").gameObject;

            checkHeart();
        }

    }

    void getMoney()
    {
        //moneyText.GetComponent<TextMeshProUGUI>().text = PlayerPrefs.GetFloat("Money").ToString();
        moneyText.GetComponent<TextMeshProUGUI>().text = string.Format("{0:N2}", PlayerPrefs.GetFloat("Money").ToString());
    }

    void checkHeart()
    {
        if (PlayerPrefs.GetInt("heartNum") == 2)
        {
            thirdHeart.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("heartNum") == 1)
        {
            thirdHeart.gameObject.SetActive(false);
            secondHeart.gameObject.SetActive(false);
        }
        else if (PlayerPrefs.GetInt("heartNum") == 0)
        {
            thirdHeart.gameObject.SetActive(false);
            secondHeart.gameObject.SetActive(false);
            firstHeart.gameObject.SetActive(false);
            SceneManager.LoadScene("GameOver");
        }

    }
}
