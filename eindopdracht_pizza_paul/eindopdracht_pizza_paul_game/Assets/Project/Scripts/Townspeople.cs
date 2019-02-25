
using UnityEngine;

public class Townspeople : MonoBehaviour
{

    public Material[] material;
    Renderer rend;

    public float health = 50f;

    void Start()
    {
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.sharedMaterial = material[0];
    }


    public void TakeDamage (float amount)
    {
        health -= amount;
        if (health <= 0f)
        {
            FullStomache();
        }

    }

    void FullStomache ()
    {
        rend.sharedMaterial = material[1];
    }
}
