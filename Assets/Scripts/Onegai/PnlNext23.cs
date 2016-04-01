using UnityEngine;
using System.Collections;

public class PnlNext23 : MonoBehaviour {

	public void Next23() {
		GameObject pnl = GameObject.Find ("PnlOnegaiComp");
		Animator anim = pnl.GetComponent<Animator> ();
		anim.SetTrigger ("next23");
	}
}
