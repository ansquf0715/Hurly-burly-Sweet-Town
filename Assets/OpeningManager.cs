using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Playables;
using UnityEngine.Timeline;



public class OpeningManager : MonoBehaviour
{

    public PlayableDirector playableDirector;
    public TimelineAsset timeline;
    double previousTime;


    

    private void Awake()
    {
            
    }
    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

        if (scene.name == "Title")
        {
            //playableDirector.Stop();
            //playableDirector.time = 0;
            //playableDirector.Play();
            //playableDirector.Play(timeline);
            playableDirector.time = previousTime;
            playableDirector.Resume();
        }
        //Debug.Log(mode);
        //playableDirector.time = 0;
        //playableDirector.Play();
        //Debug.Log(playableDirector.state);
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnDestroy()
    {
        if (SceneManager.GetActiveScene().name == "Title")
        {
            previousTime = playableDirector.time;
            playableDirector.Pause();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetFloat("Money", 0);

        GameObject gameSetting = GameObject.Find("GameSetting");

        if(gameSetting != null)
        {
            Object.DontDestroyOnLoad(gameSetting);
        }
        Destroy(gameSetting);

        playableDirector.Play();
        //playableDirector.Play(timeline);

        if (SceneManager.GetActiveScene().name == "Title")
        {
            playableDirector.Play();
            //PlayerPrefs.SetFloat("Money", 0);
            playableDirector.Play(timeline);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetTime()
    {
        playableDirector.time = 1;
    }

    public void replay()
    {
        playableDirector.time = 0;
        //playableDirector.Play();
       
    }
}
