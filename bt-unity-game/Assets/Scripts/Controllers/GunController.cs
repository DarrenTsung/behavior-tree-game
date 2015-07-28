using MeshTools;
using System.Collections;
using System.Collections.Generic;
﻿using UnityEngine;

public class GunController : MonoBehaviour {
	[SerializeField]
	protected float LINE_WIDTH = 0.5f;
	
	protected GameObject _muzzleFlash;
	protected Transform _gunStart;
	protected Transform _gunEnd;
	
	protected void Awake() {
		_gunStart = transform.Find("GunStart");
		_gunEnd = transform.Find("GunEnd");
		
		Shape muzzleFlash = Circle2D.Instance.Build(LINE_WIDTH * 2.0f, 100, Color.yellow);
		_muzzleFlash = muzzleFlash.BuiltGameObject;
		_muzzleFlash.transform.SetParent(_gunEnd);
	}
	
	public bool Fire() {
		Vector2 start = _gunStart.position;
		Vector2 end = _gunEnd.position;
		
		Vector2 dir = (end - start).normalized;
		
		RaycastHit2D hit = Physics2D.Raycast(start, dir, 1000.0f);
		Vector2 hitPoint;
		if (hit.collider != null) {
			hitPoint = hit.point;
		} else {
			hitPoint = start + (1000.0f * dir);
		}
		
		List<Vector2> linePoints = new List<Vector2>();
		linePoints.Add(_gunEnd.position);
		linePoints.Add(hitPoint);
		
		Shape bulletLine = Line2D.Instance.Build(linePoints, LINE_WIDTH, Color.yellow, null);
		Destroy(bulletLine.BuiltGameObject, 0.5f);
		
		return true;
	}
}
