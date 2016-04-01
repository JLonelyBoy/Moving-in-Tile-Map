using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GenTileMap))]
public class TileMapInspector : Editor {
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Generate Ground")) {
			GenTileMap tilemap = (GenTileMap)target;
			tilemap.BuildMesh ();
		}
		if (GUILayout.Button ("Generate Wall")) {
			GenTileMap tilemap = (GenTileMap)target;
			tilemap.initWall ();
		}
	

	}
}
