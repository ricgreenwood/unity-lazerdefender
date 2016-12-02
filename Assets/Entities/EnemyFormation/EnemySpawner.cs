using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public GameObject enemyPrefab;
	public float width = 10f;
	public float height = 5f;
	public float formationSpeed = 15f;

	private bool movingRight = true;
	private float xMin, xMax, yMin, yMax;

	// Use this for initialization
	void Start () {

		float distance = transform.position.z - Camera.main.transform.position.z;
		Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0,0,distance));
		Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1,1,distance));
		xMin = topLeft.x + (width / 2);
		xMax = bottomRight.x - (width / 2);
		yMin = topLeft.y + (height / 2);
		yMax = bottomRight.y - (height / 2);

		foreach(Transform child in transform) {
			GameObject enemy = (GameObject) Instantiate(enemyPrefab, child.transform.position, Quaternion.identity);
			//GameObject enemy = Instantiate(enemyPrefab, new Vector3(0,0,0), Quaternion.identity) as GameObject;
			enemy.transform.parent = child;
		}
	}

	public void OnDrawGizmos() {
		Gizmos.DrawWireCube(transform.position, new Vector3(width, height));
	}
	
	// Update is called once per frame
	void Update () {
		moveFormation();
	}

	void moveFormation() {
		float horizontalSpeed = formationSpeed * Time.deltaTime;
		float verticalSpeed = horizontalSpeed / 2f;

		if(movingRight) {
			transform.position += Vector3.right * horizontalSpeed;
			if(transform.position.x > xMax) {
				movingRight = false;
				transform.position += Vector3.down * verticalSpeed;
			}
		} else {
			transform.position += Vector3.left * horizontalSpeed;
			if(transform.position.x < xMin) {
				movingRight = true;
				transform.position += Vector3.down * verticalSpeed;
			}
		}

		// restrict the enemies to the game space.
		float newX = Mathf.Clamp(transform.position.x, xMin, xMax);
		float newY = Mathf.Clamp(transform.position.y, yMin, yMax);
		transform.position = new Vector3(newX, newY, transform.position.z);
	}
}
