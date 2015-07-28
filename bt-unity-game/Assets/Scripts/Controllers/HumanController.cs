using DT;
﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class HumanController : MonoBehaviour, IControllableHumanoid {
	// PRAGMA MARK - IControllableHumanoid
	int IControllableHumanoid.Health() {
		return _healthComponent.Health;
	}
	
	Vector3 IControllableHumanoid.Position() {
		return transform.position;
	}
	
	List<Vector3> IControllableHumanoid.VisibleEnemies() {
		// TODO (darren): implement this
		return null;
	}
	
	void IControllableHumanoid.AimArmsAt(Vector3 aimPoint) {
		float horizontalDiff = (aimPoint - transform.position).x;
		if (horizontalDiff < 0.0f && _facingRight) { 
			Flip();
		} else if (horizontalDiff > 0.0f && !_facingRight) {
			Flip();
		}
		
		this.AimTransformAt(_leftArm, aimPoint);
		this.AimTransformAt(_rightArm, aimPoint);
	}
	
	// NOTE (darren): currently only horizontal movement is supported, no pathfinding
	void IControllableHumanoid.MoveTo(Vector3 movePosition) {
		_targetMovePosition = movePosition;
	}
	
	void IControllableHumanoid.HandleFire1() {
		bool success = this.FireGun();
		if (!success) {
			this.MeleeAttack();
		}
	}
	
	void IControllableHumanoid.HandleFire2() {
		this.MeleeAttack();
	}
	
	// PRAGMA MARK - Begin Class
	protected const float HUMAN_SPEED = 20.0f;
	
	protected HealthComponent _healthComponent;
	protected GunController _gunController;
	protected Vector3 _targetMovePosition;
	protected Rigidbody2D _rigidbody;
	protected float _targetAimAngle;
	protected bool _facingRight;
	
	protected Transform _rightArm;
	protected Transform _leftArm;
	
	// PRAGMA MARK - Public 

	// PRAGMA MARK - Protected 
	protected void Awake() {
		_healthComponent = GetComponent<HealthComponent>();
		_gunController = GetComponentsInChildren<GunController>()[0];
		
		_rigidbody = GetComponent<Rigidbody2D>();
		
		_rightArm = transform.Find("Body/RightArm");
		_leftArm = transform.Find("Body/LeftArm");
	}
	
	protected void Start() {
		_targetMovePosition = transform.position;
	}
	
	protected void Update() {
		MoveTowardsTargetPosition();
		AimTowardsTargetPosition();
	}
	
	protected void AimTowardsTargetPosition() {
	}
	
	protected void MoveTowardsTargetPosition() {
		// remove all vertical component
		Vector3 diff = _targetMovePosition - transform.position;
	  float horizontalDiff = diff.x;
		
		if (Mathf.Abs(horizontalDiff) < 0.1) {
			transform.position = new Vector3(_targetMovePosition.x, transform.position.y, transform.position.z);
			return;
		}
		
		float dir = (horizontalDiff < 0.0f) ? -1.0f : 1.0f;
		_rigidbody.velocity = new Vector3(dir * HUMAN_SPEED, _rigidbody.velocity.y); 
	}
	
	// returns false if method fails
	protected bool FireGun() {
		if (_gunController) {
			return _gunController.Fire();
		}
		return false;
	}
	
	protected bool MeleeAttack() {
		// TODO (darren): implement this
		return false;
	}
	
	protected void Flip() {
		_facingRight = !_facingRight;
		
		if (_facingRight) {
			transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
		} else {
			transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
		}
	}
	
	protected void AimTransformAt(Transform trans, Vector3 aimPoint) {
		Vector3 diff = aimPoint - trans.position;
		
		// flip the diff so that x is always positive b/c the base sprite faces right
		Vector3 absDiff = new Vector3(Mathf.Abs(diff.x), diff.y, diff.z);
		
		float angleZ = -Vector3.Angle(Vector3.up, absDiff);
		trans.localRotation = Quaternion.Euler(0.0f, 0.0f, angleZ);
	}
}
