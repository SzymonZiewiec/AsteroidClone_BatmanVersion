using UnityEngine;
using System.Collections;

public class MineScript : MonoBehaviour {
	
	public float minTorque = -100f;
	public float maxTorque = 100f;
	public float minForce = 10f;
	public float maxForce = 20f;
	public GameObject Explosion;
			
	void OnTriggerEnter2D(Collider2D collider){
		if ((collider.tag == "ShipBullet1") || (collider.tag == "ShipBullet2")) {
			//Explosion
			Instantiate (Explosion, transform.position, transform.rotation);
			
			//Removing destroied objects
			Destroy (gameObject);
			if(!(collider.tag == "ShipBullet2"))
				Destroy (collider.gameObject);
		}
	}
}
