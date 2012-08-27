using UnityEngine;
using System.Collections;

public class HitManager : MonoBehaviour {
	Vector3 gregorInitialPosition;
	Vector3 enemyInitialPosition;
	bool doOnce;
	
	
	private static HitManager _instance;
    public static HitManager shared { get{ return _instance; } }
	
	void Awake() {
		_instance = this;
		doOnce = true;
	}
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (doOnce) {
			doOnce = false;
			
			gregorInitialPosition = GameObject.FindGameObjectWithTag("GregorHitTens").transform.position;
			enemyInitialPosition = GameObject.FindGameObjectWithTag("EnemyHitTens").transform.position;
			hideEnemy();
			hideGregor();
		}
	}
	
	public void hideEnemy() {
		GameObject.FindGameObjectWithTag("EnemyHitTens").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		GameObject.FindGameObjectWithTag("EnemyHitSign").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		GameObject.FindGameObjectWithTag("EnemyHitOnes").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
	}
	
	public void hideGregor() {
		GameObject.FindGameObjectWithTag("GregorHitTens").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		GameObject.FindGameObjectWithTag("GregorHitSign").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		GameObject.FindGameObjectWithTag("GregorHitOnes").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
	}
	
	public void showGregorDelta(int delta) {
		StopCoroutine("showGregorCoroutine");	
        StartCoroutine("showGregorCoroutine", delta);
	}
	
	IEnumerator showGregorCoroutine(int delta) {
		if (delta < 0) {
			OT.Sprite("GregorHitSign").image = (Texture)Resources.Load("Numbers/neg");
		} else {
			OT.Sprite("GregorHitSign").image = (Texture)Resources.Load("Numbers/pos");
		}
		delta = (int)Mathf.Abs((float)delta);
		int ones = delta % 10;
		int tens = delta / 10;
		if (tens == 0) {
			GameObject.FindGameObjectWithTag("GregorHitTens").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		} else {
			OT.Sprite("GregorHitTens").image = (Texture)Resources.Load(string.Format("Numbers/tens-{0}", tens));
			GameObject.FindGameObjectWithTag("GregorHitTens").transform.position = gregorInitialPosition;
		}
		OT.Sprite("GregorHitOnes").image = (Texture)Resources.Load(string.Format("Numbers/ones-{0}", ones));
		GameObject.FindGameObjectWithTag("GregorHitSign").transform.position = gregorInitialPosition;
		GameObject.FindGameObjectWithTag("GregorHitOnes").transform.position = gregorInitialPosition;
		yield return new WaitForSeconds(1.0f);
		hideGregor();
	}
	
	public void showEnemyDamage(int damage) {
		StopCoroutine("showEnemyCoroutine");	
        StartCoroutine("showEnemyCoroutine", damage);
	}
	
	IEnumerator showEnemyCoroutine(int damage) {
		int ones = damage % 10;
		int tens = damage / 10;
		if (tens == 0) {
			GameObject.FindGameObjectWithTag("EnemyHitTens").transform.position = new Vector3(1000.0f, 0.0f, 0.0f);
		} else {
			OT.Sprite("EnemyHitTens").image = (Texture)Resources.Load(string.Format("Numbers/tens-{0}", tens));
			GameObject.FindGameObjectWithTag("EnemyHitTens").transform.position = enemyInitialPosition;
		}
		OT.Sprite("EnemyHitOnes").image = (Texture)Resources.Load(string.Format("Numbers/ones-{0}", ones));
		GameObject.FindGameObjectWithTag("EnemyHitSign").transform.position = enemyInitialPosition;
		GameObject.FindGameObjectWithTag("EnemyHitOnes").transform.position = enemyInitialPosition;
		yield return new WaitForSeconds(1.0f);
		hideEnemy();
	}
}
