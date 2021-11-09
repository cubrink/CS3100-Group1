using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDropScript : MonoBehaviour
{
    PlacerScript levelController;
    public GameObject health;
    //May want to change dropDelay dynamically later to control difficulty
    public float dropDelay = 1.0f;
    float dropDelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<PlacerScript>();
        dropDelayTimer = dropDelay;
    }

    // Update is called once per frame
    void Update()
    {
        if(levelController.ShipsReady())
        {
            // Drops health at a set interval
            dropDelayTimer -= Time.deltaTime;
            if (dropDelayTimer < 0)
            {
                dropDelayTimer = dropDelay;
                // Pick a random position on the grid
                Vector2 position;
                position.x = 0.5f + Random.Range(-6, 6);
                position.y = 0.5f + Random.Range(-6, 6);
                Instantiate(health, position, Quaternion.identity);
            }
        }
    }
}

