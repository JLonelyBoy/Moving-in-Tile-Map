using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneManager : MonoBehaviour {
	public static SceneManager instance; 
	static GameObject _selectedChar;
	static GameObject _selectedItem;
	static GameObject _selectedGuessChar;
	static bool _isAutoMode = true;
	static GameObject _selectedCube;
	static GenTileMap _tileScript;
	static GameObject _tileMap;
	static List<Vector2> _obsToMatrix;
	static List<GameObject> _GuessList;
//	static List<> _itemBusyCheckList;

	// Use this for initialization
	void Awake(){
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy (instance);
		}
		initScene ();
	}

	void initScene(){
		if(_selectedCube == null) _selectedCube = GameObject.Find ("SelectedInspector");
		if(_tileMap == null) _tileMap = GameObject.Find ("TileMap");
		if(_tileScript == null) _tileScript = _tileMap.GetComponent<GenTileMap> ();
		//他おキャラ一覧
		if (_GuessList == null) {
			_GuessList = new List<GameObject> ();
			foreach(Transform child in _tileMap.transform){
				if (child.tag == "Player") {
					if (!child.GetComponent<MyCharacter> ().getIsMyChar ()) {
						_GuessList.Add (child.gameObject);
					}
				}
			}
//			Debug.Log ("My Character List " + _GuessList.Count);
		}
	}
	
	// Update is called once per frame
	void Update () {
		initScene ();
	}

	//Autoモードを設定します 
	public static void setAutoMode(){
		if (!_isAutoMode) {
			_selectedCube.SetActive (false);
			setSelectedChar (null);
			_isAutoMode = true;
			Debug.Log (_isAutoMode);
		} else {
			_selectedCube.SetActive (true);
			_isAutoMode = false;
		}
	}
	public static bool getAutoMode(){
		return _isAutoMode;
	}

	public static GameObject getSelectingChar(){
		return _selectedChar;
	}

	public static GameObject getSelectingGuess(){
		return _selectedGuessChar;
	}

	public static GameObject getTileMap(){
		return _tileMap;
	}

	public static GenTileMap getTileScript(){
		return _tileScript;
	}

	public static List<Vector2> getObstacleList(){
		if (_obsToMatrix == null || _obsToMatrix.Count == 0) {
			instance.transferObsToMatrix ();
		}
		return _obsToMatrix;
	}

	public static GameObject getSelectionCube(){
		return _selectedCube;
	}

	public static List<GameObject> getGuessList(){
		return _GuessList;
	}

	/* 
	 * Delegateでキャラを選択します
	*/
	public static void setSelectedChar(GameObject character){
		if (!_isAutoMode) {
			if (_selectedChar == character && _selectedChar!= null) {
				_selectedChar.GetComponent<MyCharacter> ().selectChar ();
				_selectedChar = null;
				return;
			}
			if (_selectedChar != null)
				_selectedChar.GetComponent<MyCharacter> ().selectChar ();
			if (character != null)
				character.GetComponent<MyCharacter> ().selectChar ();
			_selectedChar = character;
//			Debug.Log (_selectedChar.name);
		}
	}

	public static void setSelectedGuessChar(GameObject guessCharacter){
		if (!_isAutoMode) {
			
		}
	}
		
	 /*もしキャラまだ選択してない場合は return
	  * キャラ選択された場合あキャラがコマンド表示されます 
	  * */
	public static void setSelectItem(GameObject item){
		if (!_isAutoMode) {
			if (_selectedChar == null)
			//we will add some process if have any requirement here
				return;
			if (_selectedItem != null)
				_selectedItem.GetComponent<MyItem> ().selectItem ();
			if (item != null)
				item.GetComponent<MyItem> ().selectItem (); 		
			_selectedItem = item;
		}
	}

	//ObstacleをMatrix化処理
	void transferObsToMatrix(){
		_obsToMatrix = new List<Vector2> ();
		Transform obstacle = _tileMap.transform.Find ("Item");
		foreach(Transform child in obstacle){
//			if (child.name == "SelectedInspector" || child.tag == "Player")
//				continue;
			float x = child.position.x - _tileScript.tileSize / 2;
			float z = child.position.z + _tileScript.tileSize / 2;
			//			Debug.Log("Position x: "+ x +" pos z: " + z);
			Vector2 pos = new Vector2 (x, z);
//			Debug.Log ("obstacle pos: "+pos + "transform name " + child.name);
			_obsToMatrix.Add (pos);
		}
	}
}
