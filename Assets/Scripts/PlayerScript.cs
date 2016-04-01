using UnityEngine;
using System.Collections;

public class PlayerScript : AbstractPlayer {
	public static bool isMoving = false;
	Vector3 standingPos = new Vector3 ();
	Vector3 tempPos = new Vector3 ();
	public Vector3 _desPos;
	GenTileMap _tile;

	public override void Start(){
		_tile = _plane.GetComponent<GenTileMap>();
		Debug.Log (_tile);
	}

	// Update is called once per frame
	public override void Update () {
//		_desPos = randomDesPos ();
//		Debug.Log (_desPos);
//		checkIsMoving ();
	}

	public Vector3 randomDesPos(){
		float x = Random.Range(0f, (float)_tile.size_x);
		float y = transform.position.y;
		float z = Random.Range(0f, (float)-_tile.size_z);
		return new Vector3 (x,y,z);
	}


}
