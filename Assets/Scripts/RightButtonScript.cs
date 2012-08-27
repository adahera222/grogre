using UnityEngine;
using System.Collections;

public class RightButtonScript : ButtonScript {

	// Use this for initialization
	void Start () {
	
		//Vector3 oldScale = transform.localScale;
		//transform.localScale = new Vector3(oldScale.x * 0.5f, oldScale.y * 0.5f, oldScale.z);
	}
	
	// Update is called once per frame
	void Update () {
		if (IsClickingUp()) {
			MapRoom room = Gregor.shared.currentRoom;
			if (room.hasBaddy)
				room.monster.Hit(1);
			else if (room.hasPrize && room.prize.canCollect)
				room.prize.Collect();
			else 
				room.clearRoom(); // for now, forfeit treasure.
		}
	}
}
