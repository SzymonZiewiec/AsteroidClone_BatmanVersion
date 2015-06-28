using UnityEngine;
using System.Collections;

public class TeleporterScript : MonoBehaviour {

	//Teleporting object whenver in fly off screan to the oposite side of the screen.

	public float minX = -11f;
	public float minY = -4.5f;
	public float maxX = 11f;
	public float maxY = 4.5f;

	void FixedUpdate(){
		float x = GetComponent<Transform>().position.x;
		float y = GetComponent<Transform>().position.y;
		
		if (x > maxX)
			x = minX;
		else if (x < minX)
			x = maxX;
		if (y > maxY)
			y = minY;
		else if (y < minY)
			y = maxY;
		
		GetComponent<Transform> ().position = new Vector3 (x,y, GetComponent<Transform> ().position.z);
	}

}
