using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour {

	GameObject proyectil;

	Vector3 startingPos;

	public float minX;
	public float maxX;

	bool goToStart = false;
	// Use this for initialization
	void Start () {
		startingPos = new Vector3(0,0,-10);
		Invoke ("GoToTheStartPosition",2);
	}
	
	// Update is called once per frame
	void Update () {
		if (goToStart) {
			proyectil = GameObject.FindGameObjectWithTag ("Lanzado");
			if (proyectil != null) {
				transform.position = new Vector3 (Mathf.Clamp (proyectil.transform.position.x, minX, maxX), transform.position.y, transform.position.z);
			}else {
				if(Vector3.Distance(transform.position,startingPos)>0.1f)
					transform.position = Vector3.Lerp (transform.position, startingPos, 1f * Time.deltaTime);
			}
		}
	}

	void GoToTheStartPosition(){
		goToStart = true;
	}
}
