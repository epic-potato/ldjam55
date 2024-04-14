using UnityEngine;

public class Arrow : MonoBehaviour {
	public float speed;

	Transform xform;
	Rigidbody2D rb;

	// Start is called before the first frame update
	void Start() {
		xform = transform;
		rb = GetComponent<Rigidbody2D>();
		rb.velocity = xform.localRotation * Vector2.right * speed;
	}

	// Update is called once per frame
	void OnCollisionEnter2D(Collision2D collision) {
		Destroy(gameObject);
	}
}
