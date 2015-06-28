using UnityEngine;
using System.Collections;

public class AlienScript : MonoBehaviour {
	
	public float minTorque = -100f;
	public float maxTorque = 100f;
	public float minForce = 20f;
	public float maxForce = 40f;
	public float directionChangeInterval = 1f;
	public float shootInterval = 5f;
	public GameObject bulletPrefab;
	public GameObject Explosion;

	private float _directionChangeInterval;
	private float _shootInterval;
	private GameObject _ship;

	void Start () {
		Push ();
		_directionChangeInterval = directionChangeInterval;
		_shootInterval = shootInterval;
	}

	void Update () {
		_directionChangeInterval -= Time.deltaTime;
		_shootInterval -= Time.deltaTime;
		
		if (_directionChangeInterval <= 0) {
			Push ();
			_directionChangeInterval = directionChangeInterval;
		}

		if (_shootInterval <= 0) {
			Shoot ();
			_shootInterval = shootInterval;
		}
	}

	//Function for pushing alien ship
	void Push(){
		float force = Random.Range (minForce, maxForce);
		float x = Random.Range (-1f,1f);
		float y = Random.Range (-1f,1f);

		GetComponent<Rigidbody2D> ().AddForce (force * new Vector2 (x,y));
	}

	void Shoot(){		
		//Reseting interval
		_shootInterval = shootInterval;

		_ship = UnityEngine.GameObject.FindGameObjectWithTag("Ship");

		if (_ship != null) {
			float angle = (Mathf.Atan2 (
			_ship.transform.position.y - transform.position.y,
			_ship.transform.position.x - transform.position.x) - Mathf.PI / 2) * Mathf.Rad2Deg;

			Instantiate (bulletPrefab, transform.position, Quaternion.Euler (new Vector3 (0f, 0f, angle)));
		}
	}

	void OnTriggerEnter2D(Collider2D collider){
		if (!false) {
			if ((collider.tag == "ShipBullet1") || (collider.tag == "ShipBullet2")) {
				//Explosion
				Instantiate (Explosion, transform.position, transform.rotation);
			
				//Removing destroied objects
				Destroy (gameObject);
				if(!(collider.tag == "ShipBullet2"))
					Destroy (collider.gameObject);
			}
			if (collider.tag == "Asteroid") {
				//Explosion
				Instantiate (Explosion, transform.position, transform.rotation);
			
				//Removing destroied objects
				Destroy (gameObject);
			}
		}
	}

}
