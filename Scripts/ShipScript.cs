using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipScript : MonoBehaviour
{
    Rigidbody2D body;

    public float speed = 1.0f;
    public bool isVertical = true;
    public int maxHealth;

    float horizontal;
    float vertical;
    int health;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
        body = GetComponent<Rigidbody2D>();

        if (!isVertical)
        {
            // Rotate ship to be horizontal
            body.SetRotation(body.rotation + 90.0f);
            // Constrain rotation and movement in wrong direction
            body.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
        }
        else
        {
            body.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
        }
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
        Vector2 position = body.position;

        // Ship moves on parallel axis
        if (isVertical)
        {
            // Move ship if the corresponding key is held
            if (Input.GetKey(KeyCode.Alpha1) && this.name == "Destroyer")
                position.y = position.y + speed * vertical * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha2) && this.name == "Submarine")
                position.y = position.y + speed * vertical * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha3) && this.name == "Cruiser")
                position.y = position.y + speed * vertical * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha4) && this.name == "Battleship")
                position.y = position.y + speed * vertical * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha5) && this.name == "Carrier")
                position.y = position.y + speed * vertical * Time.deltaTime;
        }
        else
        {
            if (Input.GetKey(KeyCode.Alpha1) && this.name == "Destroyer")
                position.x = position.x + speed * horizontal * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha2) && this.name == "Submarine")
                position.x = position.x + speed * horizontal * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha3) && this.name == "Cruiser")
                position.x = position.x + speed * horizontal * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha4) && this.name == "Battleship")
                position.x = position.x + speed * horizontal * Time.deltaTime;
            if (Input.GetKey(KeyCode.Alpha5) && this.name == "Carrier")
                position.x = position.x + speed * horizontal * Time.deltaTime;
        }

        body.MovePosition(position);
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
    }
}
