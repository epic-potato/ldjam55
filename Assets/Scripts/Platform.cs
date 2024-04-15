using UnityEngine;

public class Platform : MonoBehaviour {
	[SerializeField] Facing dir;
	[SerializeField] float speed;
	Trigger trigger;

	BoxCollider2D col;
	Rigidbody2D rb;
	Wait wait;

	void Start() {
		col = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		trigger = GetComponent<Trigger>();
		wait = new Wait(0);
	}

	void Update() {
		if (!wait.CheckFinished()) {
			return;
		}

		if (trigger != null && !trigger.IsTriggered()) {
			return;
		}

		rb.velocity = dir.GetDir() * speed;
	}

	void OnCollisionEnter2D(Collision2D collision) {
		var other = collision.gameObject;
		foreach (var contact in collision.contacts) {
			var otherRb = other.GetComponent<Rigidbody2D>();

			if (wait.CheckFinished() && otherRb.bodyType == RigidbodyType2D.Static) {
				rb.velocity = Vector2.zero;
				wait = new Wait(2);
				dir = dir.Flip();
			}
		}
	}
}
