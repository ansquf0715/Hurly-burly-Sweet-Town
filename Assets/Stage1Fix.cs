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

    int[] menuList = new int[3]; //1:flavor 2~:topping
    public bool[] cutAllFruits = new bool[3];

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
            needBanana = false;
        }
    }

}
