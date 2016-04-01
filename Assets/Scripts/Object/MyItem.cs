using UnityEngine;
using System.Collections;

public class MyItem : BaseItem {
	bool isSelected = false;
	public float waitTime = 2.0f;
	bool isReactReg = false;

	// Use this for initialization
	public override void Start () {
		init ();	
	}
	
	// Update is called once per frame
	public override void Update () {
		if (transform.FindChild("Ball/Comment").gameObject.activeSelf) StartCoroutine(delCommentAfterTime(waitTime));
	}

	void init(){
		selectItem = furnitureSelect;
	}

	//家具を選択します
	void furnitureSelect(){
		isSelected = true;
		transform.FindChild ("Ball/Comment").gameObject.SetActive (true);
		TextMesh cmt = transform.FindChild ("Ball/Comment/CmtText").gameObject.GetComponent<TextMesh>();
		cmt.text = commentString;
		selectItem = LeaveSelect;
	}

	//家具を外します 
	void LeaveSelect(){
		isSelected = false;
		transform.FindChild ("Ball/Comment").gameObject.SetActive (false);
		selectItem = furnitureSelect;
	}

	//コマンド表示されたから何分ぐらい後消します
	IEnumerator delCommentAfterTime(float waitTime)
	{
		yield return new WaitForSeconds(2f);
		transform.FindChild ("Ball/Comment").gameObject.SetActive (false);
	}

	void OnMouseDown(){
		SceneManager.setSelectItem (gameObject);
	}

	void OnCollisionEnter (Collision col)
	{
		if(col.gameObject.tag == "Player")
		{
			Debug.Log ("is colliding ");
			Destroy(col.gameObject);
		}
	}






}
