using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
public class menuChoice : MonoBehaviour
{
    public Sprite buttonDefaultBackground;
    public Sprite buttonChoiceBackground;
    public Sprite ChoiceMenuBackground;
    public Sprite MenuBackground;

    //public Sprite BananaImage;
    //public Sprite BlueberryImage;
    //public Sprite StrawberryImage;

    public Sprite[] Stage1ToppingImage;
    public Sprite[] Stage1FlavorImage;
    public Sprite[] Stage1AddShotCreamImage;
    public Sprite[] Stage1BeverageImage;

    public Sprite[] Stage2ToppingImage;
    public Sprite[] Stage2FlavorImage;
    public Sprite[] Stage2AddShotCreamImage;
    public Sprite[] Stage2BeverageImage;


    GameObject[] OptionImage = new GameObject[3];
    bool showMenu = false;

    GameObject ToppingButton;
    GameObject FlavorButton;
    GameObject AddShotCreamButton;
    GameObject BeverageButton;

    menu menuScript;

    bool NextMenuCheck;

    bool isChoice = false;


    public bool[] ToppingList;
    public bool[] FlavorList;
    public bool[] AddShotCreamList;
    public bool[] BeverageList;

    string pageName;
    string listName;
    public bool checkNext;

    int stageNum = 0;

    List<Dictionary<string, object>> orders;

    // Start is called before the first frame update
    void Start()
    {
        orders = CSVReader.Read("order");

        //menuScript = GameObject.Find("GameSetting").GetComponent<menu>();
        menuScript = GameObject.Find("Canvas").GetComponent<menu>();

        ToppingButton = transform.Find("Button").GetChild(0).gameObject;
        FlavorButton = transform.Find("Button").GetChild(1).gameObject;
        AddShotCreamButton = transform.Find("Button").GetChild(2).gameObject;
        BeverageButton = transform.Find("Button").GetChild(3).gameObject;

        //Option1Image = transform.Find("Option1Image").gameObject;
        //Option2Image = transform.Find("Option2Image").gameObject;
        //Option3Image = transform.Find("Option3Image").gameObject;

        //stageNum = transform.parent.gameObject.GetComponent<test>().StageNum;
        stageNum = GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum;

        if (stageNum == 1)
        {
            ToppingList = new bool[Stage1ToppingImage.Length];
            FlavorList = new bool[Stage1FlavorImage.Length];
            AddShotCreamList = new bool[Stage1AddShotCreamImage.Length];
            BeverageList = new bool[Stage1BeverageImage.Length];
        }
        else if(stageNum == 2)
        {
            ToppingList = new bool[Stage2ToppingImage.Length];
            FlavorList = new bool[Stage2FlavorImage.Length];
            AddShotCreamList = new bool[Stage2AddShotCreamImage.Length];
            BeverageList = new bool[Stage2BeverageImage.Length];
        }

        for (int i = 0; i < 3; i++)
        {
            string obName = "Option" + (i + 1) + "Image";
            //Debug.Log(obName);
            OptionImage[i] = transform.Find(obName).gameObject;
        }

        ClickToppingButton();

    }

    // Update is called once per frame
    void Update()
    {
        //if (gameObject.activeSelf)
        //{
        //    Time.timeScale = 0;
        //}
        //if (!gameObject.activeSelf)
        //{
        //    Time.timeScale = 1;
        //}

        if (pageName == "Beverage" && BeverageList.Length > 3) setNextButton(true);
        else setNextButton(false);
    }

    void resetButtonBackground()
    {
        ToppingButton.GetComponent<Image>().sprite = buttonDefaultBackground;
        FlavorButton.GetComponent<Image>().sprite = buttonDefaultBackground;
        AddShotCreamButton.GetComponent<Image>().sprite = buttonDefaultBackground;
        BeverageButton.GetComponent<Image>().sprite = buttonDefaultBackground;
    }

