using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Timers;

public class Stage2main : MonoBehaviour
{
    public Sprite[] backGrounds = new Sprite[3];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;

    public GameObject startButton;

    List<GameObject> toDestroy = new List<GameObject>();

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
    public GameObject bowl;
    GameObject clonedBowl;
    public GameObject milkButton;
    GameObject clonedMilkButton;
    public GameObject flourButton;
    GameObject clonedFlourButton;
    public GameObject eggButton;
    GameObject clonedEggButton;
    public GameObject filledMilk;
    GameObject clonedFilledMilk;
    public GameObject filledFlour;
    GameObject clonedFilledFlour;
    public GameObject insideEgg;
    GameObject clonedInsideEgg;
    public GameObject crackedEgg;
    GameObject clonedCrackedEgg;
    public GameObject sugarButton;
    GameObject clonedSugarButton;
    public GameObject filledSugar;
    GameObject clonedFilledSugar;
    public GameObject butterButton;
    GameObject clonedButterButton;
    public GameObject slicedButter;
    GameObject clonedSlicedButter;
    public GameObject whipper;
    GameObject clonedWhipper;

    public GameObject muffinTray;
    GameObject clonedMuffinTray;
    public GameObject pipingBag;
    GameObject clonedPipingBag;
    public GameObject pinkDough;
    GameObject clonedPinkDough;
    public GameObject chocoDough;
    GameObject clonedChocoDough;

    public GameObject openedOven;
    GameObject clonedOpenedOven;
    public GameObject closedOven;
    GameObject clonedClosedOven;
    public GameObject filledMuffinTray;
    GameObject clonedFilledMuffinTray;
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
    public GameObject whiteSharpCream;
    GameObject clonedWhiteSharpCream;
    public GameObject cheeseSharpCream;
    GameObject clonedCheeseSharpCream;
    public GameObject miniGameTitle;
    GameObject clonedMiniGameTitle;
    public GameObject miniGameDirection;
    GameObject clonedMiniGameDirection;
    public GameObject chocoMuffinWithWhipping;
    GameObject clonedChocoMuffinWithWhipping;
    public GameObject cherryMuffin;
    GameObject clonedCherryMuffin;
    public GameObject iceBox;
    GameObject clonedIceBox;
    public GameObject yogurt;
    GameObject clonedYogurt;
    public GameObject strawberryCup;
    GameObject clonedStrawberryCup;
    public GameObject mixer;
    GameObject clonedMixer;
    public GameObject filledMilkForMixer;
    GameObject clonedFilledMilkForMixer;
    public GameObject ice;
    GameObject clonedIce;
    public GameObject strawberryBundle;
    GameObject clonedStrawberryBundle;
    public GameObject filledYogurt;
    GameObject clonedFilledYogurt;
    public GameObject mixerLid;
    GameObject clonedMixerLid;
    public GameObject mixerButton;
    GameObject clonedMixerButton;
    public GameObject fullMixer;
    GameObject clonedFullMixer;
    public GameObject completeMixer;
    GameObject clonedCompleteMixer;
    public GameObject pinkCup;
    GameObject clonedPinkCup;
    public GameObject whipping;
    GameObject clonedWhipping;
    public GameObject whiteCream;
    GameObject clonedWhiteCream;
    public GameObject chocoSyrup;
    GameObject clonedChocoSyrup;
    public GameObject dottedLine;
    GameObject clonedDottedLine;
    public GameObject chocoLine;
    GameObject clonedChocoLine;
    public GameObject bell;
    GameObject clonedBell;
    public GameObject finishMuffin;
    GameObject clonedFinishMuffin;
    public GameObject finishDrink;
    GameObject clonedFinishDrink;
    public GameObject complete;
    GameObject clonedComplete;


    Animator MixerAnim;

    bool isMixing = false;
    bool isMuffinDough = false;
    bool isbaking = false;
    bool isMuffinWhipping = false;
    bool isMinigame = false;
    bool isMixer = false;
    bool isDrink = false;
    bool isDeliver = false;

    public bool[] checkIngredients = new bool[5]; //0:����, 1:�а���, 2:���, 3:����, 4:����
    public bool[] checkMuffinDough = new bool[2]; //0:��ũ����, 1:���ڵ���
    public bool[] checkMuffinWhipping = new bool[2]; //0:�Ͼ� ũ��, 1: ġ��ũ��
    public bool[] checkMixerIngredients = new bool[4]; //0:����, 1:����, 2:����, 3:���Ʈ
    public bool[] checkDrinkDeco = new bool[2]; //0:����, 1: ���ڽ÷�

    bool isPerfect = false;
    bool isButterReady = false;
    bool isPinkDough = false;
    bool isChocoDough = false;
    bool isOvenReady = false;
    bool spinArrow = false;
    public bool changeMuffin = false;
    bool canMixThings = false;
    bool ChangeMixer = false;
    bool canMoveDish = false;
    bool canFinish = false;

    //GameObject[] randomDeco = new GameObject[5]; //����, ü��, ������, �ٳ���
    List<GameObject> clonedItems = new List<GameObject>();

