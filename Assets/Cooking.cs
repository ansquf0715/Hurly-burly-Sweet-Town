using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cooking : MonoBehaviour //, IDragHandler
{
    public Sprite[] backGrounds = new Sprite[2];
    public GameObject BackGround;
    public SpriteRenderer backRenderer;
    public GameObject maker;
    public GameObject CookingMenu;
    public GameObject startButton;

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

    bool bowlBack = false; //사용하는지 확인할 것
    bool[] checkIngredients = new bool[3];
    Vector2 whipperPos;
    float distance = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        startButton.SetActive(false);
        Invoke("showMaker", 1f);
        Invoke("showMenu", 1.5f);
        Invoke("showStart", 2f);

        for (int i = 0; i < checkIngredients.Length; i++)
            checkIngredients[i] = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bowlBack == true)
            putIngredients();
        Mixing();
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

    void showBowl()
    {
        clonedBowl = Instantiate(bowl, new Vector3(0.18f, -1.4f, 0), Quaternion.identity);
    }

    void showBowlBack()
    {
        backRenderer.sprite = backGrounds[1];
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
                    checkIngredients[0] = true;
                    clonedFilledMilk = Instantiate(filledMilk, new Vector3(0.1679f, -2.1505f, 0), Quaternion.identity);
                    clonedMilkButton = Instantiate(milkButton, new Vector3(4.55f, -1.35f, 0), Quaternion.identity);
                    Destroy(clonedMilkButton, 1f);
                }
                if (rayHit.collider.gameObject.tag.Equals("flour"))
                {
                    checkIngredients[1] = true;
                    clonedFilledFlour = Instantiate(filledFlour, new Vector3(0.2046f, -0.9852f, 0), Quaternion.identity);
                    clonedFlourButton = Instantiate(flourButton, new Vector3(4.55f, -1.35f, 0), Quaternion.identity);
                    Destroy(clonedFlourButton, 1f);
                }
                if (rayHit.collider.gameObject.tag.Equals("egg"))
                {
                    checkIngredients[2] = true;
                    clonedCrackedEgg = Instantiate(crackedEgg, new Vector3(1.47f, 3.24f, 0), Quaternion.identity);
                    clonedEggButton = Instantiate(eggButton, new Vector3(3.85f, 1.83f, 0), Quaternion.identity);
                    Invoke("showInsideEgg", 0.5f);

                    Destroy(clonedCrackedEgg, 1.5f);
                    Destroy(clonedEggButton, 1.5f);
                }
                //if(rayHit.collider.gameObject.tag.Equals("whipper"))
                //{
                //    Debug.Log("whipper clicked");
                //}
            }
        }
    }

    bool isReady()
    {
        for(int i=0; i<checkIngredients.Length; i++)
        {
            if (checkIngredients[i] != true)
                return false;
        }
        return true;
    }

    void showWhipper()
    {
        clonedWhipper = Instantiate(whipper, new Vector3(2.07f, 1.3582f, 0), Quaternion.identity);
    }

    void Mixing()
    {
        if(isReady()==true)
        {
            Invoke("showWhipper", 1f);
        }
    }
    //void IDragHandler.OnDrag(PointerEventData eventData)
    //{
    //    //Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
    //    if(this.gameObject.tag.Equals("whipper"))
    //    {
    //        Debug.Log("whipper");
    //        clonedWhipper.transform.position = eventData.position;

    //    }
    //}

    private void OnMouseDrag()
    {
        Vector2 mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        Vector2 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //Debug.Log("mouse " + mousePosition);
        this.gameObject.transform.position = objPosition;

        //if (this.gameObject.tag.Equals("whipper"))
        //{
        //    Debug.Log("Drag!!");

        //    Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10);
        //    Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        //    this.gameObject.transform.position = objPosition;
        //}
    }
}
