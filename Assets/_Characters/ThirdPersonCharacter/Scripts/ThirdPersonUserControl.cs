using System;
using UnityEngine;

namespace UnityStandardAssets.Characters.ThirdPerson
{
    [RequireComponent(typeof (ThirdPersonCharacter))]
    public class ThirdPersonUserControl : MonoBehaviour
    {
        private ThirdPersonCharacter thirdPersonCharacter; // A reference to the ThirdPersonCharacter on the object
        private Transform cameraTransform;                  // A reference to the main camera in the scenes transform
        private Vector3 cameraForward;             // The current forward direction of the camera
        private Vector3 moveVector;
        private bool jump;                      // the world-relative desired move direction, calculated from the camForward and user input.

        
        private void Start()
        {
            // get the transform of the main camera
            if (Camera.main != null)
            {
                cameraTransform = Camera.main.transform;
            }
            else
            {
                Debug.LogWarning(
                    "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
                // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
            }

            // get the third person character ( this should never be null due to require component )
            thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        }


  

        // Fixed update is called in sync with physics
        private void FixedUpdate()
        {
            // read inputs
            bool crouch = Input.GetKey(KeyCode.C);

            // calculate move direction to pass to character
            if (cameraTransform != null)
            {
                // calculate camera relative direction to move:
                cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1, 0, 1)).normalized;
            }
            else
            {
                // we use world-relative directions in the case of no main camera
       //         moveVector = v*Vector3.forward + h*Vector3.right;
            }
#if !MOBILE_INPUT
			// walk speed multiplier
	        if (Input.GetKey(KeyCode.LeftShift)) moveVector *= 0.5f;
#endif

            // pass all parameters to the character control script
            thirdPersonCharacter.Move(moveVector, crouch, jump);
            jump = false;
        }
    }
}
