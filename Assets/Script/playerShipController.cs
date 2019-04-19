using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerShipController : MonoBehaviour{

    [Header("General")]
    [Tooltip("m/deltatime")][SerializeField] float Speed = 4f;
    [SerializeField] float xRange = 4f;
    [SerializeField] float yRange = 2f;

    [Header("Screen Postion")]
    [SerializeField] float posPitchFactor = -5f;
    [SerializeField] float posYawFactor = 10f;

    [Header("Controll Throw")]
    [SerializeField] float controllPitchFactor = -10f;
    [SerializeField] float controllYawFactor = -10f;

    [Header("Gun Controll")]
    [SerializeField] GameObject[] guns;

    // the intensity we press the bottom
    float hz, vt;
    float initx, inity;
    bool enableControl = true;

    // Start is called before the first frame update
    void Start()
    {
        initx = transform.localPosition.x;
        inity = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update() {
        if (enableControl) {
            posControll();
            rotationControll();
            proccesFire();
        }
    }

    private void proccesFire() {
        if (CrossPlatformInputManager.GetButton("Fire1")) {
            foreach (GameObject gun in guns) gun.SetActive(true);
        }
        else {
            foreach (GameObject gun in guns) gun.SetActive(false);
        }
    }

    //private void OnCollisionEnter(Collision collision) {
    //    print("Collide!!!!!!!!!!");
    //}

    //private void OnTriggerEnter(Collider other) {
    //    print("Triggerrrrrrrrrrrrrrrrrrrr!");
    //}

    private void rotationControll() {
        // right hand rule x y z
        float pitch = (transform.localPosition.y) * posPitchFactor + controllPitchFactor * vt;
        float yaw = (transform.localPosition.x) * posYawFactor;
        float roll = controllYawFactor * hz;

        transform.localRotation = Quaternion.Euler(pitch, yaw, roll);
    }

    private void posControll() {
        hz = CrossPlatformInputManager.GetAxis("Horizontal");
        vt = CrossPlatformInputManager.GetAxis("Vertical");

        float xOffset = hz * Speed * Time.deltaTime;
        float yOffset = vt * Speed * Time.deltaTime;

        // set up the range of movement
        float newXpos = Mathf.Clamp(transform.localPosition.x + xOffset, -xRange, xRange);
        float newYpos = Mathf.Clamp(transform.localPosition.y + yOffset, -yRange, yRange);

        // change the relative position of the ship
        transform.localPosition = new Vector3(newXpos, newYpos, transform.localPosition.z);
    }

    void die() {
        print("YOU DIE!!");
        enableControl = false;
    }
}
