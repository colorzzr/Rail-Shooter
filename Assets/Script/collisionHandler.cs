using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class collisionHandler : MonoBehaviour{
    [SerializeField] float deathLoadDelay = 1f;
    [SerializeField] GameObject deathFX;
    private void OnCollisionEnter(Collision collision) {
        print("Collide!!!!!!!!!!");
    }

    private void OnTriggerEnter(Collider other) {
        print("Triggerrrrrrrrrrrrrrrrrrrr!");
        deathProcess();
    }

    void deathProcess() {
        SendMessage("die");

        // active the explosive
        deathFX.SetActive(true);
        Invoke("reloadLevel", deathLoadDelay);
    }

    void reloadLevel() {
        SceneManager.LoadScene(1);
    }
}
