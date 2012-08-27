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
			
			OTSprite evol = OT.Sprite(gameObject.tag);
			if (gameObject.tag == "DefOverlay") {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/Baby/{0}", string.Format("baby-def-{0}", Gregor.shared.defLevel+1)));
			}
			if (gameObject.tag == "StrongOverlay"){
				evol.image = (Texture)Resources.Load(string.Format("Gregor/Baby/{0}", string.Format("baby-strong-{0}", Gregor.shared.strongLevel+1)));
			}
			if (gameObject.tag == "BrainsOverlay") {
				evol.image = (Texture)Resources.Load(string.Format("Gregor/Baby/{0}", string.Format("baby-brains-{0}", Gregor.shared.brainsLevel+1)));
			}
		}
	}
}
