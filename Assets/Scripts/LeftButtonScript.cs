using UnityEngine;
using System.Collections;

public class LeftButtonScript : ButtonScript {

	// Use this for initialization
	void Start () {
	
		//Vector3 oldScale = transform.localScale;
		//transform.localScale = new Vector3(oldScale.x * 0.5f, oldScale.y * 0.5f, oldScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsClickingUp()) {
			Gregor.shared.runAway();
		}
	}
}
