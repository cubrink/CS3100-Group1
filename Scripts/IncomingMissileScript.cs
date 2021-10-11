using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncomingMissileScript : MonoBehaviour
{
    public float missileHitTime = 5.0f;
    float missileTimer;

    // Start is called before the first frame update
    void Start()
    {
        missileTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Red circle get bigger to indicate when the missile will "land"
        missileTimer += Time.deltaTime;
        float diameter = (missileTimer / missileHitTime) * 1.15f;
        transform.localScale = new Vector3(diameter, diameter, 0f);
        if (missileTimer > missileHitTime)
        {
            // Spawn Hit/Miss script instance
            Destroy(transform.parent.gameObject);
        }
    }
}
