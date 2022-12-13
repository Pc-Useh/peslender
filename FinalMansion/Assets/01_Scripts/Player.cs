using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class Player : MonoBehaviour
{
    public float speed = 4;
    public Rigidbody rb;
    Vector2 dir = Vector3.zero;
    Inputs inputs;
    public int hp = 1;
    public int points = 0;
    public float timer = 0;
    public float timer2 = 0;
    public float timer3 = 0;
    public TMPro.TMP_Text uipoints;
    public Image enBar;
    public float energyRun = 10;
    public GameObject finalcanvas;
    public GameObject lightLamp;
    public GameObject finaldoor;
    public GameObject rawImage1;
    public bool uselamp = false;
    public AudioSource lanternSound;
    public AudioSource pickSound;
    public VideoPlayer videoplay;
    public GameObject canvastohide1;
    public GameObject canvastohide2;
    public float multiplierspeed = 1.5f;
    public float originalspeed = 4;
    public bool isrunning = false;
    public float runningseconds = 3;


    void Awake()
    {
        inputs = new Inputs();
        inputs.Player.Movement.performed += ctx => dir = ctx.ReadValue<Vector3>();
        inputs.Player.Movement.canceled += ctx => dir = Vector3.zero;
        inputs.Player.LightOn.performed += ctx => TurnLight();
        inputs.Player.Run.performed += ctx => TryRun();
        inputs.Player.Again.performed += ctx => FallBack();
    }

    void OnEnable()
    {
        inputs.Enable();
    }

    void OnDisable()
    {
        inputs.Disable();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        enBar.fillAmount = energyRun * 0.1f;
        uipoints.text = points.ToString();
        timer += Time.deltaTime;
        timer2 += Time.deltaTime;
        timer3 -= Time.deltaTime;

        if(isrunning.Equals(true))
        {    
            enBar.fillAmount = timer3 - 0.000000001f;
        }
        if(timer > 6)
        {
            rawImage1.SetActive(false);
        }
        if(timer2 > runningseconds)
        {
            speed = originalspeed;
            isrunning = false;
            enBar.fillAmount = 1f;
        }
    }

    void Move()
    {
        //float x = Input.GetAxis("Horizontal");
        //float y = Input.GetAxis("Vertical");
        rb.velocity = new Vector3(dir.x * speed, 0, dir.y * speed);
    }

    public void TakeDamage()
    {
        hp--;
        if(hp <= 0)
        {
            canvastohide2.SetActive(false);
            canvastohide1.SetActive(false);
            rawImage1.SetActive(true);
            videoplay.Play();
            timer = 0;
            finalcanvas.SetActive(true);

        }
    }

     public void TakePoint()
    {
        pickSound.Play();
        points++;

        if(points >= 10)
        {
            finaldoor.SetActive(true);

        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
        }

        if (collision.gameObject.CompareTag("Point"))
        {
            TakePoint();
        }

        if (collision.gameObject.CompareTag("Exit"))
        {
            ToEnd();
        }

        if (collision.gameObject.CompareTag("ExitApp"))
        {
            ToExit();
        }

        if (collision.gameObject.CompareTag("Restart"))
        {
            ToRestart();
        }
    }


    public void TurnLight()
    {
       if(uselamp == false)
       {
            lanternSound.Play();
            lightLamp.SetActive(true);
            uselamp = true;
       }
       else
       {
            lanternSound.Play();
            lightLamp.SetActive(false);
            uselamp = false;
       }
    }
    public void TryRun()
    {
        if(isrunning.Equals(false))
        {
            timer2 = 0;
            timer3 = 1;
            isrunning = true;
            speed = speed * multiplierspeed;
        }
    }

    public void minusRun()
    {
        
    }

    public void ToEnd()
    {
        finaldoor.SetActive(true);
    }

    public void ToExit()
    {
        Application.Quit();
    }

    public void ToRestart()
    {
        SceneManager.LoadScene("Game");
    }

    public void FallBack()
    {
        if(hp <= 0)
        {
            SceneManager.LoadScene("RStart");
        }
    }
}