    public void ClickToppingButton()
    {
        pageName = "Topping";

        //GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        resetButtonBackground();
        ToppingButton.GetComponent<Image>().sprite = buttonChoiceBackground;
        

        for (int i = 0; i < 3; i++)
        {
            if (ToppingList[i] == true)
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = ChoiceMenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = MenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = "$ " + menuScript.Topping[i].Price.ToString();
            }

            if (stageNum == 1)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage1ToppingImage[i];
            }
            else if (stageNum == 2)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage2ToppingImage[i];
            }


            OptionImage[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = menuScript.Topping[i].Name;
        }
    }

    public void ClickFlavorButton()
    {
        pageName = "Flavor";

        resetButtonBackground();
        FlavorButton.GetComponent<Image>().sprite = buttonChoiceBackground;

        for (int i = 0; i < 3; i++)
        {
            if (FlavorList[i] == true)
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = ChoiceMenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = MenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = "$ " + menuScript.Flavor[i].Price.ToString();
            }

            if (stageNum == 1)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage1FlavorImage[i];
            }
            else if (stageNum == 2)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage2FlavorImage[i];
            }


            OptionImage[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = menuScript.Flavor[i].Name;

        }
    }

    public void ClickAddShotCreamButton()
    {
        pageName = "AddShotCream";

        resetButtonBackground();
        AddShotCreamButton.GetComponent<Image>().sprite = buttonChoiceBackground;

        for (int i = 0; i < 3; i++)
        {
            if (AddShotCreamList[i] == true)
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = ChoiceMenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = MenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = "$ " + menuScript.AddShotCream[i].Price.ToString();
            }

            if (stageNum == 1)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage1AddShotCreamImage[i];
            }
            else if (stageNum == 2)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage2AddShotCreamImage[i];
            }

            OptionImage[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = menuScript.AddShotCream[i].Name;
        }
    }

    void setNextButton(bool state)
    {
        transform.Find("Button").GetChild(4).gameObject.SetActive(state);
    }
    public void ClickBeverageButton()
    {
        pageName = "Beverage";

        resetButtonBackground();
        BeverageButton.GetComponent<Image>().sprite = buttonChoiceBackground;


        for (int i = 0; i < 3; i++)
        {
            if (BeverageList[i] == true)
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = ChoiceMenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
            }
            else
            {
                OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = MenuBackground;
                OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                = "$ " + menuScript.Beverage[i].Price.ToString();
            }


            if (stageNum == 1)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage1BeverageImage[i];
            }
            else if (stageNum == 2)
            {
                OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage2BeverageImage[i];
            }

            OptionImage[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                = menuScript.Beverage[i].Name;
        }
    }

    public void ClickNextMenuButton()
    {
        NextMenuCheck = !NextMenuCheck;

        if (NextMenuCheck)
        {
            for (int i = 0; i < 3; i++)
            {
                if (BeverageList[i+3] == true)
                {
                    OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = ChoiceMenuBackground;
                    OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = MenuBackground;
                    OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = "$ " + menuScript.Beverage[i + 3].Price.ToString();
                }

                if (stageNum == 1)
                {
                    OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage1BeverageImage[i+3];
                }
                else if (stageNum == 2)
                {
                    OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage2BeverageImage[i+3];
                }

                //OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                //    = "$ " + menuScript.Beverage[i+3].Price.ToString();
                OptionImage[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = menuScript.Beverage[i+3].Name;
            }
        }
        else
        {
            for (int i = 0; i < 3; i++)
            {
                if (BeverageList[i] == true)
                {
                    OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = ChoiceMenuBackground;
                    OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
                }
                else
                {
                    OptionImage[i].transform.GetChild(0).GetComponent<Image>().sprite = MenuBackground;
                    OptionImage[i].transform.GetChild(0).GetChild(0).gameObject.SetActive(true);
                    OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                    = "$ " + menuScript.Beverage[i].Price.ToString();
                }

                if (stageNum == 1)
                {
                    OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage1BeverageImage[i];
                }
                else if (stageNum == 2)
                {
                    OptionImage[i].transform.GetChild(2).GetComponent<Image>().sprite = Stage2BeverageImage[i];
                }
               
                //OptionImage[i].transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text
                //    = "$ " + menuScript.Beverage[i].Price.ToString();
                OptionImage[i].transform.GetChild(1).GetComponent<TextMeshProUGUI>().text
                    = menuScript.Beverage[i].Name;
            }
        }
    }

    public void ChoiceMenu()
    {
        isChoice = !isChoice;

        listName = pageName + "List";

        GameObject clickObject = EventSystem.current.currentSelectedGameObject;
        if (clickObject.GetComponent<Image>().sprite == MenuBackground)
        {

            setChoiceState(clickObject, true, ChoiceMenuBackground);
            //ToppingList[int.Parse(clickObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = true;
            //setChoiceState(listName, clickObject);
            //Debug.Log("ToppingList" + (int.Parse(clickObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1) + ": "
            //    + ToppingList[int.Parse(clickObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1]);
        }
        else
        {
            setChoiceState(clickObject, false, MenuBackground);

            //ToppingList[int.Parse(clickObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = false;

            //Debug.Log("ToppingList" + (int.Parse(clickObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1) + ": "
            //               + ToppingList[int.Parse(clickObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1]);

        }

    }

    void setChoiceState(GameObject gameObject, bool state, Sprite background)
    {
        gameObject.GetComponent<Image>().sprite = background;
        //gameObject.transform.GetChild(0).gameObject.SetActive(!state);
        gameObject.transform.GetChild(0).gameObject.SetActive(!state);

        if (pageName == "Topping")
        {
            ToppingList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = state;
        }
        else if (pageName == "Flavor")
        {
            FlavorList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = state;
        }
        else if (pageName == "AddShotCream")
        {
            AddShotCreamList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = state;
        }
        else if (pageName == "Beverage")
        {
            if(Stage1BeverageImage.Length > 3)
            {
                if (NextMenuCheck)
                {
                    BeverageList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) + 2] = state;

                }
                else
                {
                    BeverageList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = state;

                }
            }
        }
    }

    //void setDefaultState(GameObject gameObject)
    //{
    //    if (pageName == "Topping")
    //    {
    //        ToppingList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = false;
    //    }
    //    else if (pageName == "Flavor")
    //    {
    //        FlavorList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = false;
    //    }
    //    else if (pageName == "AddShotCream")
    //    {
    //        AddShotCreamList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = false;
    //    }
    //    else if (pageName == "Beverage")
    //    {
    //        BeverageList[int.Parse(gameObject.transform.parent.transform.gameObject.name.Substring(6, 1)) - 1] = false;
    //    }
    //}

    public void ClickNextButton()
    {
        if (checkMenu())
        {
            Debug.Log("YES");
            //Time.timeScale = 1;

            for (int i = 0; i < ToppingList.Length; i++)
            {
                ToppingList[i] = false;
            }

            for (int i = 0; i < FlavorList.Length; i++)
            {
                FlavorList[i] = false;
            }

            for (int i = 0; i < AddShotCreamList.Length; i++)
            {
                AddShotCreamList[i] = false;
            }

            for (int i = 0; i < BeverageList.Length; i++)
            {
                BeverageList[i] = false;
            }
            //checkNext = false;
            //int index = FindIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);
            //float temp = 0;
            //Debug.Log(orders[index]["TotalPrice"]);

            ////temp = float.Parse(orders[index]["TotalPrice"].ToString());
            //temp = (float)orders[index]["TotalPrice"];
            //PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + temp);

            ClickToppingButton();
            transform.Find("Button").GetChild(4).gameObject.SetActive(false);
            gameObject.SetActive(false);

        }
        else
        {
            Debug.Log("NO");
        }
    }

    bool checkMenu()
    {
        string check = "";
        checkNext = true;

        //int index = transform.parent.gameObject.GetComponent<test>().FindIndex(stageNum, transform.parent.GetComponent<test>().OrderNum);
        int index = transform.parent.gameObject.GetComponent<test>().FindIndex(stageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);
        
        
        for (int i = 2; i >= 0; i--)
        {
            if (ToppingList[i] == true)
            {
                check += i.ToString();
            }
        }

        if (orders[index]["Topping"].ToString() != check) checkNext = false;

        check = "";
        for (int i = 2; i >= 0; i--)
        {
            if (FlavorList[i] == true)
            {
                check += i.ToString();
            }
        }
        if (orders[index]["Flavor"].ToString() != check) checkNext = false;

        check = "";
        for (int i = 2; i >= 0; i--)
        {
            if (AddShotCreamList[i] == true)
            {
                check += i.ToString();
            }
        }

        if (orders[index]["AddShotCream"].ToString() != check) checkNext = false;

        check = "";
        for (int i = BeverageList.Length-1; i >= 0; i--)
        {
            if (BeverageList[i] == true)
            {
                check += i.ToString();
            }
        }

        if (orders[index]["Beverage"].ToString() != check) checkNext = false;

        return checkNext;
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
