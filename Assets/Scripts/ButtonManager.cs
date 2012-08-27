using UnityEngine;
using System.Collections;

public class ButtonManager : MonoBehaviour {
	GameObject leftButton;
	GameObject rightButton;
	bool returning;
	
	private static ButtonManager _instance;
    public static ButtonManager shared { get{ return _instance; } }
	
	void Awake () {
		_instance = this;
		returning = true;
		leftButton = GameObject.FindGameObjectWithTag("LeftButton");
		rightButton = GameObject.FindGameObjectWithTag("RightButton");
	}
	
	void Start() {
	}
	
	// Update is called once per frame
	void Update () {
		if (returning) {
			returning = false;
			Gregor.shared.currentRoom.notify();
		}
	}
	
	public void hideButton(GameObject button) {
		button.transform.position = new Vector3(1000.0f, -4.0f, -2.0f);
	}
	
	public void showLeftButton(string buttonName) {
		OTSprite leftSprite = OT.Sprite("LeftButton");
		leftSprite.image = (Texture)Resources.Load(string.Format("Buttons/{0}", buttonName));
		leftButton.transform.position = new Vector3(-5.0f, -7.0f, -1.0f);
	}
	public void showRightButton(string buttonName) {
		OTSprite rightSprite = OT.Sprite("RightButton");
		rightSprite.image = (Texture)Resources.Load(string.Format("Buttons/{0}", buttonName));
		rightButton.transform.position = new Vector3(5.0f, -7.0f, -1.0f);
	}
}
