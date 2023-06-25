using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterLevel7 : MonoBehaviour
{
    float horizontal = 0, vertical = 0;

    bool moveBool = true;
    bool firstQuestionBool = true;
    bool secondQuestionBool = true;
    bool secondBoolExtra = true;
    bool thirdQuestionBool = true;
    bool thirdExtraBool = true;
    bool thirdExtraBool2 = true;
    bool thirdExtraBool3 = true;
    bool fourthQuestionBool = true;
    bool fourthExtraBool = true;
    bool fourthExtraBool2 = true;
    bool jumpBool = true;

    Rigidbody physic;
    Animator animator;

    public Material door;

    public AudioClip[] sound;
    public AudioSource soundsPlay;

    public GameObject doorLight;
    public GameObject[] cubes;
    public GameObject[] questionPlanesObject;
    public GameObject[] questionField;

    public Button goBackMenu;
    public Button playAgain;
    public Button jumpButton;
    public Button continueButton;

    public Text firstText;
    public Text questionText;
    public Text correctAnswer;
    public Text WinText;

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

    IEnumerator objectOn()
    {
        if (firstQuestionBool)
        {
            soundsPlay.clip = sound[2];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[12];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[1];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[14];
            soundsPlay.Play();
            cubes[0].SetActive(true);
            cubes[1].SetActive(true);
            cubes[2].SetActive(true);
        }
        else if (!firstQuestionBool && secondQuestionBool)
        {
            yield return new WaitForSecondsRealtime(1);
            animator.SetBool("WictoryBool", false);
            Destroy(questionField[0].gameObject);
        }
        else if (!secondQuestionBool && thirdQuestionBool)
        {
            soundsPlay.clip = sound[3];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[12];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[2];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[14];
            soundsPlay.Play();
            cubes[3].SetActive(true);
            cubes[4].SetActive(true);
            cubes[5].SetActive(true);
        }
        else if (!secondBoolExtra)
        {
            yield return new WaitForSecondsRealtime(1);
            animator.SetBool("WictoryBool", false);
            Destroy(questionField[1].gameObject);
            yield return new WaitForSecondsRealtime(1);
            secondBoolExtra = true;
        }
        else if (!thirdExtraBool2)
        {
            soundsPlay.clip = sound[4];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[12];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[2];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[14];
            soundsPlay.Play();
            cubes[6].SetActive(true);
            cubes[7].SetActive(true);
            cubes[8].SetActive(true);

        }
        else if (!thirdExtraBool3)
        {

            yield return new WaitForSecondsRealtime(1);
            animator.SetBool("WictoryBool", false);
            Destroy(questionField[2].gameObject);
            yield return new WaitForSecondsRealtime(1);
            thirdExtraBool3 = true;
            thirdExtraBool = true;
            fourthQuestionBool = false;
        }
        else if (!fourthExtraBool)
        {
            soundsPlay.clip = sound[9];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[12];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[0];
            soundsPlay.Play();
            yield return new WaitForSecondsRealtime(1);
            soundsPlay.clip = sound[14];
            soundsPlay.Play();
            cubes[9].SetActive(true);
            cubes[10].SetActive(true);
            cubes[11].SetActive(true);

        }
        else if (!fourthExtraBool2)
        {
            yield return new WaitForSecondsRealtime(1);
            animator.SetBool("WictoryBool", false);
            Destroy(questionField[3].gameObject);
            door.color = Color.green;

            doorLight.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(1);
            fourthExtraBool2 = true;
            fourthQuestionBool = true;
        }



    }
    private void OnTriggerEnter(Collider collision)
    {
        if (firstQuestionBool)
        {
            if (collision.tag == "Trigger1")
            {
                firstText.gameObject.SetActive(false);
                questionText.text = "2 x 1 = ?";
                Destroy(questionPlanesObject[0]);
                StartCoroutine(objectOn());
            }
            if (collision.tag == "two")
            {
                Destroy(collision.gameObject);
                animator.SetBool("WictoryBool", true);
                soundsPlay.clip = sound[15];
                soundsPlay.Play();
                questionText.gameObject.SetActive(false);
                correctAnswer.gameObject.SetActive(true);
                firstQuestionBool = false;
                StartCoroutine(objectOn());
            }
            else if (collision.tag == "three")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
            else if (collision.tag == "four")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
        }
        if (!firstQuestionBool)
        {
            if (collision.tag == "Trigger2")
            {
                questionText.gameObject.SetActive(true);
                secondQuestionBool = false;
                correctAnswer.gameObject.SetActive(false);
                questionText.text = "3 x 2 = ?";
                Destroy(questionPlanesObject[1]);
                StartCoroutine(objectOn());
            }
            if (collision.tag == "three")
            {
                Destroy(collision.gameObject);
                animator.SetBool("WictoryBool", true);
                soundsPlay.clip = sound[15];
                soundsPlay.Play();
                questionText.gameObject.SetActive(false);
                correctAnswer.gameObject.SetActive(true);
                thirdQuestionBool = false;
                secondBoolExtra = false;
                StartCoroutine(objectOn());
                thirdExtraBool = false;
            }
            else if (collision.tag == "one")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
            else if (collision.tag == "four")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
        }
        if (!thirdExtraBool)
        {
            if (collision.tag == "Trigger3")
            {
                questionText.gameObject.SetActive(true);
                correctAnswer.gameObject.SetActive(false);
                questionText.text = "4 x 2 = ?";
                Destroy(questionPlanesObject[2]);
                thirdExtraBool2 = false;
                StartCoroutine(objectOn());
            }
            if (collision.tag == "seven")
            {
                Destroy(collision.gameObject);
                animator.SetBool("WictoryBool", true);
                soundsPlay.clip = sound[15];
                soundsPlay.Play();
                questionText.gameObject.SetActive(false);
                correctAnswer.gameObject.SetActive(true);
                thirdExtraBool3 = false;
                thirdExtraBool2 = true;

                StartCoroutine(objectOn());

            }
            else if (collision.tag == "five")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
            else if (collision.tag == "one")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
        }
        if (!fourthQuestionBool)
        {
            if (collision.tag == "Trigger4")
            {
                questionText.gameObject.SetActive(true);
                correctAnswer.gameObject.SetActive(false);
                questionText.text = "9 x 0 = ?";
                Destroy(questionPlanesObject[3]);
                fourthExtraBool = false;
                StartCoroutine(objectOn());
            }
            if (collision.tag == "six")
            {
                Destroy(collision.gameObject);
                animator.SetBool("WictoryBool", true);
                soundsPlay.clip = sound[15];
                soundsPlay.Play();
                questionText.gameObject.SetActive(false);
                WinText.gameObject.SetActive(true);
                fourthExtraBool = true;
                fourthExtraBool2 = false;
                StartCoroutine(objectOn());

            }
            else if (collision.tag == "five")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
            else if (collision.tag == "one")
            {
                animator.SetBool("sadBool", true);
                moveBool = false;
                playAgain.gameObject.SetActive(true);
                goBackMenu.gameObject.SetActive(true);
            }
        }
        if (collision.tag == "levelWall")
        {

            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(currentSceneIndex + 1);

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
