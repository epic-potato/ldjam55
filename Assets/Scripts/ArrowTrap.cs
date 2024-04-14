using UnityEngine;

public class ArrowTrap : MonoBehaviour {
	[SerializeField] Facing facing;
	[SerializeField] GameObject arrowPrefab;
	[SerializeField] float speed;

	Trigger trigger;
	Transform xform;
	Wait wait = new Wait(0);

	void Start() {
		xform = transform;
		trigger = GetComponent<Trigger>();

		xform.localRotation = facing.GetRotation();
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
		}
	}
}
