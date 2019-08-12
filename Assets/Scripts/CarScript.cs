﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarScript : MonoBehaviour
{
    //Adjustable speed for the car forward and turning movement
    public float speed;
    //Factor of when the phone just start changing direction input out of 1f
    [SerializeField]
    private float tiltFactor;
    //Orginal car rotation on the z axis
    private float orginalZRotation;
    //Store car rotation
    private float rotationDirection;


    void Start()
    {
		//Set orginal z rotation
        orginalZRotation = transform.rotation.z;
    }
    void FixedUpdate()
    {
        var v3 = transform.up;
        v3.y = 0.0f;
        //Move the car upward(As the model is already rotated)
        transform.Translate(v3 * Time.fixedDeltaTime * speed, Space.World);

        //Move car left or right based on phone tilt
        if (Input.acceleration.normalized.x > tiltFactor)
        {
            transform.Rotate(0f, 0f, 1f);
            rotationDirection = 1f;
        }
        else if (Input.acceleration.normalized.x <= -tiltFactor)
        {
            transform.Rotate(0f, 0f, -1f);
            rotationDirection = -1f;
        }
        else if(!Mathf.Approximately(orginalZRotation,transform.rotation.z))
        {
            transform.Rotate(0f, 0f, rotationDirection);
        }
    }

	//Car collision method
    void OnCollisionEnter(Collision collision)
    {
		//If car has hit an object, called end game method
        if(collision.gameObject.tag == "Object")
        {
			GameObject.Find("GameManger").GetComponent<GenScript>().EndGameScreenDisplay();
        }
    }
}