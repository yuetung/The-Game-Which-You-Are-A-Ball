using UnityEngine;
using UnityEngine.Networking;

public class PlayerController : NetworkBehaviour {

	[Tooltip("Movement Speed")]
	public float moveSpeed=1.5f;
	[Tooltip("How much distance to drag mouse to distinguish between a click and a drag")]
	public float clickDragSensitivity=0.01f;
	[Tooltip("true projectile speed = player shoot speed * projectile speed")]
	public float playerShootSpeed=2.0f;
	[Tooltip("Projectie for Default element")]
	public Rigidbody2D projectileDefault=null;
	[Tooltip("Projectie for Fire element")]
	public Rigidbody2D projectileFire=null;
	[Tooltip("Projectie for Water element")]
	public Rigidbody2D projectileWater=null;
	[Tooltip("Projectie for Lightning element")]
	public Rigidbody2D projectileLightning=null;
	[Tooltip("Projectie for Earth element")]
	public Rigidbody2D projectileEarth=null;
	[Tooltip("Projectie for Wind element")]
	public Rigidbody2D projectileWind=null;
	[Tooltip("Projectie for Antimatter element")]
	public Rigidbody2D projectileAntimatter=null;
	[Tooltip("Trail Renderer Material")]
	public Material trailMaterial=null;
	public float trailAlpha=0.2f;
	public ElementType elementType = ElementType.Default;
	public int elementLevel = 1;
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
		
	// Use this for initialization
	void Start () {
		_animator=gameObject.GetComponent<Animator>();
		_rigidbody = gameObject.GetComponent<Rigidbody2D> ();
		_trailRenderer = gameObject.GetComponent<TrailRenderer> ();
		_trailRenderer.material = trailMaterial;
		mouseDownLocation = transform.position;
		moveTarget = transform.position;
		gainElementType (ElementType.Default);
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
				moveTarget = mouseUpLocation;
				Vector2 faceDirection = moveTarget - new Vector2(transform.position.x,transform.position.y);
				_rigidbody.rotation = Mathf.Atan2 (faceDirection.y, faceDirection.x) * Mathf.Rad2Deg +90;
			}

			// Mouse Dragged
			else { 
				//Rigidbody2D projectile = getProjectileFromType();
				//// Create Projectile
				//Rigidbody2D clone;
				//clone = Instantiate (projectile, transform.position, transform.rotation) as Rigidbody2D;
				//clone.velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
				//clone.rotation = Mathf.Atan2 (shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
                CmdShoot(shootDirection);
			}
		}
		Vector2 moveVector = moveTarget - (Vector2)transform.position;
		if (moveVector.magnitude <= moveSpeed*Time.deltaTime) {
            _rigidbody.velocity = Vector2.zero;
        } else {
			_rigidbody.velocity = moveVector.normalized * moveSpeed;
        }
		//transform.position = Vector2.MoveTowards (transform.position, moveTarget, speed * Time.deltaTime);
	}

	// Cannot pass through wall
	/*void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Wall") {
			moveTarget = transform.position;
		}

	}*/

	// Return the corresponding projectile based on current elementType

    [Command]
    private void CmdShoot(Vector2 shootDirection)
    {
        Rigidbody2D projectile = getProjectileFromType();
        // Create Projectile
        Rigidbody2D clone;
        clone = Instantiate(projectile, transform.position, transform.rotation);
        clone.velocity = shootDirection.normalized * playerShootSpeed * projectile.GetComponent<ProjectileController>().projectileSpeed;
        clone.rotation = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        //NetworkServer.Spawn(clone);
    }
    
	private Rigidbody2D getProjectileFromType() {
		switch (elementType) {
		case ElementType.Fire:
			return projectileFire;
		case ElementType.Water:
			return projectileWater;
		case ElementType.Lightning:
			return projectileLightning;
		case ElementType.Earth:
			return projectileEarth;
		case ElementType.Wind:
			return projectileWind;
		case ElementType.Antimatter:
			return projectileAntimatter;
		default:
			return projectileDefault;
		}
	}

	// Change user's current ElementType if element obtained is different from current elementType
	public void gainElementType(ElementType newElementType) {
		if (elementType != newElementType) {
			elementType = newElementType;
			elementLevel = 1;
			Color color = Color.white;

			// Change animation sprite, decide trailRenderer color
			if (elementType==ElementType.Fire) {
				_animator.SetTrigger ("FireType");
				color = Color.red;
			}
			else if (elementType==ElementType.Water) {
				_animator.SetTrigger ("WaterType");
				color = Color.blue;
			}
			else if (elementType==ElementType.Lightning) {
				_animator.SetTrigger ("LightningType");
				color = Color.yellow;
			}
			else if (elementType==ElementType.Earth) {
				_animator.SetTrigger ("EarthType");
				color = new Color(120,82,45); //Brown color
			}
			else if (elementType==ElementType.Wind) {
				_animator.SetTrigger ("WindType");
				color = Color.green;
			}
			else if (elementType==ElementType.Antimatter) {
				_animator.SetTrigger ("AntimatterType");
				color = Color.black;
			}
			else {
				_animator.SetTrigger ("DefaultType");
				color = Color.gray;
			}

			// Change TrailRenderer colour
			Gradient gradient = new Gradient();
			gradient.SetKeys (
				new GradientColorKey[]{ new GradientColorKey (color, 0.0f), new GradientColorKey (Color.white, 1.0f) },
				new GradientAlphaKey[]{ new GradientAlphaKey (trailAlpha, 0.0f), new GradientAlphaKey (0, 1.0f) }
			);
			_trailRenderer.colorGradient = gradient;

		} else {
			if (elementType != ElementType.Default) {
				elementLevel++;
			}
		}

	}
}
