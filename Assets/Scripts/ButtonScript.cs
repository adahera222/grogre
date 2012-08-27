using UnityEngine;
using System.Collections;

public class ButtonScript : MonoBehaviour {
	
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	protected bool IsClickingUp() {
		if(Input.GetMouseButtonUp(0)) {
	        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
	        RaycastHit hit;
	        
	        if(Physics.Raycast(ray, out hit, 10000.0f)) {
	            if(hit.collider.gameObject == gameObject) {
					//Debug.LogWarning("Button Pressed!", gameObject);
					return true;
	            }
	        }
		}
		return false;
	}
}
