using UnityEngine;

public class Mushroom : MonoBehaviour {
	[SerializeField] float power;
	Animator anim;

	// Start is called before the first frame update
	void Start() {
		anim = GetComponentInChildren<Animator>();
		anim.Play("idle");
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
			otherRb.AddForce(Vector2.up * power * otherRb.mass, ForceMode2D.Impulse);
			anim.Play("boing");
		}
	}
}
