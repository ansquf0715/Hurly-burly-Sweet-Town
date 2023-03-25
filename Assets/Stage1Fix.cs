using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Stage1Fix : MonoBehaviour
{

    public Sprite[] backGrounds = new Sprite[3];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;

    List<Dictionary<string, object>> orders;
    List<GameObject> toDestroy = new List<GameObject>();

    public GameObject blueList;
    public GameObject bluePlus;
    public GameObject bluePlusUI;
    public GameObject nextButton;
    public GameObject startButton;
    public Slider slTimer;

    public GameObject maker;
    //GameObject clonedMaker;
    public GameObject CookingMenu;
    //GameObject clonedCookingMenu;

    public GameObject perfect;
    GameObject clonedPerfect;

    public GameObject milk;
    GameObject clonedMilk;
    public GameObject flour;
    GameObject clonedFlour;
    public GameObject eggs;
    GameObject clonedEggs;
    public GameObject asset1;
    //GameObject clonedAsset1;
    public GameObject asset2;
    //GameObject clonedAsset2;
    public GameObject bowl;
    GameObject clonedBowl;
    public GameObject filledMilk;
    GameObject clonedFilledMilk;
    public GameObject filledFlour;
    GameObject clonedFilledFlour;
    public GameObject crackedEgg;
    GameObject clonedCrackedEgg;
    public GameObject insideEgg;
    GameObject clonedInsideEgg;
    public GameObject milkButton;
    GameObject clonedMilkButton;
    public GameObject flourButton;
    GameObject clonedFlourButton;
    public GameObject eggButton;
    GameObject clonedEggButton;
    public GameObject whipper;
    GameObject clonedWhipper;

    public GameObject pan;
    GameObject clonedPan;
    public GameObject filledBowl;
    GameObject clonedFilledBowl;
    public GameObject dough;
    GameObject clonedDough;
    public GameObject thickDough;
    GameObject clonedThickDough;

    public GameObject strawberry;
    GameObject clonedStrawberry1;
    GameObject clonedStrawberry2;
    public GameObject dottedLine;
    GameObject clonedDottedLine;
    public GameObject knife;
    GameObject clonedKnife;
    public GameObject halfStrawberry;
    GameObject clonedHalfStrawberry;
    GameObject clonedHalfStrawberry2;

    bool isBowlBack = false;
    bool isInductionBack = false;
    bool isCuttingBoardBack = false;

    bool[] checkIngredients = new bool[3]; //1:milk, 2:flour, 3:egg
    int pancakeCount = 0;
    bool makeDough = false;
    int isLine = 0;
    int isKnife = 1;

    int cutStrawberryCount = 0; //맞게 잘랐는지
    int cutBananaCount = 0;

    bool needStrawberry = false;
    bool needBanana = false;
    bool needChocolate = false;

    float dragTime = 0;
    float fadeTime = 3f;
    SpriteRenderer whippingRenderer;
    Color originalColor;
    float blinkDuration = 1f;

    LineRenderer lr;
    EdgeCollider2D heartCol;
    List<Vector2> kettlePoints = new List<Vector2>();
    bool isKettleDragging = false;

    int[] menuList = new int[3]; //1:flavor 2~:topping
    public bool[] cutAllFruits = new bool[3];
    public int[] decoFruitCounts = new int[4]; //0:strawberry, 1:banana, 2:blueberry, 3:chocolate
    //List<GameObject> finishedPancakesDeco = new List<GameObject>();
    public int[] decoClickedCounts = new int[4];
    public bool[] checkLiquid = new bool[2]; //0:coffee, 1:milk

    List<GameObject> EachStrawberrys = new List<GameObject>();
    List<GameObject> EachBlueberrys = new List<GameObject>();
    List<GameObject> EachBananas = new List<GameObject>();
    List<GameObject> EachChocos = new List<GameObject>();

    List<Vector3> eachStrawberryPos = new List<Vector3>();
    List<Vector3> eachBlueberryPos = new List<Vector3>();
    List<Vector3> eachBananasPos = new List<Vector3>();
    List<Vector3> eachChocosPos = new List<Vector3>();

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

        checkMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (isBowlBack)
        {
            putIngredients();
        }
        if (isInductionBack)
        {
            baking();
        }
        if (isCuttingBoardBack)
        {
            cutting();
        }
    }

    public void checkMenu()
    {
        int index = findIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum,
            GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);

        string flavor = orders[index]["Flavor"].ToString();
        string topping = orders[index]["Topping"].ToString();

        menuList[0] = int.Parse(flavor); //flavor
        menuList[1] = int.Parse(topping.Substring(0, 1));
        menuList[2] = int.Parse(topping.Substring(1, 1));
    }

    public int findIndex(int stage, int orderNum)
    {
        for (int i = 0; i < orders.Count; i++)
        {
            if (orders[i]["stage"].ToString() == stage.ToString()
                && orders[i]["orderNum"].ToString() == orderNum.ToString())
            {
                return i;
            }
        }
        return -1;
    }

    public void clickStart()
    {
        startButton.SetActive(false);

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        Invoke("showBowlBack", 1f);
    }

    void showMaker()
    {
        //clonedMaker = Instantiate(maker, new Vector3(3.820f, -1.48f, 0), Quaternion.identity);
        //toDestroy.Add(clonedMaker);

        toDestroy.Add(Instantiate(maker, new Vector3(3.820f, -1.48f, 0), Quaternion.identity));
    }

    void showMenu()
    {
        //clonedCookingMenu = Instantiate(CookingMenu, new Vector3(440, 510, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        //toDestroy.Add(clonedCookingMenu);

        toDestroy.Add(Instantiate(CookingMenu, new Vector3(440, 510, 0), Quaternion.identity, GameObject.Find("Canvas").transform));
    }

    void showStart()
    {
        startButton.SetActive(true);
    }

    void showBowlBack()
    {
        isBowlBack = true;

        backRenderer.sprite = backGrounds[0];
        blueList.SetActive(true);
        bluePlus.SetActive(true);

        clonedMilk = Instantiate(milk, new Vector3(-8.59f, -0.36f, 0), Quaternion.identity);
        toDestroy.Add(clonedMilk);
        clonedFlour = Instantiate(flour, new Vector3(-7.23f, 0.69f, 0), Quaternion.identity);
        toDestroy.Add(clonedFlour);
        clonedEggs = Instantiate(eggs, new Vector3(5.67f, -2.34f, 0), Quaternion.identity);
        toDestroy.Add(clonedEggs);

        //후추통 1,2
        toDestroy.Add(Instantiate(asset1, new Vector3(6.58f, -0.92f, 0), Quaternion.identity));
        toDestroy.Add(Instantiate(asset2, new Vector3(7.4654f, -0.9054f, 0), Quaternion.identity));

        Invoke("showBowl", 0.5f);
    }

    void showBowl()
    {
        clonedBowl = Instantiate(bowl, new Vector3(0.18f, -1.4f, 0), Quaternion.identity);
        toDestroy.Add(clonedBowl);

        SpriteRenderer sr = null;
        sr = clonedBowl.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 3;
    }

    void putIngredients()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("milk"))
                {
                    checkIngredients[0] = true;

                    if (!toDestroy.Contains(clonedFilledMilk))
                    {
                        clonedFilledMilk = Instantiate(filledMilk, new Vector3(0.1679f, -2.1505f, 0), Quaternion.identity);
                        toDestroy.Add(clonedFilledMilk);

                        clonedMilkButton = Instantiate(milkButton, new Vector3(4.55f, -1.35f, 0), Quaternion.identity);
                        Destroy(clonedMilkButton, 1f);
                    }
                }

                if (rayHit.collider.gameObject.tag.Equals("flour"))
                {
                    checkIngredients[1] = true;

                    if (!toDestroy.Contains(clonedFilledFlour))
                    {
                        clonedFilledFlour = Instantiate(filledFlour, new Vector3(0.2046f, -0.9852f, 0), Quaternion.identity);
                        toDestroy.Add(clonedFilledFlour);

                        clonedFlourButton = Instantiate(flourButton, new Vector3(4.55f, -1.35f, 0), Quaternion.identity);
                        Destroy(clonedFlourButton, 1f);
                    }
                }

                if (rayHit.collider.gameObject.tag.Equals("egg"))
                {
                    checkIngredients[2] = true;

                    if (!toDestroy.Contains(clonedCrackedEgg))
                    {
                        clonedCrackedEgg = Instantiate(crackedEgg, new Vector3(1.47f, 3.24f, 0), Quaternion.identity);
                        toDestroy.Add(clonedCrackedEgg);
                        Destroy(clonedCrackedEgg, 1.5f);

                        Invoke("showInsideEgg", 0.5f);

                        clonedEggButton = Instantiate(eggButton, new Vector3(3.85f, 1.83f, 0), Quaternion.identity);
                        Destroy(clonedEggButton, 1.5f);
                    }
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("whipper"))
                {
                    Vector2 mousePos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 objPos = Camera.main.ScreenToWorldPoint(mousePos);
                    float checkTime = 2;

                    if (objPos.x >= -0.95 && objPos.x <= 3.49
                        && objPos.y >= 0.16 && objPos.y <= 1.47)
                    {
                        clonedWhipper.transform.position = objPos;

                        dragTime += Time.deltaTime;
                        if (dragTime > checkTime)
                        {
                            if (!toDestroy.Contains(clonedPerfect))
                            {
                                clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
                                toDestroy.Add(clonedPerfect);

                                Invoke("showInductionBack", 1f);

                                dragTime = 0;
                            }
                        }
                    }
                }
            }
        }

        if (checkMixIngredients())
        {
            Invoke("showWhipper", 1f);

            for (int i = 0; i < checkIngredients.Length; i++)
            {
                checkIngredients[i] = false;
            }
        }
    }

    void showInsideEgg()
    {
        clonedInsideEgg = Instantiate(insideEgg, new Vector3(0, 1.4761f, 0), Quaternion.identity);
        Destroy(clonedInsideEgg, 1f);
    }

    bool checkMixIngredients()
    {
        for (int i = 0; i < checkIngredients.Length; i++)
        {
            if (checkIngredients[i] == false)
                return false;
        }
        return true;
    }

    void showWhipper()
    {
        clonedWhipper = Instantiate(whipper, new Vector3(2.07f, 1.3582f, 0), Quaternion.identity);
        toDestroy.Add(clonedWhipper);

        SpriteRenderer sr = null;
        sr = clonedWhipper.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 2;
    }

    void showInductionBack()
    {
        isBowlBack = false;
        isInductionBack = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[1];

        Invoke("showPan", 1f);
        Invoke("showFilledBowl", 1.5f);
    }

    void showPan()
    {
        clonedPan = Instantiate(pan, new Vector3(-1.21f, -0.7f, 0), Quaternion.identity);
        toDestroy.Add(clonedPan);
    }

    void showFilledBowl()
    {
        clonedFilledBowl = Instantiate(filledBowl, new Vector3(4.58f, 1.45f, 0), Quaternion.identity);
        toDestroy.Add(clonedFilledBowl);
    }

    void baking()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("bowl"))
                {
                    makeDough = true;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("bowl"))
                {
                    if (makeDough)
                    {
                        clonedFilledBowl.transform.Rotate(Vector3.forward * Time.deltaTime * 15f);

                        if (!toDestroy.Contains(clonedDough))
                        {
                            clonedDough = Instantiate(dough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                            toDestroy.Add(clonedDough);
                        }
                        clonedDough.transform.localScale += Time.deltaTime * new Vector3(0.1f, 0.1f, 0);

                        if (clonedDough.transform.localScale.x > 0.2308615
                            && clonedDough.transform.localScale.y > 0.2902491)
                        {
                            Destroy(clonedDough);
                            toDestroy.Remove(clonedDough);
                            makeDough = false;

                            clonedThickDough = Instantiate(thickDough, new Vector3(0, -0.96f, 0), Quaternion.identity);
                            toDestroy.Add(clonedThickDough);
                            Destroy(clonedFilledBowl);
                            pancakeCount++;

                            Invoke("showFilledBowlAgain", 2f);
                        }
                    }
                }
            }
        }

        if (pancakeCount >= 2)
        {
            Invoke("showPerfect", 2f);
            Invoke("showCuttingBoardBack", 3f);
        }

    }

    void showFilledBowlAgain()
    {
        if (pancakeCount < 2)
        {
            Destroy(clonedThickDough);
            toDestroy.Remove(clonedThickDough);

            clonedFilledBowl = Instantiate(filledBowl, new Vector3(4.58f, 1.45f, 0), Quaternion.identity);
            toDestroy.Add(clonedFilledBowl);
        }
    }

    void showPerfect()
    {
        if (!toDestroy.Contains(clonedPerfect))
        {
            clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
            toDestroy.Add(clonedPerfect);
        }
    }

    void showCuttingBoardBack()
    {
        isInductionBack = false;
        isCuttingBoardBack = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[2];


        //조건문 달아서 배열에 나와야 하는 과일 담아두고 출력?
        //showStrawberry();
        //Invoke("showStrawberry", 0.5f);
        //Debug.Log("여기서부터 문제일까?");

        chooseFlavorToCut();
    }

    void chooseFlavorToCut() //flavor 대로 자르기
    {
        switch (menuList[0])
        {
            case 0:
                Invoke("showStrawberry", 0.5f);
                break;
            case 1:
                Invoke("showBanana", 0.5f);
                break;
            case 2:
                needChocolate = true;
                break;
        }
    }

    void showStrawberry()
    {

        if (!toDestroy.Contains(clonedStrawberry1))
        {
            //Debug.Log("딸기1");
            clonedStrawberry1 = Instantiate(strawberry, new Vector3(-1.7f, 0, 0), Quaternion.identity);
            toDestroy.Add(clonedStrawberry1);
        }
        if (!toDestroy.Contains(clonedStrawberry2))
        {
            clonedStrawberry2 = Instantiate(strawberry, new Vector3(2.13f, 0, 0), Quaternion.identity);
            clonedStrawberry2.transform.localEulerAngles = new Vector3(0, 0, 40);
            toDestroy.Add(clonedStrawberry2);
        }
        //Debug.Log("딸기 몇개" + toDestroy.Count);

        Invoke("delayLine", 0.5f);
        needStrawberry = true;
    }

    void delayLine()
    {
        Debug.Log("isLine" + isLine);
        isLine++;
    }

    void cutting()
    {
        if (needStrawberry) //딸기 자를 때
        {
            cuttingStrawberry();
        }

        if (!needBanana)
        {
            //Debug.Log("잘 넘어 오는지");
        }
    }

    void cuttingStrawberry()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (isLine == 0) //첫번째 점선일 때
        {
            //isLine = 0;
            if (!toDestroy.Contains(clonedDottedLine))
            {
                clonedDottedLine = Instantiate(dottedLine, new Vector3(-1.65f, -0.02f, 0), Quaternion.identity);
                toDestroy.Add(clonedDottedLine);
                clonedDottedLine.transform.localEulerAngles = new Vector3(0, 0, 45);
            }
        }

        if (isLine == 1)
        {
            if (!toDestroy.Contains(clonedDottedLine))
            {
                //Debug.Log("여기 안걸리나");
                clonedDottedLine = Instantiate(dottedLine, new Vector3(2.35f, 0.01f, 0), Quaternion.identity);
                toDestroy.Add(clonedDottedLine);
                clonedDottedLine.transform.localEulerAngles = new Vector3(0, 0, 85);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("dottedLine"))
                {
                    if (isLine == 1) //첫번째 점선일 때
                    {
                        clonedKnife = Instantiate(knife, new Vector3(0.27f, 0.01f, 0), Quaternion.identity);
                        toDestroy.Add(clonedKnife);

                        Destroy(clonedKnife, 1f);

                        Destroy(clonedDottedLine);
                        toDestroy.Remove(clonedDottedLine);
                        Destroy(clonedStrawberry1);

                        if (!toDestroy.Contains(clonedHalfStrawberry))
                        {
                            clonedHalfStrawberry = Instantiate(halfStrawberry, new Vector3(-1.7f, 0, 0), Quaternion.identity);
                            toDestroy.Add(clonedHalfStrawberry);
                        }
                        Invoke("delayLine", 0.5f);
                    }

                    if (isLine == 2)
                    {
                        clonedKnife = Instantiate(knife, new Vector3(4.39f, -0.22f, 0), Quaternion.identity);
                        toDestroy.Add(clonedKnife);

                        Destroy(clonedKnife, 1f);

                        Destroy(clonedDottedLine);
                        //toDestroy.Remove(clonedDottedLine);
                        Destroy(clonedStrawberry2);

                        if (!toDestroy.Contains(clonedHalfStrawberry2))
                        {
                            clonedHalfStrawberry2 = Instantiate(halfStrawberry, new Vector3(2.13f, 0, 0), Quaternion.identity);
                            clonedHalfStrawberry2.transform.localEulerAngles = new Vector3(0, 0, 45);
                            toDestroy.Add(clonedHalfStrawberry2);
                        }

                        Invoke("finishCutStrawberry", 1f);
                    }
                }
            }
        }

        //if (isLine > 2)
        //    Invoke("finishCutStrawberry", 1f);
    }

    void finishCutStrawberry()
    {
        if (cutStrawberryCount == 2)
        {
            needStrawberry = false;
            Debug.Log("strawberry count" + cutStrawberryCount);
        }
    }

    void showBanana()
    {
        if (!toDestroy.Contains(clonedBanana))
        {
            clonedBanana = Instantiate(banana, new Vector3(0.31f, -0.33f, 0), Quaternion.identity);
            toDestroy.Add(clonedBanana);
        }

        Invoke("showThreeLine", 0.5f);

        isLine = 0;
        Invoke("delayLine", 1.5f);

        needBanana = true;
    }

    void cuttingBanana()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (isLine == 1)
        {
            if (!toDestroy.Contains(clonedTempLine1))
            {
                clonedTempLine1 = Instantiate(dottedLine, new Vector3(-2, 0, 0), Quaternion.identity);
                toDestroy.Add(clonedTempLine1);
            }
        }

        if (isLine == 2)
        {
            if (!toDestroy.Contains(clonedTempLine2))
            {
                clonedTempLine2 = Instantiate(dottedLine, new Vector3(0, 0, 0), Quaternion.identity);
                toDestroy.Add(clonedTempLine2);
            }
        }

        if (isLine == 3)
        {
            if (!toDestroy.Contains(clonedTempLine3))
            {
                clonedTempLine3 = Instantiate(dottedLine, new Vector3(2, 0, 0), Quaternion.identity);
                toDestroy.Add(clonedTempLine3);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("dottedLine"))
                {
                    if (isLine == 1)
                    {
                        clonedBackKnife = Instantiate(backKnife, new Vector3(-1.71f, -1.39f, 0), Quaternion.identity);

                        Destroy(clonedBackKnife, 1f);

                        Destroy(clonedTempLine1);
                        //toDestroy.Remove(clonedDottedLine);

                        cutBananaCount++;
                        Invoke("delayLine", 0.5f);
                    }

                    if (isLine == 2)
                    {
                        clonedBackKnife = Instantiate(backKnife, new Vector3(0.52f, -1.39f, 0), Quaternion.identity);

                        Destroy(clonedBackKnife, 1f);
                        Destroy(clonedTempLine2);
                        toDestroy.Remove(clonedTempLine1);

                        cutBananaCount++;
                        Invoke("delayLine", 0.5f);
                    }

                    if (isLine == 3)
                    {
                        clonedBackKnife = Instantiate(backKnife, new Vector3(2.59f, -1.2f, 0), Quaternion.identity);

                        Destroy(clonedBackKnife, 1f);
                        Destroy(clonedTempLine3);
                        toDestroy.Remove(clonedTempLine2);

                        Destroy(clonedBanana);
                        toDestroy.Remove(clonedBanana);

                        if (!toDestroy.Contains(clonedCutBanana))
                        {
                            clonedCutBanana = Instantiate(cutBanana, new Vector3(0.31f, -0.33f, 0), Quaternion.identity);
                            toDestroy.Add(clonedCutBanana);
                        }

                        cutBananaCount++;
                        Invoke("finishCutBanana", 1f);
                    }
                }
            }
        }
    }

    void showThreeLine()
    {
        clonedTempLine1 = Instantiate(dottedLine, new Vector3(-2, 0, 0), Quaternion.identity);
        clonedTempLine2 = Instantiate(dottedLine, new Vector3(0, 0, 0), Quaternion.identity);
        clonedTempLine3 = Instantiate(dottedLine, new Vector3(2, 0, 0), Quaternion.identity);

        Destroy(clonedTempLine1, 0.5f);
        Destroy(clonedTempLine2, 0.5f);
        Destroy(clonedTempLine3, 0.5f);
    }

    void finishCutBanana()
    {
        if (cutBananaCount == 3)
        {
            cutCount++;

            needBanana = false;
            Destroy(clonedCutBanana, 0.5f);
            isLine = 0;

            if (cutCount == 1)
            {
                chooseToppingToCut(menuList[1]);
            }
            else if (cutCount == 2)
            {
                chooseToppingToCut(menuList[2]);
            }
        }
    }

    void showChocolate()
    {
        if (!toDestroy.Contains(clonedChocolate))
        {
            clonedChocolate = Instantiate(chocolate, new Vector3(0.14f, -0.49f, 0), Quaternion.identity);
            toDestroy.Add(clonedChocolate);
        }

        isLine = 0;
        Invoke("delayLine", 0.5f);

        needChocolate = true;
    }

    void cuttingChocolate()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (isLine == 1)
        {
            if (!toDestroy.Contains(clonedDottedLine))
            {
                clonedDottedLine = Instantiate(dottedLine, new Vector3(-0.01f, -0.33f, 0), Quaternion.identity);
                toDestroy.Add(clonedDottedLine);

                SpriteRenderer sr = null;
                sr = clonedDottedLine.GetComponent<SpriteRenderer>();
                sr.sortingOrder = 3;
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("dottedLine"))
                {
                    if (isLine == 1)
                    {
                        clonedKnife = Instantiate(knife, new Vector3(1.932142f, -0.5250874f, 0), Quaternion.identity);
                        toDestroy.Add(clonedKnife);

                        Destroy(clonedKnife, 1f);

                        Destroy(clonedDottedLine);
                        Destroy(clonedChocolate);

                        if (!toDestroy.Contains(clonedCutChocolate))
                        {
                            clonedCutChocolate = Instantiate(cutChocolate, new Vector3(0.11f, -0.43f, 0), Quaternion.identity);
                            toDestroy.Add(clonedCutChocolate);
                        }

                        cutChocolateCount++;
                        Invoke("finishCutChocolate", 1f);
                    }
                }
            }
        }
    }

    void finishCutChocolate()
    {
        cutCount++;
        if (cutChocolateCount == 1)
        {
            needChocolate = false;
            Destroy(clonedCutChocolate);

            isLine = 0;

            if (cutCount == 1)
            {
                chooseToppingToCut(menuList[1]);
            }
            else if (cutCount == 2)
            {
                chooseToppingToCut(menuList[2]);
            }
        }
    }

    void chooseDecoBack()
    {
        switch (menuList[0]) //flavor
        {
            case 0:
                clonedStrawberryCup = Instantiate(strawberryCup, new Vector3(-6.25f, 2.57f, 0), Quaternion.identity);
                toDestroy.Add(clonedStrawberryCup);
                break;
            case 1:
                clonedBananaCup = Instantiate(bananaCup, new Vector3(-6.25f, 2.57f, 0), Quaternion.identity);
                toDestroy.Add(clonedBananaCup);
                break;
            case 2:
                clonedChocolateCup = Instantiate(chocolateCup, new Vector3(-6.25f, 2.57f, 0), Quaternion.identity);
                toDestroy.Add(clonedChocolateCup);
                break;
        }

        switch (menuList[1]) //topping 1
        {
            case 0:
                clonedBananaCup = Instantiate(bananaCup, new Vector3(-6.19f, -1.79f, 0), Quaternion.identity);
                toDestroy.Add(clonedBananaCup);
                break;
            case 1:
                clonedBlueberryCup = Instantiate(blueberryCup, new Vector3(-6.19f, -1.79f, 0), Quaternion.identity);
                toDestroy.Add(clonedBlueberryCup);
                break;
            case 2:
                clonedStrawberryCup = Instantiate(strawberryCup, new Vector3(-6.19f, -1.79f, 0), Quaternion.identity);
                toDestroy.Add(clonedStrawberryCup);
                break;
        }

        switch (menuList[2])
        {
            case 0:
                clonedBananaCup = Instantiate(bananaCup, new Vector3(6.19f, -1.9f, 0), Quaternion.identity);
                toDestroy.Add(clonedBananaCup);
                break;
            case 1:
                clonedBlueberryCup = Instantiate(blueberryCup, new Vector3(6.19f, -1.9f, 0), Quaternion.identity);
                toDestroy.Add(clonedBlueberryCup);
                break;
            case 2:
                clonedStrawberryCup = Instantiate(strawberryCup, new Vector3(6.19f, -1.9f, 0), Quaternion.identity);
                toDestroy.Add(clonedStrawberryCup);
                break;
        }
    }

    void showDecoratingBack()
    {
        isCuttingBoardBack = false;
        isDecoratingBack = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[3];

        if (!toDestroy.Contains(clonedPancakes))
        {
            clonedPancakes = Instantiate(pancakes, new Vector3(0, 0, 0), Quaternion.identity);
            toDestroy.Add(clonedPancakes);
        }
        if (!toDestroy.Contains(clonedWhipping))
        {
            clonedWhipping = Instantiate(whipping, new Vector3(4.88f, 2.57f, 0), Quaternion.identity);
            toDestroy.Add(clonedWhipping);
        }
        chooseDecoBack();

        nextButton.SetActive(true);
    }

    void showDecoRecipe()
    {
        clonedDecoRecipe = Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        toDestroy.Add(clonedDecoRecipe);
        clonedDecoRecipe.transform.localPosition = new Vector3(0, 0, 0);
        changeUIRecipe(); //이게 뭐하는거지?

        Destroy(clonedDecoRecipe, 2f);
    }

    //ㅇㅣ게 뭐지?
    void changeUIRecipe()
    {
        Destroy(clonedCookingMenu);
        clonedCookingMenu = Instantiate(decoRecipe, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        clonedCookingMenu.transform.localPosition = new Vector3(0, 0, 0);
        clonedCookingMenu.SetActive(false);
    }

    void decorating()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("whipping"))
                {
                    if (!toDestroy.Contains(clonedWhipped))
                    {
                        clonedWhipped = Instantiate(whipped, new Vector3(0, 0, 0), Quaternion.identity);
                        toDestroy.Add(clonedWhipped);

                        whippingRenderer = clonedWhipped.GetComponent<SpriteRenderer>();
                        originalColor = whippingRenderer.color;
                        whippingRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);

                        StartCoroutine(FadeInRoutine());
                    }
                }

                if (rayHit.collider.gameObject.tag.Equals("strawberryCup"))
                {
                    decoClickedCounts[0]++;
                    //Debug.Log("decoclickedcounts[0]" + decoClickedCounts[0]);
                    if (menuList[0] == 0) //flavor == strawberry
                    {
                        if (decoClickedCounts[0] == 1) //첫 번째 클릭
                        {
                            GameObject flavorStrawberry1;
                            SpriteRenderer sr = null;

                            flavorStrawberry1 = Instantiate(eachStrawberry, new Vector3(-0.58f, 0.11f, 0), Quaternion.identity);
                            sr = flavorStrawberry1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            //finishedPancakesDeco.Add(flavorStrawberry1);
                            EachStrawberrys.Add(flavorStrawberry1);
                            //eachStrawberryPos.Add(flavorStrawberry1.transform.position);

                            toDestroy.Add(flavorStrawberry1);

                            showFlavorFirstButton();
                        }

                        if (decoClickedCounts[0] == 2) //두 번째 클릭
                        {
                            GameObject flavorStrawberry2;
                            SpriteRenderer sr = null;

                            flavorStrawberry2 = Instantiate(strawberry, new Vector3(0.43f, 0.16f, 0), Quaternion.identity);
                            flavorStrawberry2.transform.localEulerAngles = new Vector3(0, 0, -90);
                            flavorStrawberry2.transform.localScale = new Vector3(0.15f, 0.15f, 1);

                            sr = flavorStrawberry2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            EachStrawberrys[0].transform.position = new Vector3(-0.78f, 0.25f, 0);
                            //eachStrawberryPos[0] = EachStrawberrys[0].transform.position;
                            //eachStrawberryPos.Add(flavorStrawberry2.transform.position);

                            //finishedPancakesDeco.Add(flavorStrawberry2);
                            EachStrawberrys.Add(flavorStrawberry2);
                            toDestroy.Add(flavorStrawberry2);

                            showFlavorSecondButton();
                        }

                        if (decoClickedCounts[0] == 3)
                        {
                            GameObject flavorStrawberry3;
                            SpriteRenderer sr = null;

                            flavorStrawberry3 = Instantiate(eachStrawberry, new Vector3(0.59f, 0.08f, 0), Quaternion.identity);
                            flavorStrawberry3.transform.localEulerAngles = new Vector3(0, 0, -85.96f);
                            sr = flavorStrawberry3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            //EachStrawberrys[0].transform.position = new Vector3(-0.9f, 0.11f, 0);
                            //EachStrawberrys[1].transform.position = new Vector3(-0.07f, 0.18f, 0);

                            EachStrawberrys[0].transform.position = new Vector3(-0.9f, 0.11f, 0);
                            //eachStrawberryPos[0] = EachStrawberrys[0].transform.position;

                            EachStrawberrys[1].transform.position = new Vector3(-0.07f, 0.18f, 0);
                            //eachStrawberryPos[1] = EachStrawberrys[1].transform.position;



                            //eachStrawberryPos.Add(flavorStrawberry3.transform.position);
                            //finishedPancakesDeco.Add(flavorStrawberry3);
                            EachStrawberrys.Add(flavorStrawberry3);
                            toDestroy.Add(flavorStrawberry3);

                            showFlavorThirdButton();
                        }
                    }

                    else if (menuList[1] == 2) //첫 번째 토핑이 딸기일 때
                    {
                        //Debug.Log("걸?");
                        if (decoClickedCounts[0] == 1) //첫 번째 클릭
                        {
                            GameObject toppingStrawberry1;
                            SpriteRenderer sr = null;

                            toppingStrawberry1 = Instantiate(eachStrawberry, new Vector3(-0.91f, 0.47f, 0), Quaternion.identity);
                            sr = toppingStrawberry1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //eachStrawberryPos.Add(toppingStrawberry1.transform.position);
                            //finishedPancakesDeco.Add(toppingStrawberry1);
                            EachStrawberrys.Add(toppingStrawberry1);
                            toDestroy.Add(toppingStrawberry1);

                            showTopping1FirstButton();
                        }

                        if (decoClickedCounts[0] == 2)
                        {
                            GameObject toppingStrawberry2;
                            SpriteRenderer sr = null;

                            toppingStrawberry2 = Instantiate(strawberry, new Vector3(0.65f, 0.7f, 0), Quaternion.identity);
                            toppingStrawberry2.transform.localScale = new Vector3(0.15f, 0.15f, 1);

                            sr = toppingStrawberry2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //eachStrawberryPos.Add(toppingStrawberry2.transform.position);
                            //finishedPancakesDeco.Add(toppingStrawberry2);
                            EachStrawberrys.Add(toppingStrawberry2);
                            toDestroy.Add(toppingStrawberry2);

                            showTopping1SecondButton();
                        }

                        if (decoClickedCounts[0] == 3)
                        {
                            GameObject toppingStrawberry3;
                            SpriteRenderer sr = null;

                            toppingStrawberry3 = Instantiate(eachStrawberry, new Vector3(-0.21f, -0.8f, 0), Quaternion.identity);
                            sr = toppingStrawberry3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //EachStrawberrys[0].transform.position = new Vector3(-1.16f, -0.3f, 0);
                            //EachStrawberrys[1].transform.position = new Vector3(0.7f, -0.27f, 0);

                            //eachStrawberryPos.Add(toppingStrawberry3.transform.position);
                            //finishedPancakesDeco.Add(toppingStrawberry3);
                            EachStrawberrys.Add(toppingStrawberry3);
                            toDestroy.Add(toppingStrawberry3);

                            showTopping1ThirdButton();
                        }
                    }

                    else if (menuList[2] == 2) //두 번째 토핑이 딸기일 때
                    {
                        if (decoClickedCounts[0] == 1)
                        {
                            GameObject toppingStrawberry1;
                            SpriteRenderer sr = null;

                            toppingStrawberry1 = Instantiate(eachStrawberry, new Vector3(-0.05f, 0.73f, 0), Quaternion.identity);
                            sr = toppingStrawberry1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //eachStrawberryPos.Add(toppingStrawberry1.transform.position);
                            //finishedPancakesDeco.Add(toppingStrawberry1);
                            EachStrawberrys.Add(toppingStrawberry1);
                            toDestroy.Add(toppingStrawberry1);

                            showTopping2FirstButton();
                        }
                        if (decoClickedCounts[0] == 2)
                        {
                            GameObject toppingStrawberry2;
                            SpriteRenderer sr = null;

                            toppingStrawberry2 = Instantiate(strawberry, new Vector3(-0.76f, -0.57f, 0), Quaternion.identity);
                            toppingStrawberry2.transform.localScale = new Vector3(0.15f, 0.15f, 1);
                            sr = toppingStrawberry2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //eachStrawberryPos.Add(toppingStrawberry2.transform.position);
                            //finishedPancakesDeco.Add(toppingStrawberry2);
                            EachStrawberrys.Add(toppingStrawberry2);
                            toDestroy.Add(toppingStrawberry2);

                            showTopping2SecondButton();
                        }
                        if (decoClickedCounts[0] == 3)
                        {
                            GameObject toppingStrawberry3;
                            SpriteRenderer sr = null;

                            toppingStrawberry3 = Instantiate(eachStrawberry, new Vector3(0.65f, -0.54f, 0), Quaternion.identity);
                            sr = toppingStrawberry3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //EachBlueberrys[1].transform.position = new Vector3(1.03f, 0.67f, 0);

                            //eachStrawberryPos.Add(toppingStrawberry3.transform.position);
                            //finishedPancakesDeco.Add(toppingStrawberry3);
                            EachStrawberrys.Add(toppingStrawberry3);
                            toDestroy.Add(toppingStrawberry3);

                            showTopping2ThirdButton();
                        }
                    }
                }

                if (rayHit.collider.gameObject.tag.Equals("bananaCup"))
                {
                    decoClickedCounts[1]++;

                    if (menuList[0] == 1) //flavor == banana
                    {
                        if (decoClickedCounts[1] == 1)
                        {
                            GameObject flavorBanana1;
                            SpriteRenderer sr = null;

                            flavorBanana1 = Instantiate(eachBanana, new Vector3(-0.58f, 0.11f, 0), Quaternion.identity);
                            sr = flavorBanana1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            //eachBananasPos.Add(flavorBanana1.transform.position);
                            //finishedPancakesDeco.Add(flavorBanana1);
                            EachBananas.Add(flavorBanana1);
                            toDestroy.Add(flavorBanana1);

                            showFlavorFirstButton();
                        }
                        if (decoClickedCounts[1] == 2)
                        {
                            GameObject flavorBanana2;
                            SpriteRenderer sr = null;

                            flavorBanana2 = Instantiate(eachBanana, new Vector3(0.43f, 0.16f, 0), Quaternion.identity);
                            sr = flavorBanana2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;
                            //EachBananas[0].transform.position = new Vector3(-0.78f, 0.25f, 0);

                            //eachBananasPos.Add(flavorBanana2.transform.position);
                            //finishedPancakesDeco.Add(flavorBanana2);
                            EachBananas.Add(flavorBanana2);
                            toDestroy.Add(flavorBanana2);

                            showFlavorSecondButton();
                        }
                        if (decoClickedCounts[1] == 3)
                        {
                            GameObject flavorBanana3;
                            SpriteRenderer sr = null;

                            flavorBanana3 = Instantiate(eachBanana, new Vector3(0.59f, 0.08f, 0), Quaternion.identity);
                            sr = flavorBanana3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            EachBananas[0].transform.position = new Vector3(-0.9f, 0.11f, 0);
                            //eachBananasPos[0] = EachBananas[0].transform.position;

                            EachBananas[1].transform.position = new Vector3(-0.07f, 0.18f, 0);
                            //eachBananasPos[1] = EachBananas[1].transform.position;

                            //eachBananasPos.Add(flavorBanana3.transform.position);
                            //finishedPancakesDeco.Add(flavorBanana3);
                            EachBananas.Add(flavorBanana3);
                            toDestroy.Add(flavorBanana3);

                            showFlavorThirdButton();
                        }
                    }
                    else if (menuList[1] == 0) //토핑1 바나나
                    {
                        if (decoClickedCounts[1] == 1)
                        {
                            GameObject toppingBanana1;
                            SpriteRenderer sr = null;

                            toppingBanana1 = Instantiate(eachBanana, new Vector3(-0.91f, 0.47f, 0), Quaternion.identity);
                            sr = toppingBanana1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //eachBananasPos.Add(toppingBanana1.transform.position);
                            //finishedPancakesDeco.Add(toppingBanana1);
                            EachBananas.Add(toppingBanana1);
                            toDestroy.Add(toppingBanana1);

                            showTopping1FirstButton();
                        }
                        if (decoClickedCounts[1] == 2)
                        {
                            GameObject toppingBanana2;
                            SpriteRenderer sr = null;

                            toppingBanana2 = Instantiate(eachBanana, new Vector3(0.65f, 0.7f, 0), Quaternion.identity);
                            sr = toppingBanana2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //eachBananasPos.Add(toppingBanana2.transform.position);
                            //finishedPancakesDeco.Add(toppingBanana2);
                            EachBananas.Add(toppingBanana2);
                            toDestroy.Add(toppingBanana2);

                            showTopping1SecondButton();
                        }
                        if (decoClickedCounts[1] == 3)
                        {
                            GameObject toppingBanana3;
                            SpriteRenderer sr = null;

                            toppingBanana3 = Instantiate(eachBanana, new Vector3(-0.21f, -0.8f, 0), Quaternion.identity);
                            sr = toppingBanana3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //EachBananas[0].transform.position = new Vector3(-1.16f, -0.3f, 0);
                            //EachBananas[1].transform.position = new Vector3(0.7f, -0.27f, 0);

                            //eachBananasPos.Add(toppingBanana3.transform.position);
                            //finishedPancakesDeco.Add(toppingBanana3);
                            EachBananas.Add(toppingBanana3);
                            toDestroy.Add(toppingBanana3);

                            showTopping1ThirdButton();
                        }
                    }
                    else if (menuList[2] == 0) //토핑2 바나나
                    {
                        if (decoClickedCounts[1] == 1)
                        {
                            GameObject toppingBanana1;
                            SpriteRenderer sr = null;

                            toppingBanana1 = Instantiate(eachBanana, new Vector3(-0.05f, 0.73f, 0), Quaternion.identity);
                            sr = toppingBanana1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //eachBananasPos.Add(toppingBanana1.transform.position);
                            //finishedPancakesDeco.Add(toppingBanana1);
                            EachBananas.Add(toppingBanana1);
                            toDestroy.Add(toppingBanana1);

                            showTopping2FirstButton();
                        }
                        if (decoClickedCounts[1] == 2)
                        {
                            GameObject toppingBanana2;
                            SpriteRenderer sr = null;

                            toppingBanana2 = Instantiate(eachBanana, new Vector3(-0.76f, -0.57f, 0), Quaternion.identity);
                            sr = toppingBanana2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //eachBananasPos.Add(toppingBanana2.transform.position);
                            //finishedPancakesDeco.Add(toppingBanana2);
                            EachBananas.Add(toppingBanana2);
                            toDestroy.Add(toppingBanana2);

                            showTopping2SecondButton();
                        }
                        if (decoClickedCounts[1] == 3)
                        {
                            GameObject toppingBanana3;
                            SpriteRenderer sr = null;

                            toppingBanana3 = Instantiate(eachBanana, new Vector3(0.65f, -0.54f, 0), Quaternion.identity);
                            sr = toppingBanana3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //EachBananas[1].transform.position = new Vector3(1.03f, 0.67f, 0);

                            //eachBananasPos.Add(toppingBanana3.transform.position);
                            //finishedPancakesDeco.Add(toppingBanana3);
                            EachBananas.Add(toppingBanana3);
                            toDestroy.Add(toppingBanana3);

                            showTopping2ThirdButton();
                        }
                    }
                }
                if (rayHit.collider.gameObject.tag.Equals("blueberryCup"))
                {
                    decoClickedCounts[2]++;

                    if (menuList[1] == 1) //토핑1 블루베리
                    {
                        if (decoClickedCounts[2] == 1)
                        {
                            GameObject toppingBlueberry1;
                            SpriteRenderer sr = null;

                            toppingBlueberry1 = Instantiate(eachBlueberry, new Vector3(-0.91f, 0.47f, 0), Quaternion.identity);
                            sr = toppingBlueberry1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //eachBlueberryPos.Add(toppingBlueberry1.transform.position);
                            //finishedPancakesDeco.Add(toppingBlueberry1);
                            EachBlueberrys.Add(toppingBlueberry1);
                            toDestroy.Add(toppingBlueberry1);

                            showTopping1FirstButton();
                        }
                        if (decoClickedCounts[2] == 2)
                        {
                            GameObject toppingBlueberry2;
                            SpriteRenderer sr = null;

                            toppingBlueberry2 = Instantiate(eachBlueberry, new Vector3(0.65f, 0.7f, 0), Quaternion.identity);
                            sr = toppingBlueberry2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //eachBlueberryPos.Add(toppingBlueberry2.transform.position);
                            //finishedPancakesDeco.Add(toppingBlueberry2);
                            EachBlueberrys.Add(toppingBlueberry2);
                            toDestroy.Add(toppingBlueberry2);

                            showTopping1SecondButton();
                        }
                        if (decoClickedCounts[2] == 3)
                        {
                            GameObject toppingBlueberry3;
                            SpriteRenderer sr = null;

                            toppingBlueberry3 = Instantiate(eachBlueberry, new Vector3(-0.21f, -0.8f, 0), Quaternion.identity);
                            sr = toppingBlueberry3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 5;

                            //EachBlueberrys[0].transform.position = new Vector3(-1.16f, -0.3f, 0);
                            //EachBlueberrys[1].transform.position = new Vector3(0.7f, -0.27f, 0);

                            //eachBlueberryPos.Add(toppingBlueberry3.transform.position);
                            //finishedPancakesDeco.Add(toppingBlueberry3);
                            EachBlueberrys.Add(toppingBlueberry3);
                            toDestroy.Add(toppingBlueberry3);

                            showTopping1ThirdButton();
                        }
                    }
                    else if (menuList[2] == 1) //토핑2 블루베리
                    {
                        if (decoClickedCounts[2] == 1)
                        {
                            GameObject toppingBlueberry1;
                            SpriteRenderer sr = null;

                            toppingBlueberry1 = Instantiate(eachBlueberry, new Vector3(-1.05f, 0.63f, 0), Quaternion.identity);
                            sr = toppingBlueberry1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //eachBlueberryPos.Add(toppingBlueberry1.transform.position);
                            //finishedPancakesDeco.Add(toppingBlueberry1);
                            EachBlueberrys.Add(toppingBlueberry1);
                            toDestroy.Add(toppingBlueberry1);

                            showTopping2FirstButton();
                        }
                        if (decoClickedCounts[2] == 2)
                        {
                            GameObject toppingBlueberry2;
                            SpriteRenderer sr = null;

                            toppingBlueberry2 = Instantiate(eachBlueberry, new Vector3(0.87f, 0.42f, 0), Quaternion.identity);
                            sr = toppingBlueberry2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            //eachBlueberryPos.Add(toppingBlueberry2.transform.position);
                            //finishedPancakesDeco.Add(toppingBlueberry2);
                            EachBlueberrys.Add(toppingBlueberry2);
                            toDestroy.Add(toppingBlueberry2);

                            showTopping2SecondButton();
                        }
                        if (decoClickedCounts[2] == 3)
                        {
                            GameObject toppingBlueberry3;
                            SpriteRenderer sr = null;

                            toppingBlueberry3 = Instantiate(eachBlueberry, new Vector3(-0.05f, -0.87f, 0), Quaternion.identity);
                            sr = toppingBlueberry3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 4;

                            EachBlueberrys[1].transform.position = new Vector3(1.03f, 0.67f, 0);
                            //eachBlueberryPos[1] = EachBlueberrys[1].transform.position;

                            //eachBlueberryPos.Add(toppingBlueberry3.transform.position);
                            //finishedPancakesDeco.Add(toppingBlueberry3);
                            EachBlueberrys.Add(toppingBlueberry3);
                            toDestroy.Add(toppingBlueberry3);

                            showTopping2ThirdButton();
                        }
                    }
                }
                if (rayHit.collider.gameObject.tag.Equals("chocolateCup"))
                {
                    decoClickedCounts[3]++;
                    if (menuList[0] == 2) //flavor == chocolate
                    {
                        if (decoClickedCounts[3] == 1)
                        {
                            GameObject flavorChocolate1;
                            SpriteRenderer sr = null;

                            flavorChocolate1 = Instantiate(eachLeftChocolate, new Vector3(-0.58f, 0.11f, 0), Quaternion.identity);
                            sr = flavorChocolate1.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            //eachChocosPos.Add(flavorChocolate1.transform.position);
                            //finishedPancakesDeco.Add(flavorChocolate1);
                            EachChocos.Add(flavorChocolate1);
                            toDestroy.Add(flavorChocolate1);

                            showFlavorFirstButton();
                        }
                        if (decoClickedCounts[3] == 2)
                        {
                            GameObject flavorChocolate2;
                            SpriteRenderer sr = null;

                            flavorChocolate2 = Instantiate(eachRightChocolate, new Vector3(0.43f, 0.16f, 0), Quaternion.identity);
                            sr = flavorChocolate2.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;


                            EachChocos[0].transform.position = new Vector3(-0.78f, 0.25f, 0);
                            //eachChocosPos[0] = EachChocos[0].transform.position;

                            //eachChocosPos.Add(flavorChocolate2.transform.position);
                            //finishedPancakesDeco.Add(flavorChocolate2);
                            EachChocos.Add(flavorChocolate2);
                            toDestroy.Add(flavorChocolate2);

                            showFlavorSecondButton();
                        }
                        if (decoClickedCounts[3] == 3)
                        {
                            GameObject flavorChocolate3;
                            SpriteRenderer sr = null;

                            flavorChocolate3 = Instantiate(eachLeftChocolate, new Vector3(0.59f, 0.08f, 0), Quaternion.identity);
                            sr = flavorChocolate3.GetComponent<SpriteRenderer>();
                            sr.sortingOrder = 6;

                            EachChocos[0].transform.position = new Vector3(-1.15f, 0.25f, 0);
                            //eachChocosPos[0] = EachChocos[0].transform.position;
                            EachChocos[1].transform.position = new Vector3(-0.21f, 0.3f, 0.54f);
                            //eachChocosPos[1] = EachChocos[1].transform.position;

                            //eachChocosPos.Add(flavorChocolate3.transform.position);
                            //finishedPancakesDeco.Add(flavorChocolate3);
                            EachChocos.Add(flavorChocolate3);
                            toDestroy.Add(flavorChocolate3);

                            showFlavorThirdButton();
                        }
                    }
                }
            }
        }
    }

    IEnumerator FadeInRoutine()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeTime)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeTime);
            whippingRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        whippingRenderer.color = originalColor;
    }


    void showFlavorFirstButton()
    {
        if (!toDestroy.Contains(clonedFirstButton))
        {
            clonedFirstButton = Instantiate(firstButton, new Vector3(-3.96f, 3.72f, 0), Quaternion.identity);
            toDestroy.Add(clonedFirstButton);
            Destroy(clonedFirstButton, 0.5f);
            //Invoke("removeButton1", 0.5f);
        }
    }

    void showFlavorSecondButton()
    {
        if (!toDestroy.Contains(clonedSecondButton))
        {
            clonedSecondButton = Instantiate(secondButton, new Vector3(-3.96f, 3.72f, 0), Quaternion.identity);
            toDestroy.Add(clonedSecondButton);
            Destroy(clonedSecondButton, 0.5f);
            //Invoke("removeButton2", 0.5f);
        }
    }

    void showFlavorThirdButton()
    {
        if (!toDestroy.Contains(clonedThirdButton))
        {
            clonedThirdButton = Instantiate(thirdButton, new Vector3(-3.96f, 3.72f, 0), Quaternion.identity);
            toDestroy.Add(clonedThirdButton);
            Destroy(clonedThirdButton, 0.5f);
            //Invoke("removeButton3", 0.5f);
        }
    }

    void showTopping1FirstButton()
    {
        if (!toDestroy.Contains(clonedFirstButton2))
        {
            clonedFirstButton2 = Instantiate(firstButton, new Vector3(-4.12f, -0.51f, 0), Quaternion.identity);
            toDestroy.Add(clonedFirstButton2);
            Destroy(clonedFirstButton2, 0.5f);
        }
    }

    void showTopping1SecondButton()
    {
        if (!toDestroy.Contains(clonedSecondButton2))
        {
            clonedSecondButton2 = Instantiate(secondButton, new Vector3(-4.12f, -0.51f, 0), Quaternion.identity);
            toDestroy.Add(clonedSecondButton2);
            Destroy(clonedSecondButton2, 0.5f);
        }
    }

    void showTopping1ThirdButton()
    {
        if (!toDestroy.Contains(clonedThirdButton2))
        {
            clonedThirdButton2 = Instantiate(thirdButton, new Vector3(-4.12f, -0.51f, 0), Quaternion.identity);
            toDestroy.Add(clonedThirdButton2);
            Destroy(clonedThirdButton2, 0.5f);
        }
    }

    void showTopping2FirstButton()
    {
        SpriteRenderer sr = null;

        if (!toDestroy.Contains(clonedOppositeFirstButton))
        {
            clonedOppositeFirstButton = Instantiate(oppositeFirstButton, new Vector3(5.03f, -0.34f, 0), Quaternion.identity);
            sr = clonedOppositeFirstButton.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 5;

            toDestroy.Add(clonedOppositeFirstButton);
            Destroy(clonedOppositeFirstButton, 0.5f);
            //Invoke("removeOppositeButton1", 0.5f);
        }
    }

    void showTopping2SecondButton()
    {
        SpriteRenderer sr = null;

        if (!toDestroy.Contains(clonedOppositeSecondButton))
        {
            clonedOppositeSecondButton = Instantiate(oppositeSecondButton, new Vector3(5.03f, -0.34f, 0), Quaternion.identity);
            sr = clonedOppositeSecondButton.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 5;

            toDestroy.Add(clonedOppositeSecondButton);
            Destroy(clonedOppositeSecondButton, 0.5f);
            //Invoke("removeOppositeButton2", 0.5f);
        }
    }

    void showTopping2ThirdButton()
    {
        SpriteRenderer sr = null;

        if (!toDestroy.Contains(clonedOppositeThirdButton))
        {
            clonedOppositeThirdButton = Instantiate(oppositeThirdButton, new Vector3(5.03f, -0.34f, 0), Quaternion.identity);
            sr = clonedOppositeThirdButton.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 5;

            toDestroy.Add(clonedOppositeThirdButton);
            Destroy(clonedOppositeThirdButton, 0.5f);
            //Invoke("removeOppositeButton3", 0.5f);
        }
    }

    public void showCoffeeMachineBack()
    {
        isDecoratingBack = false;
        isCoffeeMachineBack = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[4];
        nextButton.SetActive(false);

        if (!toDestroy.Contains(clonedGreenButton))
        {
            clonedGreenButton = Instantiate(greenButton, new Vector3(-1.86f, 3.78f, 0), Quaternion.identity);
            toDestroy.Add(clonedGreenButton);
        }

        if (!toDestroy.Contains(clonedGreenButton2))
        {
            clonedGreenButton2 = Instantiate(greenButton, new Vector3(2.97f, 3.78f, 0), Quaternion.identity);
            toDestroy.Add(clonedGreenButton2);
            clonedGreenButton2.gameObject.tag = "greenButton2";
        }

        Invoke("showCups", 1f);
    }

    void showCups()
    {
        if (!toDestroy.Contains(clonedWhiteCup))
        {
            clonedWhiteCup = Instantiate(whiteCup, new Vector3(-1.58f, -1.3f, 0), Quaternion.identity);
            toDestroy.Add(clonedWhiteCup);
        }

        if (!toDestroy.Contains(clonedKettle))
        {
            clonedKettle = Instantiate(kettle, new Vector3(1.53f, -0.61f, 0), Quaternion.identity);
            toDestroy.Add(clonedKettle);
        }
    }

    void makeCoffee()
    {
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
                    if (!toDestroy.Contains(clonedDropCoffee))
                    {
                        clonedDropCoffee = Instantiate(dropCoffee, new Vector3(-1.69f, 1.68f, 0), Quaternion.identity);
                        toDestroy.Add(clonedDropCoffee);

                        Destroy(clonedDropCoffee, 1f);
                    }
                    checkLiquid[0] = true;
                }

                if (rayHit.collider.gameObject.tag.Equals("greenButton2"))
                {
                    if (!toDestroy.Contains(clonedDropMilk))
                    {
                        clonedDropMilk = Instantiate(dropMilk, new Vector3(1.4412f, 1.6f, 0), Quaternion.identity);
                        toDestroy.Add(clonedDropMilk);

                        Destroy(clonedDropMilk, 1f);
                    }
                    checkLiquid[1] = true;
                }
            }
        }
        if (checkLiquid[0] && checkLiquid[1])
        {
            checkLiquid[0] = false;
            checkLiquid[1] = false;

            Invoke("showLatteArt", 2f);
        }
    }

    void showLatteArt()
    {
        isCoffeeMachineBack = false;
        isLatteArt = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[5];

        if (!toDestroy.Contains(clonedTopWhiteCup))
        {
            clonedTopWhiteCup = Instantiate(topWhiteCup, new Vector3(-2.98f, 0.13f, 0), Quaternion.identity);
            toDestroy.Add(clonedTopWhiteCup);
        }

        Invoke("showKettle", 1f);
    }

    void showKettle()
    {
        if (!toDestroy.Contains(clonedKettle))
        {
            clonedKettle = Instantiate(kettle, new Vector3(4.23f, -0.18f, 0), Quaternion.identity);
            clonedKettle.transform.localScale = new Vector3(0.26f, 0.26f, 1);

            toDestroy.Add(clonedKettle);
        }

        Invoke("showHeart", 0.5f);
    }

    void showHeart()
    {
        if (!toDestroy.Contains(clonedHeart))
        {
            clonedHeart = Instantiate(heart, new Vector3(-3.59f, -0.26f, 0), Quaternion.identity);
            clonedHeart.transform.localScale = new Vector3(0.6f, 0.6f, 1);

            heartLineLenderer = clonedHeart.GetComponent<LineRenderer>();

            for (int i = 0; i < heartLineLenderer.positionCount; i++)
            {
                Vector3 temp = heartLineLenderer.GetPosition(i);
                temp.z = 0f; // Set z value to 0

                temp.x = Mathf.Round(temp.x * 100f) / 100f; // Round x value to 2 decimal places
                temp.y = Mathf.Round(temp.y * 100f) / 100f; // Round y value to 2 decimal places

                heartPositions.Add(temp);
            }

            Vector3 pointToFind = new Vector3(-3.65f, 0.57f, 0f); // Set z value to 0
            bool pointExists = heartPositions.Contains(pointToFind);

            if (pointExists)
            {
                Debug.Log("Point exists in heart position list.");
            }
            else
                Debug.Log("no");

            //Debug.Log("heart positions" + heartPositions.Count);
            //Debug.Log("heartposition1" + heartPositions[0]);

            toDestroy.Add(clonedHeart);
            StartCoroutine(BlinkCoroutine());
        }
    }

    IEnumerator BlinkCoroutine()
    {
        float startTime = Time.time;

        while (Time.time - startTime < blinkDuration)
        {
            clonedHeart.SetActive(false);
            yield return new WaitForSeconds(0.1f);
            clonedHeart.SetActive(true);
            yield return new WaitForSeconds(0.1f);
        }
    }

    void doLatteArt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isKettleDragging = true;

            Vector2 mousePos = new Vector2(Input.mousePosition.x * Camera.main.aspect, Input.mousePosition.y);
            Vector3 dragPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            isKettleInsideSquare = true;
            kettlePoints.Clear();
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isKettleDragging = false;
            isKettleInsideSquare = false;

            lr.loop = true;

            //checkLineRendererPoints();

            if (checkLineRendererPoints()) //하트를 그렸으면
            {
                Invoke("showHeartCoffee", 0.5f);

                Invoke("showPerfect", 2.5f);
            }
            else if (!checkLineRendererPoints())
            {
                if (!toDestroy.Contains(clonedTryAgain))
                {
                    clonedTryAgain = Instantiate(tryAgain, new Vector3(0, 0, 0), Quaternion.identity);
                    toDestroy.Add(clonedTryAgain);

                    Invoke("hideTryAgain", 1.5f);
                }
            }

        }
        if (isKettleDragging)
        {
            Vector3 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            dragPosition.z = 0f;
            clonedKettle.transform.position = dragPosition;

            if (dragPosition.x >= -2.487374 && dragPosition.x <= 1.63
                && dragPosition.y >= -5.3 && dragPosition.y <= -1.29)
            {
                Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                pos.x -= 3.17f;
                pos.y += 3.15f;

                if (kettlePoints.Count == 0)
                {
                    //GameObject go = Instantiate(linePrefab, new Vector2(pos.x, pos.y), Quaternion.identity);
                    if (!toDestroy.Contains(clonedLinePrefab))
                    {
                        clonedLinePrefab = Instantiate(linePrefab, new Vector2(pos.x, pos.y), Quaternion.identity);
                        toDestroy.Add(clonedLinePrefab);
                    }

                    //lr = go.GetComponent<LineRenderer>();
                    //lineCol = go.GetComponent<EdgeCollider2D>();
                    //lr.useWorldSpace = true;
                    //lr.sortingOrder = 4;

                    lr = clonedLinePrefab.GetComponent<LineRenderer>();
                    lineCol = clonedLinePrefab.GetComponent<EdgeCollider2D>();
                    lr.useWorldSpace = true;
                    lr.sortingOrder = 4;

                    kettlePoints.Add(pos);
                    lr.positionCount++;
                    lr.SetPosition(lr.positionCount - 1, pos);
                    lineCol.points = kettlePoints.ToArray();

                    //drawnPositions.Add(pos);

                    Vector3 temp = new Vector3(Mathf.Round(pos.x * 100) / 100, Mathf.Round(pos.y * 100) / 100, 0);
                    drawnPositions.Add(temp);
                }
                else
                {
                    kettlePoints.Add(pos);
                    lr.positionCount++;
                    lr.sortingOrder = 4;
                    lr.SetPosition(lr.positionCount - 1, pos);
                    lineCol.points = kettlePoints.ToArray();

                    //drawnPositions.Add(pos);

                    Vector3 temp = new Vector3(Mathf.Round(pos.x * 100) / 100, Mathf.Round(pos.y * 100) / 100, 0);
                    drawnPositions.Add(temp);
                }
            }
        }
    }

    void showHeartCoffee()
    {
        if (!toDestroy.Contains(clonedHeartCoffee))
        {
            Destroy(clonedTopWhiteCup);
            Destroy(clonedLinePrefab);
            Destroy(clonedHeart);

            clonedHeartCoffee = Instantiate(heartCoffee, new Vector3(-3.4105f, -0.0157f, 0), Quaternion.identity);
            toDestroy.Add(clonedHeartCoffee);
        }
    }


    void hideTryAgain()
    {
        toDestroy.Remove(clonedTryAgain);
        Destroy(clonedTryAgain);
        Destroy(clonedLinePrefab);

        kettlePoints.Clear();
        Debug.Log("kettle point count " + kettlePoints.Count);
        toDestroy.Remove(clonedLinePrefab);

    }

    bool checkLineRendererPoints()
    {
        Vector3[] points = new Vector3[drawnPositions.Count];

        for (int i = 0; i < drawnPositions.Count; i++)
        {
            points[i] = drawnPositions[i];
        }

        Bounds bounds = GetBounds(points);
        Collider2D[] overlaps = Physics2D.OverlapAreaAll(bounds.min, bounds.max);

        Collider2D heartCol = clonedHeart.GetComponent<Collider2D>();

        float overlapPercentage = 0f;
        foreach (Collider2D overlap in overlaps)
        {
            if (overlap.gameObject.CompareTag("heart"))
            {
                overlapPercentage = CalculateOverlapPercentage(lr, overlap);
                break;
            }
        }

        //Debug.Log("heartCol" + heartCol);
        //Debug.Log("overlapPercentage" + overlapPercentage);

        if (overlapPercentage > 0.8f)
        {
            //Debug.Log("Shape overlaps with heart prefab by more than 80%!");
            return true;
        }
        else
        {
            //Debug.Log("Shape does not overlap with heart prefab enough");
            return false;
        }
    }

    Bounds GetBounds(Vector3[] points)
    {
        float minX = float.MaxValue;
        float minY = float.MaxValue;
        float maxX = float.MinValue;
        float maxY = float.MinValue;

        foreach (Vector3 point in points)
        {
            if (point.x < minX) minX = point.x;
            if (point.y < minY) minY = point.y;
            if (point.x < maxX) maxX = point.x;
            if (point.y > maxY) maxY = point.y;
        }

        return new Bounds(new Vector3((minX + maxX) / 2, (minY + maxY) / 2, 0), new Vector3(maxX - minX, maxY - minY, 1));
    }

    float CalculateOverlapArea(Bounds bounds)
    {
        Collider2D[] overlaps = Physics2D.OverlapAreaAll(bounds.min, bounds.max);
        float overlapArea = 0f;

        foreach (Collider2D overlap in overlaps)
        {
            //Debug.Log("overlap detected with " + overlap.gameObject.name);

            if (overlap.gameObject.CompareTag("heart"))
            {
                Collider2D heartCollider = overlap.gameObject.GetComponent<Collider2D>();

                //Debug.Log("bounds" + bounds);
                //Debug.Log("heartCollider bounds" + heartCollider.bounds);

                Camera mainCamera = Camera.main;
                if (GeometryUtility.TestPlanesAABB(GeometryUtility.CalculateFrustumPlanes(mainCamera), heartCollider.bounds))
                {
                    overlapArea += OverlapArea(bounds, heartCollider.bounds);
                }
            }
        }
        return overlapArea;
    }

    Bounds CalculateBounds(LineRenderer lineRenderer)
    {
        Vector3 min = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
        Vector3 max = new Vector3(float.MinValue, float.MinValue, float.MinValue);

        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            Vector3 point = lineRenderer.GetPosition(i);
            min = Vector3.Min(min, point);
            max = Vector3.Max(max, point);
        }

        Vector3 center = (min + max) / 2f;
        Vector3 size = max - min;

        return new Bounds(center, size);
    }

    float CalculateOverlapPercentage(LineRenderer lr, Collider2D heartCollider)
    {
        Bounds lineBounds = CalculateBounds(lr);
        Bounds heartBounds = heartCollider.bounds;

        if (!lineBounds.Intersects(heartBounds))
        {
            return 0f;
        }

        Bounds overlap = lineBounds;
        overlap.Intersects(heartBounds);

        float overlapArea = OverlapArea(overlap, heartBounds);
        float heartArea = heartBounds.size.x * heartBounds.size.y;

        float overlapPercentage = overlapArea / heartArea;

        return overlapPercentage;
    }

    float OverlapArea(Bounds bounds1, Bounds bounds2)
    {
        float xMin = Mathf.Max(bounds1.min.x, bounds2.min.x);
        float yMin = Mathf.Max(bounds1.min.y, bounds2.min.y);
        float xMax = Mathf.Min(bounds1.max.x, bounds2.max.x);
        float yMax = Mathf.Min(bounds1.max.y, bounds2.max.y);

        if (xMax > xMin && yMax > yMin)
        {
            return (xMax - xMin) * (yMax - yMin);
        }

        return 0f;
    }

}
