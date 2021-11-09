using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacerScript : MonoBehaviour
{
    public GameObject ship1;
    public GameObject ship2;
    public GameObject ship3;
    public GameObject ship4;
    public GameObject ship5;
    GameObject currentShip = null;
    ShipScript[] ships;
    int ctr = 1;
    bool levelStart = true;
    bool shipsPlaced = false;
    bool newShip = true;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
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

        //Ship needs to be moved
        if (!newShip)
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
            {
                newShip = true;
                if (ctr == 6)
                {
                    ships = FindObjectsOfType<ShipScript>();
                    foreach (ShipScript ship in ships)
                    {
                        ship.ToggleCollider();
                        ship.AddContraints();
                    }
                    shipsPlaced = true;
                    ctr = 0;
                }
            }
        }
    }

    //Used to start gameplay
    public bool ShipsReady()
    {
        return shipsPlaced;
    }
}
