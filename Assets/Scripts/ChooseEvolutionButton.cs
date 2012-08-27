using UnityEngine;
using System.Collections;

public class ChooseEvolutionButton : ButtonScript {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (IsClickingUp()) {
			Gregor.shared.evolveTrait(gameObject.tag);
			Application.LoadLevel("playScene");
		}
	}
}
