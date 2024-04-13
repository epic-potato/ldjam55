using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {
	List<Object> triggers;


	public void Start() {
		triggers = new List<Object>();
	}

	public bool IsTriggered() {
		return triggers.Count > 0;
	}

	public void DoTrigger(Object from, bool active) {
		if (active) {
			triggers.Add(from);
		} else {
			triggers.Remove(from);
		}
	}
}
