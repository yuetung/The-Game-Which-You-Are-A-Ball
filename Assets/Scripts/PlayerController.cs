﻿using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	[Tooltip("Movement Speed")]
	public float moveSpeed=1.5f;
	[Tooltip("How much distance to drag mouse to distinguish between a click and a drag")]
	public float clickDragSensitivity=0.01f;
	[Tooltip("true projectile speed = player shoot speed * projectile speed")]
	public float playerShootSpeed=2.0f;
	[Tooltip("Trail Renderer Material")]
	public Material trailMaterial=null;
	public float trailAlpha=0.2f;
	public ElementType elementType = ElementType.Default;
	public float energy = 0;
	public int elementLevel = 0;
	private Vector2 mouseDownLocation; // where the mouse is initially held down
	private Vector2 moveTarget;  // target point to move player towards

	// Possible element types
	public enum ElementType {
		Default,
		Fire,
		Water,
		Lightning,
		Earth,
		Wind,
		Antimatter
	};

	// Store references to gamebject Components
	Animator _animator;
	Rigidbody2D _rigidbody;
	TrailRenderer _trailRenderer;
	ProjectileFactory projectileFactory;
		
	// Use this for initialization
	void Start () {
		_animator=gameObject.GetComponent<Animator>();
		_rigidbody = gameObject.GetComponent<Rigidbody2D> ();
		_trailRenderer = gameObject.GetComponent<TrailRenderer> ();
		_trailRenderer.material = trailMaterial;
		mouseDownLocation = transform.position;
		moveTarget = transform.position;
		elementType = ElementType.Default;
		projectileFactory = GameManager.gm.GetComponent<ProjectileFactory>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!isLocalPlayer)
        {
            return;
        }
		// Mouse Down
		if (Input.GetMouseButtonDown (0)) { // Record initial mouseDown location
			mouseDownLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		}
		// Mouse Released: considered click if mouse release location is close to mouse down location, considered drag otherwise
		if (Input.GetMouseButtonUp (0)) { // Record mouseUp location to determine click vs drag
			Vector2 mouseUpLocation = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			Vector2 shootDirection = mouseUpLocation - mouseDownLocation;

			// Mouse Clicked
			if (shootDirection.magnitude < clickDragSensitivity) {
				setMovementTarget (mouseUpLocation);
			}

			// Mouse Dragged
			else { 
                CmdShoot(shootDirection);
			}
		}

		move ();

		Debug.Log ("Element: "+elementType.ToString()+" Level: "+elementLevel.ToString()+" Energy: "+energy.ToString());
	}

	// set moveTarget of where the player should go to
	private void setMovementTarget(Vector2 targetLocation) {
		moveTarget = targetLocation;
		Vector2 faceDirection = moveTarget - new Vector2(transform.position.x,transform.position.y);
		_rigidbody.rotation = Mathf.Atan2 (faceDirection.y, faceDirection.x) * Mathf.Rad2Deg +90;
	}


	// Obtain a projectile from ProjectileFactory and shoots it
	private void shootProjectile (Vector2 shootDirection) {
		Rigidbody2D projectile = projectileFactory.getProjectileFromType (elementType, elementLevel);
		Rigidbody2D clone;
		clone = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody2D;
		GameObject cloneGameObject = clone.gameObject;
		Vector2 velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
		float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
		cloneGameObject.GetComponent<ProjectileController> ().setVelocityAndRotation (velocity, rotation);
		depletesEnergy (elementLevel * 10);
	}

	/*
	[Command]
    private void CmdShoot(Vector2 shootDirection)
    {
        GameObject projectile = getProjectileFromType();
        // Create Projectile
        GameObject clone;
        clone = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
        clone.GetComponent<Rigidbody2D>().velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
        clone.GetComponent<Rigidbody2D>().rotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        NetworkServer.Spawn(clone);
    }
	*/
	
	// constant movement if player haven't reached the moveTarget
	private void move() {
		Vector2 moveVector = moveTarget - (Vector2)transform.position;
		if (moveVector.magnitude <= moveSpeed * Time.deltaTime) {
			_rigidbody.velocity = Vector2.zero;
		} else {
			_rigidbody.velocity = moveVector.normalized * moveSpeed;
		}
	}

	// Change user's current ElementType if element obtained is different from current elementType
	public void gainPowerUp(ElementType newElementType, float energyAmount) {
		if (elementType != newElementType) {
			elementType = newElementType;
			elementLevel = 1;
			this.energy = energyAmount;
			changeSprite ();

		} else {
			if (elementType != ElementType.Default) {
				gainEnergy (energyAmount);
			}
		}

	}

	private void gainEnergy(float amount) {
		energy = energy + amount;
		while (energy >= 100) {
			elementLevel++;
			energy -= 100;
		}

	}

	private void depletesEnergy(float amount) {
		if (elementType == ElementType.Default || elementLevel==0)
			return;
		energy -= amount;
		if (energy <= 0) {
			if (elementLevel == 1) {
				energy = 0;
				elementType = ElementType.Default;
				changeSprite ();
				elementLevel = 0;
			} else {
				while (energy <= 0) {
					elementLevel--;
					energy += 100;
				}
			}
		}

	}

	// Set the sprite animation and trailRenderer color
	private void changeSprite () {
		Color color = Color.white;
		if (elementType == ElementType.Fire) {
			_animator.SetTrigger ("FireType");
			color = Color.red;
		} else if (elementType == ElementType.Water) {
			_animator.SetTrigger ("WaterType");
			color = Color.blue;
		} else if (elementType == ElementType.Lightning) {
			_animator.SetTrigger ("LightningType");
			color = Color.yellow;
		} else if (elementType == ElementType.Earth) {
			_animator.SetTrigger ("EarthType");
			color = new Color (120, 82, 45); //Brown color
		} else if (elementType == ElementType.Wind) {
			_animator.SetTrigger ("WindType");
			color = Color.green;
		} else if (elementType == ElementType.Antimatter) {
			_animator.SetTrigger ("AntimatterType");
			color = Color.black;
		} else {
			_animator.SetTrigger ("DefaultType");
			color = Color.gray;
		}
			
		Gradient gradient = new Gradient ();
		gradient.SetKeys (
			new GradientColorKey[]{ new GradientColorKey (color, 0.0f), new GradientColorKey (Color.white, 1.0f) },
			new GradientAlphaKey[]{ new GradientAlphaKey (trailAlpha, 0.0f), new GradientAlphaKey (0, 1.0f) }
		);
		_trailRenderer.colorGradient = gradient;
	}
}
