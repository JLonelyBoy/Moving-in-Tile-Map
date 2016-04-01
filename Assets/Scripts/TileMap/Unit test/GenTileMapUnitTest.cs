using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Collections.Generic;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class GenTileMapUnitTest: MonoBehaviour {

	public int size_x = 8;
	public int size_z = 8;
	public float tileSize = 1.0f;
	[Range(0,1)]
	public float wallThickness = 0.01f;
	public float wallHeigh = 1.0f;
	Vector2 pos;
	public Queue moveAbleMatrix;
	public Queue obsToMatrix;

	List<GameObject> childObj ;
	// Use this for initialization
	void Start () {
		init ();
		transferToMatrix ();
		transferObsToMatrix ();
	}

	//初期化したから、マップをMatrix化処理
	void transferToMatrix(){
		moveAbleMatrix = new Queue ();
		for (int x = 0; x < size_x; x++) {
			for (int z = 0; z < size_z; z++) {
				pos = new Vector2 (x, -z);
				moveAbleMatrix.Enqueue (pos);
			}
		}
	}

	//ObstacleをMatrix化処理
	void transferObsToMatrix(){
		obsToMatrix = new Queue ();
		foreach(Transform child in transform){
			if (child.name == "SelectedInspector")
				continue;
			int x = Mathf.FloorToInt (child.position.x / tileSize);
			int z = Mathf.FloorToInt (child.position.z / tileSize) + 1;
			Debug.Log("Position x: "+ x +" pos z: " + z);
			pos = new Vector2 (x, z);
			obsToMatrix.Enqueue(pos);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void init(){
		BuildMesh ();
	}

	public void BuildMesh() {
		int numTiles = size_x * size_z;
		int numTris = numTiles * 2;

		int vsize_x = size_x + 1;
		int vsize_z = size_z + 1;
		int numVerts = vsize_x * vsize_z;

		// Generate the mesh data
		Vector3[] vertices = new Vector3[ numVerts ];
		Vector3[] normals = new Vector3[numVerts];
		Vector2[] uv = new Vector2[numVerts];

		int[] triangles = new int[ numTris * 3 ];

		int x, z;
		for(z=0; z < vsize_z; z++) {
			for(x=0; x < vsize_x; x++) {
				vertices[ z * vsize_x + x ] = new Vector3( x*tileSize, 0, -z*tileSize );
				normals[ z * vsize_x + x ] = Vector3.up;
				uv[ z * vsize_x + x ] = new Vector2( (float)x / size_x, 1f - (float)z / size_z );
			}
		}

		for(z=0; z < size_z; z++) {
			for(x=0; x < size_x; x++) {
				int squareIndex = z * size_x + x;
				int triOffset = squareIndex * 6;
				triangles[triOffset + 0] = z * vsize_x + x + 		   0;
				triangles[triOffset + 2] = z * vsize_x + x + vsize_x + 0;
				triangles[triOffset + 1] = z * vsize_x + x + vsize_x + 1;

				triangles[triOffset + 3] = z * vsize_x + x + 		   0;
				triangles[triOffset + 5] = z * vsize_x + x + vsize_x + 1;
				triangles[triOffset + 4] = z * vsize_x + x + 		   1;
			}
		}
		//Create new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;

		//Assign Mesh to MeshFilter/MeshRenderer/MeshCollider
		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();

		mesh_filter.mesh = mesh;
		mesh_renderer.material = Resources.Load("Material/FloorGround", typeof(Material)) as Material;
		mesh_collider.sharedMesh = mesh;
	}

	//Example with simple Mesh (Basic to build a mesh)
	void BuildBasicMesh(){
		//Gen Mesh Data
		Vector3[] vertices = new Vector3[4]; /* Mesh is array of square wich made by 3 strangles and square always have 4 vertices */
		int[] triangles = new int[2 * 3];
		/* 1 trangle have three ways to build ex: trangle ABC, trangle CAB, trangle BCA. 
		So if we build trangle where have to write on right order vertex.
		Here we have 2 trangles for each square so the vertices for 2 trangles = 2*3
		*/
		Vector3[] normals = new Vector3[4]; /* define the direction of one vertice, to make the render more smooth */
		Vector2[] uv = new Vector2[4];

		vertices [0] = new Vector3 (0, 0, 0);
		vertices [1] = new Vector3 (1, 0, 0);
		vertices [2] = new Vector3 (0, 0, -1);
		vertices [3] = new Vector3 (1, 0, -1);

		triangles [0] = 0;
		triangles [1] = 3;
		triangles [2] = 2;

		triangles [3] = 0;
		triangles [4] = 1;
		triangles [5] = 3;

		normals [0] = Vector3.up;
		normals [1] = Vector3.up;
		normals [2] = Vector3.up;
		normals [3] = Vector3.up;

		uv [0] = new Vector2 (0, 1);
		uv [1] = new Vector2 (1, 1);
		uv [2] = new Vector2 (0, 0);
		uv [3] = new Vector2 (1, 0);

		//Create new Mesh and populate with the data
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = triangles;
		mesh.normals = normals;
		mesh.uv = uv;
		//Assign Mesh to MeshFilter/MeshRenderer/MeshCollider

		MeshFilter mesh_filter = GetComponent<MeshFilter> ();
		MeshRenderer mesh_renderer = GetComponent<MeshRenderer> ();
		MeshCollider mesh_collider = GetComponent<MeshCollider> ();

		mesh_filter.mesh = mesh;
		mesh_renderer.material = Resources.Load("Material/Wall", typeof(Material)) as Material;
		mesh_collider.sharedMesh = mesh;
	}

	public void ClearAll(string objName){
		if (Application.isEditor) {
			foreach (Transform child in transform) {
				if (child.name == objName)
					GameObject.DestroyImmediate (child.gameObject);
			}
		} else {
			foreach (Transform child in transform) {
				if (child.name == objName)
					GameObject.Destroy (child.gameObject);
			}
		}
	}
}
