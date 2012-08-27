using UnityEngine;
using System.Collections;

public class UpButton : ButtonScript {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isVisible()) {
			gameObject.transform.position = new Vector3(1000.0f, 0.0f, -1.0f);
			return;
		}
		gameObject.transform.position = new Vector3(5.0f, 4.0f, -3.0f);
		if (IsClickingUp()) {
			Gregor.shared.moveUp();
		}
	}
	
	bool isVisible() {
		MapRoom newRoom = Gregor.shared.currentRoom;
		if (newRoom.hasBaddy || newRoom.hasPrize)
			return false;
		if (Gregor.shared.xLocation >= MapManager.shared.mapWidth - 1)
			return false;
		return true;
	}
}
