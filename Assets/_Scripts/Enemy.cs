using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public float score = 100;
    protected BoundsCheck bndCheck;

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


    void OnCollisionEnter( Collision coll ) { 
	GameObject otherGO = coll.gameObject; 
	switch (otherGO.tag) { 
	    case "ProjectileHero": 
		ProjectileHero p = otherGO.GetComponent<ProjectileHero>(); 
		// If this Enemy is off screen, don't damage it. 
		if ( !bndCheck.isOnScreen ) { 
		    Destroy( otherGO ); 
		    break; 
		} 
		 // Hurt this Enemy 
		    // Get the damage amount from the Main WEAP_DICT. 
		    health -= Main .GetWeaponDefinition(p.type).damageOnHit; 
		if (health <= 0 ) { 
		    // Destroy this Enemy 
		    Destroy(this.gameObject); 
		} 
                Destroy(otherGO); 
		break; 
		
	     default: 
                print( "Enemy hit by non-ProjectileHero: " + otherGO.name ); // f 
		break; 
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

