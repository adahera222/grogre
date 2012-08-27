using UnityEngine;
using System.Collections;

public class ChooseEvolutionButton : ButtonScript {
	bool runOnce = true;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (runOnce) {
			runOnce = false;
			OTSprite activeSprite = OT.Sprite(gameObject.tag);
			if ((GrowthStage)Gregor.shared.growthStage == GrowthStage.TEEN) {
				activeSprite.image = (Texture)Resources.Load(string.Format("Gregor/Teen/{0}teen", gameObject.tag.ToLower()));
			}
			if ((GrowthStage)Gregor.shared.growthStage == GrowthStage.ADULT) {
				activeSprite.image = (Texture)Resources.Load(string.Format("Gregor/Adult/{0}adult", gameObject.tag.ToLower()));
			}
		}
		if (IsClickingUp()) {
			Gregor.shared.evolveTrait(gameObject.tag);
			Application.LoadLevel("playScene");
		}
	}
}
