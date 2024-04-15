using UnityEngine;

public class Portal : MonoBehaviour {
	public Collider2D collider;

	[SerializeField] Portal nextPortal;
	[SerializeField] bool open = false;

	Animator anim;
	Trigger trigger;

	// Start is called before the first frame update
	void Start() {
		trigger = GetComponent<Trigger>();
		anim = GetComponentInChildren<Animator>();
		collider = GetComponent<Collider2D>();
		if (!open) {
			anim.Play("closed");
		}
	}

	public void Open() {
		if (!open) {
			anim.Play("opening");
			open = true;

			nextPortal?.Open();
		}
	}

	// Update is called once per frame
	void Update() {
		if (trigger.IsTriggered()) {
			Open();
		}

		var animState = anim.GetCurrentAnimatorStateInfo(0);
		if (animState.IsName("opening")) {
			if (animState.normalizedTime > 1) {
				anim.Play("open");
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (open && nextPortal != null) {
			var other = col.gameObject;
			var offset = (col.bounds.center - other.transform.position);
			// offset.y -= 0.32f; // extra vertical padding just in case
			var newPosition = nextPortal.collider.bounds.center - offset;
			other.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
		}
	}
}
