using UnityEngine;
using System.Collections;

public class NavigateScript : MonoBehaviour {
	private PlayerScript _playerBehaviour;
	public bool rotation;
	NavMeshAgent agent;
	public float playerSpeed;
	Vector3 des = new Vector3 ();
	// Use this for initialization
	void Start () {
		_playerBehaviour = GetComponent<PlayerScript>();
		agent = GetComponent<NavMeshAgent> ();
		agent.speed = playerSpeed;
		agent.updateRotation = rotation;
//		Debug.Log (_playerBehaviour._desPos);
	}
	
	// Update is called once per frame
	void Update () {
//		agent.SetDestination (_playerBehaviour.randomDesPos());
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				agent.SetDestination (hit.point);
			}
		}
//		Debug.Log("agent speed: "+agent.velocity);
	}
}
