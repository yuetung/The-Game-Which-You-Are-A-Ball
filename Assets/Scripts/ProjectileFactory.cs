﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileFactory : MonoBehaviour {

	[Tooltip("Projectie for Default element")]
	public Rigidbody2D projectileDefault=null;
	[Tooltip("Projectie for Fire element lv1")]
	public Rigidbody2D projectileFire1=null;
	[Tooltip("Projectie for Fire element lv2")]
	public Rigidbody2D projectileFire2=null;
	[Tooltip("Projectie for Fire element lv3")]
	public Rigidbody2D projectileFire3=null;
	[Tooltip("Projectie for Water element lv1")]
	public Rigidbody2D projectileWater1=null;
	[Tooltip("Projectie for Water element lv2")]
	public Rigidbody2D projectileWater2=null;
	[Tooltip("Projectie for Water element lv3")]
	public Rigidbody2D projectileWater3=null;
	[Tooltip("Projectie for Lightning element lv1")]
	public Rigidbody2D projectileLightning1=null;
	[Tooltip("Projectie for Lightning element lv2")]
	public Rigidbody2D projectileLightning2=null;
	[Tooltip("Projectie for Lightning element lv3")]
	public Rigidbody2D projectileLightning3=null;
	[Tooltip("Projectie for Earth element lv1")]
	public Rigidbody2D projectileEarth1=null;
	[Tooltip("Projectie for Earth element lv2")]
	public Rigidbody2D projectileEarth2=null;
	[Tooltip("Projectie for Earth element lv3")]
	public Rigidbody2D projectileEarth3=null;
	[Tooltip("Projectie for Wind element lv1")]
	public Rigidbody2D projectileWind1=null;
	[Tooltip("Projectie for Wind element lv2")]
	public Rigidbody2D projectileWind2=null;
	[Tooltip("Projectie for Wind element lv3")]
	public Rigidbody2D projectileWind3=null;
	[Tooltip("Projectie for Antimatter element")]
	public Rigidbody2D projectileAntimatter=null;

	// Return the corresponding projectile based on current elementType
	public Rigidbody2D getProjectileFromType(PlayerControler.ElementType element, int level) {
		switch (element) {
		case PlayerControler.ElementType.Fire:
			switch (level) {
			case 1:
				return projectileFire1;
			case 2:
				return projectileFire2;
			case 3:
				return projectileFire3;
			default:
				return projectileDefault;
			}
		case PlayerControler.ElementType.Water:
			switch (level) {
			case 1:
				return projectileWater1;
			case 2:
				return projectileWater2;
			case 3:
				return projectileWater3;
			default:
				return projectileDefault;
			}
		case PlayerControler.ElementType.Lightning:
			switch (level) {
			case 1:
				return projectileLightning1;
			case 2:
				return projectileLightning2;
			case 3:
				return projectileLightning3;
			default:
				return projectileDefault;
			}
		case PlayerControler.ElementType.Earth:
			switch (level) {
			case 1:
				return projectileEarth1;
			case 2:
				return projectileEarth2;
			case 3:
				return projectileEarth3;
			default:
				return projectileDefault;
			}
		case PlayerControler.ElementType.Wind:
			switch (level) {
			case 1:
				return projectileWind1;
			case 2:
				return projectileWind2;
			case 3:
				return projectileWind3;
			default:
				return projectileDefault;
			}
		case PlayerControler.ElementType.Antimatter:
			return projectileAntimatter;
		default:
			return projectileDefault;
		}
	}
}
