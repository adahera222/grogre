using UnityEngine;
using System.Collections;

public class TraitOverlay : MonoBehaviour {
	bool runOnce;

	// Use this for initialization
	void Start () {
		runOnce = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (runOnce) {
			runOnce = false;
			string phase = "Baby";
			if ((GrowthStage)Gregor.shared.growthStage == GrowthStage.ADULT)
				phase = "Adult";
			if ((GrowthStage)Gregor.shared.growthStage == GrowthStage.TEEN)
				phase = "Teen";
			
			OTSprite evol = OT.Sprite(gameObject.tag);
			if (gameObject.tag == "DefOverlay") {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/{0}/{1}", phase, string.Format("{1}-def-{0}", Gregor.shared.defLevel+1, phase.ToLower())));
			}
			if (gameObject.tag == "StrongOverlay"){
				evol.image = (Texture)Resources.Load(string.Format("Gregor/{0}/{1}", phase, string.Format("{1}-strong-{0}", Gregor.shared.strongLevel+1, phase.ToLower())));
			}
			if (gameObject.tag == "BrainsOverlay") {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/{0}/{1}", phase, string.Format("{1}-brains-{0}", Gregor.shared.brainsLevel+1, phase.ToLower())));
			}
		}
	}
}
