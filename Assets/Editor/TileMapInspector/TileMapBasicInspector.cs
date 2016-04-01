using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GenerateGroundScript))]
public class TileMapBasicInspector : Editor {
	public override void OnInspectorGUI(){
		DrawDefaultInspector ();
		if (GUILayout.Button ("Generate")) {
			GenerateGroundScript tilemap = (GenerateGroundScript)target;
			tilemap.init ();
		}
	}
}
