using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour {
	[SerializeField] GameObject intro;

	void Update() {
		if (Input.GetKey(KeyCode.Return)) {
			SceneManager.LoadScene("Tutorial1");
		}
	}

	void OnMouseDown() {
		intro.SetActive(true);
		transform.localScale = Vector3.zero;
	}

}
