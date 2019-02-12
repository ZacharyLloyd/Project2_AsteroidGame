using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class laserProjection : MonoBehaviour
{
    public float laser_velocity; //How fast the laser will go
    public Rigidbody2D rigidBody; //This in able to apply physics to laser
    //before first frame
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        rigidBody.velocity = transform.right * laser_velocity;
    }
}
