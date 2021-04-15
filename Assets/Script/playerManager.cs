using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerManager : MonoBehaviour
{
    public int lifeMax;
    public int life;
    public int resource;
    public float timeLifeUp;
    public float timeSheeld;

    public GameObject deadPanel;
    public GameObject cam;
    public GameObject[] feedBackLife;
    public GameObject sheeldFeedBack;

    public GameObject sheeldNotReady;
    public GameObject sheeldReadyFB;
    public GameObject healNotReady;
    public GameObject healReadyFB;
    public GameObject healSpawn;

    private bool sheeld;
    private bool lifeDown;
    private bool healReady;
    private bool sheeldReady;
    private bool AnimHeal;
    private float timeAnimHeal;
    private float timePast = 0f;
    private float timeLifeUpSet = 0f;
    private float timeSheeldSet = 0f;
    public deplacement playerMove;

    // Start is called before the first frame update
    void Start()
    {
        life = lifeMax;
        sheeld = false;
    }

    // Update is called once per frame
    void Update()
    {
        Sheeld();
        FeedBackLifeManager();
        LifeUp();
        ShackCam();
        GestionAction();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!playerMove.startDash)
        {
            if(collision.collider.gameObject.tag == "Enemie" || collision.collider.gameObject.tag == "BulletEnemie")
            {
                Destroy(collision.gameObject);
                lifeDown = true;
                timeLifeUpSet = 0;
            
                if (sheeld)
                    sheeld = false;
                else
                    life--;
            }

            if(collision.gameObject.tag == "Resource")
            {
                resource++;
                Destroy(collision.gameObject);
            }
        }

    }

    public void LifeManager()
    {
        if(life==0)
        {
            Time.timeScale = 0;
            deadPanel.SetActive(true);
        }
    }

    public void ShackCam()
    {
        //Vector3 coCamStart = cam.transform.position;
        if(lifeDown)
        {
            timePast += Time.deltaTime;
            if(timePast <= 0.1f )
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y+(10*Time.deltaTime),-10);

            if (timePast <= 0.2f)
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - (10 * Time.deltaTime),-10);

            if (timePast <= 0.3f)
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y + (10 * Time.deltaTime),-10);
            
            if (timePast <= 0.4f)
                cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y - (5 * Time.deltaTime), -10);
            else
            {
                //cam.transform.position = coCamStart;
                timePast = 0;
                lifeDown = false;
            }
        }
    }

    public void FeedBackLifeManager()
    {
        for (int i = 0;i<life;i++)
            feedBackLife[i].SetActive(true);

        for(int i = lifeMax-1;i>life-1;--i)
            feedBackLife[i].SetActive(false);
        
        if(!lifeDown)
            LifeManager();
    }

    public void LifeUp()
    {
        if(life!=lifeMax)
        {
            timeLifeUpSet += Time.deltaTime;
            if (timeLifeUpSet >= timeLifeUp)
                healReady = true;
                         
            if(Input.GetKeyDown(KeyCode.DownArrow) && healReady)
            {
                AudioSource.PlayClipAtPoint(playerMove.sound.heal, transform.position);
                healReady = false;
                life++;
                timeLifeUpSet = 0;
                AnimHeal = true;
            }

        }

        if(AnimHeal == true)
        {
            timeAnimHeal += Time.deltaTime;
            if(timeAnimHeal>=1)
            {
                AnimHeal = false;
                timeAnimHeal = 0;
            }
        }
    }

    public void Sheeld()
    {
        sheeldFeedBack.SetActive(sheeld);
        if(!sheeld)
        {
            timeSheeldSet += Time.deltaTime;

            if (timeSheeldSet >= timeSheeld)
                sheeldReady = true;
               
            if(Input.GetKeyDown(KeyCode.LeftArrow))
            {
                AudioSource.PlayClipAtPoint(playerMove.sound.sheeld, transform.position);
                sheeld = true;
                sheeldReady = false;
                timeSheeldSet = 0;
            }              
        }
    }

    public void GestionAction()
    {
        healSpawn.SetActive(AnimHeal);
        healReadyFB.SetActive(healReady);
        healNotReady.SetActive(!healReady);
        sheeldReadyFB.SetActive(sheeldReady);
        sheeldNotReady.SetActive(!sheeldReady);
    }

}
