using UnityEngine;

enum Facing {
	Left,
	Right,
}

enum PlayerState {
	Idle,
	Barking,
	Walking,
	Running,
	Jumping,
}

struct Wait {
	float elapsed;
	float duration;

	public Wait(float dur) {
		elapsed = 0;
		duration = dur;
	}

	public bool CheckFinished() {
		elapsed += Time.deltaTime;
		return elapsed >= duration;
	}
}


public class PlayerCtrl : MonoBehaviour {
	[SerializeField] float speed = 5;
	[SerializeField] float runSpeed = 8f;
	[SerializeField] float accel = 0.8f;
	[SerializeField] float airAccel = 0.1f;
	[SerializeField] float jumpForce = 2;
	Necromancer necro;
	PlayerState state = PlayerState.Idle;
	Transform xform;
	bool grounded = false;
	bool running = false;
	BoxCollider2D col;
	Rigidbody2D rb;
	Facing facing = Facing.Right;
	Animator anim;
	Wait wait;

	// Start is called before the first frame update
	void Start() {
		xform = this.transform;
		col = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		necro = GameObject.Find("Necromancer").GetComponent<Necromancer>();
	}

	RaycastHit2D? GetHit(Vector2 moveVec) {
		var hits = Physics2D.BoxCastAll(col.bounds.center, col.size, 0, moveVec.normalized, moveVec.magnitude);
		foreach (var hit in hits) {
			if (hit.collider != col) {
				return hit;
			}
		}

		return null;
	}

	bool WouldCollide(Vector2 moveVec) {
		return GetHit(moveVec) != null;
	}


	void MoveAndSlide(Vector2 move) {
		var dt = Time.deltaTime;
		var moveVec = move * dt;

		var normalized = moveVec.normalized;
		var hit = GetHit(moveVec);
		if (hit.HasValue) {
			moveVec -= (hit.Value.normal * hit.Value.centroid);
		}
	}

	void SetState(PlayerState st) {
		if (st == state) {
			return;
		}

		state = st;
		switch (state) {
			case PlayerState.Idle:
				anim.speed = 1;
				anim.Play("idle_paw");
				break;
			case PlayerState.Jumping:
				anim.speed = 1;
				anim.Play("run");
				break;
			case PlayerState.Barking:
				anim.speed = 1;
				anim.Play("bark");
				break;
			case PlayerState.Running:
				anim.speed = 3;
				anim.Play("run");
				break;
			case PlayerState.Walking:
				anim.speed = 3;
				anim.Play("run");
				break;
		}
	}

	void Bark() {
		wait = new Wait(1);
		SetState(PlayerState.Barking);
		necro.Signal(xform.position);
		return;
	}

	void Update() {
		var dt = Time.deltaTime;
		var newState = PlayerState.Idle;
		running = Input.GetKey(KeyCode.LeftShift);

		var spd = running ? runSpeed : speed;
		grounded = WouldCollide(Vector2.down * 0.1f);

		if (grounded && Input.GetKeyDown("j")) {
			Bark();
			return;
		}

		if (state == PlayerState.Barking) {
			if (wait.CheckFinished()) {
				newState = PlayerState.Idle;
			} else {
				return;
			}
		}


		if (Input.GetKeyDown("w") && grounded) {
			rb.AddForceY(jumpForce, ForceMode2D.Impulse);
			grounded = false;
		}

		var currAccel = grounded ? accel : airAccel;
		var horizontalInput = Input.GetAxis("horizontal");
		if (horizontalInput == 0) {
			if (grounded) {
				rb.velocityX *= currAccel;
			}
		}

		rb.velocityX = Input.GetAxis("horizontal") * spd;
		if (Input.GetKeyUp("w") && rb.velocityY > 0) {
			rb.velocityY /= 2;
		}

		if (rb.velocityX > 0) {
			facing = Facing.Right;
		}

		if (rb.velocityX < 0) {
			facing = Facing.Left;
		}

		xform.localScale = new Vector3(facing == Facing.Right ? 1 : -1, 1, 1);

		if (Mathf.Abs(rb.velocityX) > 0) {
			newState = PlayerState.Walking;
			if (running) {
				newState = PlayerState.Running;
			}
		}

		if (!grounded) {
			newState = PlayerState.Jumping;
		}

		SetState(newState);
	}
}
