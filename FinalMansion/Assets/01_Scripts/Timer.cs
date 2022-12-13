using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float ran = -1;
    public float ran2 = 40;
    public float timer = 0;
    public float timeToSpawn = 5f;
    public GameObject enemyPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if(timer < 0)
        {
            timer = -5f;
        }

        if(timer >= timeToSpawn)
        {
            Vector3 newPos = new Vector3(ran, 0.5f, ran2);

            Instantiate(enemyPrefab, newPos, Quaternion.identity);
            timer = -5f;
        }
    }
}
