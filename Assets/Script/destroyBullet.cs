using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyBullet : MonoBehaviour
{
    public GameObject[] repaires;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Bullet"))
        {
            if (Vector2.Dot(repaires[0].transform.position - repaires[1].transform.position, new Vector2(element.transform.position.x, element.transform.position.y) - new Vector2(repaires[1].transform.position.x, repaires[1].transform.position.y)) <= 0)
            {
                if(element.tag == "Bullet")
                    Destroy(element);
            }
                
        }
    }
}
