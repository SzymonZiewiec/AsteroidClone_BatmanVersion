using UnityEngine;
using System.Collections;

public class FollowScript : MonoBehaviour {
	
	public GameObject Mine;
	public float followCooldown = 2f;

	private float _followCooldown;
	private float _force;	
	
	void Start () {
		_force = Mine.GetComponent<MineScript> ().minForce;
		_followCooldown = 0;
	}
	
	void Update () {
		_followCooldown -= Time.deltaTime;
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if (_followCooldown <= 0 && collider.tag == "Ship") {
			Vector2 direction = collider.transform.position - Mine.transform.position;
			direction.Normalize ();
			Mine.GetComponent<Rigidbody2D> ().AddForce (direction * _force);

			_followCooldown = followCooldown;
		}
	}
}
