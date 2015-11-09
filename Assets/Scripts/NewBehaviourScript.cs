using UnityEngine;
using System.Collections;

public class NewBehaviourScript : MonoBehaviour {
	public Rigidbody prefab;
	public GameObject Barrel;
	public float shootSpeed = 10;
	public GameObject gunParticle;
	public float timeBetweenShots = 0.5f;
	float timeSinceLastShot = 0;

	// Use this for initialization
	void Start () {
		//prefab = Resources.Load ("projectile")as GameObject;
		}
	
	// Update is called once per frame
	void Update () {
		timeSinceLastShot += Time.deltaTime;
		if ((Input.GetMouseButtonDown (0)) && (timeSinceLastShot > timeBetweenShots)) {
			//launch projectile
			if (prefab == null)
			{
				Debug.Log ("No cannonball prefab declared!");
			}
			else
			{
			Rigidbody projectile;
			projectile = Instantiate (prefab, Barrel.transform.position, Barrel.transform.rotation) as Rigidbody;
			projectile.velocity = projectile.transform.TransformDirection (Vector3.forward * shootSpeed);
			}
			//create explosion effect
			if (gunParticle == null)
			{
				Debug.Log ("No explosion particle effect declared!");
			}
			else
			{
			Instantiate (gunParticle, Barrel.transform.position, Barrel.transform.rotation);
			}
			timeSinceLastShot = 0;

		}



	}
}
