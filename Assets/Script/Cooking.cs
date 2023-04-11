using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

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
    public GameObject nextButton;

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
    public GameObject eachStrawberry; //�ݵ���, �ѵ���� strawberry �� ��
    public GameObject eachBlueberry; //3����� ȸ������ �ٲٸ� �ɵ�
    public GameObject eachBanana;

    public GameObject firstButton;
    public GameObject secondButton;
    public GameObject thirdButton;

    public GameObject greenButton;
    public GameObject redButton;

    public GameObject whiteCup;
    public GameObject dropCoffee;
    public GameObject kettle;
    public GameObject dropMilk;

    public GameObject topWhiteCup;
    public GameObject heart;

    public GameObject heartCoffee;
    public GameObject syrup;
    public GameObject leaf;
    public GameObject bell;

    public GameObject complete;

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

    GameObject clonedFirstButton;
    GameObject clonedSecondButton;
    GameObject clonedThirdButton;

    GameObject clonedGreenButton;
    GameObject clonedGreenButton2;
    GameObject clonedRedButton;
    GameObject clonedRedButton2;

    GameObject clonedWhiteCup;
    GameObject clonedDropCoffee;
    GameObject clonedKettle;
    GameObject clonedDropMilk;

    GameObject clonedTopWhiteCup;
    GameObject clonedHeart;
    GameObject clonedHeartCoffee;
    GameObject clonedSyrup;
    GameObject clonedLeaf;
    GameObject clonedBell;

    GameObject clonedComplete;
 
    bool bowlBack = false; //����ϴ��� Ȯ���� ��
    bool inductionBack = false;
    bool cuttingBoardBack = false;
    bool decoratingBack = false;
    bool coffeeMachineBack = false;
    bool latteArtBack = false;
    bool deliveryBack = false;

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
    bool decoWhipped = false;
    int decoStrawberryCount = 0;
    int decoBlueberryCount = 0;
    int decoBananaCount = 0;

    bool isWhiteCup = false;
    bool isShot = false;
    bool isHotMilk = false;

    bool drawHeart = false;
    int[] remakePancakes = new int[4]; //[0]:����, [1]:����, [2]:��纣��, [3]:�ٳ���

    Vector2 whipperPos;

    public Slider slTimer;
    //float fSliderBarTime;
    float tempTime = 0; //������ Ȯ��


    List<Dictionary<string, object>> orders;


    // Start is called before the first frame update
    void Start()
    {
        orders = CSVReader.Read("order");

        startButton.SetActive(false);
        blueList.SetActive(false);
        bluePlus.SetActive(false);
        slTimer.gameObject.SetActive(false);
        nextButton.SetActive(false);

        Invoke("showMaker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStart", 2f);

    }

    // Update is called once per frame
    void Update()
    {
        if (bowlBack)
        {
            putIngredients();
        }

        if(inductionBack)
        {
            Timer();
            baking();
        }

        if(cuttingBoardBack)
        {
            cutIngredients();
        }

        if(decoratingBack)
        {
            Decorating();
        }

        if(coffeeMachineBack)
        {
            makeCoffee();
        }

        if(latteArtBack)
        {
            doLatteArt();
        }

        if(deliveryBack)
        {
            deliver();
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
         //clonedCookingMenu = Instantiate(CookingMenu, new Vector3(-4.2622f, 0.0368f, 0), Quaternion.identity);
         clonedCookingMenu = Instantiate(CookingMenu, new Vector3(440, 510, 0), Quaternion.identity,GameObject.Find("Canvas").transform);
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


        if (!isMenu)
        {
            if (clonedCookingMenu == null)
            {
                clonedCookingMenu = Instantiate(CookingMenu, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                clonedCookingMenu.transform.localPosition = new Vector3(0, 0, 0);
            }
            else
            {
                clonedCookingMenu.SetActive(true);
            }

            isMenu = true;

        }
        else if (isMenu)
        {
            if (clonedCookingMenu == null)
            {
                clonedCookingMenu.SetActive(true);
                Destroy(clonedCookingMenu);
            }
            else
            {
                clonedCookingMenu.SetActive(false);
            }
            isMenu = false;
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

                            //Animator animator = GetComponent<Animator>();
                            //if (animator.GetCurrentAnimatorStateInfo(0).IsName("ThickDough")
                            //    && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f)
                            //{
                            //    Destroy(clonedThickDough);
                            //}

                            //isThickDough = true;
                            isThickDough = false;
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
                    //    //Debug.Log("��?");
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
        //Debug.Log("�̰� �ҷ�����?");
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
        if(cuttingStrawberry == true) //���� �ڸ��� ���
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

        nextButton.SetActive(true);
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

                if (rayHit.collider.gameObject.tag.Equals("whipping"))
                {
                    clonedWhipped = Instantiate(whipped, new Vector3(0, 0, 0), Quaternion.identity);
                    decoWhipped = true;
                    remakePancakes[0] = 1;

                    //SpriteRenderer whippedSR = clonedWhipped.GetComponent<SpriteRenderer>();
                    //int i = 10;
                    //while (i>0)
                    //{
                    //    i -= 1;
                    //    Color c = whippedSR.color;
                    //    c.a += 25.5f;
                    //    whippedSR.color = c;
                    //    Debug.Log("color " + c.a);
                    //    //whippedSR.material.color = c;
                    //}
                }

                if (rayHit.collider.gameObject.tag.Equals("strawberryCup"))
                {
                    decoStrawberryCount++;
                    remakePancakes[1]++;

                    if(decoStrawberryCount == 1)
                    {
                        GameObject temp;
                        temp = Instantiate(eachStrawberry, new Vector3(-0.58f, 0.11f, 0), Quaternion.identity);
                        clonedEachStrawberry.Add(temp);

                        clonedFirstButton = Instantiate(firstButton, new Vector3(-3.96f, 3.72f, 0), Quaternion.identity);
                        Invoke("hideNumberButton", 0.5f);
                    }

                    if (decoStrawberryCount == 2)
                    {
                        GameObject temp;
                        SpriteRenderer sr = null;

                        temp = Instantiate(strawberry, new Vector3(0.43f, 0.16f, 0), Quaternion.identity);
                        temp.transform.localEulerAngles = new Vector3(0, 0, -90);
                        temp.transform.localScale = new Vector3(0.15f, 0.15f, 1);

                        sr = temp.GetComponent<SpriteRenderer>();
                        sr.sortingOrder = 8;
                        
                        clonedEachStrawberry.Add(temp);
                        clonedSecondButton = Instantiate(secondButton, new Vector3(-3.96f, 3.72f, 0), Quaternion.identity);
                        Invoke("hideNumberButton", 0.5f);

                    }

                    if (decoStrawberryCount == 3)
                    {
                        GameObject temp;
                        temp = Instantiate(eachStrawberry, new Vector3(0.59f, 0.08f, 0), Quaternion.identity);
                        temp.transform.localEulerAngles = new Vector3(0, 0, -85.96f);
                        clonedEachStrawberry.Add(temp);
                        clonedEachStrawberry[0].transform.position = new Vector3(-0.9f, 0.11f, 0);
                        clonedEachStrawberry[1].transform.position = new Vector3(-0.07f, 0.18f, 0);

                        clonedThirdButton = Instantiate(thirdButton, new Vector3(3.96f, 3.72f, 0), Quaternion.identity);
                        Invoke("hideNumberButton", 0.5f);
                    }
                }

                if(rayHit.collider.gameObject.tag.Equals("blueberryCup"))
                {
                    decoBlueberryCount++;
                    remakePancakes[2]++;

                    if (decoBlueberryCount == 1)
                    {
                        GameObject temp;
                        temp = Instantiate(eachBlueberry, new Vector3(-0.91f, 0.47f, 0), Quaternion.identity);
                        clonedEachBlueberry.Add(temp);

                        clonedFirstButton = Instantiate(firstButton, new Vector3(-4.12f, -0.51f, 0), Quaternion.identity);
                        Invoke("hideNumberButton", 0.5f);
                    }

                    if (decoBlueberryCount == 2)
                    {
                        GameObject temp;
                        temp = Instantiate(eachBlueberry, new Vector3(0.65f, 0.7f, 0), Quaternion.identity);
                        temp.transform.localEulerAngles = new Vector3(0, 0, -181.34f);
                        clonedEachBlueberry.Add(temp);

                        clonedSecondButton = Instantiate(secondButton, new Vector3(-4.12f, -0.51f, 0), Quaternion.identity);
                        Invoke("hideNumberButton", 0.5f);
                    }

                    if (decoBlueberryCount == 3)
                    {
                        GameObject temp;
                        SpriteRenderer sr = null;

                        temp = Instantiate(eachBlueberry, new Vector3(-0.21f, -0.45f, 0), Quaternion.identity);
                        temp.transform.localEulerAngles = new Vector3(0, 0, 158.51f);

                        sr = temp.GetComponent<SpriteRenderer>();
                        sr.sortingOrder = 6;

                        clonedEachBlueberry.Add(temp);

                        clonedThirdButton = Instantiate(thirdButton, new Vector3(-4.12f, -0.51f, 0), Quaternion.identity);
                        Invoke("hideNumberButton", 0.5f);
                    }
                }

                if(rayHit.collider.gameObject.tag.Equals("bananaCup"))
                {
                    decoBananaCount++;
                    remakePancakes[3]++;

                    if (decoBananaCount == 1)
                    {
                        GameObject temp;
                        temp = Instantiate(eachBanana, new Vector3(-0.05f, 0.73f, 0), Quaternion.identity);
                        clonedEachBanana.Add(temp);

                        clonedFirstButton = Instantiate(firstButton, new Vector3(5.03f, -0.34f, 0), Quaternion.identity);
                        clonedFirstButton.transform.localEulerAngles = new Vector3(0, 180, 0);
                        Invoke("hideNumberButton", 0.5f);

                    }

                    if (decoBananaCount == 2)
                    {
                        GameObject temp;
                        temp = Instantiate(eachBanana, new Vector3(-0.76f, -0.57f, 0), Quaternion.identity);
                        clonedEachBanana.Add(temp);

                        clonedSecondButton = Instantiate(secondButton, new Vector3(5.03f, -0.34f, 0), Quaternion.identity);
                        clonedSecondButton.transform.localEulerAngles = new Vector3(0, 180, 0);
                        Invoke("hideNumberButton", 0.5f);
                    }

                    if(decoBananaCount == 3)
                    {
                        GameObject temp;
                        temp = Instantiate(eachBanana, new Vector3(0.65f, -0.54f, 0), Quaternion.identity);
                        temp.transform.localEulerAngles = new Vector3(0, 0, -18.65f);
                        clonedEachBanana.Add(temp);

                        clonedThirdButton = Instantiate(thirdButton, new Vector3(5.03f, -0.34f, 0), Quaternion.identity);
                        clonedThirdButton.transform.localEulerAngles = new Vector3(0, 180, 0);
                        Invoke("hideNumberButton", 0.5f);
                    }
                }
            }
        }

    }

    void showDecoRecipe()
    {
        clonedDecoRecipe = Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        clonedDecoRecipe.transform.localPosition = new Vector3(0, 0, 0);
        changeUIRecipe();

        Invoke("hideDecoRecipe", 2f);
    }

    void hideDecoRecipe()
    {
        //Debug.Log("����");
        Destroy(clonedDecoRecipe);
    }

    void changeUIRecipe()
    {
        Destroy(clonedCookingMenu);
        //clonedCookingMenu = Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity);
        clonedCookingMenu= Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        clonedCookingMenu.transform.localPosition = new Vector3(0, 0, 0);
        clonedCookingMenu.SetActive(false);
    }

    void hideNumberButton()
    {
        if(clonedFirstButton != null)
        {
            Destroy(clonedFirstButton);
        }
        else if(clonedSecondButton != null)
        {
            Destroy(clonedSecondButton);
        }
        else if(clonedThirdButton != null)
        {
            Destroy(clonedThirdButton);
        }
    }

    public void showCoffeMachineBack()
    {
        decoratingBack = false;
        coffeeMachineBack = true;

        Destroy(clonedStrawberryCup);
        Destroy(clonedBlueberryCup);
        Destroy(clonedBananaCup);
        Destroy(clonedWhipping);
        Destroy(clonedPancakes);
        Destroy(clonedWhipped);
        for (int i = 0; i < clonedEachStrawberry.Count; i++)
            Destroy(clonedEachStrawberry[i]);
        for (int i = 0; i < clonedEachBlueberry.Count; i++)
            Destroy(clonedEachBlueberry[i]);
        for (int i = 0; i < clonedEachBanana.Count; i++)
            Destroy(clonedEachBanana[i]);

        backRenderer.sprite = backGrounds[5];
        nextButton.SetActive(false);
        clonedGreenButton = Instantiate(greenButton, new Vector3(-1.86f, 3.78f, 0), Quaternion.identity);
        clonedRedButton = Instantiate(redButton, new Vector3(-3.2f, 3.78f, 0), Quaternion.identity);

        clonedGreenButton2 = Instantiate(greenButton, new Vector3(2.97f, 3.78f, 0), Quaternion.identity);
        clonedGreenButton2.gameObject.tag = "greenButton2";
        clonedRedButton2 = Instantiate(redButton, new Vector3(1.6f, 3.78f, 0), Quaternion.identity);
        clonedRedButton2.gameObject.tag = "redButton2";

        isWhiteCup = true;
    }

    void makeCoffee()
    {
        if(isWhiteCup == true)
        {
            Invoke("showWhiteCup", 1f);
            isWhiteCup = false;
        }
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("greenButton"))
                {
                    //0.7038139
                    clonedDropCoffee = Instantiate(dropCoffee, new Vector3(-1.7094f, 1.307f, 0), Quaternion.identity);
                    //clonedDropCoffee.transform.localScale += Time.deltaTime * new Vector3(0, 3f, 0);
                    isShot = true;
                }

                if (rayHit.collider.gameObject.tag.Equals("redButton"))
                {

                }

                if(rayHit.collider.gameObject.tag.Equals("greenButton2"))
                {
                    //Debug.Log("�ǳ�");
                    clonedDropMilk = Instantiate(dropMilk, new Vector3(1.47f, 1.35f, 0), Quaternion.identity);
                    isHotMilk = true;
                }
            }
        }
        if(isShot == true && isHotMilk == true)
        {
            isShot = false;
            isHotMilk = false;

            Invoke("showLatteArt", 1f);
        }
    }
    void showWhiteCup()
    {
        clonedWhiteCup = Instantiate(whiteCup, new Vector3(-1.58f, -1.3f, 0), Quaternion.identity);
        clonedKettle = Instantiate(kettle, new Vector3(1.53f, -0.61f, 0), Quaternion.identity);
    }

    void showLatteArt()
    {
        coffeeMachineBack = false;
        latteArtBack = true;

        backRenderer.sprite = backGrounds[6];

        Destroy(clonedGreenButton);
        Destroy(clonedGreenButton2);
        Destroy(clonedRedButton);
        Destroy(clonedRedButton2);
        Destroy(clonedDropCoffee);
        Destroy(clonedDropMilk);
        Destroy(clonedWhiteCup);
        Destroy(clonedKettle);

        isPerfect = false;

        clonedTopWhiteCup = Instantiate(topWhiteCup, new Vector3(-2.98f, 0.13f, 0), Quaternion.identity);
        Invoke("showKettle", 1f);
    }

    void showKettle()
    {
        clonedKettle = Instantiate(kettle, new Vector3(4.23f, -0.18f, 0), Quaternion.identity);
        clonedKettle.transform.localScale = new Vector3(0.26f, 0.26f, 1);

        Invoke("showHeart", 0.5f);
    }

    void showHeart()
    {
        clonedHeart = Instantiate(heart, new Vector3(-3.59f, -0.26f, 0), Quaternion.identity);
    }

    void doLatteArt()
    {

        if (drawHeart)
        {
            drawHeart = false;
            Invoke("delayPerfect", 1f);
            //Debug.Log("draw heart " + drawHeart);
            //isPerfect = true;
            //Invoke("showJustPerfect", 1f);
            //Invoke("hidePerfect", 2f);
        }

        if (isPerfect == true)
        {
            isPerfect = false;

            showJustPerfect();
            //Debug.Log("show perfect");
            Invoke("hidePerfect", 1f);
            CancelInvoke("delayPerfect");
            Invoke("showDeliveryBack", 1f);
        }

        if (Input.GetMouseButton(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayHit.collider != null)
            {
                if(rayHit.collider.gameObject.tag.Equals("kettle"))
                {
                    checkHeart();
                }
            }
        }
    }

    void checkHeart()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        clonedKettle.transform.position = objPosition;

        if (objPosition.x >= -6 && objPosition.x <= -1.3
            && objPosition.y >= -2.3 && objPosition.y <= 1.8)
        {
            //clonedKettle.transform.position = objPosition;
            drawHeart = true;
        }
    }

    void showDeliveryBack()
    {
        latteArtBack = false;
        deliveryBack = true;

        Destroy(clonedKettle);
        Destroy(clonedTopWhiteCup);
        Destroy(clonedHeart);

        backRenderer.sprite = backGrounds[7];

        clonedBell = Instantiate(bell, new Vector3(-5.65f, 2.27f, 0), Quaternion.identity);

        Invoke("showCompleteCoffee", 1f);
        Invoke("showCompletePancakes", 1f);

    }

    void showCompleteCoffee()
    {
        clonedHeartCoffee = Instantiate(heartCoffee, new Vector3(5.9f, 1.62f, 0), Quaternion.identity);
    }

    void showCompletePancakes()
    {

        clonedEachStrawberry.Clear();
        clonedEachBlueberry.Clear();
        clonedEachBanana.Clear();

        //SpriteRenderer whippedSr = null;
        //SpriteRenderer strawberrySr = null;
        //SpriteRenderer halfStrawberrySr = null;
        //SpriteRenderer bananaSr = null;
        //SpriteRenderer blueberrySr = null;

        //whippedSr = clonedWhipped.GetComponent<SpriteRenderer>();
        //whippedSr.sortingOrder = 4;


        clonedPancakes = Instantiate(pancakes, new Vector3(-0.17f, -0.47f, 0), Quaternion.identity);
        clonedPancakes.transform.localScale = new Vector3(0.16f, 0.16f, 1);

        //sr = clonedPancakes.GetComponent<SpriteRenderer>();
        //sr.sortingOrder = 4;
        
        clonedSyrup = Instantiate(syrup, new Vector3(-0.16f, -0.35f, 0), Quaternion.identity);

        clonedLeaf = Instantiate(leaf, new Vector3(-0.14f, -0.28f, 0), Quaternion.identity);

        //����
        if(remakePancakes[0] == 1)
        {
            clonedWhipped = Instantiate(whipped, new Vector3(-0.07f, -0.42f, 0), Quaternion.identity);
            clonedWhipped.transform.localScale = new Vector3(1.1f, 1.1f, 0);
        }

        //����1��
        if(remakePancakes[1] == 1)
        {
            //SpriteRenderer sr = null;
            GameObject temp;
            temp = Instantiate(halfStrawberry, new Vector3(-0.1f, -0.42f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.15f, 0.15f, 0);
            //sr = temp.GetComponent<SpriteRenderer>();
            //sr.sortingOrder = 6;
            clonedEachStrawberry.Add(temp);
        }
        //����2��
        if (remakePancakes[1] ==2)
        {
            SpriteRenderer sr = null;

            clonedEachStrawberry.Clear();
            GameObject temp;
            temp = Instantiate(halfStrawberry, new Vector3(-0.56f, -0.42f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.15f, 0.15f, 0);

            clonedEachStrawberry.Add(temp);

            temp = Instantiate(strawberry, new Vector3(0.11f, -0.42f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.12f, 0.12f, 0);
            sr = temp.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 8;
            clonedEachStrawberry.Add(temp);
        }
        //����3��
        if (remakePancakes[1]==3)
        {
            clonedEachStrawberry.Clear();
            SpriteRenderer sr = null;
            GameObject temp;
            temp = Instantiate(halfStrawberry, new Vector3(-0.78f, -0.45f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.15f, 0.15f, 1);
            clonedEachStrawberry.Add(temp);

            temp = Instantiate(strawberry, new Vector3(-0.11f, -0.42f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.12f, 0.12f, 1);
            sr = temp.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 8;
            clonedEachStrawberry.Add(temp);

            temp = Instantiate(halfStrawberry, new Vector3(0.51f, -0.45f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.15f, 0.15f, 1);
            clonedEachStrawberry.Add(temp);
        }

        //��纣��1��
        if(remakePancakes[2]==1)
        {
            clonedEachBlueberry.Clear();
            GameObject temp;

            temp = Instantiate(eachBlueberry, new Vector3(-0.2f, -0.97f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            temp.transform.localEulerAngles = new Vector3(0, 180f, 38.67f);
            clonedEachBlueberry.Add(temp);
        }
        //��纣��2��
        if (remakePancakes[2]==2)
        {
            clonedEachBlueberry.Clear();
            GameObject temp;

            temp = Instantiate(eachBlueberry, new Vector3(0.08f, -1f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            temp.transform.localEulerAngles = new Vector3(0, 180f, 38.67f);
            clonedEachBlueberry.Add(temp);

            temp = Instantiate(eachBlueberry, new Vector3(-0.43f, -0.03f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            temp.transform.localEulerAngles = new Vector3(0, 0, -19.03f);
            clonedEachBlueberry.Add(temp);
        }
        //��纣��3��
        if(remakePancakes[2]==3)
        {
            clonedEachBlueberry.Clear();
            GameObject temp;

            temp = Instantiate(eachBlueberry, new Vector3(-0.52f, -0.05f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            temp.transform.localEulerAngles = new Vector3(0, 0, -19.03f);
            clonedEachBlueberry.Add(temp);

            temp = Instantiate(eachBlueberry, new Vector3(0.21f, -0.01f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            temp.transform.localEulerAngles = new Vector3(0, 0, -105.16f);
            clonedEachBlueberry.Add(temp);

            temp = Instantiate(eachBlueberry, new Vector3(-0.22f, -1f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            temp.transform.localEulerAngles = new Vector3(0, 180, 38.67f);
            clonedEachBlueberry.Add(temp);
        }

        //�ٳ���1��
        if(remakePancakes[3]==1)
        {
            //SpriteRenderer sr = null;
            clonedEachBanana.Clear();
            GameObject temp;

            temp = Instantiate(eachBanana, new Vector3(-0.12f, 0.24f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            //sr = temp.GetComponent<SpriteRenderer>();
            //sr.sortingOrder = 
            clonedEachBanana.Add(temp);
        }
        //�ٳ���2��
        if (remakePancakes[3]==2)
        {
            clonedEachBanana.Clear();
            GameObject temp;

            temp = Instantiate(eachBanana, new Vector3(-0.67f, -0.99f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            clonedEachBanana.Add(temp);

            temp = Instantiate(eachBanana, new Vector3(0.41f, -0.97f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            clonedEachBanana.Add(temp);
        }
        //�ٳ���3��
        if (remakePancakes[3]==3)
        {
            clonedEachBanana.Clear();
            GameObject temp;

            temp = Instantiate(eachBanana, new Vector3(-0.67f, -0.99f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            clonedEachBanana.Add(temp);

            temp = Instantiate(eachBanana, new Vector3(0.41f, -0.97f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            clonedEachBanana.Add(temp);

            temp = Instantiate(eachBanana, new Vector3(-0.14f, 0.24f, 0), Quaternion.identity);
            temp.transform.localScale = new Vector3(0.3f, 0.3f, 1);
            clonedEachBanana.Add(temp);
        }
    }

    void deliver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
            
            int index = FindIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);

            if (rayHit.collider != null)
            {
                if(rayHit.collider.gameObject.tag.Equals("bell"))
                {
                    Invoke("showComplete", 0.5f);
                    Debug.Log(checkDecoCount());

                    if (!checkDecoCount())
                    {
                        PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") - float.Parse(orders[index]["TotalPrice"].ToString()) / 2);
                    }
                }
            }
        }
    }

    void showComplete()
    {
        clonedComplete = Instantiate(complete, new Vector3(0, 0, 0), Quaternion.identity);

        GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum++;
        if(GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum == 3)
        {
            if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
            {
                Invoke("LoadStage2Scene", 1f);
            }

            GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum++;
            GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum = 0;

        }
        Invoke("LoadStage1Scene", 1f);
    }

    void LoadStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }
    void LoadStage2Scene()
    {
      SceneManager.LoadScene("Stage2");
    }

    bool checkDecoCount()
    {        
        if (remakePancakes[0] != 1) return false;
        else if (remakePancakes[1] != 2) return false;
        else if (remakePancakes[2] != 3) return false;
        else if (remakePancakes[3] != 3) return false;

        return true;
    }

    public int FindIndex(int stage, int orderNum)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i]["stage"].ToString() == stage.ToString()
                && orders[i]["orderNum"].ToString() == orderNum.ToString())
            {
                //Debug.Log(i);
                return i;
            }
        }
        return -1;
    }
}
