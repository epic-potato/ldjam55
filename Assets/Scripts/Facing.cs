using UnityEngine;

public enum Facing {
	Left,
	Right,
	Up,
	Down,
}

public static class FacingMethods {
	public static Vector3 GetScale(this Facing facing) {
		switch (facing) {
			case Facing.Left:
				return new Vector3(-1, 0, 0);
			default:
				return new Vector3(1, 0, 0);
		}
	}

	public static Quaternion GetRotation(this Facing facing) {
		// NOTE (soggy): none of these numbers make sense...I just set them
		// after observing what they did in the editor
		switch (facing) {
			case Facing.Up:
				return Quaternion.Euler(0, 0, 90);
			case Facing.Left:
				return Quaternion.Euler(0, 0, 180);
			case Facing.Down:
				return Quaternion.Euler(0, 0, 270);
			default:
				return Quaternion.Euler(0, 0, 0);
		}
	}
}
