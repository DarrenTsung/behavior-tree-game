using UnityEngine;
﻿using UnityEngine.Events;
using System.Collections;

public class HealthComponent : MonoBehaviour {
	[SerializeField]
	protected int BaseHealth;
	
	public int Health {
		get { return _health; }
	}
	protected int _health;
	
	public UnityEvent OnHealthBelowZero;
	
	public void TakeDamage(int damage) {
		_health -= damage;
		if (_health <= 0) {
			OnHealthBelowZero.Invoke();
		}
	}
	
	protected void Awake() {
		_health = BaseHealth;
	}
}
