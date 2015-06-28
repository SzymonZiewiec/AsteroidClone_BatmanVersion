using UnityEngine;
using System.Collections;

public class BulletScript : MonoBehaviour {
	public float bulletForce;
	public float lifeTime;

	void Start () {
		GetComponent<Rigidbody2D> ().AddForce (transform.up * bulletForce);
		Destroy(gameObject,lifeTime);
	}

	void Update () {
		GetComponent<ParticleSystem>().Emit (5);
	}
}
