using UnityEngine;
using System.Collections;

public class CharacterScript : MonoBehaviour {
	int tickTimer;
	
	void Awake () {	
		tickTimer = (int)(Random.value * 50);
		
	}
	
	// Use this for initialization
	void Start () {
		//transform.localScale += new Vector3(-0.5f,-0.5f,0.0f);
		//Vector3 oldScale = transform.localScale;
		//transform.localScale = new Vector3(oldScale.x * 0.5f, oldScale.y * 0.5f, oldScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void FixedUpdate () {
		tickTimer++;
		tickTimer = tickTimer % 50;
		if (tickTimer == 25) {
			transform.localScale += new Vector3(-0.1f,0.2f,0.0f);
		}
		else if (tickTimer == 0) {
			transform.localScale += new Vector3(0.1f,-0.2f,0.0f);
		}
	}
}
