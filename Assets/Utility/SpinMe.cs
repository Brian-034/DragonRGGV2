using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinMe : MonoBehaviour {

	[SerializeField] float xRotationsPerMinute = 1f;
	[SerializeField] float yRotationsPerMinute = 1f;
	[SerializeField] float zRotationsPerMinute = 1f;
	

	void Update () {
      
        //xDegreesPerFrame = Time.DeltaTime,60,360, xRotationsPerMinute;
        // degrees frame^-1= seconds frame^-1, seconds minute^-1, degrees Rotation ^-1, rotations minute^-1

        // degrees frame^-1=seconds frame^-1, seconds minute^-1, (degrees Rotation ^-1 * rotations minute^-1)
        // degrees frame^-1= seconds frame^-1, seconds minute^-1, (degrees  minute^-1)

        // degrees frame^-1= seconds frame^-1,  (degrees  minute^-1) /(seconds minute^-1)
        // degrees frame^-1= seconds frame^-1,  (degrees  seconds ^-1 )

        // degrees frame^-1= seconds frame^-1,  (degrees  seconds ^-1 )

        float xDegreesPerFrame = Time.deltaTime * (xRotationsPerMinute * 360) / 60;
         // degrees frame^-1,  frame^-1 /  minute^-1, degrees Rootation ^-1, rotations minute^-1
        // degrees frame^-1,  frame^-1 /  minute^-1 * degrees Rootation ^-1, rotations minute^-1
        // degrees frame^-1,  frame^-1 /  minute^-1 * degrees Rootation ^-1* rotations minute^-1

        transform.RotateAround (transform.position, transform.right, xDegreesPerFrame);

		float yDegreesPerFrame = Time.deltaTime * (yRotationsPerMinute * 360) / 60;
        transform.RotateAround (transform.position, transform.up, yDegreesPerFrame);

        float zDegreesPerFrame = Time.deltaTime * (zRotationsPerMinute * 360) / 60;
       
        transform.RotateAround (transform.position, transform.forward, zDegreesPerFrame);
	}
}
