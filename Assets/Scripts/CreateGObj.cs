using UnityEngine;
using System.Collections;
//using UnityEditor;

public class CreateGObj : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Object pre = Resources.Load("Prefabs/Player3DEffect", typeof(GameObject));
		GameObject clone = Instantiate(pre, Vector3.zero, Quaternion.identity) as GameObject;
		// Modify the clone to your heart's content
		clone.transform.position = Vector3.one;

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
