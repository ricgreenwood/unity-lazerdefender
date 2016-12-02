using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float spaceCraftSpeed = 15.0f;
	public float laserSpeed = 15.0f;
	public float firingRate = 0.2f;
	public GameObject laserPrefab;

	private float xMin, xMax, yMin, yMax;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
		float spriteOffSetX = GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
		float spriteOffSetY = GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
		xMin = topLeft.x + spriteOffSetX;
		xMax = bottomRight.x - spriteOffSetX;
		yMin = topLeft.y + spriteOffSetY;
		yMax = bottomRight.y - spriteOffSetY;
	}
	
	// Update is called once per frame
	void Update () {
		MoveSpaceCraft();

		if(Input.GetKeyDown(KeyCode.Space)) {
			InvokeRepeating("FireLaser", 0.000001f, firingRate);
		}
		if(Input.GetKeyUp(KeyCode.Space)) {
			CancelInvoke("FireLaser");
		}
	}

	void MoveSpaceCraft() {
		float speed = spaceCraftSpeed * Time.deltaTime;
		if(Input.GetKey(KeyCode.UpArrow)) {
			transform.position += Vector3.up * speed;
		} else if(Input.GetKey(KeyCode.DownArrow)) {
			transform.position += Vector3.down * speed;
		}
		if(Input.GetKey(KeyCode.RightArrow)) {
			transform.position += Vector3.right * speed;
		} else if(Input.GetKey(KeyCode.LeftArrow)) {
			transform.position += Vector3.left * speed;
		}

		// restrict the player to the game space.
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}

	void FireLaser() {
		Vector3 startingPos = transform.position;
		startingPos.z += 1f;
		GameObject laser = (GameObject) Instantiate(laserPrefab, startingPos, Quaternion.identity);
		Rigidbody2D laserRB2D = laser.GetComponent<Rigidbody2D>();
		laserRB2D.velocity = new Vector3(0, laserSpeed);
		//Debug.Log(startingPos.z);
	}
}
