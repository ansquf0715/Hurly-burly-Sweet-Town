using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bakingEdit : MonoBehaviour
{
    public Sprite[] backGrounds = new Sprite[7];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;

    List<GameObject> toDestroy = new List<GameObject>();
    List<Dictionary<string, object>> orders;
    //0:flavor1, 1:flavor2, 2:topping1, 3:topping2, 4:cream1, 5:cream2, 6:beverage
    public int[] menuList = new int[7];

    public GameObject startButton;

    public GameObject perfect;
    GameObject clonedPerfect;

    public GameObject menu1;
    GameObject clonedMenu1;
    public GameObject cooker;
    GameObject clonedCooker;

    public GameObject milk;
    GameObject clonedMilk;
    public GameObject flour;
    GameObject clonedFlour;
    public GameObject sugar;
    GameObject clonedSugar;
    public GameObject butter;
    GameObject clonedButter;
    public GameObject eggs;
    GameObject clonedEggs;
    public GameObject insideEgg;
    GameObject clonedInsideEgg;
    public GameObject crackedEgg;
    GameObject clonedCrackedEgg;

    public GameObject bowl;
    GameObject clonedBowl;
    public GameObject whippper;
    GameObject clonedWhipper;

    public GameObject milkButton;
    GameObject clonedMilkButton;
    public GameObject flourButton;
    GameObject clonedFlourButton;
    public GameObject eggButton;
    GameObject clonedEggButton;
    public GameObject sugarButton;
    GameObject clonedSugarButton;
    public GameObject butterButton;
    GameObject clonedButterButton;

    public GameObject filledMilk;
    GameObject clonedFilledMilk;
    public GameObject filledFlour;
    GameObject clonedFilledFlour;
    public GameObject filledSugar;
    GameObject clonedFilledSugar;
    public GameObject slicedButter;
    GameObject clonedSlicedButter;

    public GameObject muffinTray;
    GameObject clonedMuffinTray;
    public GameObject pipingBag;
    GameObject clonedPipingBag;

    public GameObject pinkDough;
    GameObject clonedPinkDough;
    public GameObject chocoDough;
    GameObject clonedChocoDough;
    public GameObject vanillaDough;
    GameObject clonedVanillaDough;

    public GameObject openedOven;
    GameObject clonedOpenedOven;
    public GameObject closedOven;
    GameObject clonedClosedOven;

    public GameObject pinkChocoTray;
    GameObject clonedPinkChocoTray;
    public GameObject vanillaChocoTray;
    GameObject clonedVanillaChocoTray;
    public GameObject vanillaPinkTray;
    GameObject clonedVanillaPinkTray;

    //쓰나?
    GameObject clonedTray;

    public GameObject ovenSwitch;
    GameObject clonedOvenSwitch;
    public GameObject temperatureOrder;
    GameObject clonedTemperatureOrder;
    public GameObject temperatureChoice;
    GameObject clonedTemperatureChoice;
    public GameObject arrow;
    GameObject clonedArrow;
    public GameObject degreeOfRoasting;
    GameObject clonedDegreeOfRoasting;
    public GameObject roastingButton;
    GameObject clonedRoastingButton;
    public GameObject x;
    GameObject clonedX;
    public GameObject redButton;
    GameObject clonedRedButton;
    public GameObject warning;
    GameObject clonedWarning;
    public GameObject greyMuffinTray;
    GameObject clonedGreyMuffinTray;

    public GameObject chocoMuffinBread;
    GameObject clonedChocoMuffinBread;
    public GameObject redMuffinBread;
    GameObject clonedRedMuffinBread;


    bool isMixing = false;
    bool isMuffinDough = false;
    bool isBaking = false;

    public bool[] checkMixingIngredients = new bool[5]; //0:milk, 1:flour, 2:egg, 3:sugar, 4:butter
    public bool[] checkDoughReady = new bool[2]; //0:left tray, 1:right tray

    bool isButterReady = false;
    bool isOvenReady = false;
    bool spinArrow = false;

    Vector3 targetPos = new Vector3(2.78f, -3.328f, 0);
    Vector2 objPos = new Vector2(-5.27f, -2.48f);
    public Vector2 nowPos;

    // Start is called before the first frame update
    void Start()
    {
        orders = CSVReader.Read("order");

        startButton.SetActive(false);
        showOrderBack();

        checkMenu();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMixing)
        {
            Mix();
        }

        if (isMuffinDough)
        {
            makeMuffinDough();
        }

        if (isBaking)
        {
            Baking();
        }
    }

    public void checkMenu()
    {
        int index = findIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum,
            GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);

        string flavor = orders[index]["Flavor"].ToString();
        string topping = orders[index]["Topping"].ToString();
        string cream = orders[index]["AddShotCream"].ToString();
        string beverage = orders[index]["Beverage"].ToString();

        //0:flavor1, 1:flavor2, 2:topping1, 3:topping2, 4:cream1, 5:cream2, 6:beverage
        menuList[0] = int.Parse(flavor.Substring(0, 1)); //flavor1
        menuList[1] = int.Parse(flavor.Substring(1, 1)); //flavor2
        menuList[2] = int.Parse(topping.Substring(0, 1)); //topping1
        menuList[3] = int.Parse(topping.Substring(1, 1)); //topping2
        menuList[4] = int.Parse(cream.Substring(0, 1)); //cream1
        menuList[5] = int.Parse(cream.Substring(1, 1)); //cream2
        menuList[6] = int.Parse(beverage);
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

    void showOrderBack()
    {
        Invoke("showCooker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStartButton", 2f);
    }

    void showMenu()
    {
        if (!toDestroy.Contains(clonedMenu1))
        {
            clonedMenu1 = Instantiate(menu1, new Vector3(-4.2622f, 0.0368f, 0), Quaternion.identity);
            toDestroy.Add(clonedMenu1);
        }
    }

    void showCooker()
    {
        if (!toDestroy.Contains(clonedCooker))
        {
            clonedCooker = Instantiate(cooker, new Vector3(3.820035f, -1.48f, 0), Quaternion.identity);
            toDestroy.Add(clonedCooker);
        }
    }

    void showStartButton()
    {
        startButton.SetActive(true);
    }

    public void clickStartButton()
    {
        startButton.SetActive(false);

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        Invoke("showMixingBack", 1f);
    }

    void showMixingBack()
    {
        isMixing = true;

        backRenderer.sprite = backGrounds[0];

        if (!toDestroy.Contains(clonedMilk))
        {
            clonedMilk = Instantiate(milk, new Vector3(-8.59f, -0.36f, 0), Quaternion.identity);
            toDestroy.Add(clonedMilk);
        }

        if (!toDestroy.Contains(clonedFlour))
        {
            clonedFlour = Instantiate(flour, new Vector3(-7.23f, 0.69f, 0), Quaternion.identity);
            toDestroy.Add(clonedFlour);
        }

        if (!toDestroy.Contains(clonedSugar))
        {
            clonedSugar = Instantiate(sugar, new Vector3(6.08f, 0.17f, 0), Quaternion.identity);
            toDestroy.Add(clonedSugar);
        }

        if (!toDestroy.Contains(clonedButter))
        {
            clonedButter = Instantiate(butter, new Vector3(8.41f, -2.31f, 0), Quaternion.identity);
            toDestroy.Add(clonedButter);
        }

        if (!toDestroy.Contains(clonedEggs))
        {
            clonedEggs = Instantiate(eggs, new Vector3(5.12f, -2.36f, 0), Quaternion.identity);
            toDestroy.Add(clonedEggs);

            SpriteRenderer sr = null;
            sr = clonedEggs.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 3;
        }

        Invoke("showBowl", 1f);
    }

    void showBowl()
    {
        if (!toDestroy.Contains(clonedBowl))
        {
            clonedBowl = Instantiate(bowl, new Vector3(0.18f, -1.4f, 0), Quaternion.identity);
            toDestroy.Add(clonedBowl);
        }
    }

    void Mix()
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
                    checkMixingIngredients[0] = true;

                    if (!toDestroy.Contains(clonedFilledMilk))
                    {
                        clonedFilledMilk = Instantiate(filledMilk, new Vector3(0.1897f, -2.1815f, 0), Quaternion.identity);
                        clonedFilledMilk.transform.localScale = new Vector3(0.2437005f, 0.2567469f, 0);

                        toDestroy.Add(clonedFilledMilk);
                    }

                    clonedMilkButton = Instantiate(milkButton, new Vector3(3.93f, -2.17f, 0), Quaternion.identity);
                    clonedMilkButton.transform.localEulerAngles = new Vector3(0, 0, -30);
                    Destroy(clonedMilkButton, 1f);

                    SpriteRenderer sr = null;
                    sr = clonedMilkButton.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 4;
                }

                if (rayHit.collider.gameObject.tag.Equals("flour"))
                {
                    checkMixingIngredients[1] = true;

                    if (!toDestroy.Contains(clonedFilledFlour))
                    {
                        clonedFilledFlour = Instantiate(filledFlour, new Vector3(0.2346f, -0.99f, 0), Quaternion.identity);
                        clonedFilledFlour.transform.localScale = new Vector3(0.2387243f, 0.2413525f, 0);

                        toDestroy.Add(clonedFilledFlour);
                    }

                    clonedFlourButton = Instantiate(flourButton, new Vector3(3.93f, -2.17f, 0), Quaternion.identity);
                    clonedFlourButton.transform.localEulerAngles = new Vector3(0, 0, -30);
                    Destroy(clonedFlourButton, 1f);

                    SpriteRenderer sr = null;
                    sr = clonedFlourButton.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 4;
                }

                if (rayHit.collider.gameObject.tag.Equals("egg"))
                {
                    checkMixingIngredients[2] = true;

                    if (!toDestroy.Contains(clonedCrackedEgg))
                    {
                        clonedCrackedEgg = Instantiate(crackedEgg, new Vector3(1.47f, 3.24f, 0), Quaternion.identity);
                        toDestroy.Add(clonedCrackedEgg);

                        Invoke("showInsideEgg", 0.1f);

                        Destroy(clonedCrackedEgg, 1f);
                    }

                    clonedEggButton = Instantiate(eggButton, new Vector3(3.85f, 1.83f, 0), Quaternion.identity);
                    Destroy(clonedEggButton, 1f);
                }

                if (rayHit.collider.gameObject.tag.Equals("sugar"))
                {
                    checkMixingIngredients[3] = true;

                    if (!toDestroy.Contains(clonedFilledSugar))
                    {
                        clonedFilledSugar = Instantiate(filledSugar, new Vector3(0.22f, -0.13f, 0), Quaternion.identity);
                        toDestroy.Add(clonedFilledSugar);
                    }

                    clonedSugarButton = Instantiate(sugarButton, new Vector3(3.93f, 2.18f, 0), Quaternion.identity);
                    Destroy(clonedSugarButton, 1f);
                }

                if (rayHit.collider.gameObject.tag.Equals("butter"))
                {
                    checkMixingIngredients[4] = true;

                    if (!toDestroy.Contains(clonedSlicedButter))
                    {
                        clonedSlicedButter = Instantiate(slicedButter, new Vector3(-0.93f, 2.13f, 0), Quaternion.identity);
                        toDestroy.Add(clonedSlicedButter);
                    }

                    clonedButterButton = Instantiate(butterButton, new Vector3(3.93f, 2.18f, 0), Quaternion.identity);
                    Destroy(clonedButterButton, 1f);

                    isButterReady = true;
                }
            }
        }

        if (Input.GetMouseButton(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("whipper"))
                {
                    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                    if (objPosition.x >= -0.95 && objPosition.x <= 3.49
                        && objPosition.y >= 0.16 && objPosition.y <= 1.47)
                    {
                        clonedWhipper.transform.position = objPosition;
                    }

                    Invoke("showPerfect", 1.5f);

                    //3초 후 show muffin dough back
                    Invoke("showMuffinDough", 3f);
                }
            }
        }

        if (isButterReady)
        {
            do
            {
                clonedSlicedButter.transform.position =
                    Vector3.MoveTowards(clonedSlicedButter.transform.position, new Vector3(-0.93f, 0.11f, 0), 3 * Time.deltaTime);
            } while (clonedSlicedButter.transform.position.y == 0.11);
        }

        if (checkMixIngredients())
        {
            Invoke("showWhipper", 1f);

            for (int i = 0; i < checkMixingIngredients.Length; i++)
                checkMixingIngredients[i] = false;
        }

    }

    void showInsideEgg()
    {
        if (!toDestroy.Contains(clonedInsideEgg))
        {
            clonedInsideEgg = Instantiate(insideEgg, new Vector3(0, 1.4761f, 0), Quaternion.identity);
            toDestroy.Add(clonedInsideEgg);

            Destroy(clonedInsideEgg, 1f);
        }
    }

    bool checkMixIngredients()
    {
        for (int i = 0; i < checkMixingIngredients.Length; i++)
        {
            if (checkMixingIngredients[i] == false)
                return false;
        }
        return true;
    }

    void showWhipper()
    {
        if (!toDestroy.Contains(clonedWhipper))
        {
            clonedWhipper = Instantiate(whippper, new Vector3(2.07f, 1.3582f, 0), Quaternion.identity);
            toDestroy.Add(clonedWhipper);

            SpriteRenderer sr = null;
            sr = clonedWhipper.GetComponent<SpriteRenderer>();
            sr.sortingOrder = 2;
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

    void showMuffinDough()
    {
        isMixing = false;
        isMuffinDough = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[1];
        Invoke("showMuffinTray", 1f);
        Invoke("showPipingBag", 2f);
    }

    void showMuffinTray()
    {
        if (!toDestroy.Contains(clonedMuffinTray))
        {
            clonedMuffinTray = Instantiate(muffinTray, new Vector3(-1.32f, -0.4f, 0), Quaternion.identity);
            toDestroy.Add(clonedMuffinTray);
        }

    }

    void showPipingBag()
    {
        if (!toDestroy.Contains(clonedPipingBag))
        {
            clonedPipingBag = Instantiate(pipingBag, new Vector3(5.6f, 0.02f, 0), Quaternion.identity);
            toDestroy.Add(clonedPipingBag);
        }
    }

    void makeMuffinDough()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButton(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("pipingBag"))
                {
                    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                    clonedPipingBag.transform.position = objPosition;

                    //right tray
                    if (objPosition.x >= 2f && objPosition.x <= 5.5f
                        && objPosition.y >= 1.66f && objPosition.y <= 3.4f)
                    {
                        checkDoughReady[1] = true;
                        chooseRightDough();
                    }

                    if (objPosition.x >= -3.05f && objPosition.x <= 0.49f
                        && objPosition.y >= 1.69f && objPosition.y <= 3.49f)
                    {
                        checkDoughReady[0] = true;
                        chooseLeftDough();
                    }
                }
            }
        }

        if (checkDough())
        {
            for (int i = 0; i < checkDoughReady.Length; i++)
                checkDoughReady[i] = false;

            Invoke("showPerfect", 3f);
            Invoke("showMuffinBakingBack", 4.5f);
        }
    }

    void chooseLeftDough()
    {
        //parameter 1 means left tray
        switch (menuList[0])
        {
            case 0:
                //flavor 0: chocolate
                makeChocoDough(1);
                break;
            case 1:
                //flavor 1: red velvet
                makePinkDough(1);
                break;
            case 2:
                //flavor 2: vanilla
                makeVanillaDough(1);
                break;
        }
    }

    void chooseRightDough()
    {
        switch (menuList[1])
        {
            case 0:
                //flavor 0: chocolate
                makeChocoDough(2);
                break;
            case 1:
                //flavor 1: red velvet
                makePinkDough(2);
                break;
            case 2:
                //flavor 2 : vanilla
                makeVanillaDough(2);
                break;
        }
    }

    void makePinkDough(int pos)
    {
        if (pos == 1)
        {
            if (!toDestroy.Contains(clonedPinkDough))
            {
                clonedPinkDough = Instantiate(pinkDough, new Vector3(-3.8622f, 0.6083f, 0), Quaternion.identity);
                toDestroy.Add(clonedPinkDough);

                StartCoroutine(FadeInPinkCream());
            }
        }
        else if (pos == 2)
        {
            if (!toDestroy.Contains(clonedPinkDough))
            {
                clonedPinkDough = Instantiate(pinkDough, new Vector3(1.23f, 0.56f, 0), Quaternion.identity);
                toDestroy.Add(clonedPinkDough);

                StartCoroutine(FadeInPinkCream());
            }
        }
    }

    void makeChocoDough(int pos)
    {
        if (pos == 1)
        {
            if (!toDestroy.Contains(clonedChocoDough))
            {
                //이게 왼족
                clonedChocoDough = Instantiate(chocoDough, new Vector3(-3.8622f, 0.6083f, 0), Quaternion.identity);
                toDestroy.Add(clonedChocoDough);

                StartCoroutine(FadeInChocoCream());
            }
        }
        else if (pos == 2)
        {
            if (!toDestroy.Contains(clonedChocoDough))
            {
                clonedChocoDough = Instantiate(chocoDough, new Vector3(1.23f, 0.56f, 0), Quaternion.identity);
                toDestroy.Add(clonedChocoDough);

                StartCoroutine(FadeInChocoCream());
            }
        }

    }

    void makeVanillaDough(int pos)
    {
        if (pos == 1)
        {
            if (!toDestroy.Contains(clonedVanillaDough))
            {
                clonedVanillaDough = Instantiate(vanillaDough, new Vector3(-3.8622f, 0.6083f, 0), Quaternion.identity);
                toDestroy.Add(clonedVanillaDough);

                StartCoroutine(FadeInVanillaCream());
            }
        }
        else if (pos == 2)
        {
            if (!toDestroy.Contains(clonedVanillaDough))
            {
                clonedVanillaDough = Instantiate(vanillaDough, new Vector3(1.23f, 0.56f, 0), Quaternion.identity);
                toDestroy.Add(clonedVanillaDough);

                StartCoroutine(FadeInVanillaCream());
            }
        }
    }

    IEnumerator FadeInPinkCream()
    {
        float elapsedTime = 0f;
        SpriteRenderer pinkDoughRenderer;
        Color pinkDoughOriginalColor;
        float fadeDuration = 2f;

        pinkDoughRenderer = clonedPinkDough.GetComponent<SpriteRenderer>();
        pinkDoughOriginalColor = pinkDoughRenderer.color;
        pinkDoughRenderer.color = new Color(pinkDoughOriginalColor.r, pinkDoughOriginalColor.g,
            pinkDoughOriginalColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            pinkDoughRenderer.color = new Color(pinkDoughOriginalColor.r, pinkDoughOriginalColor.g,
                pinkDoughOriginalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        pinkDoughRenderer.color = pinkDoughOriginalColor;
    }

    IEnumerator FadeInChocoCream()
    {
        float elapsedTime = 0f;
        SpriteRenderer chocoDoughRenderer;
        Color chocoDoughOriginalColor;
        float fadeDuration = 2f;

        chocoDoughRenderer = clonedChocoDough.GetComponent<SpriteRenderer>();
        chocoDoughOriginalColor = chocoDoughRenderer.color;
        chocoDoughRenderer.color = new Color(chocoDoughOriginalColor.r, chocoDoughOriginalColor.g,
            chocoDoughOriginalColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            chocoDoughRenderer.color = new Color(chocoDoughOriginalColor.r, chocoDoughOriginalColor.g,
                chocoDoughOriginalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        chocoDoughRenderer.color = chocoDoughOriginalColor;
    }

    IEnumerator FadeInVanillaCream()
    {
        float elapsedTime = 0f;
        SpriteRenderer vanillaDoughRenderer;
        Color vanillaDoughOriginalColor;
        float fadeDuration = 2f;

        vanillaDoughRenderer = clonedVanillaDough.GetComponent<SpriteRenderer>();
        vanillaDoughOriginalColor = vanillaDoughRenderer.color;
        vanillaDoughRenderer.color = new Color(vanillaDoughOriginalColor.r, vanillaDoughOriginalColor.g,
            vanillaDoughOriginalColor.b, 0);

        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Lerp(0, 1, elapsedTime / fadeDuration);
            vanillaDoughRenderer.color = new Color(vanillaDoughOriginalColor.r, vanillaDoughOriginalColor.g,
                vanillaDoughOriginalColor.b, alpha);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        vanillaDoughRenderer.color = vanillaDoughOriginalColor;
    }

    bool checkDough()
    {
        for (int i = 0; i < checkDoughReady.Length; i++)
        {
            if (checkDoughReady[i] == false)
                return false;
        }
        return true;
    }

    void showMuffinBakingBack()
    {
        isMuffinDough = false;
        isBaking = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[1];

        if (!toDestroy.Contains(clonedClosedOven))
        {
            clonedClosedOven = Instantiate(closedOven, new Vector3(0, -0.56f, 0), Quaternion.identity);
            toDestroy.Add(clonedClosedOven);
        }

        if (!toDestroy.Contains(clonedOvenSwitch))
        {
            clonedOvenSwitch = Instantiate(ovenSwitch, new Vector3(-3.85f, 2.4f, 0), Quaternion.identity);
            toDestroy.Add(clonedOvenSwitch);
        }
    }

    void Baking()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        bool stopRoastingButton = false;

        if (Input.GetMouseButtonDown(0))
        {
            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("ovenHandle"))
                {
                    Destroy(clonedClosedOven);

                    if (!toDestroy.Contains(clonedOpenedOven))
                    {
                        clonedOpenedOven = Instantiate(openedOven, new Vector3(0, -0.56f, 0), Quaternion.identity);
                        //쓰나?
                        toDestroy.Add(clonedOpenedOven);
                    }

                    if (!toDestroy.Contains(clonedRedButton))
                    {
                        clonedRedButton = Instantiate(redButton, new Vector3(-0.98f, 2.25f, 0), Quaternion.identity);
                        clonedRedButton.transform.localScale = new Vector3(0.25f, 0.25f, 0);
                        toDestroy.Add(clonedRedButton);
                    }

                    if (!toDestroy.Contains(clonedTray))
                    {
                        clonedTray = Instantiate(chooseFilledTray(), new Vector3(0, -4.98f, 0), Quaternion.identity);
                        toDestroy.Add(clonedTray);
                        //쓰나?
                        //isOvenReady = true;
                    }
                }

                if (rayHit.collider.gameObject.tag.Equals("ovenDoor"))
                {
                    Destroy(clonedOpenedOven);
                    toDestroy.Remove(clonedOpenedOven);

                    if (!toDestroy.Contains(clonedClosedOven))
                    {
                        clonedClosedOven = Instantiate(closedOven, new Vector3(0, -0.56f, 0), Quaternion.identity);
                        toDestroy.Add(clonedClosedOven);
                    }

                    SpriteRenderer muffinTraySR = clonedTray.GetComponent<SpriteRenderer>();
                    Color c = muffinTraySR.color;
                    c.a = 0.5f;
                    muffinTraySR.color = c;
                }

                if (rayHit.collider.gameObject.tag.Equals("ovenSwitch"))
                {
                    if (!toDestroy.Contains(clonedTemperatureOrder))
                    {
                        clonedTemperatureOrder = Instantiate(temperatureOrder, new Vector3(-0.99f, 0.57f, 0), Quaternion.identity);
                        toDestroy.Add(clonedTemperatureOrder);
                        Destroy(clonedTemperatureOrder, 1f);
                    }

                    Invoke("showTemperatureChoice", 1.5f);
                }

                if (rayHit.collider.gameObject.tag.Equals("redButton"))
                {
                    stopRoastingButton = true;
                    targetPos = clonedRoastingButton.transform.position;

                    Invoke("showPerfect", 0.5f);
                    // 718
                }
            }
        }

        if (toDestroy.Contains(clonedRoastingButton))
        {
            if (clonedRoastingButton.transform.position.x >= 1.27f
                && clonedRoastingButton.transform.position.x <= 2.75f)
            {
                if (!toDestroy.Contains(clonedWarning))
                {
                    clonedWarning = Instantiate(warning, new Vector3(5.86f, 0.46f, 0), Quaternion.identity);
                    toDestroy.Add(clonedWarning);
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            spinArrow = false;
            if (checkTemperatureDegree())
            {
                Invoke("showRoasting", 1);
            }
            else if (!checkTemperatureDegree())
            {
                clonedX = Instantiate(x, new Vector3(-1.44f, -0.69f, 0), Quaternion.identity);
                Destroy(clonedX, 1f);
                spinArrow = true;
            }
        }

        if (isOvenReady)
        {
            clonedTray.transform.localScale = new Vector3(0.25f, 0.25f, 0);

            do
            {
                clonedTray.transform.localPosition =
                    Vector3.MoveTowards(clonedTray.transform.position,
                    new Vector3(0, -0.79f, 0), 5 * Time.deltaTime);
            } while (clonedTray.transform.position.y == -0.79);
        }

        if (spinArrow)
        {
            clonedArrow.transform.Rotate(0, 0, -1);
        }

        if (toDestroy.Contains(clonedDegreeOfRoasting))
        {
            if (!stopRoastingButton)
            {
                clonedRoastingButton.transform.position =
                    Vector3.MoveTowards(clonedRoastingButton.transform.position,
                    targetPos, 1 * Time.deltaTime);
            }
        }
    }

    GameObject chooseFilledTray()
    {
        if (menuList[0] == 1 && menuList[1] == 0)
            return pinkChocoTray;
        else if (menuList[0] == 2 && menuList[1] == 0)
            return vanillaChocoTray;
        else if (menuList[0] == 2 && menuList[1] == 1)
            return vanillaPinkTray;
        else
            return null;
    }

    void showTemperatureChoice()
    {
        if (!toDestroy.Contains(clonedTemperatureChoice))
        {
            clonedTemperatureChoice = Instantiate(temperatureChoice, new Vector3(-1.35f, -0.29f, 0), Quaternion.identity);
            toDestroy.Add(clonedTemperatureChoice);
        }

        if (!toDestroy.Contains(clonedArrow))
        {
            clonedArrow = Instantiate(arrow, new Vector3(-1.35f, -0.62f, 0), Quaternion.identity);
            toDestroy.Add(clonedArrow);

            Invoke("startRotateArrow", 1f);
        }
    }

    void startRotateArrow()
    {
        spinArrow = true;
    }

    bool checkTemperatureDegree()
    {
        if (clonedArrow.transform.rotation.eulerAngles.z >= 170
            && clonedArrow.transform.rotation.eulerAngles.z <= 190)
        {
            return true;
        }
        return false;
    }

    void showRoasting()
    {
        Destroy(clonedArrow);
        Destroy(clonedTemperatureChoice);

        if (!toDestroy.Contains(clonedDegreeOfRoasting))
        {
            clonedDegreeOfRoasting = Instantiate(degreeOfRoasting, new Vector3(0, -2.9f, 0), Quaternion.identity);
            toDestroy.Add(clonedDegreeOfRoasting);
        }

        if (!toDestroy.Contains(clonedRoastingButton))
        {
            clonedRoastingButton = Instantiate(roastingButton, new Vector3(-3.001f, -3.328f, 0), Quaternion.identity);
            toDestroy.Add(clonedRoastingButton);
        }
    }

}
