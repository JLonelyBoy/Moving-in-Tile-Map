using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(GenTileMapUnitTest))]
public class UnitTestTileMapInspector : Editor {
		public override void OnInspectorGUI(){
			DrawDefaultInspector ();
			if (GUILayout.Button ("Generate Ground")) {
				GenTileMapUnitTest tilemap = (GenTileMapUnitTest)target;
				tilemap.BuildMesh ();
			}
		}
}
