using UnityEngine;
using System.Collections;

public class BFSFindAlgorithm : MonoBehaviour {
	GenTileMapUnitTest _tile;
	MyPosition _pos;
	Queue open = new Queue ();
	Vector2 destination;
	public GameObject plane;


	void Start(){
		_tile = plane.GetComponent<GenTileMapUnitTest>();
		open = _tile.moveAbleMatrix;
	}

	void Update(){
		
	}

	Vector3 randomPos(){
		float x = Random.Range(0f, (float)_tile.size_x);
		float y = transform.position.y;
		float z = Random.Range(0f, (float)-_tile.size_z);
		return new Vector3 (x,y,z);
	}
}

class MyPosition
{
	public Vector2 pos;
	public bool Visited = false;
	public MyPosition Previous;
	// [...]
}


