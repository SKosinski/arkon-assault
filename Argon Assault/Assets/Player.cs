using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour
{

    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f;

    [SerializeField] float xWall = 10f;
    [SerializeField] float yWall = 5f;

    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5.2f;

    float xThrow, yThrow, xPos, yPos;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessTranslation();
        ProcessRotation();
    }

    private void ProcessRotation()
    {
        
        float pitch = transform.localPosition.y * positionPitchFactor + -yThrow * 12;
        float yaw = transform.localPosition.x * positionYawFactor;
        float roll = -xThrow * 20;
       
        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);

        /*
        float rollRot = 0;
        if(yPos>0)
        {
            rollRot = -xPos*yPos;
        }
        else
        {
            rollRot = +xPos*yPos*0.01f;
        }
        transform.localRotation = Quaternion.Euler(pitch - yPos * 0.1f, yaw -xPos*0.5f, roll + rollRot);
        */
    }

    private void ProcessTranslation()
    {
        xThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        yThrow = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = xThrow * Time.deltaTime * xSpeed;
        float yOffset = yThrow * Time.deltaTime * ySpeed;

        float xrawPos = transform.localPosition.x + xOffset;
        float yrawPos = transform.localPosition.y + yOffset;

        xPos = Mathf.Clamp(xrawPos, -xWall, xWall);
        yPos = Mathf.Clamp(yrawPos, -yWall, yWall);

        transform.localPosition = new Vector3(xPos, yPos, transform.localPosition.z);
        //Vector3 newPos = new Vector3(horizontalThrow * Time.deltaTime, verticalThrow * Time.deltaTime, 0);
        //print(newPos);
        //transform.position += newPos;
    }
}
