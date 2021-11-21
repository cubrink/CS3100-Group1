using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacerScript : MonoBehaviour
{
    public CanvasGroup gameOver;
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;
    public int roundCount = 3;
    public float roundTime = 20.0f;
    float roundTimer;
    int roundCounter;
    GameObject currentShip = null;
    ShipScript[] ships;
    int ctr = 1;
    bool levelStart = true;
    bool levelEnd = false;
    bool shipsPlaced = false;
    bool newShip = true;
    bool isGameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        roundTimer = roundTime;
        roundCounter = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (shipsPlaced)
        {
            //Controls rounds and when ships are active
            roundTimer -= Time.deltaTime;
            if (roundTimer < 0 && isGameOver == false)
            {
                Debug.Log("Round " + roundCounter + " Complete");
                roundCounter += 1;
                if (roundCounter > roundCount)
                {
                    Debug.Log("Level Complete");
                    levelEnd = true;
                    //Display score
                }
                roundTimer = roundTime;
                shipsPlaced = false;
                newShip = true;
                ships = FindObjectsOfType<ShipScript>();
                foreach (ShipScript ship in ships)
                {
                    ship.ToggleCollider();
                }
            }
        }

        //Spawn ships if at start of level
        if (levelStart && newShip)
        {
            //Cycle through and spawn ships
            if (ctr == 1)
                currentShip = Instantiate(ship1, new Vector2(1.0f, 0.5f), Quaternion.identity);
            else if (ctr == 2)
                currentShip = Instantiate(ship2, new Vector2(0.5f, 0.5f), Quaternion.identity);
            else if (ctr == 3)
                currentShip = Instantiate(ship3, new Vector2(0.5f, 0.5f), Quaternion.identity);
            else if (ctr == 4)
                currentShip = Instantiate(ship4, new Vector2(1.0f, 0.5f), Quaternion.identity);
            else if (ctr == 5)
            {
                currentShip = Instantiate(ship5, new Vector2(0.5f, 0.5f), Quaternion.identity);
                levelStart = false;
            }
            ctr += 1;
            //New ship has been chosen
            newShip = false;
        }
        //If end of round
        else if (newShip)
        {
            //Cycle through ships
            if (ctr == 1)
                currentShip = GameObject.Find("Destroyer(Clone)");
            else if (ctr == 2)
                currentShip = GameObject.Find("Cruiser(Clone)");
            else if (ctr == 3)
                currentShip = GameObject.Find("Submarine(Clone)");
            else if (ctr == 4)
                currentShip = GameObject.Find("Battleship(Clone)");
            else if (ctr == 5)
            {
                currentShip = GameObject.Find("Carrier(Clone)");
                levelStart = false;
            }
            ctr += 1;
            //New ship has been chosen
            newShip = false;
        }

        //Debug.Log(ctr);
        //Ship needs to be moved
        if (!shipsPlaced && currentShip == null)
            newShip = true;
        if (!newShip && !shipsPlaced && !levelEnd)
        {
            //Move ship
            ShipScript curShip = currentShip.GetComponent<ShipScript>();
            if (Input.GetKeyDown(KeyCode.UpArrow))
                curShip.MoveUp();
            if (Input.GetKeyDown(KeyCode.RightArrow))
                curShip.MoveRight();
            if (Input.GetKeyDown(KeyCode.DownArrow))
                curShip.MoveDown();
            if (Input.GetKeyDown(KeyCode.LeftArrow))
                curShip.MoveLeft();
            if (Input.GetKeyDown(KeyCode.R))
                curShip.Rotate();

            //Place ship
            if (Input.GetKeyDown(KeyCode.Space) && curShip.VerifyPlacement())
                newShip = true;
        }
        //Switch to gameplay
        if (ctr >= 7)
        {
            ships = FindObjectsOfType<ShipScript>();
            foreach (ShipScript ship in ships)
                ship.ToggleCollider();
            shipsPlaced = true;
            newShip = false;
            ctr = 1;
        }

        //Press enter to load next level
        if (levelEnd && Input.GetKeyDown(KeyCode.Return))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }

        //Press enter to retry level
        if (isGameOver && Input.GetKeyDown(KeyCode.Return))
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex);
        }
    }

    //Used to start gameplay
    public bool ShipsReady()
    {
        return shipsPlaced;
    }

    public void EndGame()
    {
        ships = FindObjectsOfType<ShipScript>();
        if (ships.Length <= 1)
        {
            //Display game over
            gameOver.alpha = 1;
            isGameOver = true;
        }
    }
}
