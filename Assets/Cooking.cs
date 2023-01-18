using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooking : MonoBehaviour
{
    public Sprite[] backGrounds = new Sprite[2];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;
    public GameObject maker;
    public GameObject CookingMenu;
    public GameObject startButton;

    public GameObject milk;
    public GameObject flour;
    public GameObject asset1;
    public GameObject asset2;

    GameObject clonedMaker;
    GameObject clonedCookingMenu;
    GameObject clonedMilk;
    GameObject clonedFlour;
    GameObject clonedAsset1;
    GameObject clonedAsset2;

    // Start is called before the first frame update
    void Start()
    {
        //backRenderer = GetComponent<SpriteRenderer>();
        startButton.SetActive(false);
        Invoke("showMaker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStart", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void showMaker()
    {
        clonedMaker = Instantiate(maker, new Vector3(3.820f, -1.48f, 0), Quaternion.identity);
    }

    void showMenu()
    {
         clonedCookingMenu = Instantiate(CookingMenu, new Vector3(-4.2622f, 0.0368f, 0), Quaternion.identity);
    }

    void showStart()
    {
        startButton.SetActive(true);
    }

    public void clickStart()
    {
        startButton.SetActive(false);
        Destroy(clonedMaker);
        Destroy(clonedCookingMenu);
        Invoke("showBowlBack", 1f);
    }

    void showBowlBack()
    {
        backRenderer.sprite = backGrounds[1];
        clonedMilk = Instantiate(milk, new Vector3(-8.59f, -0.36f, 0), Quaternion.identity);
        clonedFlour = Instantiate(flour, new Vector3(-7.23f, 0.69f, 0), Quaternion.identity);
        clonedAsset1 = Instantiate(asset1, new Vector3(6.58f, -0.92f, 0), Quaternion.identity);
        clonedAsset2 = Instantiate(asset2, new Vector3(7.4654f, -0.9054f, 0), Quaternion.identity);
    }
}
