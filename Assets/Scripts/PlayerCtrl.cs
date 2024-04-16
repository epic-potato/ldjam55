using UnityEngine;

enum PlayerState {
	Idle,
	Barking,
	Walking,
	Running,
	Jumping,
	Slink,
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
	public bool hasKey = false;

	[SerializeField] float speed = 5;
	[SerializeField] float runSpeed = 8f;
	[SerializeField] float accel = 0.8f;
	[SerializeField] float airAccel = 0.1f;
	[SerializeField] float jumpForce = 2;
	[SerializeField] GameObject slinky;

	Necromancer necro;
	PlayerState state = PlayerState.Jumping; // this is a hack so we can transition to Idle immediately
	Transform xform;
	bool grounded = false;
	bool running = false;
	BoxCollider2D col;
	Rigidbody2D rb;
	Facing facing = Facing.Right;
	Animator anim;
	Wait wait;
	Vector2 externalVelocity = Vector2.zero;
	AudioSource bark;

	// collectible state
	int bones = 0;
	bool hasThroneKey = false;

	// Start is called before the first frame update
	void Start() {
		xform = this.transform;
		col = GetComponent<BoxCollider2D>();
		rb = GetComponent<Rigidbody2D>();
		anim = GetComponentInChildren<Animator>();
		necro = GameObject.Find("Necromancer").GetComponent<Necromancer>();
		SetState(PlayerState.Idle);
		bark = GetComponent<AudioSource>();
	}

	public Collider2D GetCollider() {
		return col;
	}

	RaycastHit2D? GetHit(Vector2 moveVec) {
		var hits = Physics2D.BoxCastAll(col.bounds.center, col.size, 0, moveVec.normalized, moveVec.magnitude);
		foreach (var hit in hits) {
			if (hit.collider != col && !hit.collider.isTrigger) {
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

	public void GetCollectible(CollectibleKind kind) {
		switch (kind) {
			case CollectibleKind.Bone:
				bones += 1;
				break;
			case CollectibleKind.Key:
				hasKey = true;
				break;
			case CollectibleKind.ThroneKey:
				hasThroneKey = true;
				break;
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
				anim.speed = 1.5f;
				anim.Play("bark");
				bark.Play(0);
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

	void SetExternalVelocity(Vector2 vel) {
		if (vel.y > 0) {
			vel.y = 0;
		}

		externalVelocity = vel;
	}

	void Slink() {
		var dir = facing.GetDir();
		var slinkyIns = Instantiate(slinky, xform.position + dir * -0.12f, Quaternion.identity);

		var slink = slinkyIns.GetComponent<Slinky>();
		slink.segments = bones;
		slink.dir = facing.GetDir();

		col.enabled = false;
		rb.isKinematic = true;

		foreach (Transform child in transform) {
			child.gameObject.SetActive(false);
		}

		slinkyIns.transform.SetParent(xform);
		SetState(PlayerState.Slink);
	}

	void Unslink() {
		var slinky = GetComponentInChildren<Slinky>();
		Destroy(slinky.gameObject);
		col.enabled = true;
		rb.isKinematic = false;

		foreach (Transform child in transform) {
			child.gameObject.SetActive(true);
		}
		SetState(PlayerState.Idle);
	}

	void Update() {
		// this has to happen first because _reasons_
		if (state == PlayerState.Slink) {
			if (Input.GetKeyUp("l")) {
				Unslink();
			} else {
				return;
			}
		}


		var dt = Time.deltaTime;
		var newState = PlayerState.Idle;
		running = Input.GetKey(KeyCode.LeftShift);

		var spd = running ? runSpeed : speed;
		var ground = GetHit(Vector2.down * 0.1f);
		grounded = ground.HasValue;
		if (ground != null) {
			SetExternalVelocity(ground.Value.rigidbody.velocity);
		} else {
			SetExternalVelocity(Vector2.zero);
		}

		if (grounded && Input.GetKeyDown("j")) {
			rb.velocity = Vector2.zero;
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

		if (Input.GetKeyDown(KeyCode.Space) && grounded) {
			rb.AddForceY(jumpForce, ForceMode2D.Impulse);
			grounded = false;
		}

		if (Input.GetKeyDown("l") && grounded) {
			rb.velocity = Vector2.zero;
			Slink();
			return;
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
		rb.velocity += externalVelocity;
	}

	public void Kill() {
		xform.position = necro.transform.position;
	}
}
