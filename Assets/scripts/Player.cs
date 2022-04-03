using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Player : MonoBehaviour
{

    public float forceMultiplier = 3f;
    public float maximumVelocity = 3f;
    public ParticleSystem deathParticles;
    public GameObject mainVCam;
    public GameObject zoomVCam;

    private Rigidbody rb;
    private CinemachineImpulseSource cinemachineImpulseSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    // Update is called once per frame
    void Update()
    {
        var horizontalInput = Input.GetAxis("Horizontal");

        if(GetComponent<Rigidbody>().velocity.magnitude <= 5f)
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(horizontalInput * forceMultiplier * Time.deltaTime, 0, 0));
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Hazard"))
        {
            GameManager.GameOver();
            Destroy(gameObject);
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            cinemachineImpulseSource.GenerateImpulse();

            mainVCam.SetActive(false);
            zoomVCam.SetActive(true);
        }
    }


}
