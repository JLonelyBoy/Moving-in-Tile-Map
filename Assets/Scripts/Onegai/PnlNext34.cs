using UnityEngine;
using System.Collections;

public class PnlNext34 : MonoBehaviour {

	public void Next34() {
		GameObject pnl = GameObject.Find ("PnlOnegaiComp");
		Animator anim = pnl.GetComponent<Animator> ();
		anim.SetTrigger ("next34");
	}
}
