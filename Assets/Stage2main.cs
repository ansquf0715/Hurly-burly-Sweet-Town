using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    bool isMixing = false;
    bool isMuffinDough = false;
    bool isbaking = false;

    public bool[] checkIngredients = new bool[5]; //0:우유, 1:밀가루, 2:계란, 3:설탕, 4:버터
    public bool[] checkMuffinDough = new bool[2]; //0:핑크도우, 1:초코도우

    bool isPerfect = false;
    bool isButterReady = false;
    bool isPinkDough = false;
    bool isChocoDough = false;
    bool isOvenReady = false;
    bool spinArrow = false;

    // Start is called before the first frame update
    void Start()
    {
        //cooking = GameObject.Find("Cooking").GetComponent<>

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
    }

    void showOrderBack()
    {
        Invoke("showCooker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStartButton", 2f);
    }

    void showMenu()
    {
        clonedMenu1 = Instantiate(menu1, new Vector3(-4.2622f, 0.0368f, 0), Quaternion.identity);
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

                //오른쪽 트레이
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

                //왼쪽 트레이
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
            }
        }

        //if(Input.GetMouseButton(0))
        //{
        //    clonedArrow.transform.Rotate(Vector3.forward * Time.deltaTime * 15);
        //}

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

        if(toDestroy.Contains(clonedDegreeOfRoasting))
        {
            do
            {
                clonedRoastingButton.transform.localPosition =
                    Vector3.MoveTowards(clonedRoastingButton.transform.position, new Vector3(2.78f, -3.328f, 0), 1 * Time.deltaTime);
            } while (clonedRoastingButton.transform.position.x == 2.78f);
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

        clonedDegreeOfRoasting = Instantiate(degreeOfRoasting, new Vector3(0, -2.9f, 0), Quaternion.identity);
        toDestroy.Add(clonedDegreeOfRoasting);
        clonedRoastingButton = Instantiate(roastingButton, new Vector3(-3.001f, -3.328f, 0), Quaternion.identity);
        toDestroy.Add(clonedRoastingButton);
    }
}
