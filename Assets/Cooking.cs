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

    public GameObject strawberry;
    public GameObject dottedLine;
    public GameObject knife;
    public GameObject halfStrawberry;

    public GameObject banana;
    public GameObject backKnife;
    public GameObject cutBanana;

    public GameObject decoRecipe;
    public GameObject pancakes;
    public GameObject strawberryCup;
    public GameObject blueberryCup;
    public GameObject bananaCup;
    public GameObject whipping;

    public GameObject whipped;
    public GameObject eachStrawberry; //반딸기, 한딸기는 strawberry 쓸 것
    public GameObject eachBlueberry; //3번모두 회전으로 바꾸면 될듯
    public GameObject eachBanana;

    //public GameObject[] eachStrawberry = new GameObject[3];
    //public GameObject[] eachBlueberry = new GameObject[3];
    //public GameObject[] eachBanana = new GameObject[3];

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

    GameObject clonedStrawberry1;
    GameObject clonedStrawberry2;
    GameObject clonedDottedLine;

    GameObject clonedPerfect;
    GameObject clonedPlusUI;
    GameObject clonedKnife;
    GameObject clonedHalfStrawberry1;
    GameObject clonedHalfStrawberry2;

    GameObject clonedBanana;
    GameObject clonedTempLine1;
    GameObject clonedTempLine2;
    GameObject clonedTempLine3;
    GameObject clonedCutBanana;

    GameObject clonedDecoRecipe;
    GameObject clonedPancakes;
    GameObject clonedStrawberryCup;
    GameObject clonedBlueberryCup;
    GameObject clonedBananaCup;
    GameObject clonedWhipping;

    GameObject clonedWhipped;
    List<GameObject> clonedEachStrawberry = new List<GameObject>();
    List<GameObject> clonedEachBlueberry = new List<GameObject>();
    List<GameObject> clonedEachBanana = new List<GameObject>();

 
    bool bowlBack = false; //사용하는지 확인할 것
    bool inductionBack = false;
    bool cuttingBoardBack = false;
    bool decoratingBack = false;

    //bool[] checkIngredients = new bool[3];
    bool checkMilk = false;
    bool checkFlour = false;
    bool checkEgg = false;
    bool isMenu = false;
    bool isPlus = false;

    bool isPerfect = false;
    bool isDough = false;
    bool isThickDough = false;
    int pancakeCount = 0;

    bool cuttingStrawberry = false;
    bool cuttingBanana = false;
    int isLine = 0;
    int isKnife = 1;
    bool isBanana = false;

    bool isDecoRecipe = false;

    Vector2 whipperPos;

    public Slider slTimer;
    //float fSliderBarTime;
    float tempTime = 0; //쓰는지 확인

    float i = 10;

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

        if(cuttingBoardBack == true)
        {
            cutIngredients();
        }

        if(decoratingBack == true)
        {
            Decorating();
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

    void showJustPerfect()
    {
        clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
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

    void getRay()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
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
                    if(isDough==true || isThickDough == true)
                    {
                        changeDoughSize();

                        tempTime += Time.deltaTime;

                        if(tempTime>=1.3f)
                        {
                            //Destroy(clonedFilledBowl, 1.3f);
                            //Invoke("showThickDough", 2f);
                            Destroy(clonedFilledBowl);
                            Destroy(clonedDough);
                            isDough = false;
                            clonedThickDough = Instantiate(thickDough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                            isThickDough = true;
                            tempTime = 0;
                        }
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
                    //clonedDough = Instantiate(dough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                    //isDough = true;

                    if(pancakeCount < 2)
                    {
                        makeDough();

                    }
                }

                if(rayHit.collider.gameObject.tag.Equals("pan"))
                {
                    //var originPos = clonedThickDough.transform.position;
                    //var tempPos = clonedThickDough.transform.position;
                    //tempPos.y += 1000 * Time.deltaTime;
                    //clonedThickDough.transform.position = tempPos;

                    //if(clonedThickDough.transform.position.y >= 2)
                    //{
                    //    //Debug.Log("걸?");
                    //    //clonedThickDough.transform.position = originPos;
                    //    tempPos.y -= 1000 * Time.deltaTime;
                    //    clonedThickDough.transform.position = tempPos;
                    //}

                    if(clonedThickDough != null)
                    {
                        //Debug.Log("pancake Cout" + pancakeCount);
                        pancakeCount++;
                        if(pancakeCount == 1)
                        {
                            showFilledBowl();
                            //makeDough();

                            Destroy(clonedThickDough);
                            isThickDough = false;
                            //clonedDough = Instantiate(dough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                            //isDough = true;

                            //Debug.Log("isDough" + isDough);


                        }
                        else if(pancakeCount == 2)
                        {
                            isThickDough = false;
                            showJustPerfect();
                            Invoke("showCuttingBoardBack", 1f);
                            
                        }
                    }

                }
            }
        }
        if(Input.GetMouseButtonUp(0))
        {
            isDough = false;
        }
    }

    void makeDough()
    {
        clonedDough = Instantiate(dough, new Vector3(0, -0.96f, 0), Quaternion.identity);
        isDough = true;
    }

    void showThickDough()
    {
        clonedThickDough = Instantiate(thickDough, new Vector3(0, -0.96f, 0), Quaternion.identity);
    }

    void changeDoughSize()
    {
        clonedDough.transform.localScale += Time.deltaTime * new Vector3(0.1f, 0.1f, 0);
    }

    void hidePerfect()
    {
        Destroy(clonedPerfect);
    }

    void showCuttingBoardBack()
    {
        //Debug.Log("이거 불러지나?");
        inductionBack = false;
        cuttingBoardBack = true;

        Destroy(clonedPan);
        Destroy(clonedThickDough);
        isThickDough = false;

        backRenderer.sprite = backGrounds[3];
        slTimer.gameObject.SetActive(false);

        Invoke("hidePerfect", 0.7f);

        Invoke("showStrawberry", 1f);

    }

    void showStrawberry()
    {
        clonedStrawberry1 = Instantiate(strawberry, new Vector3(-1.7f, 0, 0), Quaternion.identity);
        clonedStrawberry2 = Instantiate(strawberry, new Vector3(2.13f, 0, 0), Quaternion.identity);
        clonedStrawberry2.transform.localEulerAngles = new Vector3(0, 0, 40);
        cuttingStrawberry = true;

        Invoke("delayLine", 0.5f);

    }

    void cutIngredients()
    {
        if(cuttingStrawberry == true) //딸기 자르는 장면
        {
            if(isLine==1)
            {
                isLine = 0;

                clonedDottedLine = Instantiate(dottedLine, new Vector3(-1.65f, -0.02f, 0), Quaternion.identity);
                clonedDottedLine.transform.localEulerAngles = new Vector3(0, 0, 45);
            }

            if(isLine == 2)
            {
                isLine = 0;

                clonedDottedLine = Instantiate(dottedLine, new Vector3(2.35f, 0.01f, 0), Quaternion.identity);
                clonedDottedLine.transform.localEulerAngles = new Vector3(0, 0, 85);
                isKnife = 2;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
                Ray2D ray = new Ray2D(touchPos, Vector2.zero);
                RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

                if (rayHit.collider != null)
                {
                    //Debug.Log(rayHit.collider.name);
                    //isKnife = true;

                    //if(isKnife == true)
                    if(isKnife==1)
                    {
                        isKnife = 0;
                        clonedKnife = Instantiate(knife, new Vector3(0.27f, 0.01f, 0), Quaternion.identity);
                        Invoke("hideKnife", 1f);
                        Destroy(clonedDottedLine);
                        Destroy(clonedStrawberry1);
                        clonedHalfStrawberry1 = Instantiate(halfStrawberry, new Vector3(-1.7f, 0, 0), Quaternion.identity);
                        isLine = 2;
                    }

                    if(isKnife==2)
                    {
                        isKnife = 0;
                        clonedKnife = Instantiate(knife, new Vector3(4.39f, -0.22f, 0), Quaternion.identity);
                        Invoke("hideKnife", 1f);
                        Destroy(clonedDottedLine);
                        Destroy(clonedStrawberry2);
                        clonedHalfStrawberry2 = Instantiate(halfStrawberry, new Vector3(2.13f, 0, 0), Quaternion.identity);
                        clonedHalfStrawberry2.transform.localEulerAngles = new Vector3(0, 0, 45);
                        Invoke("changeToBanana", 2f);

                        isBanana = true;

                    }
                }
            }
        }

        if(cuttingBanana == true)
        {
            Destroy(clonedHalfStrawberry1);
            Destroy(clonedHalfStrawberry2);

            if(isBanana == true)
            {
                Invoke("showBanana", 1.5f);

                isBanana = false;

                //if (isLine == 1)
                //{
                //    clonedDottedLine = Instantiate(dottedLine, new Vector3(-2, 0, 0), Quaternion.identity);
                //    isKnife = 1;
                //}
            }

            if (isLine == 1)
            {
                isLine = 0;
                clonedDottedLine = Instantiate(dottedLine, new Vector3(-2, 0, 0), Quaternion.identity);
                isKnife = 1;
            }

            if(isLine == 2)
            {
                isLine = 0;
                //clonedDottedLine = Instantiate(dottedLine, new Vector3(0, -0.09f, 0), Quaternion.identity);
                clonedDottedLine = Instantiate(dottedLine, new Vector3(0, 0, 0), Quaternion.identity);
                isKnife = 2;
            }

            if(isLine == 3)
            {
                isLine = 0;
                clonedDottedLine = Instantiate(dottedLine, new Vector3(2, 0, 0), Quaternion.identity);
                isKnife = 3;
            }

            if (Input.GetMouseButtonDown(0))
            {
                Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
                Ray2D ray = new Ray2D(touchPos, Vector2.zero);
                RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

                if(isKnife == 1)
                {
                    isKnife = 0;
                    clonedKnife = Instantiate(backKnife, new Vector3(-1.71f, -1.39f, 0), Quaternion.identity);
                    Invoke("hideKnife", 1f);
                    Destroy(clonedDottedLine);
                    isLine = 2;
                }

                if(isKnife==2)
                {
                    isKnife = 0;
                    clonedKnife = Instantiate(backKnife, new Vector3(0.52f, -1.39f, 0), Quaternion.identity);
                    Invoke("hideKnife", 1f);
                    Destroy(clonedDottedLine);
                    isLine = 3;
                }

                if(isKnife == 3)
                {
                    isKnife = 0;
                    isLine = 0;
                    clonedKnife = Instantiate(backKnife, new Vector3(2.59f, -1.2f, 0), Quaternion.identity);
                    //Invoke("hideKnife", 1f);
                    Destroy(clonedDottedLine);
                    Destroy(clonedBanana);
                    clonedCutBanana = Instantiate(cutBanana, new Vector3(0.31f, -0.33f, 0), Quaternion.identity);
                    //isBanana = false;
                    //showJustPerfect();
                    Invoke("showJustPerfect", 1f);
                    Invoke("showDecoratingBack", 2f);
                }
            }


        }
    }

    void delayLine()
    {
        isLine++;
    }

    void hideKnife()
    {
        Destroy(clonedKnife);
    }

    void changeToBanana()
    {
        cuttingStrawberry = false;
        cuttingBanana = true;
        isBanana = true;
    }

    void showBanana()
    {
        clonedBanana = Instantiate(banana, new Vector3(0.31f, -0.33f, 0), Quaternion.identity);
        //isBanana = false;

        Invoke("showThreeLine", 0.5f);
        Invoke("hideThreeLine", 1f);
    }

    void showThreeLine()
    {
        clonedTempLine1 = Instantiate(dottedLine, new Vector3(-2, 0, 0), Quaternion.identity);
        clonedTempLine2 = Instantiate(dottedLine, new Vector3(0, 0, 0), Quaternion.identity);
        clonedTempLine3 = Instantiate(dottedLine, new Vector3(2, 0, 0), Quaternion.identity);
    }

    void hideThreeLine()
    {
        Destroy(clonedTempLine1);
        Destroy(clonedTempLine2);
        Destroy(clonedTempLine3);

        isLine = 1;
        //Debug.Log("isLine" + isLine);
    }

    void showDecoratingBack()
    {
        cuttingBoardBack = false;
        decoratingBack = true;

        //Destroy(clonedPerfect);
        Invoke("hidePerfect", 0.7f);
        Destroy(clonedCutBanana);
        Destroy(clonedKnife);

        backRenderer.sprite = backGrounds[4];
        clonedPancakes = Instantiate(pancakes, new Vector3(0, 0, 0), Quaternion.identity);
        clonedStrawberryCup = Instantiate(strawberryCup, new Vector3(-6.25f, 2.57f, 0), Quaternion.identity);
        clonedBlueberryCup = Instantiate(blueberryCup, new Vector3(-6.19f, -1.79f, 0), Quaternion.identity);
        clonedBananaCup = Instantiate(bananaCup, new Vector3(6.19f, -1.9f, 0), Quaternion.identity);
        clonedWhipping = Instantiate(whipping, new Vector3(4.88f, 2.57f, 0), Quaternion.identity);

        isDecoRecipe = true;
    }

    void Decorating()
    {
        if(isDecoRecipe==true)
        {
            isDecoRecipe = false;

            Invoke("showDecoRecipe", 1f);
            //Invoke("hideDecoRecipe", 3f);
        }

        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if(rayHit.collider != null)
            {
                if(rayHit.collider.gameObject.tag.Equals("whipping"))
                {
                    clonedWhipped = Instantiate(whipped, new Vector3(0, 0, 0), Quaternion.identity);
                    SpriteRenderer whippedSR = clonedWhipped.GetComponent<SpriteRenderer>();

                    //int i = 10;
                    while (i>0)
                    {
                        //i -= 1;

                        float time = 0;
                        time += Time.deltaTime;
                        if(time == 1)
                        {
                            time = 0;
                            i -= 1;
                        }
                        Color c = whippedSR.color;
                        c.a += 25.5f;
                        whippedSR.color = c;
                        Debug.Log("color " + c.a);
                        //whippedSR.material.color = c;
                    }
                }

                if (rayHit.collider.gameObject.tag.Equals("strawberryCup"))
                {

                }
            }
        }

    }

    void showDecoRecipe()
    {
        clonedDecoRecipe = Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity);
        //changeUIRecipe();

        Invoke("hideDecoRecipe", 2f);

    }

    void hideDecoRecipe()
    {
        Destroy(clonedDecoRecipe);
    }

    void changeUIRecipe()
    {
        Destroy(clonedCookingMenu);
        clonedCookingMenu = Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity);
    }

}
