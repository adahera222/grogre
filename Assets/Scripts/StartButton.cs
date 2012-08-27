using UnityEngine;
using System.Collections;

public class StartButton : ButtonScript {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (IsClickingUp()) {
			Application.LoadLevel("playScene");
		}
	}
}
