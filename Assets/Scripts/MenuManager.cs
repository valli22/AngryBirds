using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	[SerializeField]
	GameObject playScene;
	[SerializeField]
	GameObject levelsScene;
	[SerializeField]
	GameObject exitSure;
	[SerializeField]
	AudioSource Sounds;

	int scene = 0;

	void Awake(){
		PlayerPrefs.SetInt ("Level1",1);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if(Input.GetKeyDown(KeyCode.Escape)){
			switch (scene) {
			default:
				Debug.LogError ("Scene unkown");
				break;
			case 0:
				_EXITSURE();
				break;
			case 1:
				_GOTOSCENE0();
				break;
			case 2:	
				_EXITSURE();
				break;
			}
		}

	}

	public void _PLAY(){
		playScene.SetActive (false);
		levelsScene.SetActive (true);
		ButtonPressSound ();
		scene = 1;
	}

	public void _GOTOSCENE0(){
		playScene.SetActive (true);
		levelsScene.SetActive (false);
		ButtonPressSound ();
		scene = 0;
	}

	public void _EXITSURE(){
		exitSure.SetActive (true);
		ButtonPressSound ();
		scene = 2;
	}

	public void _QUITGAME(){
		ButtonPressSound ();
		Application.Quit ();
	}
	public void _QUITEXITSURE(){
		exitSure.SetActive (false);
		ButtonPressSound ();
		scene = 0;
	}

	public void _LOADSCENE(string name){
		ButtonPressSound ();
		SceneManager.LoadScene (name);
	}

	public void _RESETEVERYTHING(){
		PlayerPrefs.DeleteAll ();
		PlayerPrefs.SetInt ("Level1",1);
		ButtonPressSound ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void _CHEATSON(){
		PlayerPrefs.SetInt ("Level1",1);
		PlayerPrefs.SetInt ("Level2",1);
		PlayerPrefs.SetInt ("Level3",1);
		PlayerPrefs.SetInt ("Level4",1);
		ButtonPressSound ();
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void _GAMEWIN(){
		ButtonPressSound ();
		SceneManager.LoadScene ("MainMenu");

	}

	void ButtonPressSound(){
		Sounds.Play();
	}
}
