using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cooking : MonoBehaviour //, IDragHandler
{
    public Sprite[] backGrounds = new Sprite[3];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;
    public GameObject maker;
    public GameObject CookingMenu;
    public GameObject startButton;
    public GameObject blueList;
    public GameObject bluePlus;
    public GameObject bluePlusUI;

    public GameObject milk;
    public GameObject flour;
    public GameObject eggs;
    public GameObject asset1;
    public GameObject asset2;
    public GameObject bowl;
    public GameObject filledMilk;
    public GameObject filledFlour;
    public GameObject crackedEgg;
    public GameObject insideEgg;
    public GameObject milkButton;
    public GameObject flourButton;
    public GameObject eggButton;

    public GameObject whipper;

    public GameObject perfect;

    public GameObject pan;
    public GameObject filledBowl;
    public GameObject dough;
    public GameObject thickDough;

    GameObject clonedMaker;
    GameObject clonedCookingMenu;
    GameObject clonedMilk;
    GameObject clonedFlour;
    GameObject clonedEggs;
    GameObject clonedAsset1;
    GameObject clonedAsset2;
    GameObject clonedBowl;
    GameObject clonedFilledMilk;
    GameObject clonedFilledFlour;
    GameObject clonedCrackedEgg;
    GameObject clonedInsideEgg;
    GameObject clonedMilkButton;
    GameObject clonedFlourButton;
    GameObject clonedEggButton;

    GameObject clonedWhipper;

    GameObject clonedPan;
    GameObject clonedFilledBowl;
    GameObject clonedDough;
    GameObject clonedThickDough;

    GameObject clonedPerfect;
    GameObject clonedPlusUI;

    bool bowlBack = false; //사용하는지 확인할 것
    bool inductionBack = false;

    //bool[] checkIngredients = new bool[3];
    bool checkMilk = false;
    bool checkFlour = false;
    bool checkEgg = false;
    bool isMenu = false;
    bool isPlus = false;

    bool isPerfect = false;
    bool isDough = false;

    Vector2 whipperPos;

    public Slider slTimer;
    //float fSliderBarTime;

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
        blueList.SetActive(false);
        bluePlus.SetActive(false);
        slTimer.gameObject.SetActive(false);

        Invoke("showMaker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStart", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        if (bowlBack == true)
        {
            putIngredients();
        }

        if(inductionBack == true)
        {
            Timer();
            baking();
        }
    }

    void Timer()
    {
        if (slTimer.value > 0.0f)
            slTimer.value -= Time.deltaTime;
        else
            Debug.Log("Time is zero");
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

    public void clickBlueList()
    {
        if(isMenu == false)
        {
            clonedCookingMenu = Instantiate(CookingMenu, new Vector3(0, 0, 0), Quaternion.identity);
            isMenu = true;
        }
        else if (isMenu == true)
        {
            isMenu = false;
            Destroy(clonedCookingMenu);
        }
    }

    public void clickBluePlus()
    {
        if(isPlus == false)
        {
            clonedPlusUI = Instantiate(bluePlusUI, new Vector3(0, 0, 0), Quaternion.identity);
            isPlus = true;
        }
        else if(isPlus==true)
        {
            isPlus = false;
            Destroy(clonedPlusUI);
        }
    }

    void showBowl()
    {
        clonedBowl = Instantiate(bowl, new Vector3(0.18f, -1.4f, 0), Quaternion.identity);
    }

    void showBowlBack()
    {
        backRenderer.sprite = backGrounds[1];
        blueList.SetActive(true);
        bluePlus.SetActive(true);

        clonedMilk = Instantiate(milk, new Vector3(-8.59f, -0.36f, 0), Quaternion.identity);
        clonedFlour = Instantiate(flour, new Vector3(-7.23f, 0.69f, 0), Quaternion.identity);
        clonedAsset1 = Instantiate(asset1, new Vector3(6.58f, -0.92f, 0), Quaternion.identity);
        clonedAsset2 = Instantiate(asset2, new Vector3(7.4654f, -0.9054f, 0), Quaternion.identity);
        clonedEggs = Instantiate(eggs, new Vector3(5.67f, -2.34f, 0), Quaternion.identity);
        bowlBack = true;
        Invoke("showBowl", 0.5f);
    }



    void showInsideEgg()
    {
        clonedInsideEgg = Instantiate(insideEgg, new Vector3(0, 1.4761f, 0), Quaternion.identity);
        Destroy(clonedInsideEgg, 1f);
    }

    void putIngredients()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
            //RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("milk"))
                {
                    //checkIngredients[0] = true;
                    checkMilk = true;
                    clonedFilledMilk = Instantiate(filledMilk, new Vector3(0.1679f, -2.1505f, 0), Quaternion.identity);
                    clonedMilkButton = Instantiate(milkButton, new Vector3(4.55f, -1.35f, 0), Quaternion.identity);
                    Destroy(clonedMilkButton, 1f);
                }
                if (rayHit.collider.gameObject.tag.Equals("flour"))
                {
                    //checkIngredients[1] = true;
                    checkFlour = true;
                    clonedFilledFlour = Instantiate(filledFlour, new Vector3(0.2046f, -0.9852f, 0), Quaternion.identity);
                    clonedFlourButton = Instantiate(flourButton, new Vector3(4.55f, -1.35f, 0), Quaternion.identity);
                    Destroy(clonedFlourButton, 1f);
                }
                if (rayHit.collider.gameObject.tag.Equals("egg"))
                {
                    //checkIngredients[2] = true;
                    checkEgg = true;
                    clonedCrackedEgg = Instantiate(crackedEgg, new Vector3(1.47f, 3.24f, 0), Quaternion.identity);
                    clonedEggButton = Instantiate(eggButton, new Vector3(3.85f, 1.83f, 0), Quaternion.identity);
                    Invoke("showInsideEgg", 0.5f);

                    Destroy(clonedCrackedEgg, 1.5f);
                    Destroy(clonedEggButton, 1.5f);
                }

            }
        }
        if(Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayHit.collider.gameObject.tag.Equals("whipper"))
            {
                OnMouseDrag();
                //Invoke("delayPerfect", 3f);
                Invoke("delayPerfect", 1f);

                if (isPerfect == true)
                {
                    showPerfect();
                    CancelInvoke("delayPerfect");
                }
            }
        }

        if(checkMilk && checkFlour&& checkEgg)
        {
            Invoke("showWhipper", 1f);
            checkMilk = false;
            checkFlour = false;
            checkEgg = false;
        }
    }

    void delayPerfect()
    {
        isPerfect = true;
    }

    void showWhipper()
    {
        clonedWhipper = Instantiate(whipper, new Vector3(2.07f, 1.3582f, 0), Quaternion.identity);
    }

    void showPerfect()
    {
        clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
        isPerfect = false;
        Invoke("showInductionBack", 0.5f);
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if(objPosition.x >= -0.95 && objPosition.x <= 3.49
            && objPosition.y >=0.16 && objPosition.y <= 1.47)
        {
            clonedWhipper.transform.position = objPosition;
        }
    }
    void showInductionBack()
    {
        bowlBack = false;
        inductionBack = true;

        Destroy(clonedMilk);
        Destroy(clonedFlour);
        Destroy(clonedEggs);
        Destroy(clonedAsset1);
        Destroy(clonedAsset2);
        Destroy(clonedWhipper);
        Destroy(clonedBowl);
        Destroy(clonedPerfect);
        Destroy(clonedFilledFlour);
        Destroy(clonedFilledMilk);

        backRenderer.sprite = backGrounds[2];
        slTimer.gameObject.SetActive(true);

        Invoke("showPan", 1f);
    }

    void showPan()
    {
        clonedPan = Instantiate(pan, new Vector3(-1.21f, -0.7f, 0), Quaternion.identity);
        Invoke("showFilledBowl", 1f);
    }
    
    void showFilledBowl()
    {
        clonedFilledBowl = Instantiate(filledBowl, new Vector3(4.58f, 1.45f, 0), Quaternion.identity);
    }

    void baking()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("bowl"))
                {
                    clonedFilledBowl.transform.Rotate(Vector3.forward * Time.deltaTime * 15f);
                    if(isDough==true)
                    {
                        changeDoughSize();
                        Destroy(clonedFilledBowl, 1.3f);
                        clonedThickDough = Instantiate(thickDough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                    }
                }
            }
        }
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if(rayHit.collider != null)
            {
                if(rayHit.collider.gameObject.tag.Equals("bowl"))
                {
                    clonedDough = Instantiate(dough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                    isDough = true;

                    //clonedDough.transform.localScale += new Vector3(0.2f, 0.2f, 0);

                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isDough = false;
        }
    }

    void changeDoughSize()
    {
        //Debug.Log("change Dough Size");
        //Vector2 originScale = clonedDough.transform.localScale;
        //float time = 0;
        //float speed = 1;

        //clonedDough.transform.localScale = originScale * (1f + time * speed);
        //time += Time.deltaTime;

        clonedDough.transform.localScale += Time.deltaTime * new Vector3(0.1f, 0.1f, 0);
    }

}
