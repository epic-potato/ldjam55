using UnityEngine;

public class Gate : MonoBehaviour {
	BoxCollider2D col;
	SpriteRenderer spr;
	Trigger trigger;


	// Start is called before the first frame update
	void Start() {
		col = GetComponent<BoxCollider2D>();
		spr = GetComponentInChildren<SpriteRenderer>();
		trigger = GetComponent<Trigger>();
	}

	// Update is called once per frame
	void Update() {
		var triggered = trigger.IsTriggered();
		col.enabled = !triggered;
		spr.enabled = !triggered;
	}
}
