using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOver : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
    public void restart()
    {
        setGameNum();

        SceneManager.LoadScene("Stage1");
    }

    public void title()
    {
        setGameNum();

        SceneManager.LoadScene("Title");
    }

    void setGameNum()
    {
        GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum = 1;
        GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum = 0;
    } 
}
