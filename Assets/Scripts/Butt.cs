using UnityEngine;

public class Butt : MonoBehaviour {
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.GetComponent<Necromancer>() != null) {
			if (Mathf.Abs(collision.contacts[0].normal.y) < 0.5) {
				collision.gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 80, ForceMode2D.Impulse);
			}
		}
	}
}
