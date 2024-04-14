using UnityEngine;

public class Portal : MonoBehaviour {
	[SerializeField] Portal nextPortal;
	[SerializeField] bool open = false;
	Animator anim;
	Trigger trigger;

	// Start is called before the first frame update
	void Start() {
		trigger = GetComponent<Trigger>();
		anim = GetComponentInChildren<Animator>();
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
}
