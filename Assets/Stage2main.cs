using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage2main : MonoBehaviour
{
    public Sprite[] backGrounds = new Sprite[3];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;

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

    bool isMixing = false;
    bool isMuffinDough = false;

    public bool[] checkIngredients = new bool[5]; //0:우유, 1:밀가루, 2:계란, 3:설탕, 4:버터
    bool isPerfect = false;

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
    }

    void showCooker()
    {
        clonedCooker = Instantiate(cooker, new Vector3(3.820035f, -1.48f, 0), Quaternion.identity);
    }

    void showStartButton()
    {
        startButton.SetActive(true);
    }

    public void clickStartButton()
    {
        startButton.SetActive(false);
        Destroy(clonedCooker);
        Destroy(clonedMenu1);

        Invoke("showMixingBack", 1f);
    }

    void showMixingBack()
    {
        isMixing = true;

        backRenderer.sprite = backGrounds[0];

        clonedMilk = Instantiate(milk, new Vector3(-8.59f, -0.36f, 0), Quaternion.identity);
        clonedFlour = Instantiate(flour, new Vector3(-7.23f, 0.69f, 0), Quaternion.identity);
        clonedSugar = Instantiate(sugar, new Vector3(6.08f, 0.17f, 0), Quaternion.identity);
        clonedButter = Instantiate(butter, new Vector3(8.41f, -2.31f, 0), Quaternion.identity);
        clonedEggs = Instantiate(eggs, new Vector3(5.12f, -2.36f, 0), Quaternion.identity);

        SpriteRenderer sr = null;
        sr = clonedEggs.GetComponent<SpriteRenderer>();
        sr.sortingOrder = 3;

        Invoke("showBowl", 1f);
    }

    void showBowl()
    {
        clonedBowl = Instantiate(bowl, new Vector3(0.18f, -1.4f, 0), Quaternion.identity);
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
                    clonedFilledMilk.transform.localScale = new Vector3(0.2437005f, 0.2567469f, 0);
                    clonedMilkButton = Instantiate(milkButton, new Vector3(3.93f, -2.17f, 0), Quaternion.identity);
                    clonedMilkButton.transform.localEulerAngles = new Vector3(0, 0, -30);
                    Invoke("hideMilkButton", 1f);

                    SpriteRenderer sr = null;
                    sr = clonedMilkButton.GetComponent<SpriteRenderer>();
                    sr.sortingOrder = 4;
                }

                if(rayHit.collider.gameObject.tag.Equals("flour"))
                {
                    checkIngredients[1] = true;

                    clonedFilledFlour = Instantiate(filledFlour, new Vector3(0.2346f, -0.99f, 0), Quaternion.identity);
                    clonedFilledFlour.transform.localScale = new Vector3(0.2387243f, 0.2413525f, 0);
                    clonedFlourButton = Instantiate(flourButton, new Vector3(3.93f, -2.17f, 0), Quaternion.identity);
                    clonedFlourButton.transform.localEulerAngles = new Vector3(0, 0, -30);
                    Invoke("hideFlourButton", 1f);

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
                    Invoke("hideEggButton", 1f);
                }

                if(rayHit.collider.gameObject.tag.Equals("sugar"))
                {
                    checkIngredients[3] = true;

                    clonedFilledSugar = Instantiate(filledSugar, new Vector3(0.22f, -0.13f, 0), Quaternion.identity);
                    clonedSugarButton = Instantiate(sugarButton, new Vector3(3.93f, 2.18f, 0), Quaternion.identity);
                    Invoke("hideSugarButton", 1f);
                }

                if(rayHit.collider.gameObject.tag.Equals("butter"))
                {
                    checkIngredients[4] = true;

                    clonedSlicedButter = Instantiate(slicedButter, new Vector3(-0.93f, 2.13f, 0), Quaternion.identity);
                    clonedButterButton = Instantiate(butterButton, new Vector3(3.93f, 2.18f, 0), Quaternion.identity);

                    Invoke("butterDown", 1f);
                    Invoke("hideButterButton", 1f);
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
                        CancelInvoke("perfectDelay");
                        Invoke("showMuffinDoughBack", 1.5f);
                    }
                }
            }
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

    void hideMilkButton()
    {
        Destroy(clonedMilkButton);
    }

    void hideFlourButton()
    {
        Destroy(clonedFlourButton);
    }

    void hideEggButton()
    {
        Destroy(clonedCrackedEgg);
        Destroy(clonedEggButton);
    }

    void hideSugarButton()
    {
        Destroy(clonedSugarButton);
    }

    void hideButterButton()
    {
        Destroy(clonedButterButton);
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
    }

    void butterDown()
    {
        clonedSlicedButter.transform.position += new Vector3(0, -550 * Time.deltaTime * 2f, 0);
    }

    void perfectDelay()
    {
        isPerfect = true;
    }

    void showMuffinDoughBack()
    {
        isMixing = false;
        isMuffinDough = true;

        Destroy(clonedMilk);
        Destroy(clonedFlour);
        Destroy(clonedBowl);
        Destroy(clonedFilledMilk);
        Destroy(clonedFilledFlour);
        Destroy(clonedFilledSugar);
        Destroy(clonedSlicedButter);
        Destroy(clonedWhipper);
        Destroy(clonedSugar);
        Destroy(clonedEggs);
        Destroy(clonedButter);
        Destroy(clonedPerfect);

        backRenderer.sprite = backGrounds[1];
        Invoke("showMuffinTray", 1f);
        Invoke("showPipingBag", 1.5f);
    }

    void showMuffinTray()
    {
        clonedMuffinTray = Instantiate(muffinTray, new Vector3(-1.32f, -0.4f, 0), Quaternion.identity);
    }

    void showPipingBag()
    {
        clonedPipingBag = Instantiate(pipingBag, new Vector3(5.6f, 0.02f, 0), Quaternion.identity);
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

                //오른쪽 트레이
                if(objPosition.x >= 2 && objPosition.x<=5.5f
                    && objPosition.y >= 1.66f && objPosition.y <= 3.4)
                {

                }

                //왼쪽 트레이
                if(objPosition.x >= -3.05f && objPosition.x <= 0.49f
                    && objPosition.y >= 1.69f && objPosition.y <= 3.49f)
                {

                }
            }
        }
    }
}
