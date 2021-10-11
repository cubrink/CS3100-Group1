using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleLaunchScript : MonoBehaviour
{
    public GameObject missle;
    //May want to change launchDelay dynamically later to control difficulty
    public float launchDelay = 1.0f;
    float launchDelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        launchDelayTimer = launchDelay;
    }

    // Update is called once per frame
    void Update()
    {
        // Spawns incoming missles at a set interval
        launchDelayTimer -= Time.deltaTime;
        if (launchDelayTimer < 0)
        {
            launchDelayTimer = launchDelay;
            // Pick a random position on the grid
            Vector2 position;
            position.x = 0.5f + Random.Range(-8, 8);
            position.y = 0.5f + Random.Range(-8, 8);
            Instantiate(missle, position, Quaternion.identity);
        }
    }
}
