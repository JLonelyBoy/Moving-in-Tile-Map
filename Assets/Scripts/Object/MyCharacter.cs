using UnityEngine;
using System.Collections;

public class MyCharacter : BaseChar {
	public float waitTime = 2.0f;

	bool isSelected = false;
	NavMeshAgent agent;
	public bool rotation = false;
	public float playerMoveSpeed = 1.5f;

	// character rect transform
	private RectTransform rect;

	// Use this for initialization
	public override void Start () {
		init ();
	}

	void init(){
		selectChar = characterSelect;
		agent = gameObject.GetComponent<NavMeshAgent>();
		agent.speed = playerMoveSpeed;
		agent.updateRotation = rotation;
		rect = GetComponent<RectTransform> ();
		SceneManager.getObstacleList ();
//		createPlan ();
		createPlanTest ();
	}
	
	// Update is called once per frame
	public override void Update () {
		if (isSelected) Moving ();
		checkMoving ();
		if (checkMoving ()) {
			movingCallAnim ();
		}
		if (!isDoingPlan ()) {
			StartCoroutine(doplan ());
		}
	}

	public bool getIsMyChar(){
		return isMyCharacter;
	}


	//キャラを選択します
	void characterSelect(){
		if (!isSelected) {
			isSelected = true;
			transform.FindChild ("SubItem/SelectedArrow").gameObject.SetActive (true);
			selectChar = characterSelect;
		} else {
			isSelected = false;
			transform.FindChild ("SubItem/SelectedArrow").gameObject.SetActive (false);
			selectChar = characterSelect;	
		}
	}

//	//キャラを外します 
//	void LeaveSelect(){
//		isSelected = false;
//		transform.FindChild ("SubItem/SelectedArrow").gameObject.SetActive (false);
//		selectChar = characterSelect;
//	}
		
	//プレヤーをクリックの処理
	void OnMouseDown() {
		if (isMyCharacter) {
			SceneManager.setSelectedChar (gameObject);
		} else {
			SceneManager.setSelectedGuessChar (gameObject);
		}
	}

	void Moving(){
		if (Input.GetMouseButtonDown (0)) {
			Vector3 desPos = SceneManager.getSelectionCube ().transform.position;
			float tileSize = SceneManager.getTileScript ().tileSize;
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100)) {
				Debug.Log (new Vector3 (desPos.x + tileSize / 2, desPos.y, desPos.z - tileSize / 2));
				agent.SetDestination (new Vector3(desPos.x + tileSize/2,desPos.y,desPos.z - tileSize/2));
			}
		}
	}



	public Vector3 getPosition() {
		if (rect == null) {
			rect = GetComponent<RectTransform> ();
		}
		return rect.position;
	}

	public Vector3 getScale() {
		if (rect == null) {
			rect = GetComponent<RectTransform> ();
		}
		return rect.localScale;
	}
}
