using UnityEngine;
using System.Collections;

public class PnlNext12 : MonoBehaviour {

	public void Next12() {
		GameObject pnl = GameObject.Find ("PnlOnegaiComp");
		Animator anim = pnl.GetComponent<Animator> ();
		anim.SetTrigger ("next12");
	}
}
