using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float spaceCraftSpeed = 15.0f;

	float xMin = -5;
	float xMax = 5;

	float yMin = -5;
	float yMax = 5;

	// Use this for initialization
	void Start () {
		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
		float spriteOffSetX = (float)GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2;
		float spriteOffSetY = (float)GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2;
		xMin = topLeft.x + spriteOffSetX;
		xMax = bottomRight.x - spriteOffSetX;
		yMin = topLeft.y + spriteOffSetY;
		yMax = bottomRight.y - spriteOffSetY;
	}
	
	// Update is called once per frame
	void Update () {
		moveSpaceCraft();
	}

	void moveSpaceCraft() {
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
}
