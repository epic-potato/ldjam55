using UnityEngine;

public enum Facing {
	Left,
	Right,
	Up,
	Down,
}

public static class FacingMethods {
	public static Vector3 GetDir(this Facing facing) {
		switch (facing) {
			case Facing.Left:
				return Vector3.left;
			case Facing.Right:
				return Vector3.right;
			case Facing.Up:
				return Vector3.up;
			case Facing.Down:
				return Vector3.down;
			default:
				return Vector3.forward;
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

	public static Facing Flip(this Facing facing) {
		switch (facing) {
			case Facing.Up:
				return Facing.Down;
			case Facing.Down:
				return Facing.Up;
			case Facing.Left:
				return Facing.Right;
			default:
				return Facing.Left;
		}
	}
}
