using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PajaroVeloz : MonoBehaviour {

	public float power;
	bool used = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0) && !GameManager.instance.canShoot && !used) {
			GetComponent<Rigidbody2D> ().velocity *= power;
			used = true;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		StartCoroutine (WaitToChangeTag());
	}

	public void changeTag(string tag){
		transform.tag = tag;
	}

	IEnumerator WaitToChangeTag(){
		yield return new WaitForSeconds (2.5f);
		changeTag ("Untagged");
		GameManager.instance.lifes--;
		GameManager.instance.ChangeLife ();
		GameManager.instance.canShoot = true;
		Destroy (this.gameObject);
	}
}
