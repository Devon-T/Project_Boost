using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Rocket : MonoBehaviour {

    [SerializeField] float rcsthrust = 250f;
    [SerializeField] float mainTrust = 100f;
    [SerializeField] float levelChangeDelay = 2f;

    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip death;
    [SerializeField] AudioClip Levelup;

    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] ParticleSystem LevelupParticle;

    Rigidbody rigidBody;
    AudioSource audioSource;
    int sceneID;
    int currentScene = 0;

    
    enum State {Alive, Dying, Transcending}
    State state = State.Alive;

	// Use this for initialization
	void Start () {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        if( state == State.Alive)
        {
            respondToThrustInput();
            respondToRotateInput();
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        if (state != State.Alive) { return; } //if dead return
        audioSource.Stop();
        Rigidbody rigidbody;
        switch (collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Finish":
                state = State.Transcending;
                audioSource.PlayOneShot(Levelup);
                LevelupParticle.Play();
                Invoke("LoadNextLevel", levelChangeDelay); // parameterise time
                break;
            default:
                state = State.Dying;
                audioSource.PlayOneShot(death);
                deathParticle.Play();
                Invoke("LoadFirstLevel", levelChangeDelay);
                break;
        }
    }

    private void LoadFirstLevel()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(sceneID);
    }

    private void LoadNextLevel()
    {
        int Levels = SceneManager.sceneCountInBuildSettings;
        sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID < Levels)
        {
            SceneManager.LoadScene(sceneID + 1); // todo fix error for last level finish
        } else
        {
            SceneManager.LoadScene(0);
        }

    }

    private void respondToThrustInput()
    {
        float thrustThisFrame = mainTrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.Space)) //can thrust while rotating
        {
            Thrusting(thrustThisFrame);
        }
        else
        {
            audioSource.Stop();
            mainEngineParticle.Stop();
        }
    }

    private void Thrusting(float thrustThisFrame)
    {
        rigidBody.AddRelativeForce(Vector3.up * thrustThisFrame);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        mainEngineParticle.Play();
    }

    private void respondToRotateInput()
    {
        rigidBody.freezeRotation = true; //take manual control of rotation
        float rotationThisFrame = rcsthrust * Time.deltaTime;
        if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.D))
        {
            print("Pick one or go Straight!");
        }
        else if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        rigidBody.freezeRotation = false; //resume normal physics control
    }

}
