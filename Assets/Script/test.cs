using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

public class test : MonoBehaviour
{
    List<Dictionary<string, object>> data;
    static List<Dictionary<string, object>> orders;

    TextMeshProUGUI NameText;
    TextMeshProUGUI SalespersonMessageText;
    TextMeshProUGUI CustomerMessageText;

    public GameObject SalespersonMessage;
    public GameObject CustomerMessage;

    public bool isTyping = false;
    public int csvNum = 0;
    menu menuScript;

    public GameObject order;
    public GameObject orderSheet;
    public GameObject menuChoice;
    GameObject cloneMenuChoice;
    string menuName;
    int[] ToppingList = new int[10];

    public float totalPrice = 0;

    //public int StageNum = 1;
    //public int OrderNum = 0;

    float Timer = 2f;

    int index = 0;

    AudioSource audioSource;
    public AudioClip talkingSound;
    bool isPlaying = false;

    // Start is called before the first frame update
    void Start()
    {
        //menuScript = GameObject.Find("GameSetting").GetComponent<menu>();
        menuScript = transform.GetComponent<menu>();
        orderSheet.SetActive(false);
        NameText = SalespersonMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        SalespersonMessageText = SalespersonMessage.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        CustomerMessageText = CustomerMessage.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        //menuChoice = transform.Find("menuChoice").gameObject;
        clearMessageText(SalespersonMessageText);
        clearMessageText(CustomerMessageText);

        data = CSVReader.Read("SweetTown");
        orders= CSVReader.Read("order");
        CustomerMessage.SetActive(false);

        //Debug.Log(menuScript.Topping[0].Name);

        saveToppingList(0);
        saveTotalPrice(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);

        //index = FindIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);
        //for (int i = 0; i < orders.Count; i++)
        //{
        //    if (orders[i]["stage"].ToString() == "2")
        //    {
        //        Debug.Log(i+" "+ orders[i]["menuNum"].ToString());
        //    }
        //}

        clearMessageText(orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>());

        //menuChoice.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        Debug.Log("audio source" + audioSource);
    }

    // Update is called once per frame
    void Update()
    {
        if (csvNum == 0 && !isTyping)
        {
            NameText.GetComponent<TextMeshProUGUI>().text = FindMessage(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, "SalespersonName", -1);// data[0]["message"].ToString();
            //StartCoroutine(typing(SalespersonMessageText, data[0]["message"].ToString()));
            StartCoroutine(typing(SalespersonMessageText, FindMessage(0, "SalespersonText", 0)));
        }
        else if (csvNum == 1 && !isTyping)
        {
            CustomerMessage.SetActive(true);

            //StartCoroutine(typing(CustomerMessageText, data[1]["message"].ToString()));
            StartCoroutine(typing(CustomerMessageText, FindOrder(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum)));
        }
        else if(csvNum==2 && !isTyping)
        {
            if (Timer > 0)
            {
                //Debug.Log(Timer);
                Timer -= Time.deltaTime;
                return;
            }
            else
            {
                setMessageGameObject(CustomerMessage, false);
                SalespersonMessage.SetActive(false);
                //menuChoice.SetActive(true);
                if (cloneMenuChoice == null)
                {
                    cloneMenuChoice = Instantiate(menuChoice, new Vector3(0, 0, 0), Quaternion.identity, GameObject.Find("Canvas").transform);
                    cloneMenuChoice.transform.localPosition = new Vector3(0, 0, 0);
                }

                Timer = 2f;
            }

            clearMessageText(SalespersonMessageText);

            FindMenuName();
            index = FindIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);
            //Debug.Log("index: " + index);
            if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 1)
            {
                orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text = "";

                orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                = "1 " + menuScript.Flavor[(int)orders[index]["Flavor"]].Name + " " + menuName;

                for (int i = orders[index]["Topping"].ToString().Length - 1; i >= 0; i--)
                {
                    orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "\n+ " + menuScript.Topping[int.Parse(orders[index]["Topping"].ToString().Substring(i, 1))].Name;
                }

                orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "\n+ " + menuScript.AddShotCream[(int)orders[index]["AddShotCream"]].Name;


                orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "\n1 " + menuScript.Beverage[(int)orders[index]["Beverage"]].Name;
            }

