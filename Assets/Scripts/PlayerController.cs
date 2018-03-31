using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	public bool testMode = false;
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
	public int maxHealth = 100;
	public int health;
	public Vector2 mouseDownLocation; // where the mouse is initially held down
	public Vector2 moveTarget;  // target point to move player towards
	public float timeCounter = 1.0f;
	public float energyLossRate = 1.0f;
	public int levelCap = 3; 
	public GameObject lightningEffect;
	private GameObject currentEarthProjectileSpawner;
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
    [SyncVar(hook = "updateTrailRenderer")]
    Color color;
	public Rigidbody2D _rigidbody;
	TrailRenderer _trailRenderer;
	public ProjectileFactory projectileFactory;
	GUIManager guiManager;
    public GameObject touchIndicator;
		
	// Use this for initialization
	public void Start () {
		health = maxHealth;
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
            gameObject.layer = 9;
            // Cameras are disabled by default, this enables only one camera for each client.
            transform.Find("Main Camera").gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    public void Update () {

        if (!isLocalPlayer  && !testMode) {
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
                var worldLocation = Camera.main.ScreenToWorldPoint(mouseUpLocation);
				setMovementTarget (worldLocation);

                var tempIcon = Instantiate(touchIndicator, worldLocation + new Vector3(0,0,10), new Quaternion());
                Destroy(tempIcon, 0.5f);
            }

			// Mouse Dragged
			else {
				Shoot (shootDirection, elementType, elementLevel);
            }
            transform.Find("Target Arrow").gameObject.SetActive(false);
        }
		move ();

        // if mouse is held down,
        if (Input.GetMouseButton(0))
        {
            Vector2 draggedDistance = (Vector2)Input.mousePosition - mouseDownLocation;
            if (draggedDistance.magnitude > clickDragSensitivity)
            {
                transform.Find("Target Arrow").gameObject.SetActive(true);
                var angle = Mathf.Atan2(-draggedDistance.x, draggedDistance.y) * Mathf.Rad2Deg;
                transform.Find("Target Arrow").rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
            else
            {
                transform.Find("Target Arrow").gameObject.SetActive(false);
            }
        }

        // Reduce energy over time
        timeCounter += Time.deltaTime;

        if (timeCounter >= 1 / energyLossRate)
        {
            depleteEnergy(1);
            timeCounter = 0;
        }

        guiManager.updateAll();

        //Debug.Log ("Element: "+elementType.ToString()+" Level: "+elementLevel.ToString()+" Energy: "+energy.ToString());
    }

	// set moveTarget of where the player should go to
	public void setMovementTarget(Vector2 targetLocation) {
		moveTarget = targetLocation;
		Vector2 faceDirection = moveTarget - new Vector2(transform.position.x,transform.position.y);
		_rigidbody.rotation = Mathf.Atan2 (faceDirection.y, faceDirection.x) * Mathf.Rad2Deg +90;
	}


	// Obtain a projectile from ProjectileFactory and shoots it
    // Shoot is here instead of CmdShoot is such that Raycast would work on the local player who shoots it
    // If it's a normal projectile, nothing really happens, CmdShoot is called.
    // If it's a lightning projectile, finalPosition is calculated and passed into CmdShoot.
    public void Shoot(Vector2 shootDirection, ElementType elementType, int elementLevel)
    {
		//Earth type expand when "shoot"
		if (elementType == ElementType.Earth) {
			if (currentEarthProjectileSpawner) {
				currentEarthProjectileSpawner.GetComponent<EarthProjectileSpawner> ().startExpand ();
			}
            return;
		}
        Rigidbody2D projectile = projectileFactory.getProjectileFromType(elementType, elementLevel);
        float maxDistance = projectile.GetComponent<ProjectileController>().maxDistance;
        int layerMask = LayerMask.GetMask("Enemy", "Wall");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootDirection, Mathf.Infinity, layerMask);
        Vector2 finalPosition;
        if (hit.distance <= maxDistance)
        {
            finalPosition = hit.transform.position;
        }
        else
        {
            finalPosition = new Vector2(transform.position.x, transform.position.y) + shootDirection.normalized * maxDistance;
        }
        CmdShoot(shootDirection, elementType, elementLevel, finalPosition);
        depleteEnergy(elementLevel * 10);
    }
   
    // Since we can't pass GameObjects into Cmd, we pass similar parameters as Shoot()
    [Command]
	public void CmdShoot (Vector2 shootDirection, ElementType elementType, int elementLevel, Vector2 finalPosition)
    {
        Rigidbody2D projectile = projectileFactory.getProjectileFromType(elementType, elementLevel);
        Rigidbody2D clone;
        //Lightning projectile
        Debug.Log("Type of projectile: " + elementType);
        if (elementType == ElementType.Lightning)
        {
            clone = Instantiate(projectile, finalPosition, transform.rotation) as Rigidbody2D;
            GameObject lightning = Instantiate(lightningEffect, transform.position, transform.rotation);
            lightning.GetComponent<LightningBoltScript>().StartObject = this.gameObject;
            lightning.GetComponent<LightningBoltScript>().EndPosition = finalPosition;
            NetworkServer.Spawn(lightning);
        }
        else //other projectiles
        { 
            clone = Instantiate(projectile, transform.position, transform.rotation) as Rigidbody2D;
        }
        GameObject cloneGameObject = clone.gameObject;
        cloneGameObject.GetComponent<ProjectileController>().belongsToPlayer();
        Vector2 velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
        float rotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        cloneGameObject.GetComponent<ProjectileController>().setVelocityAndRotation(velocity, rotation);
        // assigns a shooter to the bullet
        cloneGameObject.GetComponent<ProjectileController>().shooter = transform.gameObject;
        Debug.Log("Spawn projectile");
        NetworkServer.Spawn(cloneGameObject);
    }
	
	// constant movement if player haven't reached the moveTarget
	public void move() {
		Vector2 moveVector = moveTarget - (Vector2)transform.position;
		if (moveVector.magnitude <= moveSpeed * Time.deltaTime) {
			_rigidbody.velocity = Vector2.zero;
		} else {
			_rigidbody.velocity = moveVector.normalized * moveSpeed;
		}
	}

	// Changes user's current ElementType if element obtained is different from current elementType
	/*	@Pre-condition: energyAmount>=0; newElementType!=null
	 * 	@Post-condition: Player is assigned newElementType, elementLevel is reset to 1 and energyLevel is 
	 * 					 reset to energy of powerup if different from player's current elementType.
	 * 					 Else, the player's elementType remains the same and player's energyLevel is gained
	 * 					 by amount equal to energy of powerup. 
	 */ 
	public void gainPowerUp(ElementType newElementType, int energyAmount) {
		Debug.Log ("PowerUpGained");
		if (elementType != newElementType) {
			//reset element
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
		while (energy > 100) {
			if (elementLevel + 1 > levelCap) { //hit level cap
				energy = 100;
			} else {
				setElementLevel (elementLevel + 1); // increase element level
				energy -= 100;
			}
		}
	}


	public void depleteEnergy(int amount) {
		if (elementType == ElementType.Default || elementLevel == 0) {
			return;
		}
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
		// create earth projectile spawner based on element level
		if (elementType == ElementType.Earth) {
			CmdCreateEarthProjectileSpawner (elementType, elementLevel);
		} else {
			CmdDestroyCurrentEarthProjectileSpawner ();
		}
	}

	// Set the sprite animation and trailRenderer color
	public void changeSprite () {
        //color = Color.white;
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
			color = new Color(120/255f, 82/255f, 45/255f);
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
			color = Color.grey;
		}
        updateTrailRenderer(color);
        CmdUpdateTrailRenderer(color);
    }

    [Command]
    public void CmdUpdateTrailRenderer(Color color)
    {
        updateTrailRenderer(color);
    }

    public void updateTrailRenderer(Color color)
    {
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(trailAlpha, 0.0f), new GradientAlphaKey(0, 1.0f) }
        );
        _trailRenderer.colorGradient = gradient;
    }

    
	public void depleteHealth(int damage) {
		//Handheld.Vibrate ();
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

    [Command]
    // we pass in parameters instead of using values from the class because the class might not be updated on the server
    // This way, the client is able to pass in their own information and don't have to rely on the server's data on them
	private void CmdCreateEarthProjectileSpawner(ElementType elementType, int elementLevel) {
		CmdDestroyCurrentEarthProjectileSpawner ();
		int numRockToSpawn = 0;
		if (currentEarthProjectileSpawner) {
			numRockToSpawn = currentEarthProjectileSpawner.GetComponent<EarthProjectileSpawner> ().getNumRock ();
		}
		Rigidbody2D projectile = projectileFactory.getProjectileFromType (elementType, elementLevel);
        currentEarthProjectileSpawner = Instantiate(projectile.gameObject, transform.position, transform.rotation);
		currentEarthProjectileSpawner.GetComponent<EarthProjectileSpawner> ().belongsToPlayer ();
		currentEarthProjectileSpawner.GetComponent<EarthProjectileSpawner> ().shooter = transform.gameObject;
        //CmdassignClientAuthority(currentEarthProjectileSpawner.GetComponent<NetworkIdentity>());
        //currentEarthProjectileSpawner.GetComponent<EarthProjectileSpawner> ().CmdSpawnProjectile (numRockToSpawn);
        // spawn is moved to EarthProjectileSpawner?
		NetworkServer.SpawnWithClientAuthority(currentEarthProjectileSpawner, this.gameObject);
	}

    private void CmdassignClientAuthority(NetworkIdentity inp)
    {
        inp.AssignClientAuthority(connectionToClient);
        Debug.Log(inp.localPlayerAuthority);
        Debug.Log(inp.hasAuthority);
    }

    [Command]
	private void CmdDestroyCurrentEarthProjectileSpawner() {
		if (currentEarthProjectileSpawner != null) {
			GameObject[] earthProjectiles = currentEarthProjectileSpawner.GetComponent<EarthProjectileSpawner> ().earthProjectiles;
			for (int i = 0; i < earthProjectiles.Length; i++) {
				if (earthProjectiles [i]!=null)
					earthProjectiles [i].GetComponent<ProjectileController> ().DestroyNow ();
			}
			NetworkServer.Destroy(currentEarthProjectileSpawner);
		}
		currentEarthProjectileSpawner = null;
	}

}
