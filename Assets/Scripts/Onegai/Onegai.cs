﻿using UnityEngine;
using System.Collections;

public class Onegai : MonoBehaviour {

	public GameObject panel;

	private MyCharacter myChar;
	private Animator anim;
	private GameObject eventObj;


	// Use this for initialization
	void Start () {
		GameObject player2D = GameObject.Find ("Player2D");
		myChar = player2D.GetComponent<MyCharacter> ();

		eventObj = GameObject.Find ("EventOnegai");

		panel.SetActive (true);
		anim = panel.GetComponent<Animator>();
		anim.enabled = false;
		anim.Stop ();
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 charPos = myChar.getPosition ();
		Vector3 charScale = myChar.getScale (); 
		transform.position = new Vector3(charPos.x, charPos.y + charScale.y + transform.localScale.y, charPos.z);
	}

	void aniPlay() {
		anim.enabled = true;
		anim.Play ("AniOnegai");
	}

	void OnMouseDown() {
		aniPlay ();
		eventObj.SetActive (false);
	}
}