    Vector3 targetPos = new Vector3(2.78f, -3.328f, 0);
    Vector2 objPos = new Vector2(-5.27f, -2.48f);
    public Vector2 nowPos;
    float[] bestScore = new float[5];

    // Start is called before the first frame update
    void Start()
    {
        //MixerAnim = fullMixer.GetComponent<Animator>();
        startButton.SetActive(false);
        showOrderBack();
    }

    // Update is called once per frame
    void Update()
    {
        if(isMixing)
        {
            Mix();
        }

        if(isMuffinDough)
        {
            MakeMuffinDough();
        }

        if(isbaking)
        {
            MuffinBaking();
        }

        if(isMuffinWhipping)
        {
            muffinWhipping();
        }

        if(isMinigame)
        {
            MiniGame();
        }

        if(isMixer)
        {
            Mixing();
        }

        if(isDrink)
        {
            decorateDrink();
        }

        if(isDeliver)
        {
            deliver();
        }
    }

    void showOrderBack()
    {
        Invoke("showCooker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStartButton", 2f);
    }

    void showMenu()
    {
        clonedMenu1=Instantiate(menu1, new Vector3(440, 510, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
        //clonedMenu1 = Instantiate(menu1, new Vector3(-4.2622f, 0.0368f, 0), Quaternion.identity);
        toDestroy.Add(clonedMenu1);
    }

    void showCooker()
    {
        clonedCooker = Instantiate(cooker, new Vector3(3.820035f, -1.48f, 0), Quaternion.identity);
        toDestroy.Add(clonedCooker);
    }

    void showStartButton()
    {
        startButton.SetActive(true);
    }

    public void clickStartButton()
    {
        startButton.SetActive(false);
        //Destroy(clonedCooker);
        //Destroy(clonedMenu1);

        int temp = toDestroy.Count;
        for(int i=0; i<temp; i++)
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

        clonedMilk = Instantiate(milk, new Vector3(-8.59f, -0.36f, 0), Quaternion.identity);
        toDestroy.Add(clonedMilk);
        clonedFlour = Instantiate(flour, new Vector3(-7.23f, 0.69f, 0), Quaternion.identity);
        toDestroy.Add(clonedFlour);
        clonedSugar = Instantiate(sugar, new Vector3(6.08f, 0.17f, 0), Quaternion.identity);
        toDestroy.Add(clonedSugar);
        clonedButter = Instantiate(butter, new Vector3(8.41f, -2.31f, 0), Quaternion.identity);
        toDestroy.Add(clonedButter);
        clonedEggs = Instantiate(eggs, new Vector3(5.12f, -2.36f, 0), Quaternion.identity);
        toDestroy.Add(clonedEggs);

        SpriteRenderer sr = null;
        sr = clonedEggs.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 3;

        Invoke("showBowl", 1f);
    }

    void showBowl()
    {
        clonedBowl = Instantiate(bowl, new Vector3(0.18f, -1.4f, 0), Quaternion.identity);
        toDestroy.Add(clonedBowl);
    }

    void Mix()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (Input.GetMouseButtonDown(0))
        {
            //Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            //Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            //RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if(rayHit.collider != null)
            {
                if(rayHit.collider.gameObject.tag.Equals("milk"))
                {
                    checkIngredients[0] = true;

                    clonedFilledMilk = Instantiate(filledMilk, new Vector3(0.1897f, -2.1815f, 0), Quaternion.identity);
                    toDestroy.Add(clonedFilledMilk);
                    clonedFilledMilk.transform.localScale = new Vector3(0.2437005f, 0.2567469f, 0);
                    clonedMilkButton = Instantiate(milkButton, new Vector3(3.93f, -2.17f, 0), Quaternion.identity);
                    clonedMilkButton.transform.localEulerAngles = new Vector3(0, 0, -30);
                    Destroy(clonedMilkButton, 1f);

                    SpriteRenderer sr = null;
                    sr = clonedMilkButton.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 4;
                }

                if(rayHit.collider.gameObject.tag.Equals("flour"))
                {
                    checkIngredients[1] = true;

                    clonedFilledFlour = Instantiate(filledFlour, new Vector3(0.2346f, -0.99f, 0), Quaternion.identity);
                    toDestroy.Add(clonedFilledFlour);
                    clonedFilledFlour.transform.localScale = new Vector3(0.2387243f, 0.2413525f, 0);
                    clonedFlourButton = Instantiate(flourButton, new Vector3(3.93f, -2.17f, 0), Quaternion.identity);
                    clonedFlourButton.transform.localEulerAngles = new Vector3(0, 0, -30);
                    Destroy(clonedFlourButton, 1f);

                    SpriteRenderer sr = null;
                    sr = clonedFlourButton.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 4;
                }

                if (rayHit.collider.gameObject.tag.Equals("egg"))
                {
                    checkIngredients[2] = true;

                    clonedCrackedEgg = Instantiate(crackedEgg, new Vector3(1.47f, 3.24f, 0), Quaternion.identity);
                    clonedEggButton = Instantiate(eggButton, new Vector3(3.85f, 1.83f, 0), Quaternion.identity);
                    Invoke("showInsideEgg", 0.1f);
                    Destroy(clonedEggButton, 1f);
                    Destroy(clonedCrackedEgg, 1f);
                }

                if(rayHit.collider.gameObject.tag.Equals("sugar"))
                {
                    checkIngredients[3] = true;

                    clonedFilledSugar = Instantiate(filledSugar, new Vector3(0.22f, -0.13f, 0), Quaternion.identity);
                    toDestroy.Add(clonedFilledSugar);
                    clonedSugarButton = Instantiate(sugarButton, new Vector3(3.93f, 2.18f, 0), Quaternion.identity);
                    Destroy(clonedSugarButton, 1f);
                }

                if(rayHit.collider.gameObject.tag.Equals("butter"))
                {
                    checkIngredients[4] = true;

                    clonedSlicedButter = Instantiate(slicedButter, new Vector3(-0.93f, 2.13f, 0), Quaternion.identity);
                    toDestroy.Add(clonedSlicedButter);
                    clonedButterButton = Instantiate(butterButton, new Vector3(3.93f, 2.18f, 0), Quaternion.identity);

                    //Invoke("butterDown", 1f);
                    isButterReady = true;
                    Destroy(clonedButterButton, 1f);
                }
            }
        }

        if(Input.GetMouseButton(0))
        {
            if(rayHit.collider!=null)
            {
                if(rayHit.collider.gameObject.tag.Equals("whipper"))
                {
                    OnMouseDrag();
                    Invoke("perfectDelay", 1.5f);

                    if(isPerfect)
                    {
                        isPerfect = false;
                        clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
                        toDestroy.Add(clonedPerfect);
                        CancelInvoke("perfectDelay");
                        Invoke("showMuffinDoughBack", 1.5f);
                    }
                }
            }
        }

        if(isButterReady)
        {
            do
            {
                clonedSlicedButter.transform.localPosition =
                    Vector3.MoveTowards(clonedSlicedButter.transform.position, new Vector3(-0.93f, 0.11f, 0), 3 * Time.deltaTime);
            } while (clonedSlicedButter.transform.position.y == 0.11);
            //isButterReady = false;
        }

        if(checkIngredient())
        {
            Invoke("showWhipper", 1f);

            for(int i=0; i<checkIngredients.Length; i++)
            {
                checkIngredients[i] = false;
            }
        }
    }

    bool checkIngredient()
    {
        for(int i=0; i<checkIngredients.Length; i++)
        {
            if(checkIngredients[i]==false)
            {
                return false;
            }
        }
        return true;
    }

    void showInsideEgg()
    {
        clonedInsideEgg = Instantiate(insideEgg, new Vector3(0, 1.4761f, 0), Quaternion.identity);
        Destroy(clonedInsideEgg, 1f);
    }

    private void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        if (objPosition.x >= -0.95 && objPosition.x <= 3.49
            && objPosition.y >= 0.16 && objPosition.y <= 1.47)
        {
            clonedWhipper.transform.position = objPosition;
        }
    }

