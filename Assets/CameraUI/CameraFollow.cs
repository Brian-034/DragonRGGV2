using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {
    GameObject player;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// LateUpdate is called once per frame after Updates have compleated
	void LateUpdate () {
       //float posX = player.transform.position.x;
       // float posZ = player.transform.position.z;
       // transform.position = new Vector3(posX, 0, posZ);
        transform.position = player.transform.position;
    }
}
