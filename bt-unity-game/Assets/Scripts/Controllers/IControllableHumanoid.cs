using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public interface IControllableHumanoid {
	int Health();
	Vector3 Position();
	List<Vector3> VisibleEnemies();
	
	void AimArmsAt(Vector3 aimPoint);
	void MoveTo(Vector3 movePosition);
	
	void HandleFire1();
	void HandleFire2();
}
