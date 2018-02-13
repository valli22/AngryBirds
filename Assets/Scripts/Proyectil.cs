using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proyectil : MonoBehaviour {

	[SerializeField]
	AudioClip boing;

	bool once = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D other){
		AudioSource.PlayClipAtPoint (boing, transform.position);
		if (!once) {
			StartCoroutine (WaitToChangeTag ());
			once = true;
		}
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
