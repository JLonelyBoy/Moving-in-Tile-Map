using UnityEngine;
using System.Collections;

public abstract class BaseItem : MonoBehaviour {
	public string commentString = "Hello";
	// Use this for initialization
	public virtual void Start () {
	
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}

	//Call went Select object
	public Delegate.VoidDelegate selectItem {
		get;
		set;
	}
}
