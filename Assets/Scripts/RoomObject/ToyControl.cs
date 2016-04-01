using UnityEngine;
using System.Collections;

public class ToyControl : MonoBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			Canvas canvas = GetComponentInChildren<Canvas> ();
			canvas.enabled = true;
		}
	}
		
}
