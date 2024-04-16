using System.Linq;
using UnityEngine;

public class Portal : MonoBehaviour {
	public Collider2D col;

	[SerializeField] Portal nextPortal;
	[SerializeField] bool open = false;

	AudioSource portalSnd;
	AudioSource thruSnd;
	Animator anim;
	Trigger trigger;

	// Start is called before the first frame update
	void Start() {
		trigger = GetComponent<Trigger>();
		anim = GetComponentInChildren<Animator>();
		col = GetComponent<Collider2D>();
		portalSnd = GetComponent<AudioSource>();
		thruSnd = GetComponentsInChildren<AudioSource>().First(c => c.name == "ThruSound");

		if (!open) {
			anim.Play("closed");
		}
	}

	public void Open() {
		if (!open) {
			anim.Play("opening");
			portalSnd.Play(0);
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
			thruSnd.Play(0);
			var other = col.gameObject;
			var offset = (col.bounds.center - other.transform.position);
			// offset.y -= 0.32f; // extra vertical padding just in case
			var newPosition = nextPortal.col.bounds.center - offset;
			newPosition.z = other.transform.position.z;
			other.transform.SetPositionAndRotation(newPosition, Quaternion.identity);
		}
	}
}
