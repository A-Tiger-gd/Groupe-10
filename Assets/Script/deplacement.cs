using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement : MonoBehaviour
{
    [HideInInspector] public bool canMove = true;

    public GameObject player;
    public float speed;
    
    public GameObject cam;
    public GameObject[] repaires;
    public GameObject winRepaire;

    public GameObject bullet;
    public GameObject winPanel;

    public GameObject dashReady;
    public GameObject dashNotReady;

    public soudScript sound;

    private Animator anim;

    public float speedDash;
    public float resetDash;
    public bool startDash;

    private bool moveCam;
    [HideInInspector] public bool fixedCam = false;
    private bool endDash;

    private float timeDash = 0;
    private float timeDashReset = 0;
    private bool dash = true;
    private Vector2 ligneLeft;
    private Vector2 direction;
    private Vector2 lastDirection;
    private bool canShoot = true;

    private void Awake()
    {
        sound = FindObjectOfType<soudScript>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            YouAreWin();
            Move();
            NewDash();
            Dash();

            if (canShoot && Input.GetKeyDown(KeyCode.UpArrow))
            {
                StartCoroutine(Shooting());
            }

            GestionFBDash();

            if (!fixedCam)
            {
                CamMove();
            }
        }
        else
        {
            anim.SetFloat("Speed", 0f);
            anim.SetFloat("Horizontal", 0f);
        }
    }

    public void Move()
    {
        if (direction != Vector2.zero)
            lastDirection = direction;

        timeDash += Time.deltaTime;

        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
        direction.Normalize();

        Vector3 addVec = direction * speed * Time.deltaTime;
        transform.position += addVec;

        Vector3 clampVec = new Vector3(Mathf.Clamp(transform.position.x, repaires[1].transform.position.x, repaires[0].transform.position.x + 0.5f), Mathf.Clamp(transform.position.y, repaires[0].transform.position.y, repaires[2].transform.position.y));
        transform.position = clampVec;

        anim.SetFloat("Speed", direction.magnitude);
        anim.SetFloat("Horizontal", direction.x);
    }


    public void CamMove()
    {
        if(transform.position.x >= repaires[0].transform.position.x)
        {
            cam.transform.position += (Vector3)(Vector2.right * speed * Time.deltaTime);
            for(int i = 0; i < repaires.Length;++i)
            {
                repaires[i].transform.position = new Vector2(repaires[i].transform.position.x + (speed * Time.deltaTime), repaires[i].transform.position.y );
            }
        }
    }

    public void NewDash()
    {
        if(!dash)
        {
            timeDashReset += Time.deltaTime;
            if(timeDashReset>=resetDash)
            {
                timeDashReset = 0;
                dash = true;
            }
        }
    }

    public void Dash()
    {
        if (dash)
        {
            timeDash += Time.deltaTime;
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                startDash = true;
                AudioSource.PlayClipAtPoint(sound.dash, transform.position);
            }
                
            if (startDash)
            {
                player.transform.position += (Vector3)(lastDirection * speedDash * Time.deltaTime);

                Vector3 clampVec = new Vector3(Mathf.Clamp(transform.position.x, repaires[1].transform.position.x, repaires[0].transform.position.x + 0.5f), Mathf.Clamp(transform.position.y, repaires[0].transform.position.y, repaires[2].transform.position.y));
                transform.position = clampVec;

                anim.SetBool("isDashRight", true);

                if (timeDash > 0.3f)
                {
                    dash = false;
                    startDash = false;
                    anim.SetBool("isDashRight", false);
                }
            }
            else
            {
                timeDash = 0;
                endDash = true;
            }                          
        }
    }

    public void YouAreWin()
    {
        if (player.transform.position.x >= winRepaire.transform.position.x)
        {
            Time.timeScale = 0;
            canMove = false;
            winPanel.SetActive(true);
        }
    }

    IEnumerator Shooting()
    {
        canShoot = false;

        if (sound.shoot != null)
            AudioSource.PlayClipAtPoint(sound.shoot, transform.position);

        Instantiate(bullet, player.transform.position, Quaternion.identity);

        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(.4f);

        canShoot = true;
    }

    public void GestionFBDash()
    {
        dashReady.SetActive(dash);
        dashNotReady.SetActive(!dash);
    }
}

