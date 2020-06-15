using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CustomerInstance : MonoBehaviour
{

    public float speed = 0;
    public enum DIRECTION
    {
        LEFT = -1,
        RIGHT = 1,
    }

    public DIRECTION direction = DIRECTION.RIGHT;
    public UnityAction onTouchFrontWallAction;       // triggered when the customer touch the front wall.
    public UnityAction<GameObject> onTouchBeerAction;       // triggered when the customer touch a beer.

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * (int)direction, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.tag == "beer" && direction == DIRECTION.RIGHT)
        {
            onTouchBeer(other.gameObject);
        }
        else if (other.tag == "wall" && direction == DIRECTION.LEFT)
        {
            onTouchWall();
        }
        else if (other.tag == "frontWall")
        {
            onTouchFrontWall();
        }
    }

    void onTouchWall()
    {
        Debug.Log("CustomerInstance onTouchWall");
        GameObject.Destroy(gameObject);
    }

    void onTouchFrontWall()
    {
        Debug.Log("CustomerInstance onTouchFrontWall");
        onTouchFrontWallAction();
        GameObject.Destroy(gameObject);
    }

    void onTouchBeer(GameObject beer)
    {
        Debug.Log("CustomerInstance onTouchBeer");
        direction = DIRECTION.LEFT;
        onTouchBeerAction(beer);
    }

}
