using UnityEngine;
using System.Collections;

public class BehaviourEntity{
	private BehaviourType _behaviourType;
	private Vector3 _startPos;
	private Vector3 _desPos ;
	private float _behaviourDuration = 0f;

	public BehaviourEntity(Vector3 startPosition, Vector3 DesPosition, GameObject obj = null){
		_startPos = startPosition;
		_desPos = DesPosition;
		behaviourGenerate (_startPos,_desPos, obj);
	}

	public void behaviourGenerate(Vector3 start, Vector3 end, GameObject obj = null){
		Vector2 obstacleCheck = new Vector2 (Mathf.FloorToInt(end.x), -Mathf.FloorToInt(-end.z));
		if (start == end) {
			_behaviourDuration = 5.0f;
			_behaviourType = BehaviourType.IDLE;
		} else if(start != end){
			_behaviourType = BehaviourType.MOVING;
		} 
		if (SceneManager.getObstacleList().Contains (obstacleCheck)) {
			_behaviourDuration = 5.0f;
			_behaviourType = BehaviourType.REACTING;
		}
		if (obj != null) {
			_behaviourDuration = 2f;
			_behaviourType = BehaviourType.GREETING;
		}
		Debug.Log (_behaviourType);
	}

	public BehaviourType getBehaviourType(){
		return _behaviourType;
	}

	public Vector3 getStartPos(){
		return _startPos;
	}

	public Vector3 getDesPos(){
		return _desPos;
	}

	public float getDuration(){
		return _behaviourDuration;
	}
}

public enum BehaviourType {
	MOVING,
	REACTING,
	GREETING,
	IDLE
};
