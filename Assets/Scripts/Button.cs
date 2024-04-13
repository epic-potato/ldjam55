using UnityEngine;

public class Button : MonoBehaviour {
	[SerializeField] Trigger trigger;
	public bool Pressed = false;
	BoxCollider2D col;

	void Start() {
		col = GetComponent<BoxCollider2D>();
	}

	bool IsPressed() {
		var hits = Physics2D.BoxCastAll(col.bounds.center + (Vector3.up * col.size.y), col.size, 0, Vector2.up, col.size.y);
		foreach (var hit in hits) {
			if (hit.collider != col) {
				return true;
			}
		}

		return false;
	}

	void Update() {
		if (IsPressed() != Pressed) {
			Pressed = IsPressed();
			trigger.DoTrigger(transform.gameObject, Pressed);
		}
	}
}
