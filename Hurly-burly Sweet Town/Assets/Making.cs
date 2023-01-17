using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Making : MonoBehaviour
{
    public GameObject kitchenBack;
    public GameObject maker;
    public GameObject cookingMenu1;
    public GameObject startButton;

    public GameObject bowlBack;
    bool bowlBackStatus;
    public GameObject bowl;
    public GameObject milkButton;
    //public GameObject milk;
    private GameObject filledMilk;
    private GameObject flourButton;
    private GameObject filledFlour;
    private GameObject crackedEgg1;
    private GameObject crackedEgg2;
    private GameObject comingOutEgg;
    private GameObject eggButton;

    bool[] checkIngredients = new bool[3];
    bool readyToMix;
    private GameObject whipper;

    // Start is called before the first frame update
    void Start()
    {
        maker.SetActive(false);
        cookingMenu1.SetActive(false);
        startButton.SetActive(false);
        //Invoke("showMaker", 2);
        //Invoke("showMenu", 3);
        //Invoke("showStart", 3.5f);
        Invoke("showMaker", 0.5f);
        Invoke("showMenu", 0.5f);
        Invoke("showStart", 0.5f);

        bowlBack.SetActive(false);
        bowlBackStatus = false;
        bowl.SetActive(false);
        milkButton.SetActive(false);
        filledMilk = GameObject.Find("filledMilk");
        filledMilk.SetActive(false);
        flourButton = GameObject.Find("flourbutton");
        flourButton.SetActive(false);
        filledFlour = GameObject.Find("filledFlour");
        filledFlour.SetActive(false);
        crackedEgg1 = GameObject.Find("crackedEgg1");
        crackedEgg1.SetActive(false);
        crackedEgg2 = GameObject.Find("crackedEgg2");
        crackedEgg2.SetActive(false);
        comingOutEgg = GameObject.Find("comingOutEgg");
        comingOutEgg.SetActive(false);
        eggButton = GameObject.Find("eggbutton");
        eggButton.SetActive(false);

        readyToMix = false;
        whipper = GameObject.Find("whipper");
        whipper.SetActive(false);

        for(int i=0; i<checkIngredients.Length; i++)
        {
            checkIngredients[i] = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (bowlBackStatus == true)
            Invoke("showBowl", 1);
        clickIngredient();
        Mixing();
    }

    void showMaker()
    {
        maker.SetActive(true);
    }

    void showMenu()
    {
        cookingMenu1.SetActive(true);
    }

    void showStart()
    {
        startButton.SetActive(true);
    }

    void showBowlBack()
    {
        bowlBack.SetActive(true);
        bowlBackStatus = true;
    }

    public void clickStart()
    {
        maker.SetActive(false);
        cookingMenu1.SetActive(false);
        startButton.SetActive(false);
        Invoke("showBowlBack", 0.5f);
    }

    void showBowl()
    {
        bowl.SetActive(true);
    }

    void hideMilkButton()
    {
        milkButton.SetActive(false);
    }

    void hideFlourButton()
    {
        flourButton.SetActive(false);
    }

    void showComingOutEgg()
    {
        comingOutEgg.SetActive(true);
    }

    void hideEggs()
    {
        eggButton.SetActive(false);
        crackedEgg1.SetActive(false);
        crackedEgg2.SetActive(false);
        comingOutEgg.SetActive(false);
    }

    void clickIngredient()
    {
        if(Input.GetMouseButton(0))
        {
            Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.zero);
            RaycastHit2D hit = Physics2D.Raycast(pos, Camera.main.transform.forward, 0f);

            if (hit.collider != null)
            {
                GameObject click_obj = hit.transform.gameObject;
                Debug.Log(click_obj.name);
                if(click_obj.tag == "milk")
                {
                    milkButton.SetActive(true);
                    filledMilk.SetActive(true);
                    Invoke("hideMilkButton", 1f);
                    checkIngredients[0] = true;
                }
                if(click_obj.tag == "flour")
                {
                    flourButton.SetActive(true);
                    filledFlour.SetActive(true);
                    Invoke("hideFlourButton", 1f);
                    checkIngredients[1] = true;
                }
                if (click_obj.tag == "twoEggs")
                {
                    eggButton.SetActive(true);
                    crackedEgg1.SetActive(true);
                    crackedEgg2.SetActive(true);
                    Invoke("showComingOutEgg", 0.5f);
                    Invoke("hideEggs", 1.5f);
                    checkIngredients[2] = true;
                }
            }

            //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;

            //if (Physics.Raycast(ray, out hit))
            //{
            //    Debug.Log(hit.transform.gameObject);
            //    if (hit.transform.gameObject.tag == "milk")
            //        Debug.Log("milk clicked");
            //}
        }
    }

    void showWhipper()
    {
        whipper.SetActive(true);
    }

    void Mixing()
    {
        if (checkIngredients[0] == true && checkIngredients[1] == true && checkIngredients[2] == true)
            readyToMix = true;

        if (readyToMix == true)
        {
            Invoke("showWhipper", 1f);
        }
    }

}
