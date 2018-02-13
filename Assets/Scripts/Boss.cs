using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

	float totalLife;
	public float hp = 300;
	public float umbralDaño = 2;

	[SerializeField]
	Sprite danyo1;
	[SerializeField]
	Sprite danyo2;
	[SerializeField]
	Sprite danyo3;

	[SerializeField]
	AudioClip golpe;

	SpriteRenderer spriteRendererComponent;

	// Use this for initialization
	void Start () {
		spriteRendererComponent = GetComponent<SpriteRenderer> ();
		if (spriteRendererComponent == null)
			Debug.LogError ("SpriteRenderer not founded!");
		totalLife = hp;
	}

	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.relativeVelocity.magnitude>=umbralDaño){
			AudioSource.PlayClipAtPoint (golpe,transform.position);
			hp -= other.relativeVelocity.magnitude;
			if(hp<=totalLife/4)
				spriteRendererComponent.sprite = danyo3;
			else if(hp<=(totalLife/4)*2)
				spriteRendererComponent.sprite = danyo2;
			else if(hp<=(totalLife/4)*3)
				spriteRendererComponent.sprite = danyo1;
		}
		if (hp <= 0) {
			GameManager.instance.ChangeEnemies ();
			Destroy (this.gameObject);
		}

	}
}
