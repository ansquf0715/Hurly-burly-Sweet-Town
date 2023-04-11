using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer : MonoBehaviour
{
    public Vector3 goal;
    public Sprite surprisedCustomer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.localPosition.x <= goal.x)
        {
            transform.GetComponent<Animator>().SetBool("isWalking", false);
            transform.GetComponent<SpriteRenderer>().sprite = surprisedCustomer;

        }
        else
        {
            //transform.GetComponent<Animator>().SetBool("isWalking", true);

        }
    }
}
