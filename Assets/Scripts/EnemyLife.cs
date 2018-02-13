using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLife : MonoBehaviour {

	public float hp = 100;
	public float umbralDaño = 2;

	[SerializeField]
	Sprite danyo;
	Sprite normal;

	[SerializeField]
	AudioClip golpe;

	SpriteRenderer spriteRendererComponent;
	public float timeDanyoAnimation = 2;

	// Use this for initialization
	void Start () {
		spriteRendererComponent = GetComponent<SpriteRenderer> ();
		if (spriteRendererComponent == null)
			Debug.LogError ("SpriteRenderer not founded!");

		normal = spriteRendererComponent.sprite;
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
			AudioSource.PlayClipAtPoint (golpe,transform.position);
			CancelInvoke ();
			spriteRendererComponent.sprite = danyo;
			Invoke ("HurtAnimation",timeDanyoAnimation);
		}
		if (hp <= 0) {
			GameManager.instance.ChangeEnemies ();
			Destroy (this.gameObject);
		}

	}

	void HurtAnimation(){
		spriteRendererComponent.sprite = normal;
	}
}
