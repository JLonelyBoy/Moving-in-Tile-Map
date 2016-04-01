using UnityEngine;
using System.Collections;

public class PnlCompClose : MonoBehaviour {

	public void ClosePnlOnegaiComp() {
		GameObject pnl = GameObject.Find ("PnlOnegaiComp");
		pnl.SetActive (false);
	}
}
