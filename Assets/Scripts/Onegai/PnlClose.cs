using UnityEngine;
using System.Collections;

public class PnlClose : MonoBehaviour {

	public void ClosePnlOnegai() {
		GameObject pnl = GameObject.Find ("PnlOnegai");
		pnl.SetActive (false);

		GameObject player2D = GameObject.Find ("Player2D");
		PlayerEvent playerEvent = player2D.GetComponent<PlayerEvent> ();
		playerEvent.setCompAtive (true);
	}
}