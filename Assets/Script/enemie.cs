using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    public GameObject[] repaires;

    public float speed;
    public float speedLeft;

    public bool inZoneTop;
    public bool inZoneBot;

    public int direction;
    public float reset;
    private float timeReset;

    private Vector2 ligneTop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemiesInZone();
        EnemieMovement();
    }

    public void EnemiesInZone()
    {
        ligneTop = repaires[0].transform.position - repaires[1].transform.position;

        if (Vector2.Dot(ligneTop, new Vector2(transform.position.x, transform.position.y) - new Vector2(repaires[1].transform.position.x, repaires[1].transform.position.y)) > 0)
            inZoneTop = true;
        else
            inZoneTop = false;

        if (Vector2.Dot(ligneTop, new Vector2(transform.position.x, transform.position.y) - new Vector2(repaires[0].transform.position.x, repaires[0].transform.position.y)) < 0)
            inZoneBot = true;
        else
            inZoneBot = false;
    }
    
    public void EnemieMovement ()
    {
        transform.position = new Vector2(transform.position.x - (speedLeft * Time.deltaTime), transform.position.y);

        if(timeReset >= reset)
        {
            direction = (int) Random.Range(0,3);
            timeReset = 0;
        }

        timeReset += Time.deltaTime; 

        if (direction == 0 && inZoneTop)
        {
            transform.position = new Vector2(transform.position.x , transform.position.y + (speed * Time.deltaTime));
        }

        if (direction == 1 && inZoneBot)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
        }

    }

    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.collider.gameObject.tag == "Bullet")
        {
            
            Destroy(col.gameObject);
            Destroy(this.gameObject);   
        }
    }
}
