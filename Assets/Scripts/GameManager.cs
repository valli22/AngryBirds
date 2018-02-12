using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	public static GameManager instance;

	[SerializeField]
	GameObject pajaroPref;

	[SerializeField]
	Transform pointStartingToShoot;

	GameObject instancePref;
	Vector3 startPoint;
	public float rangeShoot = 3;
	public float power = 1000;

	public int lifes = 6;
	public int enemies = 3;
	int score = 0;

	[SerializeField]
	Text hudLife;
	[SerializeField]
	Text hudScore;
	[SerializeField]
	Text hudEnemies;

	[SerializeField]
	GameObject winHud;
	[SerializeField]
	Text winScore;
	[SerializeField]
	GameObject highScoreImage;
	[SerializeField]
	GameObject looseHud;

	public bool canShoot = true;

	void Awake(){
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		ChangeLife ();
		ChangeScore ();
		ChangeEnemiesHud ();
	}
	
	// Update is called once per frame
	void Update () {
		if (lifes > 0 && canShoot) {
			if (Input.GetMouseButton (0)) {
				if (instancePref != null) {
					Vector3 mousePos = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
					Vector3 direction = mousePos - startPoint;
					float min = Mathf.Min (rangeShoot, direction.magnitude);
				
					instancePref.transform.position = startPoint + direction.normalized * min;
				}
			}

			if (Input.GetMouseButtonDown (0)) {
				RaycastHit hitInfo;
				if (Physics.Raycast (Camera.main.ScreenPointToRay (Input.mousePosition), out hitInfo)) {
					if (hitInfo.transform.tag == "Tirachinas") {
						startPoint = Camera.main.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y, -Camera.main.transform.position.z));
						instancePref = Instantiate (pajaroPref, pointStartingToShoot.position, pajaroPref.transform.rotation) as GameObject;
						instancePref.GetComponent<Rigidbody2D> ().isKinematic = true;
					}
				}
			}
			if (Input.GetMouseButtonUp (0)) {
				if (instancePref != null) {
					instancePref.GetComponent<Rigidbody2D> ().isKinematic = false;
					instancePref.GetComponent<Rigidbody2D> ().AddForce ((startPoint - instancePref.transform.position) * power);
					instancePref.GetComponent<Proyectil> ().changeTag ("Lanzado");
					instancePref = null;
					startPoint = new Vector3 (0, 0, 0);
					canShoot = false;
				}
			}

		}
	}

	public void ChangeScore(){
		hudScore.text = "Score: "+score.ToString();
	}

	public void ChangeLife(){
		if (lifes <= 0) {
			looseHud.SetActive (true);
			Time.timeScale = 0;
		}
		hudLife.text = lifes.ToString();
	}

	public void ChangeEnemies(){
		enemies--;
		score += 100 * lifes;
		if (enemies <= 0) {
			winHud.SetActive (true);
			winScore.text = "Score: " + score;
			int scoreSaved = PlayerPrefs.GetInt ("Score", 0);
			if (scoreSaved < score) {
				PlayerPrefs.SetInt ("Score",score);
				winScore.text = "NEW SCORE: " + score;
				highScoreImage.SetActive (true);
			}
			Time.timeScale = 0;
		}
		ChangeScore ();
		ChangeEnemiesHud ();
	}
	public void ChangeEnemiesHud(){
		hudEnemies.text = enemies.ToString();
	}

	public void _RESTART(){
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}

	public void _EXIT(){
		Application.Quit ();
	}
	public void _CONTINUE(){
		//ahora vuelve al mismo nivel porque no hay mas
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
		Time.timeScale = 1;
	}
}
