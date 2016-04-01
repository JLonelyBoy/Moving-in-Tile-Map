using UnityEngine;
using System.Collections;

public class PanelScript : MonoBehaviour {
	public GameObject CellPrefabs;
	public int imageCount;

	// Use this for initialization
	void Start () {
		GameObject[] ImgObj = GameObject.FindGameObjectsWithTag ("Player");
		for (int i = 0; i< ImgObj.Length;  i++){
			GameObject newCell = Instantiate (CellPrefabs) as GameObject;
			newCell.transform.SetParent (this.transform, false);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
