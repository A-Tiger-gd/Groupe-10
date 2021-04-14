using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    
    public GameObject cam;
    public GameObject[] repaires;
    public GameObject winRepaire;

    public GameObject bullet;
    public GameObject winPanel;

    public float speedDash;
    public float resetDash;

    private bool inZoneTop;
    private bool inZoneBot;
    private bool inZoneLeft;
    private bool moveCam;

    private bool top = false;
    private bool bot = false;
    private bool left = false;
    private bool right = true;

    private float timeDash = 0;
    private float timeDashReset = 0;
    private bool dash = true;
    private bool startDash;
    private bool endDash = false;
    private Vector2 ligneTop;
    private Vector2 ligneLeft;

    // Start is called before the first frame update
    void Start()
    {
        //dash = true;
    }

    // Update is called once per frame
    void Update()
    {
        YouAreWin();
        StayHere();
        Move();
        CamMove();
        NewDash();
        Dash();
        
        if (Time.timeScale == 1)
        {
            if (Input.GetMouseButtonDown(0))
                Instantiate(bullet, player.transform.position, Quaternion.identity);
        }
    }

    public void Move()
    {
        timeDash += Time.deltaTime;

        if(inZoneTop)
        {
            if (Input.GetKey(KeyCode.Z))
            {
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + (speed * Time.deltaTime));
                left = false;
                right = false;
                top = true;
                bot = false;
            }
                
        }
        
        if (Input.GetKey(KeyCode.D))
        {
            player.transform.position = new Vector2(player.transform.position.x + (speed * Time.deltaTime), player.transform.position.y);
            left = false;
            right = true;
            top = false;
            bot = false;
        }
            

        if (inZoneBot)
        {
            if (Input.GetKey(KeyCode.S))
            {
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - (speed * Time.deltaTime));
                left = false;
                right = false;
                top = false;
                bot = true;
            }
                
        }

        if(inZoneLeft)
        {
            if(Input.GetKey(KeyCode.Q))
            {
                player.transform.position = new Vector2(player.transform.position.x - (speed * Time.deltaTime), player.transform.position.y);
                left = true;
                right = false;
                top = false;
                bot = false;
            }
                
        }
        
    }

    public void StayHere()
    {        
        ligneTop = repaires[0].transform.position - repaires[2].transform.position;
        ligneLeft = repaires[1].transform.position - repaires[0].transform.position;

        if ( Vector2.Dot(ligneTop, new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2 (repaires[2].transform.position.x, repaires[2].transform.position.y)) > 0)
            inZoneTop = true;
        else
            inZoneTop = false;

        if (Vector2.Dot(ligneTop, new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(repaires[0].transform.position.x, repaires[0].transform.position.y)) < 0)
            inZoneBot = true;
        else
            inZoneBot = false;

        if (Vector2.Dot(ligneLeft, new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(repaires[1].transform.position.x, repaires[1].transform.position.y)) < 0)
            inZoneLeft = true;
        else
            inZoneLeft = false;

        if (Vector2.Dot(ligneLeft, new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(repaires[0].transform.position.x, repaires[0].transform.position.y)) <= 0)
            moveCam = true;
        else
            moveCam = false;
    }

    public void CamMove()
    {
        if(moveCam)
        {
            cam.transform.position = new Vector3(cam.transform.position.x + (speed * Time.deltaTime), cam.transform.position.y,-10 );
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
            if (Input.GetKeyDown(KeyCode.Space))
                startDash = true;

            if (startDash)
            {
                if (inZoneLeft)
                {
                    if (left)
                        player.transform.position = new Vector2(player.transform.position.x - (speedDash * Time.deltaTime), player.transform.position.y);
                }

                if (right)
                    player.transform.position = new Vector2(player.transform.position.x + (speedDash * Time.deltaTime), player.transform.position.y);

                if (inZoneTop)
                {
                    if (top)
                        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + (speedDash * Time.deltaTime));
                }

                if (inZoneBot)
                {
                    if (bot)
                        player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - (speedDash * Time.deltaTime));
                }

                if (timeDash > 0.3f)
                {
                    dash = false;
                    startDash = false;
                }
            }
            else
            {
                timeDash = 0;
                endDash = true;
                //startDash = false;
            }
                           
        }
    }

    public void YouAreWin()
    {
        if (Vector2.Dot(ligneLeft, new Vector2(player.transform.position.x, player.transform.position.y) - new Vector2(winRepaire.transform.position.x, winRepaire.transform.position.y)) < 0)
        {
            Time.timeScale = 0;
            winPanel.SetActive(true);
        }
    }
}

