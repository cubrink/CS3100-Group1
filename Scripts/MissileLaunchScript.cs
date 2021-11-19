using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileLaunchScript : MonoBehaviour
{
    PlacerScript levelController;
    public GameObject missile;
    //May want to change launchDelay dynamically later to control difficulty
    public float launchDelay = 1.0f;
    float launchDelayTimer;
    const int MAX_ATTEMPTS = 1000; // Set upper bound of attempts to make when launching missile in case somehow the grid gets filled

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<PlacerScript>();
        launchDelayTimer = launchDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if (levelController.ShipsReady())
        {
            // Spawns incoming missiles at a set interval
            launchDelayTimer -= Time.deltaTime;
            if (launchDelayTimer < 0)
            {
                launchDelayTimer = launchDelay;
                // Pick a random position on the grid
                //vector2 position;
                //position.x = 0.5f + random.range(-6, 6);
                //position.y = 0.5f + random.range(-6, 6);
                //instantiate(missile, position, quaternion.identity);
                LaunchScatter();
            }
        }
    }

    /*
     * Returns a set of positions of existing missiles on the board
     */
    HashSet<Vector2> GetExistingMissiles()
    {
        // Create a set of all spaces used by circles or missiles
        CircleCollider2D[] colliders = FindObjectsOfType<CircleCollider2D>();
        HashSet<Vector2> obstacles = new HashSet<Vector2>();
        foreach (CircleCollider2D c in colliders)
        {
            if (c.name == "Circle" || c.name == "Cannonball(Clone)")
            {
                obstacles.Add(c.transform.position);
            }
        }
        return obstacles;
    }

    /*
     * Launch a missile at a random position
     */
    void LaunchRandom()
    {
        HashSet<Vector2> obstacles = GetExistingMissiles();

        
        for (int attempt = 0; attempt < MAX_ATTEMPTS; attempt++)
        {
            Vector2 target;
            target.x = 0.5f + Random.Range(-6, 6);
            target.y = 0.5f + Random.Range(-6, 6);
            if (!obstacles.Contains(target))
            {
                Instantiate(missile, target, Quaternion.identity);
                break;
            }
        }
    }

    /*
     * Launch a missile in line with a random ship
     */
    void LaunchInline()
    {
        HashSet<Vector2> obstacles = GetExistingMissiles();
        ShipScript[] ships = FindObjectsOfType<ShipScript>();

        if (ships.Length < 1)
        {
            // No ships found, exit
            Debug.Log("Attempted to launch missile inline but no ships found");
            return;
        }
        ShipScript ship = ships[Random.Range(0, ships.Length)];

        
        for (int attempt = 0; attempt < MAX_ATTEMPTS; attempt++)
        {
            // Pick a random value along the axis of movement of the ship
            // Funky rounding is needed in case another ship has push the ship 'off the grid'
            Vector2 target;
            if (ship.is_vertical)
            {
                target.x = (float) System.Math.Round(
                    (ship.transform.position.x - 0.5f),
                    System.MidpointRounding.AwayFromZero
                ) + 0.5f;
                target.y = 0.5f + Random.Range(-6, 6);
            }
            else
            {
                target.x = 0.5f + Random.Range(-6, 6);
                target.y = (float) System.Math.Round(
                    (ship.transform.position.y - 0.5f),
                    System.MidpointRounding.AwayFromZero
                ) + 0.5f;
            }
            Debug.Log("Target: " + target);
            if (!obstacles.Contains(target))
            {
                Instantiate(missile, target, Quaternion.identity);
                break;
            }
        }
    }

    /*
     * Launches several missiles concurrently, landing in random positions
     */
    void LaunchScatter()
    {
        HashSet<Vector2> obstacles = GetExistingMissiles();
        int missile_count = Random.Range(4, 9);
        List<Vector2> targets = new List<Vector2>();

        for (int attempt = 0; targets.Count < missile_count && attempt < MAX_ATTEMPTS; attempt++)
        {
            Vector2 target;
            target.x = 0.5f + Random.Range(-6, 6);
            target.y = 0.5f + Random.Range(-6, 6);
            if (!obstacles.Contains(target))
            {
                targets.Add(target);
                obstacles.Add(target);
            }
        }

        foreach (Vector2 target in targets)
        {
            Instantiate(missile, target, Quaternion.identity);
        }
    }

    /*
     * Launches a missile in line with the weakest ship
     */
    void LaunchWeakest()
    {
        return;
    }
}
