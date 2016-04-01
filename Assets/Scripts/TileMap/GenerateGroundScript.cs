using UnityEngine;
using System.Collections;
//using UnityEditor;

public class GenerateGroundScript : MonoBehaviour {
	public int groundCol;
	public int groundRow;
	[Range(0,1)]
	public float wallThickness = 0.01f;

	// Use this for initialization
	void Start () {
		ClearAll ();
		init ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void init(){
		initGround ();
		initWall ();
		gameObject.transform.Rotate (new Vector3 (gameObject.transform.rotation.x, 45f, gameObject.transform.rotation.z));
		gameObject.transform.localScale = (new Vector3 (1.2f, 1.2f, 1.2f));
	}

	void initGround(){
		Object blackground =Resources.Load("Prefabs/TileMap/TileBlack", typeof(GameObject));
		Object whiteground = Resources.Load("Prefabs/TileMap/TileWhite", typeof(GameObject));
		Vector3 pos = new Vector3(0.5f, 0f ,0.5f);
		for (int i = 1; i <= groundRow; i++) {
			pos = new Vector3 (0.5f, 0, pos.z);
			for (int j = 1; j <= groundCol; j++) {
				GameObject obj;
				if ((j % 2 == 0 && i % 2 == 0)|| (j % 2 != 0 && i % 2 != 0))  {
					obj = Instantiate (whiteground, pos, Quaternion.identity) as GameObject;
				}else{
					obj = Instantiate (blackground, pos, Quaternion.identity) as GameObject;
				}
				obj.transform.parent = gameObject.transform;
				pos = new Vector3 (pos.x + 1, 0, pos.z);
			}
			pos.z += 1;
		}
	}

	void initWall(){
		Object wall =Resources.Load("Prefabs/TileMap/TileMapWall", typeof(GameObject));
		//右壁
		GameObject rightWall = Instantiate (wall, Vector3.zero, Quaternion.identity) as GameObject;
		rightWall.transform.localScale = new Vector3 (groundCol,rightWall.transform.localScale.y, wallThickness);
		rightWall.transform.position = new Vector3 (groundCol / 2f, 0.5f, groundRow);
		rightWall.transform.parent = gameObject.transform;

		//左壁
		GameObject leftWall = Instantiate (wall, Vector3.zero, Quaternion.identity) as GameObject;
		leftWall.transform.localScale = new Vector3 (wallThickness, leftWall.transform.localScale.y, groundRow);
		leftWall.transform.position = new Vector3 (0, 0.5f, groundRow/2f);
		leftWall.transform.parent = gameObject.transform;
	}

	void ClearAll(){
		foreach (Transform child in transform) {
			GameObject.Destroy(child.gameObject);
		}
	}

}
