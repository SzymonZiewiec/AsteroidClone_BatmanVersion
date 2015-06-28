using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {
	
	public float minTorque = -100f;
	public float maxTorque = 100f;
	public float minForce = 20f;
	public float maxForce = 40f;
	public GameObject Explosion;
	public AsteroidType aasteroidType = AsteroidType.Large;

	//Reference to next asteroid to spawn on death
	public GameObject childAsteroid;
	public int numberOfChildAsteroids = 2;

	void Start () {
		float magnitude = Random.Range (minForce, maxForce);
		float x = Random.Range (-1f,1f);
		float y = Random.Range (-1f,1f);

		//Applaying randomly generated vector ((x,y) for direction) with cetrain force (magnitude) to asteroid
		GetComponent<Rigidbody2D> ().AddForce (magnitude * new Vector2 (x,y));

		//Applaing randomly generated torque to asteroid
		float torque = Random.Range (minTorque, maxTorque);
		GetComponent<Rigidbody2D> ().AddTorque (torque);
	}

	void OnTriggerEnter2D(Collider2D collider){
		if ((collider.tag == "ShipBullet1") || (collider.tag == "ShipBullet2")) {

			//Scaling size of the explosion for Large/Medium/Small steroids
			float emissionRate = 100;
			switch(aasteroidType){
				case AsteroidType.Large: emissionRate = 500; break;
				case AsteroidType.Medium: emissionRate = 300; break;
				case AsteroidType.Smaall: emissionRate = 100; break;
			}
			Explosion.GetComponent<ParticleSystem> ().emissionRate = emissionRate;

			//Explosion
			Instantiate(Explosion, transform.position, transform.rotation);

			//New Child-Asteroids (None for Small)
			if(childAsteroid != null)
				for(int i=0;i<numberOfChildAsteroids; i++)
					Instantiate(childAsteroid, transform.position, transform.rotation);

			//Removing destroied objects
			Destroy (gameObject);
			if(!(collider.tag == "ShipBullet2"))
				Destroy (collider.gameObject);
		}
	}

}

public enum AsteroidType{
	Smaall,
	Medium,
	Large
}