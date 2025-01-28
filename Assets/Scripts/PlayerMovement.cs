// shoot
// using __ imports namespace
// Namespaces are collection of classes, data types
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// MonoBehavior is the base class from which every Unity Script Derives
public class PlayerMovement : MonoBehaviour
{
    public float speed = 25.0f;
    public float rotationSpeed = 90;
    public float force = 700f;

    // The speed of the bullets
    public float bulletSpead = 1000f;

    // The maximum number of bullets that can exist at any one time
    public int maxBullets = 5;

    public GameObject cannon;
    public GameObject bullet;

    // Tracks whether the player is on the ground
    private bool isGrounded = true;

    Rigidbody rb;
    Transform t;

    // Holds a list of the current bullets in the scene for easy management
    private List<GameObject> activeBullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        // Time.deltaTime represents the time that passed since the last frame
        //the multiplication below ensures that GameObject moves constant speed every frame
        if (Input.GetKey(KeyCode.W)) {
            rb.linearVelocity += this.transform.forward * speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.S)) {
            rb.linearVelocity -= this.transform.forward * speed * Time.deltaTime;
        }

        // Quaternion returns a rotation that rotates x degrees around the x axis and so on
        if (Input.GetKey(KeyCode.D)) {
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        }
        else if (Input.GetKey(KeyCode.A)) {
            t.rotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);
        }


        // Handles jumps (only if the player is on the ground already)
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) {

            // if (isGrounded) {
            Debug.Log("Valid Jump");
            // }

            rb.AddForce(Vector3.up * force);
            isGrounded = false; // Player is no longer on the ground
        }

        // https://docs.unity3d.com/ScriptReference/Input.html
        if (Input.GetButtonDown("Fire1")) {

            // Ensure only maxBullets exist (for performance, and to make it look better; avoids old balls all over the scene)
            if (activeBullets.Count >= maxBullets) {
                // Destroy the oldest bullet and remove it from the list
                Destroy(activeBullets[0]);
                activeBullets.RemoveAt(0);
                Debug.Log("Max number reached; removed first ball in list");
            }

            GameObject newBullet = GameObject.Instantiate(bullet, cannon.transform.position, cannon.transform.rotation) as GameObject;
            newBullet.GetComponent<Rigidbody>().linearVelocity += Vector3.up * 2;
            newBullet.GetComponent<Rigidbody>().AddForce(newBullet.transform.forward * bulletSpead);

            // Add the new bullet to the list
            activeBullets.Add(newBullet);
            Debug.Log("Bullet Fired!");

        }

    }

    // This method is called when the player collides with the ground
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true; // Player is grounded
        }
    }
}