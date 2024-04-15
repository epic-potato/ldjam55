using UnityEngine;

public class Switch : MonoBehaviour {
	[SerializeField] Trigger trigger;
	public bool active = false;
	GameObject player;
	Animator anim;
	bool ready = false;

	// Start is called before the first frame update
	void Start() {
		anim = GetComponentInChildren<Animator>();
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update() {
		if (ready && Input.GetKeyDown("k")) {
			SetActive(!active);
		}
	}

	void SetActive(bool act) {
		active = act;
		trigger.DoTrigger(gameObject, active);
		if (active) {
			anim.Play("on");
		} else {
			anim.Play("off");
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject == player) {
			ready = true;
		}
	}

	void OnTriggerLeave2D(Collider2D other) {
		if (other.gameObject == player) {
			ready = false;
		}
	}
}
