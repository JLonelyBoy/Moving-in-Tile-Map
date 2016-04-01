using UnityEngine;
using System.Collections;

public class Loader : MonoBehaviour {
	public GameObject sceneManager;

	// Use this for initialization
	void Awake () {
		if (SceneManager.instance == null)
			Instantiate (sceneManager);
	}

}
