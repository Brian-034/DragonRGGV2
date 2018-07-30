using System;
using UnityEngine;
using UnityEngine.AI;
using UnityStandardAssets.Characters.ThirdPerson;

[RequireComponent(typeof (ThirdPersonCharacter))]
[RequireComponent(typeof(NavMeshAgent))]
[RequireComponent(typeof(AICharacterControl))]

public class PlayerMovement : MonoBehaviour
{
  
    ThirdPersonCharacter thirdPersonCharacter;   // A reference to the ThirdPersonCharacter on the object
    CameraRaycaster cameraRaycaster;
    Vector3 currentDestination, clickPoint;
    AICharacterControl aiCharacterControl = null;
    GameObject walkTarget = null;

    bool isInDirectMode = false;
    bool m_Jump = false;
    const int walkableLayer = 9;
    const int enemyLayer = 10;

    void Start()
    {
        cameraRaycaster = Camera.main.GetComponent<CameraRaycaster>();
        cameraRaycaster.notifyMouseClickObservers += ProcessMouseClick;

        thirdPersonCharacter = GetComponent<ThirdPersonCharacter>();
        aiCharacterControl = GetComponent<AICharacterControl>();

        currentDestination = transform.position;
        walkTarget = new GameObject("walkTarget");
    }

    void ProcessMouseClick(RaycastHit raycastHit,int layerHit)
    {
        switch (layerHit)
        {
            case enemyLayer:
                //navigate to enemy
                GameObject enemy = raycastHit.collider.gameObject;
                aiCharacterControl.SetTarget(enemy.transform);
                break;
            case walkableLayer:
                // navigate to point on ground
                walkTarget.transform.position = raycastHit.point;
                aiCharacterControl.SetTarget(walkTarget.transform);
                break;
            default:
                Debug.LogWarning("Problem in player movement");
                return;
        }
     }


private void Update()
    {
        if (!m_Jump)
        {
            m_Jump = Input.GetKey(KeyCode.J);
           // m_Jump = Input.GetButtonDown("Jump");
        }
        
    }

    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
    }

    private void ProcessDirectMovement()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);
        

        // calculate camera relative direction to move:
        Vector3 m_CamForward = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 m_Move = v * m_CamForward + h * Camera.main.transform.right;
        thirdPersonCharacter.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }

    private void ProcessMouseMovement()
    {
        if (Input.GetMouseButton(0))
        {
            // TODO sort
           // clickPoint = cameraRaycaster.hit.point;
 
        }
        WalkToDesination();
    }

    private void WalkToDesination()
    {
        // Move without holing mouse down
        var playerToClickPoint = currentDestination - transform.position;
        if (playerToClickPoint.magnitude > 0.1f)
        {
            thirdPersonCharacter.Move(playerToClickPoint, false, false);
        }
        else
        {
            thirdPersonCharacter.Move(Vector3.zero, false, false);

        }
    }

    Vector3 ShortDestination(Vector3 destination, float shortening)
    {
        Vector3 reductionVector = (destination - transform.position).normalized * shortening;
        return destination - reductionVector;
    }

    void OnDrawGizmos()
    {
        //Gizmos.color = Color.black;
        //print("gismos");Gizmos.DrawLine(transform.position, clickPoint);
        //Gizmos.DrawSphere(currentDestination, 0.15f);
        //Gizmos.DrawSphere(clickPoint, 0.1f);
    }
}

