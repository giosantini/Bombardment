using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public float ExplosionDelay = 5f;

    public GameObject ExplosionPrefab;

    public GameObject WoodBreakingPrefab;

    public float BlastRadius = 5f;
    public int BlastDamage = 10;
    
    void Start()
    {
        StartCoroutine(explosionCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator explosionCoroutine() {
        yield return new WaitForSeconds(ExplosionDelay);

        Explode();
    }

    private void Explode () {
        Instantiate(ExplosionPrefab, transform.position, ExplosionPrefab.transform.rotation);

        Collider[] colliders =  Physics.OverlapSphere(transform.position, BlastRadius);
        foreach(Collider collider in colliders) 
        {
            GameObject hitObject = collider.gameObject;
            if (hitObject.CompareTag("Platform"))
            {
                LifeScript lifeScript = hitObject.GetComponent<LifeScript>();
                if(lifeScript != null) {
                    
                    float distance = (hitObject.transform.position - transform.position).magnitude;
                    float distanceRate = Mathf.Clamp(distance / BlastRadius, 0, 1);
                    float damageRate = 1f - Mathf.Pow(distanceRate, 4);
                    int damage = (int) Mathf.Ceil(damageRate * BlastDamage);
                    // lifeScript.health -= BlastDamage;
                    if(lifeScript.health <= 0) {
                        Instantiate(WoodBreakingPrefab, hitObject.transform.position, WoodBreakingPrefab.transform.rotation);
                        Destroy(hitObject);
                    }
                }
                Destroy(hitObject);
            }
        }

        Destroy(gameObject);
    }
}
