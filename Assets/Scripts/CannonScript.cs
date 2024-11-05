using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cannonscript : MonoBehaviour
{
    public List<GameObject> bombPrefabs;
    public Vector2 timeInterval = new Vector2(1, 1);
    public GameObject SpawnPoint;

    public GameObject target;

    public float rangeDegrees;
    public Vector2 force;
    public float arcDegrees = 45;
    public float cooldown;

 
    void Start()
    {
        cooldown = Random.Range(timeInterval.x, timeInterval.y);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.isGameOver) return;
        
        cooldown -= Time.deltaTime;

        if (cooldown < 0)
        {
            cooldown = Random.Range(timeInterval.x, timeInterval.y);

            Fire();
        }
    }

    private void Fire() {
        GameObject bombPrefab = bombPrefabs[Random.Range(0, bombPrefabs.Count)];
        GameObject bomb = Instantiate(bombPrefab, SpawnPoint.transform.position, bombPrefab.transform.rotation);


        Rigidbody bombRigidbody = bomb.GetComponent<Rigidbody>();
        Vector3 impulseVector = target.transform.position - SpawnPoint.transform.position;
        impulseVector.Scale(new Vector3(1, 0, 1));
        impulseVector.Normalize();
        impulseVector += new Vector3(0, arcDegrees / 45f, 0);
        impulseVector = Quaternion.AngleAxis(arcDegrees, Vector3.left) * impulseVector;
        impulseVector = Quaternion.AngleAxis(rangeDegrees * Random.Range(-1f, 1f), Vector3.up) * impulseVector;

        impulseVector *= Random.Range(force.x, force.y);
        bombRigidbody.AddForce(impulseVector, ForceMode.Impulse);
    }
}
