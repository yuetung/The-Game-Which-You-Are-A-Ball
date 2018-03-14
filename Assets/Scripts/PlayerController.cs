using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

    [Tooltip("Movement Speed")]
    public float moveSpeed = 1.5f;
    [Tooltip("How much distance to drag mouse to distinguish between a click and a drag")]
    public float clickDragSensitivity = 0.01f;
    [Tooltip("true projectile speed = player shoot speed * projectile speed")]
    public float playerShootSpeed = 2.0f;
    [Tooltip("Trail Renderer Material")]
    public Material trailMaterial = null;
    public float trailAlpha = 0.2f;
	public ElementType elementType = ElementType.Default;
	public int energy = 0;
	public int elementLevel = 0;
	public int health = 100;
	public Vector2 mouseDownLocation; // where the mouse is initially held down
	public Vector2 moveTarget;  // target point to move player towards
	public float timeCounter = 1.0f;
	public float energyLossRate = 1.0f;
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
	public ProjectileFactory projectileFactory;
	GUIManager guiManager;
		
	// Use this for initialization
	public void Start () {
		_animator=gameObject.GetComponent<Animator>();
		_rigidbody = gameObject.GetComponent<Rigidbody2D> ();
		_trailRenderer = gameObject.GetComponent<TrailRenderer> ();
		_trailRenderer.material = trailMaterial;
		mouseDownLocation = transform.position;
		moveTarget = transform.position;
		elementType = ElementType.Default;
		projectileFactory = GameManager.gm.GetComponent<ProjectileFactory>();
		guiManager = GameManager.gm.GetComponent<GUIManager>();
        //GetComponent<NetworkAnimator>().SetParameterAutoSend(0, true);

        if (isLocalPlayer) {
			guiManager.register (gameObject);
            // Cameras are disabled by default, this enables only one camera for each client.
            transform.Find("Main Camera").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void Update () {
		if (!isLocalPlayer) {
			return;
		}
		// Mouse Down
		if (Input.GetMouseButtonDown (0)) { // Record initial mouseDown location
			mouseDownLocation = Input.mousePosition;
		}
		// Mouse Released: considered click if mouse release location is close to mouse down location, considered drag otherwise
		if (Input.GetMouseButtonUp (0)) { // Record mouseUp location to determine click vs drag
			Vector2 mouseUpLocation = Input.mousePosition;
			Vector2 shootDirection = mouseUpLocation - mouseDownLocation;

			// Mouse Clicked
			if (shootDirection.magnitude < clickDragSensitivity) {
				setMovementTarget (Camera.main.ScreenToWorldPoint (mouseUpLocation));
			}

			// Mouse Dragged
			else { 
				CmdShoot (shootDirection, elementType, elementLevel);
				depletesEnergy (elementLevel * 10);
			}
		}
		move ();
		// Reduce energy over time
		timeCounter += Time.deltaTime;
		if (timeCounter >= 1 / energyLossRate) {
			depletesEnergy (1);
			timeCounter = 0;
		}
		guiManager.updateAll ();
		//Debug.Log ("Element: "+elementType.ToString()+" Level: "+elementLevel.ToString()+" Energy: "+energy.ToString());
	}

	// set moveTarget of where the player should go to
	public void setMovementTarget(Vector2 targetLocation) {
		moveTarget = targetLocation;
		Vector2 faceDirection = moveTarget - new Vector2(transform.position.x,transform.position.y);
		_rigidbody.rotation = Mathf.Atan2 (faceDirection.y, faceDirection.x) * Mathf.Rad2Deg +90;
	}


	// Obtain a projectile from ProjectileFactory and shoots it
    [Command]
	public void CmdShoot (Vector2 shootDirection, ElementType elementType, int elementLevel) {
		Rigidbody2D projectile = projectileFactory.getProjectileFromType (elementType, elementLevel);
		Rigidbody2D clone;
		clone = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody2D;
		GameObject cloneGameObject = clone.gameObject;
		cloneGameObject.GetComponent<ProjectileController> ().belongsToPlayer ();
		Vector2 velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
		float rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
		cloneGameObject.GetComponent<ProjectileController> ().setVelocityAndRotation (velocity, rotation);
        // assigns a shooter to the bullet
        cloneGameObject.GetComponent<ProjectileController>().shooter = transform.gameObject;
        NetworkServer.Spawn(cloneGameObject);
	}

	/*
	[Command]
    public void CmdShoot(Vector2 shootDirection)
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
	public void move() {
		Vector2 moveVector = moveTarget - (Vector2)transform.position;
		if (moveVector.magnitude <= moveSpeed * Time.deltaTime) {
			_rigidbody.velocity = Vector2.zero;
		} else {
			_rigidbody.velocity = moveVector.normalized * moveSpeed;
		}
	}

	// Change user's current ElementType if element obtained is different from current elementType
	public void gainPowerUp(ElementType newElementType, int energyAmount) {
		Debug.Log ("PowerUpGained");
		if (elementType != newElementType) {
			elementType = newElementType;
			setElementLevel (1);
			this.energy = energyAmount;
			changeSprite ();

		} else {
			if (elementType != ElementType.Default) {
				gainEnergy (energyAmount);
			}
		}

	}

	public void gainEnergy(int amount) {
		energy = energy + amount;
		while (energy >= 100) {
			setElementLevel (elementLevel + 1);
			energy -= 100;
		}

	}

	public void depletesEnergy(int amount) {
		if (elementType == ElementType.Default || elementLevel==0)
			return;
		energy -= amount;
		if (energy <= 0) {
			if (elementLevel == 1) {
				energy = 0;
				elementType = ElementType.Default;
				changeSprite ();
				setElementLevel (0);
			} else {
				while (energy <= 0) {
					setElementLevel (elementLevel - 1);
					energy += 100;
				}
			}
		}
	}

	public void setElementLevel(int newElementLevel) {
		elementLevel = newElementLevel;
		energyLossRate = Mathf.Pow (elementLevel , 1.5f);
	}

	// Set the sprite animation and trailRenderer color
	public void changeSprite () {
		Color color = Color.white;
		if (elementType == ElementType.Fire) {
            GetComponent<NetworkAnimator>().SetTrigger("FireType");
            _animator.SetTrigger ("FireType");
			color = Color.red;
		} else if (elementType == ElementType.Water) {
            GetComponent<NetworkAnimator>().SetTrigger("WaterType");
            _animator.SetTrigger ("WaterType");
			color = Color.blue;
		} else if (elementType == ElementType.Lightning) {
            GetComponent<NetworkAnimator>().SetTrigger("LightningType");
            _animator.SetTrigger ("LightningType");
			color = Color.yellow;
		} else if (elementType == ElementType.Earth) {
            GetComponent<NetworkAnimator>().SetTrigger("EarthType");
            _animator.SetTrigger ("EarthType");
			color = new Color (120, 82, 45); //Brown color
		} else if (elementType == ElementType.Wind) {
            GetComponent<NetworkAnimator>().SetTrigger("WindType");
            _animator.SetTrigger ("WindType");
			color = Color.green;
		} else if (elementType == ElementType.Antimatter) {
            GetComponent<NetworkAnimator>().SetTrigger("AntimatterType");
            _animator.SetTrigger ("AntimatterType");
			color = Color.black;
		} else {
            GetComponent<NetworkAnimator>().SetTrigger("DefaultType");
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

    
	public void depleteHealth(int damage) {
		if (health - damage <= 0) {
			health = 0;
			//TODO: implement player's death
			//Instantiate (explosionPrefab, transform.position, transform.rotation);
			guiManager.updateAll ();
			guiManager.EndGame ();
			DestroyObject (this.gameObject);
		} else {
			health -= damage;
		}
	}

}
