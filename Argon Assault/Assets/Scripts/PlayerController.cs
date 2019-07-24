using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [Header("General")]
    [Tooltip("In ms^-1")] [SerializeField] float xSpeed = 4f;
    [Tooltip("In ms^-1")] [SerializeField] float ySpeed = 4f;
    [SerializeField] float xWall = 10f;
    [SerializeField] float yWall = 5f;

    [Header("Screen-position Based")]
    [SerializeField] float positionPitchFactor = -5f;
    [SerializeField] float positionYawFactor = 5.2f;

    [Header("Particles")]
    [SerializeField] GameObject[] guns;

    float xThrow, yThrow, xPos, yPos;

    bool isControlEnabled = true;

    void Update()
    {
        if (isControlEnabled)
        {
            ProcessTranslation();
            ProcessRotation();
            ProcessFiring();
        }
    }

    private void ProcessFiring()
    {
        if (CrossPlatformInputManager.GetButton("Fire"))
        {
            foreach (GameObject gun in guns)
            {
                gun.SetActive(true);
            }
        }

        else 
        {
            foreach (GameObject gun in guns)
            {
                gun.SetActive(false);
            }
        }
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
    }

    private void OnPlayerDeath() // called by string reference
    {
        isControlEnabled = false;
    }
}
