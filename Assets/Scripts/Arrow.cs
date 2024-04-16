using UnityEngine;

public class Arrow : MonoBehaviour {
	public float speed;

	Transform xform;
	Rigidbody2D rb;
	Collider2D col;
	AudioSource sound;

	// Start is called before the first frame update
	void Start() {
		xform = transform;
		rb = GetComponent<Rigidbody2D>();
		col = GetComponent<Collider2D>();
		sound = GetComponent<AudioSource>();
		rb.velocity = xform.localRotation * Vector2.right * speed;
	}

	void Update() {
		if (col.enabled == false && !sound.isPlaying) {
			Destroy(gameObject);
		}
	}

	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D collision) {
		var other = collision.gameObject;
		var player = collision.gameObject.GetComponent<PlayerCtrl>();
		var necro = collision.gameObject.GetComponent<Necromancer>();

		if (player != null) {
			player.Kill();
		}

		if (necro != null) {
			necro.Kill();
		}

		transform.localScale = Vector3.zero;
		col.enabled = false;
	}
}
