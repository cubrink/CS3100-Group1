using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoringScript : MonoBehaviour
{
    public const int POINTS_PER_HEALTH = 200;
    public const int POINTS_PER_MISS = 10;

    private int _total_score;

    public int total_score
    {
        get { return _total_score; }
    }

    void Start()
    {
        _total_score = 0;
    }

    public void UpdateScore()
    {
        int score = 0;
        ShipScript[] ships = FindObjectsOfType<ShipScript>();
        CircleCollider2D[] misses = FindObjectsOfType<CircleCollider2D>();

        foreach (ShipScript ship in ships)
        {
            score += (ship.Health * POINTS_PER_HEALTH);
        }

        foreach (CircleCollider2D miss in misses)
        {
            if (miss.name != "Cannonball(Clone)")
            {
                continue;
            }
            score += POINTS_PER_MISS;
        }
        _total_score += score;
    }
}
