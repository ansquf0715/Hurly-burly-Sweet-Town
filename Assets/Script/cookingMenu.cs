using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class cookingMenu : MonoBehaviour
{
    List<Dictionary<string, object>> orders;
    menu menuScript;

    // Start is called before the first frame update
    void Start()
    {
        //menuScript = GameObject.Find("GameSetting").GetComponent<menu>();
        //menuScript = GameObject.Find("Canvas").GetComponent<menu>();
        menuScript = transform.parent.parent.GetComponent<menu>();
        orders = CSVReader.Read("order");

        int index = FindIndex(GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum, GameObject.Find("GameSetting").GetComponent<GameNum>().OrderNum);


        if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 1)
        {
            transform.GetComponent<TextMeshProUGUI>().text = "";


            transform.GetComponent<TextMeshProUGUI>().text
                    = "1 " + menuScript.Flavor[(int)orders[index]["Flavor"]].Name + " pancakes";

            for (int i = orders[index]["Topping"].ToString().Length - 1; i >= 0; i--)
            {
                transform.GetComponent<TextMeshProUGUI>().text
                += "\n+ " + menuScript.Topping[int.Parse(orders[index]["Topping"].ToString().Substring(i, 1))].Name;
            }

            transform.GetComponent<TextMeshProUGUI>().text
                += "\n+ " + menuScript.AddShotCream[(int)orders[index]["AddShotCream"]].Name;


            transform.GetComponent<TextMeshProUGUI>().text
                += "\n1 " + menuScript.Beverage[(int)orders[index]["Beverage"]].Name;
        }



         else if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
         {
            transform.GetComponent<TextMeshProUGUI>().text = "";

            for (int i = orders[index]["Flavor"].ToString().Length - 1; i >= 0; i--)
            {
                //Debug.Log(i + menuScript.Flavor[int.Parse(orders[index]["Flavor"].ToString().Substring(i, 1))].Name);
                if (i != orders[index]["Flavor"].ToString().Length - 1) transform.GetComponent<TextMeshProUGUI>().text += "\n";
                transform.GetComponent<TextMeshProUGUI>().text
                += "1 " + menuScript.Flavor[int.Parse(orders[index]["Flavor"].ToString().Substring(i, 1))].Name + " cupcakes";

                transform.GetComponent<TextMeshProUGUI>().text
                += "\n+ " + menuScript.Topping[int.Parse(orders[index]["Topping"].ToString().Substring(i, 1))].Name;

                transform.GetComponent<TextMeshProUGUI>().text
                += "\n+ " + menuScript.AddShotCream[int.Parse(orders[index]["AddShotCream"].ToString().Substring(i, 1))].Name;

            }

            transform.GetComponent<TextMeshProUGUI>().text
                += "\n1 " + menuScript.Beverage[(int)orders[index]["Beverage"]].Name
                + "\n+ Whipped cream"
                + "\n+ Choco syrup";
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
