using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class typing : MonoBehaviour
{
    bool isTyping = false;

    Vector2 pos;
    float delta = 20.0f;
    float speed = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        pos = transform.Find("Doughnut").position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!isTyping) 
        { 
            StartCoroutine(typingText(transform.Find("LoadingText").GetComponent<TextMeshProUGUI>(),"LOADING..."));
        }

        Vector2 v = pos;
        v.y += delta * Mathf.Sin(Time.time * speed);
        transform.Find("Doughnut").position = v;
    }

    IEnumerator typingText(TextMeshProUGUI MessageText, string message)
    {
        //yield return new WaitForSeconds(1f);
        isTyping = true;

        for (int i = 0; i < message.Length; i++)
        {
            MessageText.text = message.Substring(0, i + 1);

            yield return new WaitForSeconds(0.1f);
        }
        isTyping = false;
    }
}