            else if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
            {
                orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text = "";

                for (int i = orders[index]["Flavor"].ToString().Length - 1; i >= 0; i--)
                {
                    //Debug.Log(i + menuScript.Flavor[int.Parse(orders[index]["Flavor"].ToString().Substring(i, 1))].Name);
                    if (i != orders[index]["Flavor"].ToString().Length - 1) orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text += "\n";
                    orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "1 " + menuScript.Flavor[int.Parse(orders[index]["Flavor"].ToString().Substring(i, 1))].Name + " " + menuName;

                    orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "\n+ " + menuScript.Topping[int.Parse(orders[index]["Topping"].ToString().Substring(i, 1))].Name;

                    orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "\n+ " + menuScript.AddShotCream[int.Parse(orders[index]["AddShotCream"].ToString().Substring(i, 1))].Name;

                }

                orderSheet.transform.Find("MainMenuText").GetComponent<TextMeshProUGUI>().text
                    += "\n1 " + menuScript.Beverage[(int)orders[index]["Beverage"]].Name
                    + "\n+ Whipped cream"
                    + "\n+ Choco syrup";
            }
            //saveTotalPrice(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);

            orderSheet.transform.Find("TotalPriceText").GetComponent<TextMeshProUGUI>().text
                = "$ " + orders[index]["TotalPrice"].ToString();

            if (cloneMenuChoice.GetComponent<menuChoice>().checkNext)
            {
                orderSheet.SetActive(true);
                StartCoroutine(typing(SalespersonMessageText, FindMessage(0, "SalespersonText", 1)));
                //cloneMenuChoice.GetComponent<menuChoice>().checkNext = false;
                Destroy(cloneMenuChoice);

                SalespersonMessage.SetActive(true);
            }

