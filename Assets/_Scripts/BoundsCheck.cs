using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoundsCheck : MonoBehaviour {                                    // a 

[Header( "Set in Inspector" )] 
public float radius = 1f ; 

 [Header( "Set Dynamically" )] 
 public float camWidth; 
 public float camHeight; 

 public bool offRight, offLeft, offUp, offDown;

 public bool keepOnScreen = true;
 public bool isOnScreen = true;

  void Awake() { 
    camHeight = Camera .main.orthographicSize;                             // b 
    camWidth = camHeight * Camera .main.aspect;          // c 
  } 


 void LateUpdate () {                             // d 
    Vector3 pos = transform.position; 
    isOnScreen = true;
    offRight = offLeft = offUp = offDown = false ;


     if (pos.x > camWidth - radius) { 
	pos.x = camWidth - radius; 
	isOnScreen = false;
	offRight = true;
    } 

     if (pos.x < -camWidth + radius) { 
	pos.x = -camWidth + radius; 
	isOnScreen = false;
	offLeft = true;
    } 

     if (pos.y > camHeight - radius) { 
	pos.y = camHeight - radius; 
	isOnScreen = false;
	offUp = true;
    }

    if (pos.y < -camHeight + radius) { 
	pos.y = -camHeight + radius;
	isOnScreen = false;
	offDown = true;
    } 

    isOnScreen = !(offRight || offLeft || offUp || offDown);
    if(keepOnScreen && !isOnScreen) {
     transform.position = pos; 
     isOnScreen = true;
     offRight = offLeft = offUp = offDown = false;
    }
  } 

 // Draw the bounds in the Scene pane using OnDrawGizmos() 
    void OnDrawGizmos () { // e 
    if (! Application .isPlaying) return ; 
    Vector3 boundSize = new Vector3 (camWidth* 2, camHeight* 2, 0.1f); 
    Gizmos.DrawWireCube( Vector3 .zero, boundSize); 
   } 
}


