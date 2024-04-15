using UnityEngine;

public class Gate : MonoBehaviour {
	[SerializeField] float openSpeed = 1.5f;
	BoxCollider2D col;
	SpriteRenderer spr;
	Animator anim;
	Trigger trigger;
	bool active = false;
	Vector3 startPos;


	// Start is called before the first frame update
	void Start() {
		col = GetComponent<BoxCollider2D>();
		spr = GetComponentInChildren<SpriteRenderer>();
		trigger = GetComponent<Trigger>();
		anim = GetComponentInChildren<Animator>();
		anim.Play("off");
		startPos = transform.position;
	}

	// Update is called once per frame
	void Update() {
		var dt = Time.deltaTime;
		var triggered = trigger.IsTriggered();
		if (triggered != active) {
			active = triggered;
			if (active) {
				anim.Play("on");
			} else {
				anim.Play("off");
			}
		}

		if (active) {
			if (transform.position.y < startPos.y + col.bounds.size.y) {
				transform.position += Vector3.up * openSpeed * dt;
			}
		}

		if (!active) {
			if (transform.position.y > startPos.y) {
				transform.position += Vector3.down * openSpeed * dt;
			}
		}
	}
}
