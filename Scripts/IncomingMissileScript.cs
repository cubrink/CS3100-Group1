using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingMissileScript : MonoBehaviour
{
    public GameObject miss;
    public float missileHitTime = 5.0f;
    float missileTimer;
    //ShipScript[] ships;
    bool missileLand = false;
    bool shipTarget = false;
    ShipScript targetShip;
    PlacerScript levelController;

    void Awake()
    {
        //Audio for missile launch needs to be lowered if scatter strategy is
        //being used because it is very loud
        AudioSource audioSource = GetComponent<AudioSource>();
        MissileLaunchScript missileLaunch = FindObjectOfType<MissileLaunchScript>();
        if (missileLaunch.launchSelect == 3)
            audioSource.volume = 0.1f;
        audioSource.Play();
    }

    // Start is called before the first frame update
    void Start()
    {
        //ships = FindObjectsOfType<ShipScript>();
        missileTimer = 0;
        levelController = FindObjectOfType<PlacerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        // Red circle get bigger to indicate when the missile will "land"
        if (levelController.ShipsReady())
            missileTimer += Time.deltaTime;
        float diameter = (missileTimer / missileHitTime) * 1.15f;
        transform.localScale = new Vector3(diameter, diameter, 0f);
        if (missileTimer > missileHitTime)
        {
            //DetectHit(diameter);
            missileLand = true;
        }

        if (missileLand)
        {
            if (shipTarget)
            {
                FindObjectOfType<AudioManager>().Play("damage");
                targetShip.ChangeHealth(-1);
            }
            else
            {
                Instantiate(miss, gameObject.transform.position, Quaternion.identity);
            }
            Destroy(transform.parent.gameObject);
        }
    }

    /*
    void DetectHit(float diameter)
    {
        bool hit_ship = false;
        foreach (ShipScript ship in ships)
        {
            if (ship == null || ship.Health <= 0)
            {
                // Skip ships that are not alive
                continue;
            }

            // Get distance between missle and ship
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
            Instantiate(miss, gameObject.transform.position, Quaternion.identity);
        }
        Destroy(transform.parent.gameObject);
    }
    */

    void OnTriggerEnter2D(Collider2D other)
    {
        ShipScript ship = other.gameObject.GetComponent<ShipScript>();

        if (ship != null)
        {
            shipTarget = true;
            targetShip = ship;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ShipScript ship = other.gameObject.GetComponent<ShipScript>();

        if (ship != null)
        {
            shipTarget = false;
            targetShip = null;
        }
    }
}
