using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeerInstance : MonoBehaviour
{
    bool isSliding = false;

    public void setSliding(bool b)
    {
        isSliding = b;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSliding)
        {
            this.transform.position -= new Vector3(10, 0);
            if (this.transform.position.x < -2000)
            {
                GameObject.Destroy(gameObject);
            }
        }
    }

}
