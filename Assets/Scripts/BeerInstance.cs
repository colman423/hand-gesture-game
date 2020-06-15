using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BeerInstance : MonoBehaviour
{
    public int speed = 10;
    public bool isSliding = false;
    public UnityAction onSlideOutMapAction;       // triggered when the beer is slide out the map.
    
    private void Start()
    {
        Debug.Log("start BeerInstance");
        // setSliding(true);
    }

    public void setSliding(bool b)
    {
        isSliding = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            this.transform.position -= new Vector3(speed, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "wall")
        {
            onTouchWall();
        }
    }

    void onTouchWall()
    {
        Debug.Log("BeerInstance onTouchWall");
        if (onSlideOutMapAction != null) onSlideOutMapAction();
        GameObject.Destroy(gameObject);
    }

    public void onTouchCustomer()
    {
        GameObject.Destroy(gameObject);
    }

}
