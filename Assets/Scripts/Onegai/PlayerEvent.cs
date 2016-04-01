using UnityEngine;
using System.Collections;

public class PlayerEvent : MonoBehaviour {

	public float onegaiTime = 2;
	private GameObject eventOnegai;
	private bool isActive = false;

	private GameObject CompOnegai;
	private float compOnegaiTime = 2;
	private bool isCompActive = false;
	private bool isComp = false;

	// Use this for initialization
	void Start () {
		eventOnegai = GameObject.Find ("EventOnegai");
		if (eventOnegai != null) eventOnegai.SetActive (false);

		CompOnegai = GameObject.Find ("CompOnegai");
		if (CompOnegai != null)CompOnegai.SetActive (false);
	}
	
	// Update is called once per frame
	void Update () {
		if (eventOnegai == null || CompOnegai == null)
			return;
		if (onegaiTime > 0) {
			onegaiTime -= Time.deltaTime;
			return;
		}

		if (isActive == false) {
			isActive = true;
			eventOnegai.SetActive (true);
		}

		if (isCompActive) {
			if (compOnegaiTime > 0) {
				compOnegaiTime -= Time.deltaTime;
				return;
			}

			if (isComp == false) {
				isComp = true;
				CompOnegai.SetActive (true);
			}
		}
	}

	public void setCompAtive(bool active) {
		isCompActive = active;
	}
}
