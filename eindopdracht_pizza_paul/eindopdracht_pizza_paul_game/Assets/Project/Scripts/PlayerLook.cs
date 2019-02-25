using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Jewelle Semple | 3028243 | G&I1D

public class PlayerLook : MonoBehaviour
{
    //Dit zijn twee variabele die de Inputnamen opslaan voor de mouse X en mouse Y Axis. De volgende variabele is slaat de waarde van de mouse Sensitivity op. Deze is een float.
    //Om naar links/rechts te kijken, maken we eigenlijk gebruik van het lichaam, inplaats van de camera. Om te referen naar het lichaam maak ik de variabele "PlayerBody".
    [SerializeField] private string mouseXInputName, mouseYInputName;
    [SerializeField] private float mouseSensitivity;

    [SerializeField] private Transform PlayerBody;

    private float xAxisClamp;


    //De functie "LockCursor" wordt steeds opgeroepen als we de muis willen locken naar het midden van het scherm. Dit wordt opgeroepen wanneer het level wordt opgestart. 
    //De xAxisClamp is een aparte variabele, om bij te houden hoeveel de camera roteert.
    private void Awake()
    {
        LockCursor();
        xAxisClamp = 0.0f;
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void FixedUpdate()
    {
        CameraRotation();
    }

    //In deze functie wil ik de input van de muis van de X-axis oproepen. Om de camera uiteindelijk te rotaten, maak ik gebruik van de transform functie.
    //Om ervoor te zorgen dat de camera niet helemaal rond kan roteren, maken ik gebruik van de xAxisClamp functie. Op het moment dat de xAxisClamp, bij 90.0 graden komt, blijft deze vast op 90.0 graden. Wanneer je omhoog kijkt, kijkt de speler eigenlijk op 270graden, wanneer de speler naar beneden kijkt, blijft deze hangen op 90 graden.
    private void CameraRotation()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if(xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }

        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        PlayerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

}

//Voor deze code heb ik de youtube tutorial gevolgd van Acacia Developer. Link: https://www.youtube.com/watch?v=n-KX8AeGK7E.
