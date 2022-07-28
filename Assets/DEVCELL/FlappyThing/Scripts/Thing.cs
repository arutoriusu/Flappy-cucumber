using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Thing : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] GameObject leftVector;
    [SerializeField] GameObject rightVector;

    [SerializeField] GameObject leftFang;
    [SerializeField] GameObject rightFang;
    [SerializeField] GameObject backLeftFang;
    [SerializeField] GameObject backRightFang;

    [SerializeField] GameObject deathFX;
    [SerializeField] GameObject music;

    [SerializeField] GameObject soundsHit;
    [SerializeField] AudioClip[] flipSounds;
    [SerializeField] Transform projectorTransform;
    [SerializeField] GameObject startRoom;

    [SerializeField] GameObject continueOrRestart;
    [SerializeField] GameObject soundsScript;
    [SerializeField] GameObject bonusScore;
    [SerializeField] GameObject modelCucumber;
    [SerializeField] GameObject pauseButton;
    [SerializeField] GameObject tutorial;
    [SerializeField] GameObject flappyAnalytics;

    int angleLeft = -25;
    int angleRight = -25;
    private bool leftWingActivate = false;
    private bool rightWingActivate = false;
    private bool lWasPressed;
    private bool rWasPressed;

    bool isDestroyed = false;

    public float rotForce = 1;
    public float upForce = 3f;
    public float yVelocityLimit = 8;
    public float maxAngularVel;
    
    private float lastSoundTime;

    private bool god;
    private float godTime = 2f;
    private float resurrectionTime;
    private float blinkingTime = 0;
    private float gameTime;
    // переменная deathTime 
    double deathTime = 0f;

    [SerializeField] int endgameDelay;

    public bool God { get => god; set => god = value; }

    void Start()
    {
        gameTime = Time.time;
        resurrectionTime = Time.time;
        rb = GetComponent<Rigidbody>();
        if (!rb) return;
        rb.maxAngularVelocity = maxAngularVel;

        AddForce(1);
        AddForce(2);
        rb.angularVelocity = new Vector3(0, 0, rb.angularVelocity.z + 0.1f);
    }

    private void OnDestroy()
    {
        flappyAnalytics.GetComponent<FlappyAnalytics>().GamingTime(Time.time-gameTime);
    }

    private void NoGod()
    {
        God = false;
        modelCucumber.SetActive(true);
    }

    private void Blinking()
    {
        if (modelCucumber.gameObject.activeSelf)
        {
            modelCucumber.SetActive(false);
        }
        else
        {
            modelCucumber.SetActive(true);
        }
    }

    void Update()
    {
        if (God)
        {
            if (Time.time >= blinkingTime + 0.1f)
            {
                blinkingTime = Time.time;
                Blinking();
            }
            
            if (Time.time > resurrectionTime + godTime)
            {
                NoGod();
            }
            if (resurrectionTime + 0.1f > Time.time)
            {
                modelCucumber.SetActive(true);
            }
        }

        if (deathTime != 0)
        {
            pauseButton.GetComponent<Button>().interactable = false;
        }

        if (deathTime + endgameDelay <= DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds 
            && deathTime != 0)
        {
            music.SetActive(false);
            if (gameObject.name == "Thing")
            {
                continueOrRestart.SetActive(true);
            }
            else
            {
                SceneManager.LoadScene(2);
            }
            deathTime = 0;
        }


        projectorTransform.rotation = Quaternion.Euler(90, 0, 0);
        Vector3 worldConversion = Camera.main.WorldToScreenPoint(transform.position);

        if (worldConversion.y > Screen.height)
        {
            deathTime = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;

            PlayerPrefs.SetInt("countCollisions", PlayerPrefs.GetInt("countCollisions") + 1);
            isDestroyed = true;
            soundsHit.GetComponent<SoundsScript>().playHit();
            flappyAnalytics.GetComponent<FlappyAnalytics>().deathOfThing();
            Destroy(gameObject, 0.1f);

        }

        if (worldConversion.y > Screen.height-(Screen.height/20))
        {
            rb.AddForce(Vector3.down * 7f);
        }

        // счетчик касаний
        int i = 0;
        
        // если игра не на паузе
        if (Time.timeScale != 0)
        {
            if (Input.touchCount > 1)
            {
                flappyAnalytics.GetComponent<FlappyAnalytics>().DoubleTap();
            }
            else if(Input.touchCount == 1)
            {
                flappyAnalytics.GetComponent<FlappyAnalytics>().SingleTap();
            }
            // в цикле обрабатываем все нажатия на экран
            while (i < Input.touchCount)
            {
                // обрабатываем касание под номером i
                // если палец на месте, или двигается,
                // или палец только коснулся экрана
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {

                    if (Input.GetTouch(i).position.x > Screen.width / 2)
                    {
                        leftWingActivate = true;
                        rWasPressed = true;
                        if (PlayerPrefs.GetString("sound") == "true")
                        {
                            int randSwingSoung = 0;// UnityEngine.Random.Range(0, 3);
                            AudioSource.PlayClipAtPoint(flipSounds[randSwingSoung], Camera.main.transform.position);
                        }
                    }

                    if (Input.GetTouch(i).position.x < Screen.width / 2)
                    {
                        rightWingActivate = true;
                        lWasPressed = true;
                        if (PlayerPrefs.GetString("sound") == "true")
                        {
                            int randSwingSoung = 0;// UnityEngine.Random.Range(0, 3);
                            AudioSource.PlayClipAtPoint(flipSounds[randSwingSoung], Camera.main.transform.position);
                        }
                    }
                }
                ++i;
            }
        }
        

        if (Input.GetKeyDown(KeyCode.X))
        {
            leftWingActivate = true;
            rWasPressed = true;

            if (PlayerPrefs.GetString("sound") == "true")
            {
                int randSwingSoung = 0;// UnityEngine.Random.Range(0, 3);
                AudioSource.PlayClipAtPoint(flipSounds[randSwingSoung], Camera.main.transform.position);
            }
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            rightWingActivate = true;
            lWasPressed = true;
            
            if (PlayerPrefs.GetString("sound") == "true")
            {
                int randSwingSoung = 0;// UnityEngine.Random.Range(0, 3);
                AudioSource.PlayClipAtPoint(flipSounds[randSwingSoung], Camera.main.transform.position);
            }
        }

        if (leftWingActivate)
        {
            swingDown(angleLeft, "left");
        }

        if (rightWingActivate)
        {
            swingDown(angleRight, "right");
        }
    }

    private void FixedUpdate()
    {
        if (rb.rotation.eulerAngles.z > 35 && rb.rotation.eulerAngles.z < 325)
        {
            if (Time.time > lastSoundTime + 2.0f)
            {
                soundsHit.GetComponent<SoundsScript>().playRollover();
                lastSoundTime = Time.time;
            }
        }

        if (lWasPressed && rWasPressed) //если обе кнопки были нажаты в апдейте, то сбрасываем их и передаем в адд форс 0
        {
            AddForce(0);
            lWasPressed = false;
            rWasPressed = false;
        }
        else
        {
            if (lWasPressed && !rWasPressed) //если левая кнопка была нажата в апдейте, а правая нет, то сбрасываем левую и передаем в адд форс 1
            {
                AddForce(1);
                lWasPressed = false;
            }
            else if (!lWasPressed && rWasPressed) //если правая кнопка была нажата в апдейте, а левая нет, то сбрасываем правую и передаем в адд форс 2
            {
                AddForce(2);
                rWasPressed = false;
            }
        }

        if (angleLeft > -25 && !leftWingActivate)
        {
            angleLeft -= 9;
            
            leftFang.transform.localRotation = Quaternion.Euler(-angleLeft, 180, 0);
            backLeftFang.transform.localRotation = Quaternion.Euler(angleLeft, 180, 0);
        }

        if (angleRight > -25 && !rightWingActivate)
        {
            angleRight -= 9;
            
            rightFang.transform.localRotation = Quaternion.Euler(-angleRight, 180, 0);
            backRightFang.transform.localRotation = Quaternion.Euler(angleRight, 180, 0);
        }
    }

    void AddForce(int cond)
    {
        float upForceMod = 1;
        if (cond == 0)
        {
            if (rb.velocity.y < 0)
            {
                upForceMod = 2;
            }
        }
        rb.velocity = rb.velocity + transform.up * upForce * upForceMod; //раз мы сюда попали, значит хоть одна кнопка была нажата и задаем толчек по направлению объекта в сторону своей верхушки
        if (rb.velocity.y > yVelocityLimit) rb.velocity = new Vector3(0, yVelocityLimit, 0); //ограничиваем ускорение вверх по оси y, а то слишком разгоняется

        //if (cond == 0) //при cond == 0 ничего не делаем, но ее все равно получаем, иначе не будет работать толчек в сторону верхушки

        //отказался от всех шариков и векторов и просто стал вращать объект, так проще и круче
        if (cond == 1)
            rb.angularVelocity = new Vector3(0, 0, rb.angularVelocity.z - rotForce); //вращаем влево
        else if (cond == 2)
            rb.angularVelocity = new Vector3(0, 0, rb.angularVelocity.z + rotForce); //вращаем вправо
    }

    private void swingDown(int angle, string wing)
    {
        if (angle < 80)
        {
            if (wing == "left")
            {
                angleLeft += 10;
                leftFang.transform.localRotation = Quaternion.Euler(-angleRight, 180, 0);
                backLeftFang.transform.localRotation = Quaternion.Euler(angleRight, 180, 0);
            } else if (wing == "right")
            {
                angleRight += 10;
                rightFang.transform.localRotation = Quaternion.Euler(-angleRight, 180, 0);
                backRightFang.transform.localRotation = Quaternion.Euler(angleRight, 180, 0);
            }
            return;
        }
        
        if (wing == "left")
        {
            leftWingActivate = false;
        } else if (wing == "right")
        {
            rightWingActivate = false;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (!god || (god && collision.gameObject.CompareTag("Ground") == true))
        {
            if (collision != null && !isDestroyed && collision.gameObject.CompareTag("Star") == false)
            {
                int countCollisions;
                if (collision.gameObject != null)
                {
                    deathTime = DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalMilliseconds;
                    God = false;
                    countCollisions = PlayerPrefs.GetInt("countCollisions");
                    PlayerPrefs.SetInt("countCollisions", countCollisions + 1);
                    isDestroyed = true;
                    Instantiate(deathFX, gameObject.transform.position, gameObject.transform.rotation);
                    soundsHit.GetComponent<SoundsScript>().playHit();
                    modelCucumber.SetActive(false);
                    tutorial.SetActive(false);
                    flappyAnalytics.GetComponent<FlappyAnalytics>().deathOfThing();
                    Destroy(gameObject, 2f);
                }
            }
        }
        else
        {
            Physics.IgnoreCollision(gameObject.GetComponent<Collider>(), collision.gameObject.GetComponent<Collider>());
        }

        if (collision.gameObject.CompareTag("Star") == true)
        {
            bonusScore.SetActive(true);
            bonusScore.GetComponent<BonusScore>().BonusScoreShowStartTime = Time.time;
        }
        
    }
}
