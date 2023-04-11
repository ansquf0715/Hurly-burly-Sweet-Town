using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerScript : MonoBehaviour
{
    AudioSource audioSource;

    public AudioClip stage1backSound;
    public AudioClip stage2backSound;

    bool isChangeSound = false;

    public static AudioManagerScript instance = null;
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

    //Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.clip = stage1backSound;
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("GameSetting");
        if (obj != null)
        {
            if (isChangeSound == false)
            {
                changeSound();
            }
        }
    }

    void changeSound()
    {
        if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
        {
            audioSource.Stop();
            //audioSource.clip = Resources.Load<AudioClip>("stage2�����");
            audioSource.clip = stage2backSound;
            audioSource.Play();

            isChangeSound = true;
        }

    }

    public void replay()
    {
        audioSource.time = 0;
    }
    public void checkTitle()
    {
        audioSource.Stop();
        audioSource.clip = stage1backSound;
        audioSource.Play();
    }
}