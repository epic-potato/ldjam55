using UnityEngine;

enum NecroState {
	Idle,
	Walking,
}

public class Necromancer : MonoBehaviour {
	[SerializeField] float speed = 1;
	NecroState state = NecroState.Idle;
	Vector2 target;
	Transform xform;
	Rigidbody2D rb;

	void Start() {
		xform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (state == NecroState.Idle) {
			return;
		}

		var distance = target.x - xform.position.x;
		if (Mathf.Abs(distance) < 0.02) {
			state = NecroState.Idle;
			xform.position = new Vector3(target.x, xform.position.y, xform.position.z);
		}

		rb.velocityX = Mathf.Sign(distance) * speed;

		if (rb.velocityX < 0) {
			xform.localScale = new Vector3(-1, 1, 1);
		}

		if (rb.velocityX > 0) {
			xform.localScale = new Vector3(1, 1, 1);
		}
	}

	public void Signal(Vector2 fromPos) {
		switch (state) {
			case NecroState.Idle:
				state = NecroState.Walking;
				break;
			case NecroState.Walking:
				state = NecroState.Idle;
				break;
		}

		target = fromPos;
	}
}
