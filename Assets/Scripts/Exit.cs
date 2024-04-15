using UnityEngine;
using UnityEngine.SceneManagement;

public class Exit : MonoBehaviour {
	[SerializeField] string nextScene;
	[SerializeField] bool locked = false;
	GameObject necromancer;
	PlayerCtrl player;

	void Start() {
		necromancer = GameObject.Find("Necromancer");
		player = GameObject.FindFirstObjectByType<PlayerCtrl>();
	}

	void OnTriggerEnter2D(Collider2D collider) {
		if (collider.gameObject == necromancer) {
			if (player.hasKey) {
				SceneManager.LoadScene(nextScene);
			}
		}
	}
}
