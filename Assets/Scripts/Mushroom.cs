using UnityEngine;

public class Mushroom : MonoBehaviour {
	[SerializeField] float power;
	Animator anim;
	AudioSource sound;

	// Start is called before the first frame update
	void Start() {
		anim = GetComponentInChildren<Animator>();
		anim.Play("idle");
		sound = GetComponent<AudioSource>();
	}

	// Update is called once per frame
	void Update() {
		var animState = anim.GetCurrentAnimatorStateInfo(0);
		if (animState.IsName("boing")) {
			if (animState.normalizedTime > 1) {
				anim.Play("idle");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		var otherRb = other.GetComponent<Rigidbody2D>();
		if (otherRb != null) {
			sound.Play(0);
			otherRb.AddForce(Vector2.up * power * otherRb.mass, ForceMode2D.Impulse);
			anim.Play("boing");
		}
	}
}
