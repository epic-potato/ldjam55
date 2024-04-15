using System.Collections.Generic;
using UnityEngine;


public class Slinky : MonoBehaviour {
	[SerializeField] GameObject spinePrefab;
	public Vector3 dir = Vector3.right;
	public int segments = 5;
	GameObject butt;
	GameObject head;
	List<GameObject> spine;
	Wait wait;
	Vector3 magicOffset = new Vector3(0.17f, -0.04f, 0);

	void Start() {
		spine = new List<GameObject>();
		wait = new Wait(0);

		foreach (Transform child in transform) {
			if (child.gameObject.name == "Dog_Butt") {
				butt = child.gameObject;
			}

			if (child.gameObject.name == "Dog_head_and_body") {
				head = child.gameObject;
			}
		}
	}

	void Update() {
		if (!wait.CheckFinished()) {
			return;
		}

		if (dir.x < 0) {
			transform.localScale = new Vector3(1, 1, 1);
			// butt.transform.localScale = new Vector3(-1, 1, 1);
			// head.transform.localScale = new Vector3(-1, 1, 1);
		}

		if (dir.x > 0) {
			transform.localScale = new Vector3(1, 1, 1);
			// butt.transform.localScale = new Vector3(1, 1, 1);
			// head.transform.localScale = new Vector3(1, 1, 1);
		}

		if (spine.Count < segments) {
			var correctedMagicOffset = magicOffset;
			correctedMagicOffset.x *= dir.x;
			var offset = correctedMagicOffset + dir * (spine.Count * 0.16f);
			var bone = Instantiate(spinePrefab, transform.position + offset, Quaternion.identity);
			bone.transform.parent = gameObject.transform;
			spine.Add(bone);
			offset.y = 0;
			head.transform.position += dir * 0.16f;
		}

		wait = new Wait(0.05f);
	}
}
