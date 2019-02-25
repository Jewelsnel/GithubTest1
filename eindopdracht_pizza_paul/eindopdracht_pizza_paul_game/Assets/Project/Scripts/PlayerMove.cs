using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jewelle Semple | 3028243 | G&I1D


//Voor de move functie willen we dat de player in de richting v/d "Blauwe Pijl" van Unity loopt op het moment dat je de "w" of "up" toets drukt. 
//En voor links/rechts, willen we dat de speler in de richting van de "Rode pijl" in Unity loopt op het moment dat je de "w" of "left" key gebruikt. En natuurlijk andersom voor de "d" en de "right" keys.




public class PlayerMove : MonoBehaviour
{
    [SerializeField] private string horizontalInputName;
    [SerializeField] private string verticalInputName;
    [SerializeField] private float movementSpeed;

    private CharacterController charController;

    [SerializeField] private AnimationCurve jumpFallOff;
    [SerializeField] private float jumpMultiplier;
    [SerializeField] private KeyCode jumpKey;

    private bool isJumping;


    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        PlayerMovement();
    }


    //Voor deze functie hoef ik Input.De "Input.GetAxis(vertical/horizontalInputName) * movementSpeed" niet nog eens te vermenigvuldigen met "Time.deltaTime", omdat de "SimpleMove" functie dit al voor je doet.
    private void PlayerMovement()
    {
        float horizontalInput = Input.GetAxis(horizontalInputName) * movementSpeed;
        float verticalInput = Input.GetAxis(verticalInputName) * movementSpeed;

        Vector3 forwardMovement = transform.forward * verticalInput;
        Vector3 rightMovement = transform.right * horizontalInput;


        charController.SimpleMove(forwardMovement + rightMovement);

        JumpInput();

    }

    private void JumpInput()
    {
        if(Input.GetKeyDown(jumpKey) && !isJumping)
        {
            isJumping = true;
            StartCoroutine(JumpEvent());
        }
    }

    private IEnumerator JumpEvent()
    {
        charController.slopeLimit = 90.0f;
        float timeInAir = 0.0f;

        do
        {
            float jumpForce = jumpFallOff.Evaluate(timeInAir);
            charController.Move(Vector3.up * jumpForce * jumpMultiplier * Time.deltaTime);
            timeInAir += Time.deltaTime;


            yield return null;
        } while (!charController.isGrounded && charController.collisionFlags != CollisionFlags.Above);

        charController.slopeLimit = 45.0f;
        isJumping = false;
    }

}

//Voor deze code heb ik de youtube tutorial gevolgd van Acacia Developer. Link: https://www.youtube.com/watch?v=n-KX8AeGK7E.