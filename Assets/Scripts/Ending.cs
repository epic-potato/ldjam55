using UnityEngine;
using UnityEngine.SceneManagement;

public class Ending : MonoBehaviour {
	[SerializeField] bool isGood;

	void OnTriggerEnter2D(Collider2D other) {
		var necro = other.GetComponent<Necromancer>();
		if (necro != null) {
			if (isGood) {
				SceneManager.LoadScene("GoodEnding");
			}

			if (!isGood) {
				SceneManager.LoadScene("BadEnding");
			}
		}
	}
}
