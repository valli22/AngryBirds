using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour {

	public float hp = 25;
	public float umbralDaño = 2;
	float totalHp;

	[SerializeField]
	Sprite danyo;

	SpriteRenderer spriteRendererComponent;

	// Use this for initialization
	void Start () {
		totalHp = hp;
		spriteRendererComponent = GetComponent<SpriteRenderer> ();
		if (spriteRendererComponent == null)
			Debug.LogError ("SpriteRenderer not founded!");
	}

	// Update is called once per frame
	void Update () {
		if (hp <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if(other.relativeVelocity.magnitude>=umbralDaño){
			hp -= other.relativeVelocity.magnitude;
			if(hp <=totalHp/2)
				spriteRendererComponent.sprite = danyo;
		}
	}

}
