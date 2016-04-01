using UnityEngine;
using System.Collections;

public class TileScript : MonoBehaviour {
	Material highlightMaterial;
	Material normal;
	public Collider coll;
	public Renderer ren;
	// Use this for initialization
	void Start () {
		ren = GetComponent<Renderer> ();
		coll = GetComponent<Collider> ();
		normal = ren.material;
		highlightMaterial =Resources.Load("Material/FloorGround", typeof(Material)) as Material;
	}
	
	// Update is called once per frame
	void Update () {
		SelectedArea ();
	}

	void SelectedArea(){
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
		RaycastHit hit;
		if (coll.Raycast (ray, out hit, Mathf.Infinity)) {
			ren.material = highlightMaterial;
			Debug.Log (highlightMaterial);
		} else
			ren.material = normal;
	}
}
