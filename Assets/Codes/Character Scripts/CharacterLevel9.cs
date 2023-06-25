using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterLevel9 : MonoBehaviour
{
    int numberEnu1;
    int numberEnu2;
    int numberEnu3;
    int processEnu;
    int processEnu2;
    int CorrectAnswerCounter=0;
    int HealthCounter = 3;

    float horizontal = 0, vertical = 0;

    string cubeString;

    bool moveBool = true;
    bool jumpBool = true;
    bool QuestionBool = false;
    bool Question1 = false;
    bool levelBool = true;

    Rigidbody physic;
    Animator animator;

    public Material door;

    public AudioClip[] sound;
    public AudioSource soundsPlay;

    public GameObject doorLight;
    public GameObject cubes;
    public GameObject cubes2;
    public GameObject trigger;
    public GameObject trigger2;
    public GameObject WoodFences;
    public GameObject WoodFences2;
    public GameObject closeWall;
    public GameObject Wall;
    public GameObject Wall2;
    public GameObject directionText;

    public Button goBackMenu;
    public Button playAgain;
    public Button jumpButton;
    public Button continueButton;

    public Text firstText;
    public Text questionText;
    public Text correctAnswer;
    public Text WinText;
    public Text wrongAnswer;
    public Text CorrectCounterText;
    public Text HealthText;
    public Text gameOverText;
    
    public Transform transformPostion;
    public Transform tranformPostion2;
    
    public VirtualJoystick joystick;



    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > PlayerPrefs.GetInt("whichLevel"))
        {
            PlayerPrefs.SetInt("whichLevel", SceneManager.GetActiveScene().buildIndex);
        }
        animator = GetComponent<Animator>();

        physic = GetComponent<Rigidbody>();

        door.color = Color.black;

        jumpButton.onClick.AddListener(JumpTime);
    }

    void Update()
    {
        if (HealthCounter==0)
        {
            
            gameOverText.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
            goBackMenu.gameObject.SetActive(true);
            moveBool = false;
            animator.SetBool("sadBool", true);

            HealthCounter = 3;

        }
        if (CorrectAnswerCounter==5)
        {
            if (!levelBool)
            {
                WinText.gameObject.SetActive(true);
                cubes2.gameObject.SetActive(false);
                doorLight.gameObject.SetActive(true);
                door.color = Color.green;
                Wall2.gameObject.SetActive(false);
            }
            else if (levelBool)
            {
                questionText.text = "SIRADAKINE GEC!";
                closeWall.gameObject.SetActive(false);
                cubes.gameObject.SetActive(false);
                directionText.gameObject.SetActive(true);
                levelBool = false;
            }
            
            CorrectAnswerCounter = 0;
            moveBool = true;
            animator.SetBool("WictoryBool", false);
        }
    }

    void FixedUpdate()
    {

        characterMove();

    }

    void characterMove()
    {
        if (moveBool)
        {
            horizontal = joystick.Horizontal();
            vertical = joystick.Vertical();

            Vector3 vec = new Vector3(horizontal, 0, vertical);
            physic.position += vec * Time.deltaTime * 4f;

            animator.SetFloat("Horizontal", horizontal);
            animator.SetFloat("Vertical", vertical);
        }
    }
    

    void JumpTime()
    {
        if (jumpBool)
        {
            physic.AddForce(0, 200, 0);
            animator.SetBool("Jump", true);
            jumpBool = false;
            StartCoroutine(jumpEnu());
        }
        else if (!jumpBool)
        {
            animator.SetBool("Jump", false);
        }
    }
    IEnumerator jumpEnu()
    {
        yield return new WaitForSecondsRealtime(1);
        jumpBool = true;
    }
    IEnumerator QuestionCounter()
    {
        soundsPlay.clip = sound[numberEnu1];
        soundsPlay.Play();
        yield return new WaitForSecondsRealtime(1);
        soundsPlay.clip = sound[processEnu];
        soundsPlay.Play();
        yield return new WaitForSecondsRealtime(1);
        soundsPlay.clip = sound[numberEnu2];
        soundsPlay.Play();
        yield return new WaitForSecondsRealtime(1);
        if (!levelBool)
        {
            soundsPlay.clip = sound[processEnu2];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[numberEnu3];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            WoodFences2.gameObject.SetActive(false);
        }
        soundsPlay.clip = sound[14];
        if (levelBool)
        {
            WoodFences.gameObject.SetActive(false);
        }
        
        soundsPlay.Play();
    }
    IEnumerator Transform()
    {
        yield return new WaitForSecondsRealtime(1);
        questionText.text = "3";
        yield return new WaitForSecondsRealtime(1);
        questionText.text = "2";
        yield return new WaitForSecondsRealtime(1);
        questionText.text = "1";
        yield return new WaitForSecondsRealtime(1);
        questionText.text = "";
        wrongAnswer.gameObject.SetActive(false);
        correctAnswer.gameObject.SetActive(false);
        if (!levelBool)
        {
            trigger2.gameObject.SetActive(true);
            WoodFences2.gameObject.SetActive(true);
            gameObject.transform.position = tranformPostion2.position;
        }
        else if (levelBool)
        {
            trigger.gameObject.SetActive(true);
            WoodFences.gameObject.SetActive(true);
            gameObject.transform.position = transformPostion.position;
        }
        
        
        moveBool = true;
        animator.SetBool("WictoryBool", false);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (Question1)
        {
            if (other.tag == cubeString)
            {
                questionText.text = "";
                CorrectAnswerCounter += 1;
                CorrectCounterText.text = "DOGRU: " + CorrectAnswerCounter;
                correctAnswer.gameObject.SetActive(true);
                moveBool = false;
                animator.SetBool("WictoryBool", true);
                soundsPlay.clip = sound[15];
                soundsPlay.Play();
                if (CorrectAnswerCounter != 5)
                {
                    StartCoroutine(Transform());
                }

                Question1 = false;
            }
            else 
            {
                questionText.text = "";
                HealthCounter -= 1;
                HealthText.text = "CAN: " + HealthCounter;
                wrongAnswer.gameObject.SetActive(true);
                if (HealthCounter != 0)
                {
                    StartCoroutine(Transform());
                }

                Question1 = false;
            }
        }
        if (other.tag=="Trigger1")
        {
            trigger.gameObject.SetActive(false);
            QuestionBool = true;

            if (QuestionBool)
            {
                int a=Random.Range(0,14);
                switch (a)
                {
                    case 0:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "1 + 3 = ?";
                        cubeString = "four";
                        numberEnu1 =1;
                        processEnu =10;
                        numberEnu2 =3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 1:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "5 - 2 = ?";
                        cubeString = "three";
                        numberEnu1 = 5;
                        processEnu = 11;
                        numberEnu2 = 2;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 2:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "6 + 3 = ?";
                        cubeString = "nine";
                        numberEnu1 = 6;
                        processEnu = 10;
                        numberEnu2 = 3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 3:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "2 x 2 = ?";
                        cubeString = "four";
                        numberEnu1 = 2;
                        processEnu = 12;
                        numberEnu2 = 2;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 4:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "8 ÷ 4 = ?";
                        cubeString = "two";
                        numberEnu1 = 8;
                        processEnu = 13;
                        numberEnu2 = 4;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 5:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "3 x 3 = ?";
                        cubeString = "nine";
                        numberEnu1 = 3;
                        processEnu = 12;
                        numberEnu2 = 3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 6:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "7 - 7 = ?";
                        cubeString = "zero";
                        numberEnu1 = 7;
                        processEnu = 11;
                        numberEnu2 = 7;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 7:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "2 + 6 = ?";
                        cubeString = "eight";
                        numberEnu1 = 2;
                        processEnu = 10;
                        numberEnu2 = 6;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 8:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "0 x 0 = ?";
                        cubeString = "zero";
                        numberEnu1 = 0;
                        processEnu = 12;
                        numberEnu2 = 0;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 9:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "4 + 5 = ?";
                        cubeString = "nine";
                        numberEnu1 = 4;
                        processEnu = 10;
                        numberEnu2 = 5;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 10:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "2 x 3 = ?";
                        cubeString = "six";
                        numberEnu1 = 2;
                        processEnu = 12;
                        numberEnu2 = 3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 11:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "9 - 4 = ?";
                        cubeString = "five";
                        numberEnu1 = 9;
                        processEnu = 11;
                        numberEnu2 = 4;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 12:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "4 + 0 = ?";
                        cubeString = "four";
                        numberEnu1 = 4;
                        processEnu = 10;
                        numberEnu2 = 0;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 13:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "6 ÷ 1 = ?";
                        cubeString = "six";
                        numberEnu1 = 6;
                        processEnu = 13;
                        numberEnu2 = 1;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 14:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "5 ÷ 5 = ?";
                        cubeString = "one";
                        numberEnu1 = 5;
                        processEnu = 13;
                        numberEnu2 = 5;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                }
            }
        }
        if (other.tag == "Trigger2")
        {
            trigger2.gameObject.SetActive(false);
            Wall.gameObject.SetActive(true);
            QuestionBool = true;

            if (QuestionBool)
            {
                int a = Random.Range(0, 14);
                switch (a)
                {
                    case 0:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "2 + 1 - 3 = ?";
                        cubeString = "zero";
                        numberEnu1 = 2;
                        processEnu = 10;
                        numberEnu2 = 1;
                        processEnu2 = 11;
                        numberEnu3 = 3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 1:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "2 x 4 + 1 = ?";
                        cubeString = "nine";
                        numberEnu1 = 2;
                        processEnu = 12;
                        numberEnu2 = 4;
                        processEnu2 = 10;
                        numberEnu3 = 1;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 2:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "9 ÷ 3 + 6 = ?";
                        cubeString = "nine";
                        numberEnu1 = 9;
                        processEnu = 13;
                        numberEnu2 = 3;
                        processEnu2 = 10;
                        numberEnu3 = 6;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 3:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "9 x 9 x 0 = ?";
                        cubeString = "zero";
                        numberEnu1 = 9;
                        processEnu = 12;
                        numberEnu2 = 9;
                        processEnu2 = 12;
                        numberEnu3 = 0;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 4:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "7 - 4 + 5 = ?";
                        cubeString = "eight";
                        numberEnu1 = 7;
                        processEnu = 11;
                        numberEnu2 = 4;
                        processEnu2 = 10;
                        numberEnu3 = 5;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 5:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "3 x 1 ÷ 3 = ?";
                        cubeString = "one";
                        numberEnu1 = 3;
                        processEnu = 12;
                        numberEnu2 = 1;
                        processEnu2 = 13;
                        numberEnu3 = 3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 6:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "7 - 7 + 6 = ?";
                        cubeString = "six";
                        numberEnu1 = 7;
                        processEnu = 11;
                        numberEnu2 = 7;
                        processEnu2 = 10;
                        numberEnu3 = 6;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 7:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "5 - 3 - 2 = ?";
                        cubeString = "zero";
                        numberEnu1 = 5;
                        processEnu = 11;
                        numberEnu2 = 3;
                        processEnu2 = 11;
                        numberEnu3 = 2;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 8:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "0 x 0 + 1 = ?";
                        cubeString = "one";
                        numberEnu1 = 0;
                        processEnu = 12;
                        numberEnu2 = 0;
                        processEnu2 = 10;
                        numberEnu3 = 1;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 9:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "4 + 5 - 2 = ?";
                        cubeString = "seven";
                        numberEnu1 = 4;
                        processEnu = 10;
                        numberEnu2 = 5;
                        processEnu2 = 11;
                        numberEnu3 = 2;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 10:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "2 x 3 ÷ 3 = ?";
                        cubeString = "two";
                        numberEnu1 = 2;
                        processEnu = 12;
                        numberEnu2 = 3;
                        processEnu2 = 13;
                        numberEnu3 = 3;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 11:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "9 - 4 + 4 = ?";
                        cubeString = "nine";
                        numberEnu1 = 9;
                        processEnu = 11;
                        numberEnu2 = 4;
                        processEnu2 = 10;
                        numberEnu3 = 4;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 12:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "4 x 2 + 0 = ?";
                        cubeString = "eight";
                        numberEnu1 = 4;
                        processEnu = 12;
                        numberEnu2 = 2;
                        processEnu2 = 10;
                        numberEnu3 = 0;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 13:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "6 ÷ 6 + 1 = ?";
                        cubeString = "two";
                        numberEnu1 = 6;
                        processEnu = 13;
                        numberEnu2 = 6;
                        processEnu2 = 10;
                        numberEnu3 = 1;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                    case 14:
                        firstText.gameObject.SetActive(false);
                        questionText.text = "0 - 0 x 5 = ?";
                        cubeString = "zero";
                        numberEnu1 = 0;
                        processEnu = 11;
                        numberEnu2 = 0;
                        processEnu2 = 12;
                        numberEnu3 = 5;
                        StartCoroutine(QuestionCounter());
                        Question1 = true;
                        QuestionBool = false;
                        break;
                }
            }
        }
        if (other.tag == "levelWall")
        {  
            SceneManager.LoadScene(0);
        }
    }

    public void PlayAgainButton()
    {
        Time.timeScale = 1;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void goMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }
    public void Options()
    {
        Time.timeScale = 0;
        playAgain.gameObject.SetActive(true);
        goBackMenu.gameObject.SetActive(true);
        continueButton.gameObject.SetActive(true);
    }
    public void continuhne()
    {
        Time.timeScale = 1;
        playAgain.gameObject.SetActive(false);
        goBackMenu.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
    }
}
