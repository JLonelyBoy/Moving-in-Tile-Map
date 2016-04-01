using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class BaseChar : MonoBehaviour {
	private bool _isIdle;
	private bool _isMoving;
	private bool _isItemReacting;
	private bool _isGreetingReacting;
	private bool _isDoingPlan;
	private Vector3 prevPos;
	private float _planDelayTime = 5f;
	Queue plan = new Queue();


	public float hungry;
	public float comfort;
	public float amusement;
	public float sociability;
	public bool isMyCharacter;

	public virtual void Start () {
	}

	public virtual void Update () {
	}

	//Call when Select object
	public Delegate.VoidDelegate selectChar {
		get;
		set;
	}

	public Delegate.VoidGameObjectDelegate selectItemReaction {
		get;
		set;
	}

	public bool isDoingPlan(){
		return _isDoingPlan;
	}

	//Animation Call
	protected void movingCallAnim(){
		startAnim (AnimNameConstant.WIN, true);
		setAnimTimeScale ();
	}

	protected void greetingCallAnim(){
		startAnim (AnimNameConstant.BACK, true);
	}

	protected void itemReactCallAnim(){
		startAnim (AnimNameConstant.WIN, true);
	}

	protected void idleCallAnim(){
		startAnim (AnimNameConstant.FRONT, true);
	}

	protected Vector3 RandomPos(){
		Vector3 destinationPos = new Vector3();
		GenTileMap _tile = SceneManager.getTileScript ();
//		Debug.Log (_tile + " Char Name " + gameObject.name);
		float x = Random.Range (0, _tile.size_x - 1);
		float y = gameObject.transform.position.y;
		float z = Random.Range (0, _tile.size_z - 1);
//		x = x + _tile.tileSize / 2 >= _tile.size_x - 1 ? x - _tile.tileSize / 2 : x + _tile.tileSize / 2;
//		z = z + _tile.tileSize / 2 >= _tile.size_z - 1 ? z - _tile.tileSize / 2 : z + _tile.tileSize / 2;
		x = x + _tile.tileSize / 2 ;
		z = z + _tile.tileSize / 2 ;
		destinationPos = new Vector3 (( x * _tile.tileSize ), y, ( -z * _tile.tileSize ));
		return destinationPos;
	}

	//Character Behaviour 
	protected void createPlan(){
		int maxPlan = 5;
		Vector3 prevPos = transform.position;
		if (plan.Count == 0 || plan.Count < maxPlan) {
			for (int i = plan.Count; i <= maxPlan; i++) {
				Vector3 des = RandomPos ();
				BehaviourEntity behaviour = new BehaviourEntity (prevPos, des);
				prevPos = des;
				plan.Enqueue (behaviour);
			}
		}
	}

	//For testing we will create 4 type behaviour for each charactor by order
	protected void createPlanTest(){
		List<Vector2> obs = SceneManager.getObstacleList();
		List<GameObject> guessObjLst = SceneManager.getGuessList ();
		Vector3 prevPos = transform.position;
		Vector3 desPos = new Vector3 ();
		//create Idle for seconds
		desPos = transform.position;
		prevPos = transform.position;
		BehaviourEntity behaviour1 = new BehaviourEntity (prevPos,desPos);
		plan.Enqueue (behaviour1);
		prevPos = desPos;
		//create Moving Behaviour
		desPos = new Vector3(4.5f,0f,-4.5f);
		BehaviourEntity behaviourEnt = new BehaviourEntity(prevPos, desPos);
		plan.Enqueue (behaviourEnt);
		prevPos = desPos;
		//create Greating Behaviour
		GameObject obj = guessObjLst[Random.Range(0, guessObjLst.Count - 1)];
		desPos = obj.transform.position - Vector3.one;
		BehaviourEntity behave = new BehaviourEntity(prevPos,desPos,obj);
		prevPos = desPos;
		plan.Enqueue (behave);
		//create Reacting Behaviour
		Vector2 reactItemPos = obs[Random.Range(0,obs.Count-1)];
//		Debug.Log ("react item pos "+reactItemPos);
		desPos = new Vector3 (reactItemPos.x + SceneManager.getTileScript().tileSize / 2,
			gameObject.transform.position.y,
			reactItemPos.y == 0 ?  reactItemPos.y - SceneManager.getTileScript().tileSize / 2 : reactItemPos.y + SceneManager.getTileScript().tileSize / 2);
		BehaviourEntity behaviour = new BehaviourEntity (prevPos,desPos);
		plan.Enqueue (behaviour);
	}
		
	public IEnumerator doplan(){
		_isDoingPlan = true;
		while (plan.Count != 0) {
//			Debug.Log ("Auto Mode: " + SceneManager.getAutoMode());
			while(!SceneManager.getAutoMode()){				
				yield return new WaitForEndOfFrame ();
			}
			BehaviourEntity entity = plan.Dequeue () as BehaviourEntity;
			switch (entity.getBehaviourType()) {
			case BehaviourType.IDLE:
				Debug.Log("is idle " + entity.getDesPos());
				break;
			case BehaviourType.MOVING:
				Debug.Log ("Moving to "+entity.getDesPos());
				gameObject.GetComponent<NavMeshAgent> ().SetDestination (entity.getDesPos());
				yield return new WaitUntil(() =>_isMoving == false);
				break;
			case BehaviourType.REACTING:
				//move to reacting position
				Debug.Log("is reacting " + entity.getDesPos());
				gameObject.GetComponent<NavMeshAgent> ().SetDestination (entity.getDesPos());
				yield return new WaitUntil(() =>_isMoving == false);
				//Reacting process (In this situation is play animation)
				yield return new WaitForSeconds(entity.getDuration());
				break;
			case BehaviourType.GREETING:
				break;
			default:
				break;
			}
			yield return new WaitForSeconds (_planDelayTime);
		}
	}

	//キャラを選択したあら、家具とかおタップすると、アニメションあ表示します。
	protected void itemReaction(){
		
	}

	protected bool checkMoving(){
		if (gameObject.GetComponent<NavMeshAgent>().velocity.sqrMagnitude <= 0.1f){
			_isMoving = false;
		}else 
			_isMoving = true;
		return _isMoving;
	}

	//Animation Process
	private void startAnim(string AnimName, bool loop){
		GetComponent<SkeletonAnimation> ().state.SetAnimation (0, AnimName, loop);
	}

	private void completeAnim(){
	}

	private void clearAnim(){
		GetComponent<SkeletonAnimation> ().state.ClearTrack (0);
	}

	private void setAnimTimeScale(){
		GetComponent<SkeletonAnimation> ().timeScale = 1;
	}

	//Effect process
}
