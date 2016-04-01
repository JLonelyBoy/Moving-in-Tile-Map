using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GenTileMapUnitTest))]
public class SelectedCubeScripts: MonoBehaviour {
	GenTileMapUnitTest _TileMap;
	Collider coll;
	Vector3 currentTileCoord;
	public Transform selectionCube;
	// Use this for initialization
	void Start () {
		_TileMap = GetComponent<GenTileMapUnitTest> ();
		coll = GetComponent<Collider> ();
		selectionCube.transform.localScale = selectionCube.transform.localScale * _TileMap.tileSize;
	}

	// Update is called once per frame
	void Update () {
		SelectedArea ();
	}

	void SelectedArea(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (coll.Raycast (ray, out hit, Mathf.Infinity)){
			float x =   Mathf.FloorToInt(hit.point.x / _TileMap.tileSize);
			float z =  Mathf.FloorToInt(hit.point.z / _TileMap.tileSize);
			currentTileCoord.x = x;
			currentTileCoord.z = z + 1;
			Debug.Log ("Current Cell " + currentTileCoord);
			selectionCube.transform.position = currentTileCoord * _TileMap.tileSize;
		} 
	}
}
