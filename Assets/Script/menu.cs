using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Material {
    public string Name;
    public float Price;
}

public class menu : MonoBehaviour
{
    //Material material = new Material();
    public Material[] Topping = new Material[3];
    public Material[] Flavor = new Material[3];
    public Material[] AddShotCream = new Material[3];
    public Material[] Beverage = new Material[6];

    //public int StageNum = 1;

    // Start is called before the first frame update
    void OnEnable()
    {

        if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum==1)
        {
            initializeMaterial(Topping, 0, "Banana", 1); 
            initializeMaterial(Topping, 1, "Blueberry", 1.5f);
            initializeMaterial(Topping, 2, "Strawberry", 2);

            initializeMaterial(Flavor, 0, "Strawberry", 7);
            initializeMaterial(Flavor, 1, "Banana", 7);
            initializeMaterial(Flavor, 2, "Chocolate", 7);

            initializeMaterial(AddShotCream, 0, "Chocolate Cream", 0.3f);
            initializeMaterial(AddShotCream, 1, "Milk Cream", 0.3f);
            initializeMaterial(AddShotCream, 2, "Strawberry Cream", 0.3f);

            initializeMaterial(Beverage, 0, "Milk", 2);
            initializeMaterial(Beverage, 1, "Banana Milk", 2);
            initializeMaterial(Beverage, 2, "Coke", 0.5f);
            initializeMaterial(Beverage, 3, "Espresso", 3);
            initializeMaterial(Beverage, 4, "Cocoa", 3.5f);
            initializeMaterial(Beverage, 5, "Cafe Latte", 4);
        }

        if (GameObject.Find("GameSetting").GetComponent<GameNum>().StageNum == 2)
        {
            initializeMaterial(Topping, 0, "Cherry", 1.5f);
            initializeMaterial(Topping, 1, "Oreo", 1.5f);
            initializeMaterial(Topping, 2, "Strawberry", 2);

            initializeMaterial(Flavor, 0, "Chocolate", 3.8f);
            initializeMaterial(Flavor, 1, "Red Velvet", 3.8f);
            initializeMaterial(Flavor, 2, "Vanilla", 3.8f);

            initializeMaterial(AddShotCream, 0, "Milk Cream", 0.3f);
            initializeMaterial(AddShotCream, 1, "Cream Cheese", 0.5f);
            initializeMaterial(AddShotCream, 2, "Choco Cream", 0.3f);

            initializeMaterial(Beverage, 0, "Strawberry shake", 4);
            initializeMaterial(Beverage, 1, "Chocolate shake", 4);
            initializeMaterial(Beverage, 2, "Vanilla shake", 4);
        }
    }

    void initializeMaterial( Material[] materials, int index, string Name, float Price)
    {
        materials[index].Name = Name;
        materials[index].Price = Price;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
