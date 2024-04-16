using UnityEngine;

public class ArrowTrap : MonoBehaviour {
	[SerializeField] Facing facing;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] float speed;
	[SerializeField] float interval = 0;
	[SerializeField] float initialDelay = 0;

	Trigger trigger;
	Transform xform;
	Wait wait;

	void Start() {
		xform = transform;
		trigger = GetComponent<Trigger>();

		xform.localRotation = facing.GetRotation();
		wait = new Wait(initialDelay);
	}

	void Fire() {
		var arrowIns = Instantiate(arrowPrefab, xform.position, xform.rotation);
		var arrow = arrowIns.GetComponent<Arrow>();
		arrow.speed = speed;
	}

	void Update() {
		if (wait.CheckFinished()) {
			if (trigger.IsTriggered()) {
				Fire();
				wait = new Wait(.5f);
			}

			if (interval > 0) {
				Fire();
				wait = new Wait(interval);
			}
		}
	}
}
