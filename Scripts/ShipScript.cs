using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    PlacerScript levelController;

    Collider2D coll;
    Rigidbody2D body;

    // Sprites for each HP.
    public Sprite HP1;
    public Sprite HP2;
    public Sprite HP3;
    public Sprite HP4;
    public Sprite HP5;

    public float speed = 1.0f;
    public int maxHealth;

    float horizontal;
    float vertical;
    int health;
    bool isVertical = false;
    bool validPlacement = true;

    public int Health
    {
        get { return health; }
    }

    public bool is_vertical
    {
        get { return isVertical;  }
    }

    // Start is called before the first frame update
    void Start()
    {
        levelController = FindObjectOfType<PlacerScript>();
        health = maxHealth;
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Get Arrow Key Input
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        //Wait for ships to be placed
        if (levelController.ShipsReady())
        {
            Vector2 position = body.position;

            // Ship moves on parallel axis
            if (isVertical)
            {
                // Move ship if the corresponding key is held
                if (Input.GetKey(KeyCode.Alpha1) && this.name == "Destroyer(Clone)")
                    position.y = position.y + speed * vertical * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha2) && this.name == "Cruiser(Clone)")
                    position.y = position.y + speed * vertical * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha3) && this.name == "Submarine(Clone)")
                    position.y = position.y + speed * vertical * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha4) && this.name == "Battleship(Clone)")
                    position.y = position.y + speed * vertical * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha5) && this.name == "Carrier(Clone)")
                    position.y = position.y + speed * vertical * Time.deltaTime;
            }
            else
            {
                if (Input.GetKey(KeyCode.Alpha1) && this.name == "Destroyer(Clone)")
                    position.x = position.x + speed * horizontal * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha2) && this.name == "Cruiser(Clone)")
                    position.x = position.x + speed * horizontal * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha3) && this.name == "Submarine(Clone)")
                    position.x = position.x + speed * horizontal * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha4) && this.name == "Battleship(Clone)")
                    position.x = position.x + speed * horizontal * Time.deltaTime;
                if (Input.GetKey(KeyCode.Alpha5) && this.name == "Carrier(Clone)")
                    position.x = position.x + speed * horizontal * Time.deltaTime;
            }

            body.MovePosition(position);
        }
    }

    public void ChangeHealth(int amount)
    {
        health = Mathf.Clamp(health + amount, 0, maxHealth);
        Debug.Log(this.name + " health: " + health + "/" + maxHealth);

        if (health <= 0)
        {
            Debug.Log(this.name + " destroyed");
            Destroy(gameObject);
        }
        // Update sprite to indicate current health via switch statement. If there's no HP it happily ignores it.
        switch(health)
        {
            case 1:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HP1;
                break;
            case 2:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HP2;
                break;
            case 3:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HP3;
                break;
            case 4:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HP4;
                break;
            case 5:
                this.gameObject.GetComponent<SpriteRenderer>().sprite = HP5;
                break;
        }
    }
    

    //Movement functions for placing ships
    public void MoveUp()
    {
        Vector2 position = body.position;
        if (position.y < 5f)
            position.y += 1;
        body.MovePosition(position);
    }

    public void MoveRight()
    {
        Vector2 position = body.position;
        if (position.x < 5f)
            position.x += 1;
        body.MovePosition(position);
    }

    public void MoveDown()
    {
        Vector2 position = body.position;
        if (position.y > -5f)
            position.y -= 1;
        body.MovePosition(position);
    }

    public void MoveLeft()
    {
        Vector2 position = body.position;
        if (position.x > -5f)
            position.x -= 1;
        body.MovePosition(position);
    }

    public void Rotate()
    {
        Vector2 position = body.position;
        //Special rotate for "even" sized ships
        if (this.name == "Destroyer(Clone)" || this.name == "Battleship(Clone)")
        {
            if (position.x < 5f && position.y < 5f)
            {
                position.x += 0.5f;
                position.y += 0.5f;
            }
            else
            {
                position.x -= 0.5f;
                position.y -= 0.5f;
            }
        }
        body.MovePosition(position);
        body.SetRotation(body.rotation + 90.0f);
        isVertical = !isVertical;
    }


    //Ship placement functions
    public void ToggleCollider()
    {
        coll.isTrigger = !coll.isTrigger;
    }

    public bool IsATrigger()
    {
        return coll.isTrigger;
    }

    public void AddConstraints()
    {
        if (isVertical)
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        else
            body.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
    }

    public void RemoveConstraints()
    {
        body.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    void OnTriggerStay2D(Collider2D other)
    {
        validPlacement = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        validPlacement = true;
    }

    public bool VerifyPlacement()
    {
        if (!validPlacement)
            Debug.Log("Invalid Placement");
        return validPlacement;
    }
}
