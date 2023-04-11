using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class cookingRecipe : MonoBehaviour
{
    List<Dictionary<string, object>> orders;
    menu menuScript;

    public Sprite[] Stage1ToppingImage;
    public Sprite[] Stage1FlavorImage;

    // Start is called before the first frame update
    void Start()
    {
        orders = CSVReader.Read("order");
        menuScript = GameObject.Find("Canvas").GetComponent<menu>();
        int index = FindIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);

        for (int i = 0; i < 4; i++)
        {
            transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>().text = "";
        }

        transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = menuScript.AddShotCream[(int)orders[index]["AddShotCream"]].Name;
        transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "2 " + menuScript.Flavor[(int)orders[index]["Flavor"]].Name;
        transform.GetChild(4).gameObject.GetComponent<Image>().sprite = Stage1FlavorImage[(int)orders[index]["Flavor"]];

        for (int i = orders[index]["Topping"].ToString().Length - 1; i >= 0; i--)
        {
            int a = int.Parse(orders[index]["Topping"].ToString().Substring(i, 1));
            transform.GetChild(3 - i).gameObject.GetComponent<TextMeshProUGUI>().text = "3 " + menuScript.Topping[a].Name;
            transform.GetChild(6 - i).gameObject.GetComponent<Image>().sprite = Stage1ToppingImage[a];
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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
