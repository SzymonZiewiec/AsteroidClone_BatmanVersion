using UnityEngine;
using System.Collections;

public class DogeScript : MonoBehaviour {

	public GameObject AlienShip;
	public float dogeCooldown = 0.5f;
	private float _dogeCooldown;
	private float _force;

	void Start () {
		_force = AlienShip.GetComponent<AlienScript> ().maxForce;
		_dogeCooldown = 0;
	}

	void Update () {
		_dogeCooldown -= Time.deltaTime;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(_dogeCooldown <= 0 && collider.tag != AlienShip.tag){
			AlienShip.GetComponent<Rigidbody2D> ().AddForce (-AlienShip.GetComponent<Rigidbody2D> ().velocity.normalized * _force);
			_dogeCooldown = dogeCooldown;
		}
	}

}
