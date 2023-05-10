using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textScore;
    [SerializeField] private TextMeshProUGUI textGoal;
    private StarterAssetsInputs starterAssetsInputs;
    private Animator animator;
    private Ball ballAttachedToPlayer;
    public float timeShot = -1f;
    public const int ANIMATION_LAYER_SHOOT = 1;
    private int myScore, otherScore;
    private float goalTextColorAlpha;
    private AudioSource soundCheer;
    private AudioSource soundKick;
    private AudioSource soundGetBall;

    private bool hasBall;

    public Ball BallAttachedToPlayer {get => ballAttachedToPlayer; set => ballAttachedToPlayer = value;}


    // Start is called before the first frame update
    void Start()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        animator = GetComponent<Animator>();
        soundGetBall = GameObject.Find("Sound/GetBall").GetComponent<AudioSource>();
        soundCheer = GameObject.Find("Sound/Cheer").GetComponent<AudioSource>();
        soundKick = GameObject.Find("Sound/Kick").GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        if(starterAssetsInputs.shoot) 
        {
            starterAssetsInputs.shoot = false;
            timeShot = Time.time;
            animator.Play("Shoot", ANIMATION_LAYER_SHOOT, 0f);
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, 1f);
        }
        if (timeShot>0)
        {
            // Sparker bolden 
            if (ballAttachedToPlayer != null && Time.time - timeShot > 0.2)
            {
                soundKick.Play();
                ballAttachedToPlayer.StickToPlayer = false;
                
                Rigidbody rigidbody = ballAttachedToPlayer.transform.gameObject.GetComponent<Rigidbody>();
                Vector3 shootDirection = transform.forward;
                shootDirection.y *= 0.5f;
                rigidbody.AddForce(transform.forward * 5f, ForceMode.Impulse);

                ballAttachedToPlayer = null;
            }

            // Færdiggøre spark 
            if(Time.time - timeShot > 0.5)
            {
                timeShot = 0;
            }
        }
        else 
        {
            animator.SetLayerWeight(ANIMATION_LAYER_SHOOT, Mathf.Lerp(animator.GetLayerWeight(ANIMATION_LAYER_SHOOT), 0f, Time.deltaTime * 10f));
        }

        /*if(ballAttachedToPlayer != null)
        {
            soundGetBall.Play();
        }*/

        if(goalTextColorAlpha>0)
        {
            goalTextColorAlpha -= Time.deltaTime;
            textGoal.alpha = goalTextColorAlpha;
            textGoal.fontSize = 200 - (goalTextColorAlpha * 1-0);
        }
    }

    public void IncreaseMyScore()
    {
        myScore++;
        UpdateScore();
    }

    public void IncreaseOtherScore()
    {
        otherScore++;
        UpdateScore();
    }

    private void UpdateScore()
    {
        soundCheer.Play();
        textScore.text = "Score: " + myScore.ToString() + "-" + otherScore.ToString();
        goalTextColorAlpha = 1f;
    }
}

