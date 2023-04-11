using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursor : MonoBehaviour
{
    Texture2D DragHand;
    Texture2D Finger;
    Texture2D bellHand;
    Texture2D ovenHand;

    bool isMouseOver;

    // Start is called before the first frame update
    void Start()
    {
        DragHand = Resources.Load<Texture2D>("DragHand");
        Finger = Resources.Load<Texture2D>("Finger");
        bellHand = Resources.Load<Texture2D>("bellHand");
        ovenHand = Resources.Load<Texture2D>("ovenHand"); 
    }

    // Update is called once per frame
    void Update()
    {
        //SetOrigin();

        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(worldPos.x, worldPos.y);
        Ray2D ray = new Ray2D(touchPos, Vector2.zero);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction);
        

        if (rayHit.collider != null)
        {
            if (Input.GetMouseButton(0))
            {

                if (rayHit.collider.gameObject.tag.Equals("whipper"))
                {
                    OnDrag();
                }
                else if (rayHit.collider.gameObject.tag.Equals("bowl"))
                {
                    OnDrag();
                }
                else if (rayHit.collider.gameObject.tag.Equals("kettle"))
                {
                    OnDrag();
                }
                else if (rayHit.collider.gameObject.tag.Equals("pipingBag"))
                {
                    OnDrag();
                }
                else if (rayHit.collider.gameObject.tag.Equals("whipping"))
                {
                    OnDrag();
                }
                else if (rayHit.collider.gameObject.tag.Equals("choco"))
                {
                    OnDrag();
                }
            }

            else if (rayHit.collider.gameObject.tag.Equals("bell"))
            {
                onBell();
            }

            else if (rayHit.collider.gameObject.tag.Equals("ovenSwitch"))
            {
                OnOven();
            }
            else
            {
                SetOrigin();

            }

        }
        else
        {
            SetOrigin();
        }

        if (Input.GetMouseButtonUp(0))
        {
            SetOrigin();
        }
    }


    public void OnDrag()
    {
        Cursor.SetCursor(DragHand, new Vector2(0, 0), CursorMode.Auto);
    }

    public void SetOrigin()
    {
        Cursor.SetCursor(Finger, new Vector2(0, 0), CursorMode.Auto);
    }

    public void onBell()
    {
        Cursor.SetCursor(bellHand, new Vector2(0, 0), CursorMode.Auto);
    }
    public void OnOven()
    {
        Cursor.SetCursor(ovenHand, new Vector2(0, 0), CursorMode.Auto);

    }
}
