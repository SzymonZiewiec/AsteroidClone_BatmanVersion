using UnityEngine;
using System.Collections;

public class AsteroidExplosionScript : MonoBehaviour {

	void Start () {		
		Destroy (gameObject, 2f);
	}
}
