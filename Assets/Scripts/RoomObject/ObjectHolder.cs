using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;

public class ObjectHolder : MonoBehaviour {

	NavMeshObstacle _nav_obstacle;
	Rigidbody _rig;
	Object[] _sprList;
	GenTileMap _tilemap;

	// Use this for initialization
	void Start () {
		_tilemap = GameObject.Find ("TileMap").GetComponent<GenTileMap> ();
		_nav_obstacle = GetComponent<NavMeshObstacle> ();
		_rig = GetComponent<Rigidbody> ();
//		loadResources ();
//		int rand = Random.Range (0, _sprList.Length - 1);
//		Sprite Item = _sprList [rand] as Sprite;
//		GetComponentInChildren<SpriteRenderer>().sprite = Item;
		transform.localScale = transform.localScale * _tilemap.tileSize;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void loadResources(){
//		sprTexture = AssetDatabase.LoadAllAssetsAtPath ("Assets/Resources/ExampleFur/", typeof(Texture2D)) ;
		_sprList = Resources.LoadAll("ExampleFur", typeof(Sprite)) ;
	}

}
