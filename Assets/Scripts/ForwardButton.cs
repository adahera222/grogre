using UnityEngine;
using System.Collections;

public class ForwardButton : ButtonScript {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (!isVisible()) {
			gameObject.transform.position = new Vector3(1000.0f, 0.0f, -1.0f);
			return;
		}
		gameObject.transform.position = new Vector3(6.0f, 2.0f, -3.0f);
		if (IsClickingUp()) {
			Gregor.shared.moveForward();
		}
	}
	
	bool isVisible() {
		MapRoom newRoom = Gregor.shared.currentRoom;
		if (newRoom.hasBaddy || newRoom.hasPrize)
			return false;
		if (Gregor.shared.xLocation >= MapManager.shared.mapWidth - 1 ||
			Gregor.shared.yLocation >= MapManager.shared.mapHeight - 1)
			return false;
		return true;
	}
}