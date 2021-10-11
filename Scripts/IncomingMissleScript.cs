using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingMissleScript : MonoBehaviour
{
    public float missleHitTime = 5.0f;
    float missleTimer;

    // Start is called before the first frame update
    void Start()
    {
        missleTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Red circle get bigger to indicate when the missle will "land"
        missleTimer += Time.deltaTime;
        float diameter = (missleTimer / missleHitTime) * 1.15f;
        transform.localScale = new Vector3(diameter, diameter, 0f);
        if (missleTimer > missleHitTime)
        {
            // Spawn Hit/Miss script instance
            Destroy(transform.parent.gameObject);
        }
    }
}
