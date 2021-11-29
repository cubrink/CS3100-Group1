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
            Destroy(gameObject);
        }

    }
    
     void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag=="Missle")
        {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.tag=="Cannonball")
        {
            Destroy(other.gameObject);
            found=true;
        }
        else
        {
            ShipScript ship = other.gameObject.GetComponent<ShipScript>();

            if (ship != null)
            {
                //So ships can't pick up health between rounds
                if (!ship.IsATrigger())
                {
                    found = true;
                    currentship = ship;
                    FindObjectOfType<AudioManager>().Play("repair");
                    currentship.ChangeHealth(1);
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        
        found = false;
        currentship = null;
    }
}
