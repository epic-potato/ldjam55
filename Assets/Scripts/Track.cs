using UnityEngine;

public class Track : MonoBehaviour {
	AudioSource audioSource;

	private void Awake() {
		DontDestroyOnLoad(transform.gameObject);
		audioSource = GetComponent<AudioSource>();
	}
}
