using UnityEngine;
using System.Collections;

public class EnemyScript : CharacterScript {
	
	// Use this for initialization
	//void Start () { }
	
	// Update is called once per frame
	static bool showing = false;
	void Update () {
		if (!isVisible()) {
			transform.position = new Vector3(1000.0f, 0.0f, -1.0f);
			showing = false;
			return;
		}
		if (!showing) {
			// change texture to new baddy
			Monster newMonster = Gregor.shared.currentRoom.monster;
			OTSprite activeSprite = OT.Sprite("Enemy");
			
			Debug.Log(string.Format("New Monster: {0}", newMonster.textureName()));
			activeSprite.image = (Texture)Resources.Load(string.Format("Enemies/{0}", newMonster.textureName()));
			
			transform.position = new Vector3(7.0f, 0.0f, -1.0f);
			showing = true;
		}
	}
	
	bool isVisible() {
		MapRoom newRoom = Gregor.shared.currentRoom;
		if (newRoom.hasBaddy) {
			return true;
		}
		return false;
	}
}
