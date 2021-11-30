using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScoringScript : MonoBehaviour
{
    public const int POINTS_PER_HEALTH = 20;
    public const int POINTS_PER_MISS = 5;

    private int _total_score;

    public int total_score
    {
        get { return _total_score; }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        _total_score = 0;
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "End")
        {
            GameObject scoreObj = GameObject.Find("Final Score");
            Text scoreText = scoreObj.GetComponent<Text>();
            scoreText.text = "" + _total_score;
        }
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

    public void SetScore(int newScore)
    {
        _total_score = newScore;
    }
}