    void showWhipper()
    {
        clonedWhipper = Instantiate(whipper, new Vector3(2.07f, 1.3582f, 0), Quaternion.identity);
        toDestroy.Add(clonedWhipper);
    }

    void butterDown()
    {
        do
        {
            clonedSlicedButter.transform.localPosition =
                Vector3.MoveTowards(clonedSlicedButter.transform.position, new Vector3(0, 0.11f, 0), 3 * Time.deltaTime);
        } while (clonedSlicedButter.transform.position.y == 0.11);
    }

    void perfectDelay()
    {
        isPerfect = true;
    }

    void showMuffinDoughBack()
    {
        isMixing = false;
        isMuffinDough = true;

        isPerfect = false;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[1];
        Invoke("showMuffinTray", 1f);
        Invoke("showPipingBag", 1.5f);
    }

    void showMuffinTray()
    {
        clonedMuffinTray = Instantiate(muffinTray, new Vector3(-1.32f, -0.4f, 0), Quaternion.identity);
        toDestroy.Add(clonedMuffinTray);
    }

    void showPipingBag()
    {
        clonedPipingBag = Instantiate(pipingBag, new Vector3(5.6f, 0.02f, 0), Quaternion.identity);
        toDestroy.Add(clonedPipingBag);
    }

    void MakeMuffinDough()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if(Input.GetMouseButton(0))
        {
            if(rayHit.collider.gameObject.tag.Equals("pipingBag"))
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                clonedPipingBag.transform.position = objPosition;

                //������ Ʈ����
                if (objPosition.x >= 2 && objPosition.x<=5.5f
                    && objPosition.y >= 1.66f && objPosition.y <= 3.4)
                {
                    //Debug.Log("Right Tray");

                    Invoke("delayPinkDough", 1f);
                    if(isPinkDough)
                    {
                        CancelInvoke("delayPinkDough");
                        isPinkDough = false;
                        clonedPinkDough = Instantiate(pinkDough, new Vector3(1.23f, 0.56f, 0), Quaternion.identity);
                        toDestroy.Add(clonedPinkDough);

                        checkMuffinDough[0] = true;
                    }
                }

                //���� Ʈ����
                if(objPosition.x >= -3.05f && objPosition.x <= 0.49f
                    && objPosition.y >= 1.69f && objPosition.y <= 3.49f)
                {
                    //Debug.Log("Left Tray");

                    Invoke("delayChocoDough", 1f);
                    if(isChocoDough)
                    {
                        CancelInvoke("delayChocoDough");
                        isChocoDough = false;
                        clonedChocoDough = Instantiate(chocoDough, new Vector3(-3.8622f, 0.6083f, 0), Quaternion.identity);
                        toDestroy.Add(clonedChocoDough);

                        checkMuffinDough[1] = true;
                    }
                }
            }
        }

        if(checkDough())
        {
            for (int i = 0; i < checkMuffinDough.Length; i++)
                checkMuffinDough[i] = false;

            Invoke("perfectDelay", 1.5f);
            if(isPerfect)
            {
                isPerfect = false;
                CancelInvoke("perfectDelay");
                clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
                toDestroy.Add(clonedPerfect);

                Invoke("showMuffinBakingBack", 1.5f);
            }
        }
    }

    void delayPinkDough()
    {
        isPinkDough = true;
    }

    void delayChocoDough()
    {
        isChocoDough = true;
    }

    bool checkDough()
    {
        for(int i=0; i<checkMuffinDough.Length; i++)
        {
            if (checkMuffinDough[i] == false)
                return false;
        }
        return true;
    }

    void showMuffinBakingBack()
    {
        isMuffinDough = false;
        isbaking = true;

        isPerfect = false;

        int temp = toDestroy.Count;
        for(int i=0; i<temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[0];
        clonedClosedOven = Instantiate(closedOven, new Vector3(0, -0.56f, 0), Quaternion.identity);
        clonedOvenSwitch = Instantiate(ovenSwitch, new Vector3(-3.85f, 2.4f, 0), Quaternion.identity);
        toDestroy.Add(clonedOvenSwitch);
    }

    void MuffinBaking()
    {
        bool stopRoastingButton = false;
        //Vector3 targetPos = new Vector3(2.78f, -3.328f, 0);

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if(Input.GetMouseButtonDown(0))
        {
            if(rayHit.collider != null)
            {
                if(rayHit.collider.gameObject.tag.Equals("ovenHandle"))
                {
                    Destroy(clonedClosedOven);
                    clonedOpenedOven = Instantiate(openedOven, new Vector3(0, -0.56f, 0), Quaternion.identity);
                    //toDestroy.Add(clonedOpenedOven);
                    clonedRedButton = Instantiate(redButton, new Vector3(-0.98f, 2.25f, 0), Quaternion.identity);
                    toDestroy.Add(clonedRedButton);
                    clonedRedButton.transform.localScale = new Vector3(0.25f, 0.25f, 0);

                    if(!toDestroy.Contains(clonedFilledMuffinTray))
                    {
                        clonedFilledMuffinTray = Instantiate(filledMuffinTray, new Vector3(0, -4.98f, 0), Quaternion.identity);
                        toDestroy.Add(clonedFilledMuffinTray);
                        isOvenReady = true;
                    }
                }

                if(rayHit.collider.gameObject.tag.Equals("ovenDoor"))
                {
                    Destroy(clonedOpenedOven);
                    clonedClosedOven = Instantiate(closedOven, new Vector3(0, -0.56f, 0), Quaternion.identity);
                    toDestroy.Add(clonedClosedOven);

                    SpriteRenderer muffinTraySR = clonedFilledMuffinTray.GetComponent<SpriteRenderer>();
                    Color c = muffinTraySR.color;
                    c.a = 0.5f;
                    muffinTraySR.color = c;
                }

                if(rayHit.collider.gameObject.tag.Equals("ovenSwitch"))
                {
                    if(!toDestroy.Contains(clonedTemperatureOrder))
                    {
                        clonedTemperatureOrder = Instantiate(temperatureOrder, new Vector3(-0.99f, 0.57f, 0), Quaternion.identity);
                        toDestroy.Add(clonedTemperatureOrder);
                        Destroy(clonedTemperatureOrder, 1f);
                    }

                    Invoke("showTemperatureChoice", 1.5f);
                }

                if(rayHit.collider.gameObject.tag.Equals("redButton"))
                {
                    //if(clonedRoastingButton.transform.position.x <= 0.72f && clonedRoastingButton.transform.position.x >= -3.328f)
                    //{
                    //    if(!toDestroy.Contains(clonedPerfect))
                    //    {
                    //        clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
                    //        toDestroy.Add(clonedPerfect);
                    //    }
                    //}
                    stopRoastingButton = true;
                    targetPos = clonedRoastingButton.transform.position;

                    if (!toDestroy.Contains(clonedPerfect))
                    {
                        clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
                        toDestroy.Add(clonedPerfect);

                        Invoke("showMuffinWhippingBack", 1.5f);
                    }
                }
            }
        }

        if(toDestroy.Contains(clonedRoastingButton))
        {
            if (clonedRoastingButton.transform.position.x >= 1.27f && clonedRoastingButton.transform.position.x <= 2.75f)
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
            if(checkTemperatureDegree())
            {
                Invoke("showRoasting", 1);
            }
            else if(!checkTemperatureDegree())
            {
                clonedX = Instantiate(x, new Vector3(-1.44f, -0.69f, 0), Quaternion.identity);
                Destroy(clonedX, 1f);
                spinArrow = true;
            }


        }

        if (isOvenReady)
        {
            clonedFilledMuffinTray.transform.localScale = new Vector3(0.25f, 0.25f, 0);
            do
            {
                clonedFilledMuffinTray.transform.localPosition =
                    Vector3.MoveTowards(clonedFilledMuffinTray.transform.position, new Vector3(0, -0.79f, 0), 5 * Time.deltaTime);
            } while (clonedFilledMuffinTray.transform.position.y == -0.79);

            //isOvenReady = false;
        }

        if(spinArrow)
        {
            clonedArrow.transform.Rotate(0, 0, -1);
        }

        if (toDestroy.Contains(clonedDegreeOfRoasting))
        {
            if(!stopRoastingButton)
            {
                clonedRoastingButton.transform.position = Vector3.MoveTowards(clonedRoastingButton.transform.position, targetPos , 1 * Time.deltaTime);
            }
        }
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
        if(clonedArrow.transform.rotation.eulerAngles.z >= 170
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

        if(!toDestroy.Contains(clonedDegreeOfRoasting))
        {
            clonedDegreeOfRoasting = Instantiate(degreeOfRoasting, new Vector3(0, -2.9f, 0), Quaternion.identity);
            toDestroy.Add(clonedDegreeOfRoasting);
        }

        if(!toDestroy.Contains(clonedRoastingButton))
        {
            clonedRoastingButton = Instantiate(roastingButton, new Vector3(-3.001f, -3.328f, 0), Quaternion.identity);
            toDestroy.Add(clonedRoastingButton);
        }
    }

    void showMuffinWhippingBack()
    {
        isbaking = false;
        isMuffinWhipping = true;

        backRenderer.sprite = backGrounds[2];

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        clonedGreyMuffinTray = Instantiate(greyMuffinTray, new Vector3(-0.06f, -2.17f, 0), Quaternion.identity);
        toDestroy.Add(clonedGreyMuffinTray);
        clonedChocoMuffinBread = Instantiate(chocoMuffinBread, new Vector3(-2.01f, -0.48f, 0), Quaternion.identity);
        toDestroy.Add(clonedChocoMuffinBread);
        clonedRedMuffinBread = Instantiate(redMuffinBread, new Vector3(1.89f, -0.03f, 0), Quaternion.identity);
        toDestroy.Add(clonedRedMuffinBread);

        Invoke("ClonePipingBag", 1f);
    }

    void ClonePipingBag()
    {
        clonedPipingBag = Instantiate(pipingBag, new Vector3(6.25f, 1.14f, 0), Quaternion.identity);
        clonedPipingBag.transform.localScale = new Vector3(1, 1, 0);
        toDestroy.Add(clonedPipingBag);
    }

    void muffinWhipping()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if(Input.GetMouseButton(0))
         {
            if(rayHit.collider.gameObject.tag.Equals("pipingBag"))
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                clonedPipingBag.transform.position = objPosition;

                //���ڸ���
                if(objPosition.x >= -1.44f && objPosition.x <= 0.76f
                    && objPosition.y >= 2f && objPosition.y <= 4f)
                {
                    if(!toDestroy.Contains(clonedWhiteSharpCream))
                    {
                        clonedWhiteSharpCream = Instantiate(whiteSharpCream, new Vector3(-2.17f, 1.74f, 0), Quaternion.identity);
                        toDestroy.Add(clonedWhiteSharpCream);
                        checkMuffinWhipping[0] = true;
                    }
                }
                //�������
                if(objPosition.x >= 2.54f && objPosition.x <= 4.69f
                    && objPosition.y >= 2f && objPosition.y <= 4f)
                {
                    if(!toDestroy.Contains(clonedCheeseSharpCream))
                    {
                        clonedCheeseSharpCream = Instantiate(cheeseSharpCream, new Vector3(1.78f, 1.78f, 0), Quaternion.identity);
                        toDestroy.Add(clonedCheeseSharpCream);
                        checkMuffinWhipping[1] = true;
                    }
                }
            }
        }

        if (checkwhipppingDone())
        {
            Invoke("showMiniGameTitle", 1f);

            for (int i = 0; i < checkMuffinWhipping.Length; i++)
                checkMuffinWhipping[i] = false;
        }
    }

    bool checkwhipppingDone()
    {
        for(int i=0; i<checkMuffinWhipping.Length; i++)
        {
            if (checkMuffinWhipping[i] == false)
                return false;
        }
        return true;
    }

    void showMiniGameTitle()
    {
        clonedMiniGameTitle = Instantiate(miniGameTitle, new Vector3(0, 0, 0), Quaternion.identity);
        toDestroy.Add(clonedMiniGameTitle);

        Invoke("showMiniGameBack", 1f);
    }

    void showMiniGameBack()
    {
        isMuffinWhipping = false;
        isMinigame = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[3];
        clonedMiniGameDirection = Instantiate(miniGameDirection, new Vector3(0, 0, 0), Quaternion.identity);
        Destroy(clonedMiniGameDirection, 3f);

        Invoke("showChocoCupCake", 4f);
    }

    void MiniGame()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if(toDestroy.Contains(clonedChocoMuffinWithWhipping))
        {
            if(Input.GetMouseButton(0))
            {
                Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                objPos = Camera.main.ScreenToWorldPoint(mousePosition);
                objPos.y = clonedChocoMuffinWithWhipping.transform.position.y;
            }
            clonedChocoMuffinWithWhipping.transform.position = Vector2.Lerp(clonedChocoMuffinWithWhipping.transform.position, objPos, Time.deltaTime * 2f);
        }

        if(changeMuffin)
        {
            if (!toDestroy.Contains(clonedCherryMuffin))
            {
                clonedCherryMuffin = Instantiate(cherryMuffin, nowPos, Quaternion.identity);
                toDestroy.Add(clonedCherryMuffin);
                changeMuffin = false;

                if(!toDestroy.Contains(clonedPerfect))
                {
                    clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
                    toDestroy.Add(clonedPerfect);

                    Invoke("showMixerBack", 1f);
                }
            }
        }
    }

    void showChocoCupCake()
    {
        clonedChocoMuffinWithWhipping = Instantiate(chocoMuffinWithWhipping, new Vector3(-5.27f, -2.48f, 0), Quaternion.identity);
        toDestroy.Add(clonedChocoMuffinWithWhipping);
    }

    void showMixerBack()
    {
        isMinigame = false;
        isMixer = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[4];

        clonedMilk = Instantiate(milk, new Vector3(-8.26f, -1.43f, 0), Quaternion.identity);
        clonedMilk.transform.localScale = new Vector3(3.497254f, 2.942825f, 1);
        toDestroy.Add(clonedMilk);
        clonedIceBox = Instantiate(iceBox, new Vector3(-5.48f, -0.71f, 0), Quaternion.identity);
        toDestroy.Add(clonedIceBox);
        clonedYogurt = Instantiate(yogurt, new Vector3(6.79f, -0.99f, 0), Quaternion.identity);
        toDestroy.Add(clonedYogurt);
        clonedStrawberryCup = Instantiate(strawberryCup, new Vector3(5.13f, -3.34f, 0), Quaternion.identity);
        toDestroy.Add(clonedStrawberryCup);
        clonedMixer = Instantiate(mixer, new Vector3(0, -1.27f, 0), Quaternion.identity);
        toDestroy.Add(clonedMixer);
        clonedMixerButton = Instantiate(mixerButton, new Vector3(0.06f, -2.81f, 0), Quaternion.identity);
        toDestroy.Add(clonedMixerButton);
    }

    void Mixing()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if(Input.GetMouseButtonDown(0))
        {
            if(rayHit.collider.gameObject.tag.Equals("milk"))
            {

                if (!toDestroy.Contains(clonedFilledMilkForMixer))
                {
                    clonedFilledMilkForMixer = Instantiate(filledMilkForMixer, new Vector3(-0.0148f, -0.1353f, 0), Quaternion.identity);
                    toDestroy.Add(clonedFilledMilkForMixer);
                    checkMixerIngredients[0] = true;
                }
            }

            if(rayHit.collider.gameObject.tag.Equals("iceBox"))
            {
                if(!toDestroy.Contains(clonedIce))
                {
                    clonedIce = Instantiate(ice, new Vector3(0.01f, 2.83f, 0), Quaternion.identity);
                    toDestroy.Add(clonedIce);
                    checkMixerIngredients[1] = true;
                }    
            }

            if(rayHit.collider.gameObject.tag.Equals("strawberryCup"))
            {
                if (!toDestroy.Contains(clonedStrawberryBundle))
                {
                    clonedStrawberryBundle = Instantiate(strawberryBundle, new Vector3(-0.01f, 2.13f, 0), Quaternion.identity);
                    clonedStrawberryBundle.transform.localEulerAngles = new Vector3(0, 0, 90f);
                    toDestroy.Add(clonedStrawberryBundle);

                    checkMixerIngredients[2] = true;
                }
            }

            if(rayHit.collider.gameObject.tag.Equals("yogurt"))
            {
                if(!toDestroy.Contains(clonedFilledYogurt))
                {
                    clonedFilledYogurt = Instantiate(filledYogurt, new Vector3(-0.08f, 3.07f, 0), Quaternion.identity);
                    clonedFilledYogurt.transform.localScale = new Vector3(0.13f, 0.16f, 0);
                    toDestroy.Add(clonedFilledYogurt);

                    checkMixerIngredients[3] = true;
                }
            }

            if (canMixThings)
            {
                if(rayHit.collider.gameObject.tag.Equals("bell")) //�ͼ���ư
                {
                    Destroy(clonedMixer);
                    Destroy(clonedFilledYogurt);
                    Destroy(clonedFilledMilkForMixer);
                    Destroy(clonedIce);
                    Destroy(clonedStrawberryBundle);
                    Destroy(clonedMixerLid);
                    Destroy(clonedMixerButton);

                    if (!toDestroy.Contains(clonedFullMixer))
                    {
                        clonedFullMixer = Instantiate(fullMixer, new Vector3(0, -0.24f, 0), Quaternion.identity);
                        toDestroy.Add(clonedFullMixer);
                        ChangeMixer = true;
                        MixerAnim = clonedFullMixer.GetComponent<Animator>();
                        MixerAnim.enabled = true;
                    }
                }
            }

            if (ChangeMixer)
            {
                ChangeMixer = false;
                if (!toDestroy.Contains(clonedCompleteMixer))
                {
                    Invoke("showCompleteMixer", 2f);

                    if (!toDestroy.Contains(clonedPerfect))
                    {
                        Invoke("showPerfect", 2.5f);
                    }
                }
            }
        }

        if(checkMixerIngredients[1]) //�����̵�
        {
            clonedIce.transform.position
                = Vector3.MoveTowards(clonedIce.transform.position, new Vector3(0.01f, 0.18f, 0), 2 * Time.deltaTime);
        }

        if(checkMixerIngredients[2]) //���� �̵�
        {
            clonedStrawberryBundle.transform.position
                = Vector3.MoveTowards(clonedStrawberryBundle.transform.position, new Vector3(-0.01f, 0.24f, 0), 2 * Time.deltaTime);
        }

        if(checkMixerIngredients[3]) //���Ʈ �̵�
        {
            clonedFilledYogurt.transform.position
                = Vector3.MoveTowards(clonedFilledYogurt.transform.position, new Vector3(0.13f, 1.31f, 0), 2 * Time.deltaTime);
        }

        if(checkAllMixer())
        {
            Invoke("delayAllMixer", 1f); //�ʿ��Ѱ�?

            if (!toDestroy.Contains(clonedMixerLid))
            {
                Invoke("showMixerLid", 0.5f);
                canMixThings = true;
            }
        }
        
    }

    bool checkAllMixer()
    {
        for(int i=0; i<checkMixerIngredients.Length; i++)
        {
            if (checkMixerIngredients[i] == false)
                return false;
        }
        return true;
    }

    void delayAllMixer()
    {
        for (int i = 0; i < checkMixerIngredients.Length; i++)
            checkMixerIngredients[i] = false;
    }

    void showMixerLid()
    {
        clonedMixerLid = Instantiate(mixerLid, new Vector3(-0.02f, 2.66f, 0), Quaternion.identity);
        toDestroy.Add(clonedMixerLid);
        CancelInvoke("showMixerLid");
    }

    void showPerfect()
    {
        //MixerAnim.enabled = false;
        clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
        toDestroy.Add(clonedPerfect);

        Invoke("showDrinkBack", 1f);
    }

    void showCompleteMixer()
    {
        Destroy(clonedFullMixer);
        clonedCompleteMixer = Instantiate(completeMixer, new Vector3(0, -0.24f, 0), Quaternion.identity);
        toDestroy.Add(clonedCompleteMixer);
    }

    void showDrinkBack()
    {
        isMixer = false;
        isDrink = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[5];

        clonedPinkCup = Instantiate(pinkCup, new Vector3(0.67f, -0.29f, 0), Quaternion.identity);
        toDestroy.Add(clonedPinkCup);

        Invoke("showWhipping", 0.5f);
    }

    void showWhipping()
    {
        clonedWhipping = Instantiate(whipping, new Vector3(5.82f, 1.29f, 0), Quaternion.identity);
        toDestroy.Add(clonedWhipping);
    }

    void decorateDrink()
    {
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

        if (rayHit.collider != null)
        {
            if(Input.GetMouseButton(0))
            {
                if (rayHit.collider.gameObject.tag.Equals("whipping"))
                {
                    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                    clonedWhipping.transform.position = objPosition;

                    if (objPosition.x >= 0.31f && objPosition.x <= 3.79f
                        && objPosition.y <= 0.12f && objPosition.y >= -4.31f)
                    {
                        if (!toDestroy.Contains(clonedWhiteCream))
                        {
                            clonedWhiteCream = Instantiate(whiteCream, new Vector3(0.05f, -0.31f, 0), Quaternion.identity);
                            toDestroy.Add(clonedWhiteCream);

                            Invoke("showChocoSyrup", 0.5f);
                        }
                    }
                }

                if(rayHit.collider.gameObject.tag.Equals("choco"))
                {
                    Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                    Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);

                    clonedChocoSyrup.transform.position = objPosition;

                    if(objPosition.x >= -3.35f && objPosition.x <= 1.22f
                        && objPosition.y <= -0.1f && objPosition.y >= -4.58f)
                    {
                        Destroy(clonedDottedLine, 0.2f);
                        if(!toDestroy.Contains(clonedChocoLine))
                        {
                            clonedChocoLine = Instantiate(chocoLine, new Vector3(-0.12f, -0.23f, 0), Quaternion.identity);
                            toDestroy.Add(clonedChocoLine);

                            Invoke("showJustPerfect", 0.8f);
                        }
                    }
                }
            }
        }
    }

    void showChocoSyrup()
    {
        if(!toDestroy.Contains(clonedChocoSyrup))
        {
            clonedChocoSyrup = Instantiate(chocoSyrup, new Vector3(-6.22f, 0.24f, 0), Quaternion.identity);
            toDestroy.Add(clonedChocoSyrup);
        }

        Invoke("showDottedLine", 1f);
    }

    void showDottedLine()
    {
        if(!toDestroy.Contains(dottedLine))
        {
            clonedDottedLine = Instantiate(dottedLine, new Vector3(0.11f, -0.26f, 0), Quaternion.identity);
            toDestroy.Add(clonedDottedLine);
        }
    }

    void showJustPerfect()
    {
        if(!toDestroy.Contains(clonedPerfect))
        {
            clonedPerfect = Instantiate(perfect, new Vector3(0, 0, 0), Quaternion.identity);
            toDestroy.Add(clonedPerfect);
        }

        Invoke("showDeliverBack", 1f);
    }

    void showDeliverBack()
    {
        isDrink = false;
        isDeliver = true;

        int temp = toDestroy.Count;
        for (int i = 0; i < temp; i++)
        {
            Destroy(toDestroy[0]);
            toDestroy.RemoveAt(0);
        }

        backRenderer.sprite = backGrounds[6];

        clonedBell = Instantiate(bell, new Vector3(-5.63f, 2.17f, 0), Quaternion.identity);
        toDestroy.Add(clonedBell);

        Invoke("showCompleteDish", 1f);
    }

    void deliver()
    {
        if(canMoveDish)
        {
            clonedFinishMuffin.transform.position
                = Vector3.MoveTowards(clonedFinishMuffin.transform.position, new Vector3(-0.25f, 0.17f, 0), 3 * Time.deltaTime);
            clonedFinishDrink.transform.position
                = Vector3.MoveTowards(clonedFinishDrink.transform.position, new Vector3(5.85f, 0.36f, 0), 3 * Time.deltaTime);

            canFinish = true;
        }

        if(canFinish && Input.GetMouseButtonDown(0))
        {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
            Ray2D ray = new Ray2D(touchPos, Vector2.zero);
            RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);

            if (rayHit.collider != null)
            {
                if (rayHit.collider.gameObject.tag.Equals("bell"))
                {
                    Invoke("showComplete", 1f);
                }
            }
        }
    }

    void showCompleteDish()
    {
        if(!toDestroy.Contains(clonedFinishMuffin))
        {
            clonedFinishMuffin = Instantiate(finishMuffin, new Vector3(-0.25f, -5.28f, 0), Quaternion.identity);
            toDestroy.Add(clonedFinishMuffin);
        }

        if(!toDestroy.Contains(clonedFinishDrink))
        {
            clonedFinishDrink = Instantiate(finishDrink, new Vector3(5.85f, -5.54f, 0), Quaternion.identity);
        }

        canMoveDish = true;
    }

    void showComplete()
    {
        if(!toDestroy.Contains(clonedComplete))
        {
            clonedComplete = Instantiate(complete, new Vector3(0, 0, 0), Quaternion.identity);
            toDestroy.Add(clonedComplete);
            GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum++;
            if (GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum == 3)
            {
                setScore(PlayerPrefs.GetFloat("Money"));

                Debug.Log("Ending");
                Invoke("LoadEndingScene", 1f);
                //if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
                //{
                //    Invoke("LoadStage2Scene", 1f);
                //}
                //GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum++;
                //GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum = 0;

                //if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 3)
                //{
                //    Invoke("LoadEndingScene", 1f);
                //}

            }
            else
            {
                Invoke("LoadStage1Scene", 1f);

            }

        }
    }
    void LoadStage1Scene()
    {
        SceneManager.LoadScene("Stage1");
    }

    void LoadEndingScene()
    {
        SceneManager.LoadScene("Ending");
    }
    void setScore(float currentScore)
    {
        PlayerPrefs.SetFloat("CurrentPlayerScore", currentScore);

        float tmpScore = 0f;
        for (int i = 0; i < 5; i++)
        {
            bestScore[i] = PlayerPrefs.GetFloat(i + "BestScore");

            while (bestScore[i] < currentScore)
            {
                tmpScore = bestScore[i];
                bestScore[i] = currentScore;

                PlayerPrefs.SetFloat(i + "BestScore", currentScore);

                currentScore = tmpScore;
            }
        }
        for (int i = 0; i < 5; i++)
        {
            PlayerPrefs.SetFloat(i + "BestScore", bestScore[i]);
        }
    }
}
