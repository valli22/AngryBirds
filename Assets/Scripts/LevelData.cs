using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelData : MonoBehaviour {

	[SerializeField]
	GameObject lvlbutton;
	[SerializeField]
	GameObject candado;

	[SerializeField]
	Text tittle;
	[SerializeField]
	Text score;

	void Awake(){
		bool isActive = PlayerPrefs.GetInt (name, 0) == 0;
		lvlbutton.SetActive (!isActive);
		candado.SetActive (isActive);
		score.text = "Score: " + PlayerPrefs.GetInt (name+"score",0);
		tittle.text = name;
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

}
