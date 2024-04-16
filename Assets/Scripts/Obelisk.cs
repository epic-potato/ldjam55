using UnityEngine;

public class Obelisk : MonoBehaviour {
	[SerializeField] Speech speech;
	[SerializeField] GameObject choose;
	Animator anim;


	void Start() {
		anim = GetComponentInChildren<Animator>();
		anim.Play("dormant");
		choose.SetActive(false);
	}

	void Activate() {
		anim.Play("glowing");
		speech.gameObject.SetActive(true);
	}

	void OnTriggerEnter2D(Collider2D other) {
		var necro = other.GetComponent<Necromancer>();
		if (necro != null) {
			Activate();
		}
	}
}
