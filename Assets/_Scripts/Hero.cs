using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 public class Hero : MonoBehaviour {

    static public Hero S; // Singleton                                // a 

    private GameObject lastTriggerGo = null;

     [Header( "Set in Inspector" )] 
     // These fields control the movement of the ship
     public float speed = 30 ; 

     public float rollMult = - 45 ; 
     public float pitchMult = 30 ; 
     public float gameRestartDelay = 2f;
     public GameObject projectilePrefab;
     public float projectileSpeed = 40;

     public delegate void WeaponFireDelegate();
     public WeaponFireDelegate fireDelegate;

    [SerializeField]
    private float _shieldLevel = 1;


     void Awake() { 
	if (S == null ) { 
	    S = this ; // Set the Singleton                                 
	} else { 
	    Debug.LogError( "Hero.Awake() - Attempted to assign second Hero.S!" ); 
	} 
	fireDelegate += TempFire;
    } 

     void Update () {
	// Pull in information from the Input class 
	float xAxis = Input.GetAxis( "Horizontal" ); // b 
	float yAxis = Input.GetAxis( "Vertical" ); // b 

	// Change transform.position based on the axes 
	Vector3 pos = transform.position; 
	pos.x += xAxis * speed * Time.deltaTime; 
	pos.y += yAxis * speed * Time.deltaTime; 
	transform.position = pos; 

	// Rotate the ship to make it feel more dynamic                     
	transform.rotation = Quaternion.Euler(yAxis * pitchMult, xAxis * rollMult, 0); 

	if (Input.GetAxis("Jump") == 1 && fireDelegate != null) {            
	    fireDelegate();                                                 
        } 
    }

    void TempFire() {
	GameObject projGO = Instantiate<GameObject>(projectilePrefab);
	projGO.transform.position = transform.position;
	Rigidbody rigidB = projGO.GetComponent<Rigidbody>();
	// rigidB.velocity = Vector3.up * projectileSpeed;
	
	ProjectileHero proj = projGO.GetComponent<ProjectileHero>();                
	proj.type = WeaponType.blaster; 
	float tSpeed = Main.GetWeaponDefinition( proj.type ).velocity; 
	rigidB.velocity = Vector3.up * tSpeed;
    }


    void OnTriggerEnter(Collider other) {
	Transform rootT = other.gameObject.transform.root;
        GameObject go = rootT.gameObject;

	if(go == lastTriggerGo) {
	    return;
	}

	if(go.tag == "Enemy") {
	    shieldLevel--;
	    Destroy(go);
	} else {
	print("Triggered: " + go.name);
	}

    }

    
    public float shieldLevel  {
	get {
	    return( _shieldLevel);
	} set {
	    _shieldLevel = Mathf.Min(value, 4);
	    if(value < 0) { 
		Destroy(this.gameObject); 
		Main.S.DelayedRestart(gameRestartDelay);
	    }
	}
    }


}