            //CustomerMessage.SetActive(false);


        }

        else if (csvNum == 3 && !isTyping)
        {
            //Invoke("setActiveCustomerMessage", 1f);
            CustomerMessage.SetActive(true);

            StartCoroutine(typing(CustomerMessageText, FindMessage(0, "CustomerText", 0)));
            PlayerPrefs.SetFloat("Money", PlayerPrefs.GetFloat("Money") + float.Parse(orders[index]["TotalPrice"].ToString()));

            
        }
        else if(csvNum==4 && !isTyping)
        {

            
            if (Timer > 0)
            {
                //Debug.Log(Timer);
                Timer -= Time.deltaTime;
                return;
            }
            else
            {
                //SceneManager.LoadScene("Stage1Cooking");
                if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 1)
                {
                    loadingSceneController.LoadScene("Stage1Cooking");
                }
                else if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
                {
                    loadingSceneController.LoadScene("Stage2");
                }
                DontDestroyOnLoad(GameObject.Find("GameSetting").gameObject);

                //�ӽ�
                //GameObject.Find("GameNum").GetComponent<GameNum>().OrderNum++;
                csvNum = 0;

                closeOrderSheet();
                clearMessageText(SalespersonMessageText);
                clearMessageText(CustomerMessageText);
                CustomerMessage.SetActive(false);

                Timer = 2f;

            }
        }

        if (isTyping)
        {
            //audioSource.clip = talkingSound;
            if(!isPlaying)
            {
                //audioSource.loop = true;
                //audioSource.volume = 1f;
                audioSource.Play();
                isPlaying = true;
            }
        }
        else if (!isTyping)
        {
            //audioSource.clip = null;
            audioSource.Stop();
            //audioSource.volume = 0f;
            isPlaying = false;
        }
    }

    string FindMessage(int stage, string type, int messageNum)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (int.Parse(data[i]["stage"].ToString()) == stage 
                && data[i]["type"].ToString() == type 
                && int.Parse(data[i]["messageNum"].ToString()) == messageNum)
            {
                //Debug.Log(data[i]["message"].ToString());
                return data[i]["message"].ToString();
            }
        }
        return null;
    }

    string FindOrder(int stage, int orderNum)
    {
        for (int i = 0; i < data.Count; i++)
        {
            if (data[i]["stage"].ToString() == stage.ToString()
                && data[i]["orderNum"].ToString() == orderNum.ToString())
            {
                //Debug.Log(data[i]["message"].ToString());
                return data[i]["message"].ToString();
            }
        }
        return null;
    }

    public int FindIndex(int stage, int orderNum)
    {
        for(int i=0; i<orders.Count; i++)
        {
            if(orders[i]["stage"].ToString()==stage.ToString()
                && orders[i]["orderNum"].ToString() == orderNum.ToString())
            {
                //Debug.Log(i);
                return i;
            }
        }
        return -1;
    }

    void saveTotalPrice(int stageNum, int orderNum)
    {
        index = FindIndex(stageNum, orderNum);

        if (stageNum == 1)
        {
            //Debug.Log(menuScript.Flavor[(int)orders[0]["Flavor"]].Price);
            totalPrice += menuScript.Flavor[(int)orders[index]["Flavor"]].Price;

            for (int i = orders[index]["Topping"].ToString().Length - 1; i >= 0; i--)
            {
                totalPrice += menuScript.Topping[int.Parse(orders[index]["Topping"].ToString().Substring(i, 1))].Price;
            }

            totalPrice += menuScript.AddShotCream[(int)orders[index]["AddShotCream"]].Price;
            totalPrice += menuScript.Beverage[(int)orders[index]["Beverage"]].Price;

            AddData(index, "TotalPrice", totalPrice);

            Debug.Log(orders[index]["TotalPrice"]);

            totalPrice = 0;
        }
        else if (stageNum == 2)
        {
            for (int i = orders[index]["Flavor"].ToString().Length - 1; i >= 0; i--)
            {
                //Debug.Log(i + menuScript.Flavor[int.Parse(orders[index]["Flavor"].ToString().Substring(i, 1))].Name);
                totalPrice += menuScript.Flavor[int.Parse(orders[index]["Flavor"].ToString().Substring(i, 1))].Price;
                totalPrice += menuScript.Topping[int.Parse(orders[index]["Topping"].ToString().Substring(i, 1))].Price;
                totalPrice += menuScript.AddShotCream[int.Parse(orders[index]["AddShotCream"].ToString().Substring(i, 1))].Price;
            }
            totalPrice += menuScript.Beverage[(int)orders[index]["Beverage"]].Price;
            AddData(index, "TotalPrice", totalPrice);

            totalPrice = 0;
        }
    }

    public static void AddData(int rowIndex, string columnName, object value)
    {
        if (orders.Count > rowIndex)
        {
            orders[rowIndex][columnName] = value;

            CSVReader.Write(orders, Application.dataPath + "/Resources/order.csv");
        }
    }


    void saveToppingList(int OrderNum)
    {
        for (int i = orders[OrderNum]["Topping"].ToString().Length - 1; i >= 0; i--)
        {
            ToppingList[i] = int.Parse(orders[OrderNum]["Topping"].ToString().Substring(i, 1));

            //totalPrice += menuScript.Topping[ToppingList[i]].Price;
        }
    }
 

    void FindMenuName()
    {
        if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 1) menuName = "pancakes";
        else if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2) menuName = "cupcake";
    }
    void setActiveGameObject()
    {
        if (csvNum == 2) CustomerMessage.SetActive(false);
    }

    void setMessageGameObject(GameObject MessageGameObject, bool isShow)
    {
        //Debug.Log("setActiveCustomerMessage");
        MessageGameObject.SetActive(isShow);
        clearMessageText(MessageGameObject.transform.GetChild(0).GetComponent<TextMeshProUGUI>());
    }

    void clearMessageText(TextMeshProUGUI text)
    {
        text.text = "";
    }

    IEnumerator typing(TextMeshProUGUI MessageText,string message)
    {
        //if (!MessageText.transform.parent.gameObject.activeSelf) MessageText.transform.parent.gameObject.SetActive(true);
        isTyping = true;
        yield return new WaitForSeconds(1f);


        for(int i=0; i< message.Length; i++)
        {
            MessageText.text = message.Substring(0, i+1);

            yield return new WaitForSeconds(0.02f);
        }

        csvNum++;
        isTyping = false;
    }

    public void closeOrderSheet()
    {
        orderSheet.SetActive(false);
    }
}
