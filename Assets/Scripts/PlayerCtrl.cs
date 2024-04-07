using UnityEngine;

public class PlayerCtrl : MonoBehaviour {
	[SerializeField] float speed;
	Transform xform;

	// Start is called before the first frame update
	void Start() {
		xform = this.transform;
	}

	// Update is called once per frame
	void Update() {
		var dt = Time.deltaTime;
		var moveVec = new Vector3(Input.GetAxis("horizontal"), Input.GetAxis("vertical"), 0);

		xform.Translate(moveVec * speed * dt);
	}
}
