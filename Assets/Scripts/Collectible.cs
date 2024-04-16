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
	AudioSource audioSource;

	void Awake() {
		audioSource = GetComponent<AudioSource>();
		audioSource.Play(0);
		audioSource.Pause();
	}

	void Start() {
		player = GameObject.Find("Player").GetComponent<PlayerCtrl>();
		col = GetComponent<CapsuleCollider2D>();
	}

	// Update is called once per frame
	void Update() {
		var collider = Physics2D.OverlapCapsule(col.bounds.center, col.size, col.direction, 0);
		if (col.enabled && collider.gameObject.transform.root.gameObject == player.gameObject) {
			player.GetCollectible(kind);
			audioSource.UnPause();
			col.enabled = false;
			transform.localScale = Vector3.zero;
		}

		if (col.enabled == false && !audioSource.isPlaying) {
			Destroy(gameObject);
		}
	}
}
