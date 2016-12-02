using UnityEngine;
using System.Collections;

public class EnemyBehaviour : MonoBehaviour {

	public float health = 150;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D collider) {
		Projectile laser = collider.gameObject.GetComponent<Projectile>();
		if(laser) {
			health -= laser.GetDamage();
			laser.Hit();
			if(health <= 0) {
				Destroy(gameObject);
			}
		}
	}
}
