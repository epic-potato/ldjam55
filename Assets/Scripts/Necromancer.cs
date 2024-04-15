using UnityEngine;

enum NecroState {
	Idle,
	Walking,
}

public class Necromancer : MonoBehaviour {
	Vector2 externalVelocity = Vector2.zero;
	[SerializeField] float speed = 1;
	NecroState state = NecroState.Walking;
	CapsuleCollider2D col;
	Vector2 target;
	Transform xform;
	Rigidbody2D rb;
	Animator anim;

	void Start() {
		xform = GetComponent<Transform>();
		col = GetComponent<CapsuleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();

		SetState(NecroState.Idle);
	}

	RaycastHit2D? GetHit(Vector2 moveVec) {
		var hits = Physics2D.CapsuleCastAll(col.bounds.center, col.size, col.direction, 0, moveVec.normalized, moveVec.magnitude);
		foreach (var hit in hits) {
			if (hit.collider != col && !hit.collider.isTrigger) {
				return hit;
			}
		}

		return null;
	}

	void SetState(NecroState newState) {
		if (newState == state) {
			return;
		}

		switch (newState) {
			case NecroState.Idle:
				anim.speed = 0.5f;
				anim.Play("idle");
				break;
			case NecroState.Walking:
				anim.speed = 1;
				anim.Play("walk");
				break;
		}

		state = newState;
	}

	void SetExternalVelocity(Vector2 vel) {
		if (vel.y > 0) {
			vel.y = 0;
		}

		externalVelocity = vel;
	}

	void Update() {
		if (state == NecroState.Idle) {
			rb.velocity += externalVelocity;
			return;
		}

		var distance = target.x - xform.position.x;
		if (Mathf.Abs(distance) < 0.02) {
			SetState(NecroState.Idle);
			xform.position = new Vector3(target.x, xform.position.y, xform.position.z);
		}

		rb.velocityX = Mathf.Sign(distance) * speed;

		if (rb.velocityX < 0) {
			xform.localScale = new Vector3(-1, 1, 1);
		}

		if (rb.velocityX > 0) {
			xform.localScale = new Vector3(1, 1, 1);
		}

		var ground = GetHit(Vector2.down * 0.1f);
		if (ground.HasValue) {
			SetExternalVelocity(ground.Value.rigidbody.velocity);
		} else {
			SetExternalVelocity(Vector2.zero);
		}

		rb.velocity += externalVelocity;
	}

	public void Signal(Vector2 fromPos) {
		switch (state) {
			case NecroState.Idle:
				SetState(NecroState.Walking);
				break;
			case NecroState.Walking:
				SetState(NecroState.Idle);
				break;
		}

		target = fromPos;
	}
}
