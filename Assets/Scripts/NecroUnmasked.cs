using UnityEngine;

public class NecroUnmasked : MonoBehaviour {
	Animator anim;
	// Start is called before the first frame update
	void Start() {
		anim = GetComponent<Animator>();
		anim.speed = 0.1f;
	}
}
