using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour{

    [SerializeField] GameObject deathFX;
    [SerializeField] Transform parent;
    [SerializeField] int scorePerHit = 10;
    [SerializeField] int HP = 10;

    scoreBoard sb;

    // Start is called before the first frame update
    void Start(){
        Collider selfCollider = gameObject.AddComponent<BoxCollider>();
        selfCollider.isTrigger = false;
        // find the score board instance
        sb = FindObjectOfType<scoreBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // this is seperated one for onCollision
    private void OnParticleCollision(GameObject other) {
        // add the score
        sb.ScoreHit(scorePerHit);
        HP--;
        if(HP <= 0) {
            // create the explosive effect with no rotation
            GameObject temp = Instantiate(deathFX, transform.position, Quaternion.identity);
            temp.transform.parent = parent;
            print("enemy hit " + gameObject.name);
            Destroy(gameObject);
        }
        
    }
}
