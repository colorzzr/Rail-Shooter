using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class playerShip : MonoBehaviour{

    [Tooltip("m/deltatime")][SerializeField] float Speed = 4f;
    [SerializeField] float xRange = 2f;
    [SerializeField] float yRange = 2f;

    [SerializeField] float posPitchFactor = -5f;
    [SerializeField] float posYawFactor = -5f;
    [SerializeField] float controllPitchFactor = -10f;
    [SerializeField] float controllYawFactor = -10f;

    // the intensity we press the bottom
    float hz, vt;
    float initx, inity;

    // Start is called before the first frame update
    void Start()
    {
        initx = transform.localPosition.x;
        inity = transform.localPosition.y;
    }

    // Update is called once per frame
    void Update() {
        posControll();
        rotationControll();
    }

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
}
