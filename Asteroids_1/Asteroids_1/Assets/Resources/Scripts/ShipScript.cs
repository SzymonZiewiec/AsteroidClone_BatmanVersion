using UnityEngine;
using System.Collections;

public class ShipScript : MonoBehaviour {

	public float rotationSpeed;
	public float thrustForce;
	public ParticleSystem thrustParticleEffect;
	public AudioSource thrustAudioSource;
	public AudioSource weaponSwapAudioSource;
	public GameObject bulletPrefab_1;
	public GameObject bulletPrefab_2;
	public GameObject Explosion;
	
	private float _weapon1Cooldown_Current = 0f;
	private float _weapon2Cooldown_Current = 0f;
	private float _weapon1Cooldown_Max = 0.1f;
	private float _weapon2Cooldown_Max = 2f;
	private int _currentWeaponID = 1;
	private int _maxWeaponID = 2;
	private int _minWeaponID = 1;
	private GameObject _bulletPrefab_current;

	void Start () {
		_bulletPrefab_current = bulletPrefab_1;
	}

	void Update () {
		_weapon1Cooldown_Current -= Time.deltaTime;
		_weapon2Cooldown_Current -= Time.deltaTime;

		if (Input.GetKeyDown (KeyCode.Space)) {
			//Shoting with current weapon
			bool enableShoot = false;
			switch(_currentWeaponID){
			case(1):
				if(_weapon1Cooldown_Current<=0){
					enableShoot = true;
					_weapon1Cooldown_Current = _weapon1Cooldown_Max;
				}
				break;
			case(2):
				if(_weapon2Cooldown_Current<=0){
					enableShoot = true;
					_weapon2Cooldown_Current = _weapon2Cooldown_Max;
				}
				break;
			}

			if(enableShoot)
				Instantiate(_bulletPrefab_current, transform.position, transform.rotation);
		}
		if (Input.GetKeyDown (KeyCode.Q)) {
			//Swaping current weapon
			weaponSwapAudioSource.Play();

			_currentWeaponID +=1;
			if(_currentWeaponID>_maxWeaponID)
				_currentWeaponID = _minWeaponID;
			
			switch(_currentWeaponID){
				case(1):
					_bulletPrefab_current = bulletPrefab_1;
					break;
				case(2):
					_bulletPrefab_current = bulletPrefab_2;
					break;
			}
		}
	}

	void FixedUpdate(){
		//Rotating Ship
		if (Input.GetKey (KeyCode.A)) {
			//Rotate ship to the left
			GetComponent<Rigidbody2D>().angularVelocity = rotationSpeed;
		}
		else if (Input.GetKey (KeyCode.D)){
			//Rotate ship to the right
			GetComponent<Rigidbody2D>().angularVelocity = (-1)*rotationSpeed;
		}
		else{
			//Stoping rotation
			GetComponent<Rigidbody2D>().angularVelocity = 0f;
		}

		//Ship's engines
		if (Input.GetKey (KeyCode.W)) {
			//Thrusting forward
			GetComponent<Rigidbody2D> ().AddForce (transform.up * thrustForce);

			//Show thrust particle effect and splay sound
			thrustParticleEffect.Emit(10);//Play ();
			if(!thrustAudioSource.isPlaying)
				thrustAudioSource.Play();
		} else {
			//ThrustParticleEffect.Stop();
			thrustAudioSource.Stop();
		}
	}
	
	void OnTriggerEnter2D(Collider2D collider){
		if(collider.tag == "Asteroid"){
			//Explosion
			Instantiate(Explosion, transform.position, new Quaternion());
			
			//Removing destroied objects
			Destroy (gameObject);
		}
		else if (collider.tag == "Alien" || collider.tag == "AlienBullet") {
			//Explosion
			Instantiate(Explosion, transform.position, new Quaternion());
			
			//Removing destroied objects
			Destroy (gameObject);
			Destroy (collider.gameObject);
		}
	}
}
