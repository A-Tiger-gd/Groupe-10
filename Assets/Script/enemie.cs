using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    public GameObject[] repaires;
    public GameObject bulletEnemie;

    public float speed;
    public float speedLeft;

    public bool inZoneTop;
    public bool inZoneBot;

    public int direction;
    public float reset;
    public float timeMaxShoot;
    private float timeReset;
    private float timeShoot;
    private float timeShootSet;
    private GameObject player;
    public bool back;

    private Vector2 ligneTop;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeShoot = Random.Range(1f, timeMaxShoot);
    }

    // Update is called once per frame
    void Update()
    {
        ShootEnemie();
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
        

        if(timeReset >= reset)
        {
            direction = (int) Random.Range(0,4);
            timeReset = 0;
        }

        timeReset += Time.deltaTime;

        if (Vector2.Dot(repaires[0].transform.position + new Vector3(transform.position.x, repaires[0].transform.position.y), new Vector2(transform.position.x, transform.position.y) - new Vector2(player.transform.position.x, repaires[0].transform.position.y)) < 0)// && Vector2.Dot(repaires[0].transform.position - new Vector3(transform.position.x, repaires[0].transform.position.y), new Vector2(transform.position.x, transform.position.y) - new Vector2(repaires[0].transform.position.x, repaires[0].transform.position.y)) > 0)
        {
            back = true;
            transform.position = new Vector2(transform.position.x + (speedLeft*2 * Time.deltaTime), transform.position.y);
        }
        else
            back = false;

        if (direction == 0 && inZoneTop )
            transform.position = new Vector2(transform.position.x, transform.position.y + (speed * Time.deltaTime));

        if (direction == 1 && inZoneBot )
            transform.position = new Vector2(transform.position.x, transform.position.y - (speed * Time.deltaTime));
        
        if(direction == 2 && !back)// || Vector2.Dot(repaires[0].transform.position - new Vector3(transform.position.x, repaires[0].transform.position.y), new Vector2(transform.position.x, transform.position.y) - new Vector2(repaires[0].transform.position.x, repaires[0].transform.position.y)) > 0 )
            transform.position = new Vector2(transform.position.x - (speedLeft * Time.deltaTime), transform.position.y);

        /*if(direction == 3 && !back/*&& Vector2.Dot(repaires[0].transform.position - new Vector3(transform.position.x, repaires[0].transform.position.y), new Vector2(transform.position.x, transform.position.y) - new Vector2(player.transform.position.x, repaires[0].transform.position.y)) > 0)
            transform.position = new Vector2(transform.position.x + (speedLeft * Time.deltaTime), transform.position.y);*/



    }

    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.collider.gameObject.tag == "Bullet")
        {
            
            Destroy(col.gameObject);
            Destroy(this.gameObject);   
        }
    }

    public void ShootEnemie()
    {
        
        timeShootSet += Time.deltaTime;

        if(timeShootSet>=timeShoot)
        {
            Instantiate(bulletEnemie, transform.position, Quaternion.identity);
            timeShoot = Random.Range(1f, timeMaxShoot);
            timeShootSet = 0;
        }
    }
}
