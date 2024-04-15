using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
	[SerializeField] string nextScene;
	[SerializeField] bool locked = false;
	Animator anim;
	GameObject necromancer;
	PlayerCtrl player;
	bool playerNear = false;
	bool necroNear = false;

	void Start() {
		necromancer = GameObject.Find("Necromancer");
		player = GameObject.FindFirstObjectByType<PlayerCtrl>();
		anim = GetComponentInChildren<Animator>();
		if (locked) {
			anim.Play("locked");
		} else {
			anim.Play("unlocked");
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject == player.gameObject) {
			playerNear = true;
		}

		if (collider.gameObject == necromancer) {
			necroNear = true;
		}

		if (!locked && necroNear) {
			SceneManager.LoadScene(nextScene);
		}

		if (locked && necroNear && playerNear && player.hasKey) {
			SceneManager.LoadScene(nextScene);
		}
	}

	void OnTriggerExit2D(Collider2D collider) {
		if (collider.gameObject == player.gameObject) {
			playerNear = false;
		}

		if (collider.gameObject == necromancer) {
			necroNear = false;
		}
	}
}
