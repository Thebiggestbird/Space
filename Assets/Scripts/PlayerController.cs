﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
}
public class PlayerController : MonoBehaviour 
{
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	public float fireRate;
	private float nextFire;
	private Rigidbody rb;
	private AudioSource audioSource;

	void Update ()
	
	  {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
			audioSource.Play ();
        }
    }

	void Start() 
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	
	}
	void FixedUpdate()
	{
		float movehorizontal = Input.GetAxis ("Horizontal");
		float movevirtical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (movehorizontal, 0.0f, movevirtical);
		rb.velocity = movement*speed;

		rb.position = new Vector3 
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler (0.0f, 0.0f, rb.velocity.x * -tilt);
	
	}
}
