using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CustomerInstance : MonoBehaviour
{

    public float speed = 0;
    public enum DIRECTION
    {
        LEFT = -1,
        RIGHT = 1,
    }

    public DIRECTION direction;

    public Sprite leftSprite;
    public Sprite rightSprite;
    private Image image;

    public UnityAction onTouchFrontWallAction;       // triggered when the customer touch the front wall.
    public UnityAction<GameObject> onTouchBeerAction;       // triggered when the customer touch a beer.


    private void Start()
    {
        image = gameObject.GetComponent<Image>();
        setDirection(DIRECTION.RIGHT);
    }
    public void setDirection(DIRECTION dir)
    {
        direction = dir;
        if (dir == DIRECTION.LEFT) image.sprite = leftSprite;
        else image.sprite = rightSprite;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(speed * (int)direction, 0);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("OnTriggerEnter2D");
        if (other.tag == "beer" && direction == DIRECTION.RIGHT && other.GetComponent<BeerInstance>().isSliding)
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
        setDirection(DIRECTION.LEFT);
        onTouchBeerAction(beer);
    }

}
