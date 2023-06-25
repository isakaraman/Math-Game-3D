using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class CharacterLevel4 : MonoBehaviour
{
    float horizontal = 0, vertical = 0;
    float numbers = -1f;

    bool moveBool = true;
    bool jumpBool = true;

    Rigidbody physic;
    Animator animator;

    public Text oneToNine;
    public Text numbersText;
    public Text finishText;
    public Text wrongAnswer;

    public Material door;

    public AudioClip[] sound;
    public AudioSource soundsPlay;

    public GameObject doorLight;

    public Button goBackMenu;
    public Button playAgain;
    public Button jumpButton;
    public Button continueButton;
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

    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "zero")
        {
            if (numbers == -1)
            {
                numbers = 0;
                numbersText.text = "SAYILAR: " + numbers;
                Destroy(oneToNine);
                soundsPlay.clip = sound[0];
                soundsPlay.Play();

                Destroy(col.gameObject);
            }
            else
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                wrongAnswer.gameObject.SetActive(true);
                Destroy(oneToNine);
                goBackMenu.gameObject.SetActive(true);
                playAgain.gameObject.SetActive(true);


            }

        }
        else if (col.tag == "three")
        {
            if (numbers == 0)
            {
                numbers = 3;
                numbersText.text = "SAYILAR: " + numbers;
                soundsPlay.clip = sound[1];
                soundsPlay.Play();
                Destroy(col.gameObject);
            }
            else
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                wrongAnswer.gameObject.SetActive(true);
                Destroy(oneToNine);
                goBackMenu.gameObject.SetActive(true);
                playAgain.gameObject.SetActive(true);
            }
        }

        else if (col.tag == "six")
        {
            if (numbers == 3)
            {
                numbers = 6;
                numbersText.text = "SAYILAR: " + numbers;
                soundsPlay.clip = sound[2];
                soundsPlay.Play();
                Destroy(col.gameObject);
            }
            else
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                wrongAnswer.gameObject.SetActive(true);
                Destroy(oneToNine);
                goBackMenu.gameObject.SetActive(true);
                playAgain.gameObject.SetActive(true);
            }
        }

        else if (col.tag == "nine")
        {
            if (numbers == 6)
            {
                numbers = 9;
                numbersText.text = "SAYILAR: " + numbers;
                soundsPlay.clip = sound[3];
                soundsPlay.Play();
                Destroy(col.gameObject);


                finishText.gameObject.SetActive(true);

                door.color = Color.green;

                doorLight.gameObject.SetActive(true);
            }
            else
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                wrongAnswer.gameObject.SetActive(true);
                Destroy(oneToNine);
                goBackMenu.gameObject.SetActive(true);
                playAgain.gameObject.SetActive(true);
            }
        }
        else if (col.tag == "levelWall")
        {
            if (numbers == 9)
            {

                int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(currentSceneIndex + 1);
            }

        }
        else
        {
            animator.SetBool("sadBool", true);
            moveBool = false;
            wrongAnswer.gameObject.SetActive(true);
            Destroy(oneToNine);
            goBackMenu.gameObject.SetActive(true);
            playAgain.gameObject.SetActive(true);
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
