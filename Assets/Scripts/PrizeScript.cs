using UnityEngine;
using System.Collections;

public class PrizeScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	static bool showing = false;
	void Update () {
		if (!isVisible()) {
			transform.position = new Vector3(1000.0f, 0.0f, -1.0f);
			showing = false;
			return;
		}
		if (!showing) {
			// change texture to new baddy
			Prize newPrize = Gregor.shared.currentRoom.prize;
			OTSprite activeSprite = OT.Sprite("Prize");
			
			Debug.Log(string.Format("New Prize: {0}", newPrize.textureName()));
			Vector2 oldSize = new Vector2(activeSprite.image.width, activeSprite.image.height);
			activeSprite.image = (Texture)Resources.Load(string.Format("Prizes/{0}", newPrize.textureName()));
			
			transform.position = new Vector3(7.0f, 0.0f, -1.0f);
			showing = true;
		}
	}
	
	bool isVisible() {
		MapRoom newRoom = Gregor.shared.currentRoom;
		if (newRoom.hasPrize) {
			return true;
		}
		return false;
	}
}
