using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraScript : MonoBehaviour {
	bool _isZoomIn = false;
	bool _isZoomOut = false;
	bool _isMoveable = false;
	float _normalSize;
	float _nowSize;
	public float maxSize = 1;
	public float zoomSpeed = 0.00001f;
	public float smoothSpeed = 2.0f;
	public float minOrtho = 1.0f;
	public float maxOrtho = 20.0f;

	public bool isRacePressed = false;
	public bool isbrakePressed = false;

	Vector3 cameraPreTopRight;
	Vector3 cameraPreBotLeft;
	Vector3 cameraPosFrame;
	UnityEngine.UI.Text mytext;

	// Use this for initialization
	void Start () {
		_normalSize = Camera.main.orthographicSize;
		cameraPreBotLeft = Camera.main.ViewportToWorldPoint (new Vector3(0,0,Camera.main.nearClipPlane));
		cameraPreTopRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,Camera.main.nearClipPlane));
		mytext = GameObject.Find ("txtDebugIphone").GetComponent<UnityEngine.UI.Text>();
//		Debug.Log ("Pre Camera Pos bot-left "+ cameraPreBotLeft);
//		Debug.Log ("Pre Camera Pos top-right "+ cameraPreTopRight);
//		Debug.Log ("Vector distance: " + (cameraPreBotLeft - cameraPreTopRight));
	}
	
	// Update is called once per frame
	void Update () {
		_nowSize = Camera.main.orthographicSize;
//		Debug.Log (isRacePressed);
		if (isRacePressed )
		{
			zoom ();
//			Debug.Log ("Zoom camera pos bot-left: " + Camera.main.ViewportToWorldPoint (new Vector3(0,0,Camera.main.nearClipPlane)));
//			Debug.Log ("Zoom camera pos top-right: " + Camera.main.ViewportToWorldPoint (new Vector3(1,1,Camera.main.nearClipPlane)));

		}
//		if (_nowSize > maxSize && _nowSize < _normalSize) 
		PinchZoom ();
		_isMoveable = Camera.main.orthographicSize >= maxSize && Camera.main.orthographicSize < _normalSize;
		moveCamera ();
	}

	void zoom(){
		if (_isZoomIn) {
			zoomIn ();
		}else if(_isZoomOut){
			zoomOut ();
		}
	}
		
	public void zoomIn(){
		//現在点が最終点もっと小さい場合はズームしません
		if (_nowSize > maxSize) {
			Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, maxSize, smoothSpeed * Time.deltaTime);
		} 
	}

	public void zoomOut(){
		//現在点が普通点もっと多き場合はズームしません
		if (_nowSize < _normalSize) {
			Camera.main.orthographicSize = Mathf.MoveTowards (Camera.main.orthographicSize, _normalSize, smoothSpeed * Time.deltaTime);
		} 
	}

	void PinchZoom(){
		if (Input.touchCount == 2){
			Touch touchZero = Input.GetTouch (0);
			Touch touchOne = Input.GetTouch (1);

			//Touch Previous Position
			Vector2 touchZeroPre = touchZero.position - touchZero.deltaPosition;
			Vector2 touchOnePre = touchOne.position - touchOne.deltaPosition;

			//Magnitute of Positon Vector (distance of 2 touch point by frame)
			float touchPreMag = (touchZeroPre - touchOnePre).magnitude;
			float touchNowMag = (touchZero.position - touchOne.position).magnitude;

			//Get Differance Distance by frame
			float diffMag = touchPreMag - touchNowMag;
//			if (diffMag > 0)
//				zoomOut();
//			else
//				zoomIn();
			mytext.text = diffMag.ToString();
			Camera.main.orthographicSize += diffMag * zoomSpeed;
			Camera.main.orthographicSize = Mathf.Max (Camera.main.orthographicSize, 0.1f);
			Camera.main.orthographicSize = Mathf.Min (Camera.main.orthographicSize, _normalSize);
		}
	}

	//trigger press and hold
	public void onPointerDownRaceButton()
	{
		if (EventSystem.current.currentSelectedGameObject.name == "BtnZoomIn") {
			_isZoomIn = true;
		} else if (EventSystem.current.currentSelectedGameObject.name == "BtnZoomOut") {
			_isZoomOut = true;
		}
		isRacePressed = true;
	}

	public void onPointerUpRaceButton()
	{
		isRacePressed = false;
		_isZoomIn = false;
		_isZoomOut = false;
	}

	void moveCamera(){
	}

}
