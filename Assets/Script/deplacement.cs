using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deplacement : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public GameObject cam;
    public GameObject[] repaires;

    public GameObject bullet;

    public bool inZoneTop;
    public bool inZoneBot;
    public bool inZoneLeft;
    public bool moveCam;

    private Vector2 ligneTop;
    private Vector2 ligneLeft;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StayHere();
        Move();
        CamMove();
        if (Input.GetMouseButtonDown(0))
            Instantiate(bullet, player.transform.position, Quaternion.identity);
            
    }

    public void Move()
    {
        if(inZoneTop)
        {
            if (Input.GetKey(KeyCode.Z))
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y + (speed * Time.deltaTime));
        }
        

        if (Input.GetKey(KeyCode.D))
            player.transform.position = new Vector2(player.transform.position.x + (speed * Time.deltaTime), player.transform.position.y);

        if (inZoneBot)
        {
            if (Input.GetKey(KeyCode.S))
                player.transform.position = new Vector2(player.transform.position.x, player.transform.position.y - (speed * Time.deltaTime));
        }

        if(inZoneLeft)
        {
            if(Input.GetKey(KeyCode.Q))
                player.transform.position = new Vector2(player.transform.position.x - (speed * Time.deltaTime), player.transform.position.y);
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
}
