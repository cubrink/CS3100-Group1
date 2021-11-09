using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GettingHealth : MonoBehaviour
{
    bool found = false;
    ShipScript currentship;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(found)
        {
            currentship.ChangeHealth(1);
        }

    }
    
     void OnTriggerEnter2D(Collider2D other)
    {
        ShipScript ship = other.gameObject.GetComponent<ShipScript>();

        if (ship != null)
        {
            found = true;
            currentship = ship;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ShipScript ship = other.gameObject.GetComponent<ShipScript>();

        if (ship != null)
        {
            found = false;
            currentship = null;
        }
    }
}
