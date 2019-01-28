using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    //Initializing speed and the speed of rotation
    [HideInInspector] public float speed; //Current speed 
    [HideInInspector] public float rotationSpeed; //Current rotation speed
    public float maxSpeed; //The max speed
    public float maxRotationSpeed; //Max speed for rotation
    public float rateOfSpeed; //The amount of speed gained over time
    public float rateOfRotation; //The amount of rotation speed gained over time

    public bool enableReversedControl = true; //Reservsing the controls

    private float clockwise; //Reads the values 1 and -1, 1 for clockwise and -1 for counter clockwise
    private float rotationValue; //Rotation controls, returns the value of the controller/keyboard (-1, 0, 1)

    private bool throttleDown; //Detecting throttle
    private bool reverseThrotleDown; //Detecting reverse throttle

    private Rigidbody2D rigidBody; //Giving a name that will reference the rigid body on the sprite

    //Awake is called before the first frame
    void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>(); //Grabbing the component of the local rigid body
    }

    // Update is called once per frame
    void Update()
    {
        rotationValue = Input.GetAxisRaw("Horizontal"); //Left or right arrow keys or A and D keys

        switch (enableReversedControl)
        {
            case false:
                Rotate(-rotationValue); //rotationValue is used as a parameter for the rotate function
                break;
            case true:
                Rotate(rotationValue);
                break;
        }
    }

    private void FixedUpdate()
    {
        switch (enableReversedControl)
        {
            case false:
                if (Input.GetKey(KeyCode.W)) Forward();
                else if (Input.GetKey(KeyCode.S)) Backwards();
                break;
            case true:
                if (Input.GetKey(KeyCode.W)) Backwards();
                else if (Input.GetKey(KeyCode.S)) Forward();
                break;
        }
    }

    float Rotate(float direction)
    {
        if (Mathf.Abs(rotationSpeed) < Mathf.Abs(maxRotationSpeed))
        {
            rotationSpeed += rateOfRotation; //The rotation speed is not assigned to a number
                                             //It will increment the rateOfRotation times direction (which has values of -1 and 1
        }
        transform.Rotate(direction * Vector3.forward, rotationSpeed); //Then update the rotation of the GameObject every frame, rotationSpeed being how fast the rotation is

        return direction; //Returning the value direction for the update function
        //Controls the rotation of the player
    }

    void Forward()
    {
        switch (enableReversedControl)
        {
            case false:
                throttleDown = Input.GetKey(KeyCode.W);
                break;
            case true:
                throttleDown = Input.GetKey(KeyCode.S);
                break;
        }
        //Going forwards
        if (throttleDown == true)
        {
            if (Mathf.Abs(speed) * -1 > Mathf.Abs(maxSpeed) * -1)
                speed += rateOfSpeed;

            rigidBody.AddForce(speed * (transform.localRotation * new Vector2(0f, rotationSpeed)));
        }
        else
        {
            if (Mathf.Abs(speed) * -1 < 0)
                speed -= rateOfSpeed * Mathf.Sign(speed);
        }
    }
    //Moving backwards
    void Backwards()
    {
        switch (enableReversedControl)
        {
            case false:
                reverseThrotleDown = Input.GetKey(KeyCode.S);
                break;
            case true:
                reverseThrotleDown = Input.GetKey(KeyCode.W);
                break;
        }
        if (reverseThrotleDown == true)
        {
            if (Mathf.Abs(speed) < Mathf.Abs(maxSpeed))
                speed += rateOfSpeed;

            rigidBody.AddForce(speed * (transform.localRotation * new Vector2(0f, -rotationSpeed)));
        }
        else
        {
            if (Mathf.Abs(speed) > 0)
                speed -= rateOfSpeed * Mathf.Sign(-speed);
        }
    }
}
