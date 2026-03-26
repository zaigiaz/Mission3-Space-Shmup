using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public float score = 100;
    private BoundsCheck bndCheck;

    public Vector3 pos {
	get {
	    return(this.transform.position);
	}
	set {
	    this.transform.position = value;
	}
    }

    void Awake() {
	bndCheck = GetComponent<BoundsCheck>();
    }


    void OnCollisionEnter(Collision coll) {
	GameObject otherGO = coll.gameObject;
	if(otherGO.tag == "ProjectileHero") {
	    Destroy(otherGO);
	    Destroy(gameObject);
	} else {
	    print("Enemy hit by non-ProjectileHero: " + otherGO.name);
	}
	
    }


    // Update is called once per frame
    void Update()
    {
	Move();	

	if ( bndCheck != null && bndCheck.offDown ) {                   
		Destroy(gameObject); 
      }
}
    
    public virtual void Move() {
	Vector3 tempPos = pos;
	tempPos.y -= speed * Time.deltaTime;
	pos = tempPos;
    }

}

