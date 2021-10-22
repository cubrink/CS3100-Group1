using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingMissileScript : MonoBehaviour
{
    public float missileHitTime = 5.0f;
    float missileTimer;
    ShipScript[] ships;
    // Start is called before the first frame update
    void Start()
    {
        missileTimer = 0;
        ships = FindObjectsOfType<ShipScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Red circle get bigger to indicate when the missile will "land"
        missileTimer += Time.deltaTime;
        float diameter = (missileTimer / missileHitTime) * 1.15f;
        transform.localScale = new Vector3(diameter, diameter, 0f);
        if (missileTimer > missileHitTime)
        {
            bool hit_ship = false;
            foreach (ShipScript ship in ships)
            {
                if (ship == null || ship.Health <= 0)
                {
                    // Skip ships that are not alive
                    continue;
                }

                float distance = Vector3.Distance(
                    ship.gameObject.transform.position,
                    gameObject.transform.position
                );
                if (distance <= diameter)
                {
                    // The ship has been hit
                    ship.ChangeHealth(-1);
                    hit_ship = true;
                }
            }

            if (!hit_ship)
            {
                // Instantiate a cannonball prefab
                // Where is this script called from!?
            }
            Destroy(transform.parent.gameObject);
        }
    }
}
