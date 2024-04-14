using UnityEngine;

public enum CollectibleKind {
	Bone,
	Key,
	ThroneKey,
}

public class Collectible : MonoBehaviour {
	[SerializeField] CollectibleKind kind = CollectibleKind.Bone;
	PlayerCtrl player;
	CapsuleCollider2D col;

	void Start() {
		player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
		col = GetComponent<CapsuleCollider2D>();
	}

	// Update is called once per frame
	void Update() {
		var collider = Physics2D.OverlapCapsule(col.bounds.center, col.size, col.direction, 0);
		if (collider == player.GetCollider()) {
			player.GetCollectible(kind);
			Destroy(gameObject);
		}
	}
}
