using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D collision) {
		var other = collision.gameObject;
		var player = collision.gameObject.GetComponent<PlayerCtrl>();
		var necro = collision.gameObject.GetComponent<Necromancer>();

		if (player != null) {
			player.Kill();
		}

		if (necro != null) {
			necro.Kill();
		}
	}
}
