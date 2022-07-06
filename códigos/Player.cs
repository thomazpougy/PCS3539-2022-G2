using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public GameObject ball;
    public GameObject playerCamera;
    public GameObject forceSprite;
    public GameObject forceOutlineSprite;

    public float ballDistance = 1.3f;
    public float upFactor = 0.5f;
    public float ballMaxThrowingForce = 6500f;
    public float ballMinThrowingForce = 1000f;

    public float maxForceHoldDownTime = 1f;

    public bool holdingBall = true;

    private float holdDownStartTime;

    private Vector3 initPosSprit;
    private Vector3 initPosOutlineSprit;
    

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Inicio");
        ball.GetComponent<Rigidbody>().useGravity = false;
        initPosSprit = forceSprite.GetComponent<RectTransform>().localPosition;
        initPosOutlineSprit = forceOutlineSprite.GetComponent<RectTransform>().localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (holdingBall)
        {
            ball.transform.position = playerCamera.transform.position + playerCamera.transform.forward*ballDistance - playerCamera.transform.up*0.5f;

            if (Input.GetMouseButtonDown(0)) {
                holdDownStartTime = Time.time;
            }

            if (Input.GetMouseButtonUp(0)) {
                float holdDownTime = Time.time - holdDownStartTime;
                holdingBall = false;
                ball.GetComponent<Rigidbody>().useGravity = true;
                ball.GetComponent<Rigidbody>().AddForce(playerCamera.transform.forward * (CalculateNormalizedTime(holdDownTime) * ballMaxThrowingForce + 1000));
                ball.GetComponent<Rigidbody>().AddForce(playerCamera.transform.up * (CalculateNormalizedTime(holdDownTime) * ballMaxThrowingForce * upFactor));
            }

            if(Input.GetMouseButton(0))
            {
                float holdDownTime = Time.time - holdDownStartTime;
                float normTime = CalculateNormalizedTime(holdDownTime);
                ShowForce(normTime);
            }
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            holdingBall = true;
            // ball.ActiveTrail();
            ball.GetComponent<Rigidbody>().useGravity = false;
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            ball.GetComponent<Rigidbody>().Sleep();

            forceSprite.GetComponent<RectTransform>().localPosition = new Vector3(initPosSprit.x, (initPosSprit.y), 0f);
        }        
    }

    private float CalculateNormalizedTime(float holdTime)
    {        
        float holdTimeNormalized = Mathf.Clamp01(holdTime / maxForceHoldDownTime);
        return holdTimeNormalized;
       
    }

    private void ShowForce(float normTime)
    {
        // Debug.Log(forceSprite.transform.Find("Forca").GetComponent<Sprite>());
        forceSprite.GetComponent<RectTransform>().localPosition = new Vector3(initPosSprit.x, (initPosSprit.y) + (normTime * 590), 0f);
    }
}
