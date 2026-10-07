using UnityEngine;


public class DamageReceiver : MonoBehaviour
{


    public int Health = 100;



    // =====================================
    // RECEBER DANO
    // =====================================

    public void TakeDamage(int damage)
    {


        Health -= damage;



        Debug.Log(
            gameObject.name +
            " recebeu " +
            damage +
            " dano. HP: " +
            Health
        );



        if (Health <= 0)
        {

            Die();

        }


    }




    // =====================================
    // MORTE
    // =====================================

    private void Die()
    {

        Debug.Log(
            gameObject.name +
            " morreu."
        );


        Destroy(gameObject);


    }


}