using UnityEngine;

public class DeadDog : MonoBehaviour {
	Animator anim;
	// Start is called before the first frame update
	void Start() {
		anim = GetComponent<Animator>();
		anim.speed = 0.7f;

	}

}
