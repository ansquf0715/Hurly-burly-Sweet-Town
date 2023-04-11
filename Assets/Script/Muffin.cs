using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muffin : MonoBehaviour
{
    public GameObject tryAgain;
    GameObject clonedTryAgain;

    Vector3 clonedPos;

    bool isTryAgain = false;
    //bool isCherry = false;

    GameObject[] randomDeco = new GameObject[6]; //����, ü��, ������, �ٳ���
    List<GameObject> clonedItems = new List<GameObject>();

    public GameObject strawberry;
    GameObject clonedStrawberry;
    public GameObject cherry;
    GameObject clonedCherry;
    public GameObject orange;
    GameObject clonedOrange;
    public GameObject banana;
    GameObject clonedBanana;
    public GameObject oreo;
    GameObject clonedOreo;

    float timeSinceLastClone = 0f;

    bakingEdit baking;
    int correctToppingNum;
    string correctToppingTag;


    //Stage2main main;

    //Vector2 nowPos;

    // Start is called before the first frame update
    void Start()
    {
        //main = GameObject.Find("forScript").GetComponent<Stage2main>();
        baking = GameObject.Find("forScript").GetComponent<bakingEdit>();

        randomDeco[0] = strawberry;
        randomDeco[0].transform.localScale = new Vector3(0.15f, 0.15f, 0);
        randomDeco[1] = cherry;
        randomDeco[2] = orange;
        randomDeco[3] = banana;
        randomDeco[3].transform.localScale = new Vector3(0.4f, 0.4f, 0);
        randomDeco[4] = oreo;

        correctToppingNum = baking.menuList[3];
        if (correctToppingNum == 0)
            correctToppingTag = "cherry";
        else if (correctToppingNum == 1)
            correctToppingTag = "oreo";
        else if (correctToppingNum == 3)
            correctToppingTag = "strawberryCup";

        Debug.Log("correct topping tag" + correctToppingTag);
    }

    // Update is called once per frame
    void Update()
    {
        randomFruits();
    }

    void randomFruits()
    {
        timeSinceLastClone += Time.deltaTime;
        if(timeSinceLastClone >= 2f)
        {
            for(int i=0; i<2; i++)
            {
                int rIndex = selectRandomIndex();
                Vector2 rPos = selectRandomPos();
                GameObject toAdd = Instantiate(randomDeco[rIndex], rPos, Quaternion.identity);

                clonedItems.Add(toAdd);
                baking.toDestroy.Add(toAdd);
            }
            timeSinceLastClone = 0f;
        }
        
        for(int i=clonedItems.Count-1; i>=0; i--)
        {
            if(clonedItems[i].transform.position.y <-3f)
            {
                GameObject itemToRemove = clonedItems[i];
                clonedItems.RemoveAt(i);
                Destroy(itemToRemove, 0.3f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("collision detected");
        //if(collision.collider.tag.Equals("bananaCup") || collision.collider.tag.Equals("strawberryCup")
        //    || collision.collider.tag.Equals("orange"))
        //{
        //    clonedPos.x = this.gameObject.transform.position.x + 3.94f;
        //    clonedPos.y = this.gameObject.transform.position.y;

        //    if(!isTryAgain)
        //    {
        //        clonedTryAgain = Instantiate(tryAgain, clonedPos, Quaternion.identity);
        //        Destroy(clonedTryAgain, 0.5f);
        //        isTryAgain = true;
        //        Invoke("delayTryAgain", 1f);

        //        Destroy(collision.collider.gameObject);
        //    }
            
        //}

        //if(collision.collider.tag.Equals("cherry"))
        //{
        //    main.changeMuffin = true;
        //    //nowPos = this.gameObject.transform.position;
        //    main.nowPos = this.gameObject.transform.position;
        //    Destroy(this.gameObject, 0.1f);
        //}

        if(collision.collider.tag.Equals(correctToppingTag))
        {
            baking.clearMiniGame = true;
            baking.nowPos = this.gameObject.transform.position;
            //Destroy(this.gameObject);
            Destroy(collision.gameObject);

            Debug.Log("baking clear mini game" + baking.clearMiniGame);
        }
        else
        {
            clonedPos.x = this.gameObject.transform.position.x + 3.94f;
            clonedPos.y = this.gameObject.transform.position.y;

            if (!isTryAgain)
            {
                clonedTryAgain = Instantiate(tryAgain, clonedPos, Quaternion.identity);
                Destroy(clonedTryAgain, 0.5f);
                isTryAgain = true;
                Invoke("delayTryAgain", 1f);

                Destroy(collision.collider.gameObject);
            }
        }
    }

    void delayTryAgain()
    {
        isTryAgain = false;
    }

    int selectRandomIndex()
    {
        int index = UnityEngine.Random.Range(0, 5);
        return index;
    }

    Vector2 selectRandomPos()
    {
        Vector2 pos;
        pos.x = UnityEngine.Random.Range(-7, 7);
        pos.y = 4;

        return pos;
    }
}
