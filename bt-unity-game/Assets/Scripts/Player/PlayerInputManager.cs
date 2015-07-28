using DT;
﻿using UnityEngine;
using System.Collections;

public class PlayerInputManager : DTManager {
	protected IControllableHumanoid _actor;
	
	protected PlayerInputManager() {}
	
	protected void Start() {
		_actor = GameObject.Find("Human").GetComponent<IControllableHumanoid>();
	}
	
	protected void Update() {
		if (_actor == null) {
			return;
		}
		
		Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		_actor.AimArmsAt(mouseWorldPosition);
		
		if (Input.GetButtonDown("Move")) {
			_actor.MoveTo(mouseWorldPosition);
		}
				
		if (Input.GetButtonDown("Fire1")) {
			_actor.HandleFire1();
		} else if (Input.GetButtonDown("Fire2")) {
			_actor.HandleFire2();
		}
	}
}
