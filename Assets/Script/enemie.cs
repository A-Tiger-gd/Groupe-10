using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemie : MonoBehaviour
{
    private bool canMove = false;
    [HideInInspector] public bool canShoot = true;
    private Vector3 startPos = Vector3.zero;

    public GameObject[] repaires;
    public GameObject bulletEnemie;
    public GameObject spawnBulletLocation;
    public GameObject particul;
    public GameObject repaireEnemy;

    public float speed;
    public float speedLeft;

    public Vector3 direction;
    public float reset;
    public float timeMaxShoot;
    private float timeReset;
    private float timeShoot;
    private float timeShootSet;
    private GameObject player;
    public float playerRange = 1f;
    [Space]
    public Vector2 goStartPosition = Vector2.zero;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeShoot = Random.Range(1f, timeMaxShoot);
        timeReset = reset - Time.deltaTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (canShoot)
                ShootEnemie();

            EnemieMovement();
        }
    }

    
    public void EnemieMovement ()
    {
        if(timeReset >= reset)
        {
            direction.x = Random.Range(-1, 2);
            direction.y = Random.Range(-1, 2);
            timeReset = 0;
        }

        timeReset += Time.deltaTime;

        if (transform.position.x < player.transform.position.x + playerRange)
        {
            transform.position = new Vector2(transform.position.x + (speedLeft * Time.deltaTime), transform.position.y);
            direction.x = 1;
        }
        else
        {
            transform.position = transform.position + direction * speed * Time.deltaTime;

            if (transform.position.y > repaires[0].transform.position.y || transform.position.y < repaires[1].transform.position.y)
            {
                Vector3 nVec;
                nVec.x = transform.position.x;
                nVec.y = Mathf.Clamp(transform.position.y, repaires[1].transform.position.y, repaires[0].transform.position.y);
                nVec.z = transform.position.z;

                transform.position = nVec;
            }

            if(transform.position.x > repaireEnemy.transform.position.x)
            {
                Vector3 nVec;
                nVec.x = repaireEnemy.transform.position.x;
                nVec.y = transform.position.y;
                nVec.z = transform.position.z;

                transform.position = nVec;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    { 
        if (col.collider.gameObject.tag == "Bullet")
        {
            
            Destroy(col.gameObject);
            Instantiate(particul, transform.position, Quaternion.identity);
            Destroy(this.gameObject);   
        }
    }

    public void ShootEnemie()
    {
        timeShootSet += Time.deltaTime;

        if(timeShootSet>=timeShoot)
        {
            Instantiate(bulletEnemie, spawnBulletLocation.transform.position, Quaternion.identity);
            timeShoot = Random.Range(1f, timeMaxShoot);
            timeShootSet = 0;
        }
    }

    public void StartGoToStartPos()
    {
        StartCoroutine(GoToStartPos());
    }

    private IEnumerator GoToStartPos()
    {
        startPos = transform.position;

        while (Vector3.Distance(transform.position, startPos + (Vector3)goStartPosition) > 0.1f)
        {
            Vector3 nDir = (transform.position + (Vector3)goStartPosition - startPos).normalized;
            transform.position = transform.position + nDir * speed * 2f * Time.deltaTime;

            yield return null;
        }

        canMove = true;
        canShoot = true;
    }

    private void OnDrawGizmosSelected()
    {
        if (!canMove)
        {
            Gizmos.color = Color.white;
            Gizmos.DrawLine(transform.position, (Vector2)(startPos == Vector3.zero ? transform.position : startPos) + goStartPosition);
            Gizmos.DrawWireSphere((Vector2)(startPos == Vector3.zero ? transform.position : startPos) + goStartPosition, .25f);
        }
    }
}
