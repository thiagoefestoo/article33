using UnityEngine;


public class EnemyController : MonoBehaviour
{


    // =========================================================
    // DADOS DO INIMIGO
    // =========================================================

    public EnemyData enemyData;



    // =========================================================
    // REFERÊNCIA DO JOGADOR
    // =========================================================

    private Transform player;



    // =========================================================
    // CONFIGURAÇÃO
    // =========================================================

    public float detectionRange = 10f;

    public float attackRange = 2f;

    public float moveSpeed = 3f;


    public float attackCooldown = 2f;


    private float nextAttackTime;



    // =========================================================
    // INICIALIZAÇÃO
    // =========================================================

    private void Start()
    {


        GameObject obj =
            GameObject.FindGameObjectWithTag("Player");


        if (obj != null)
        {

            player = obj.transform;

        }


        if (enemyData != null)
        {

            enemyData.PrintStats();

        }


    }



    // =========================================================
    // UPDATE
    // =========================================================

    private void Update()
    {


        if (player == null)
            return;



        if (enemyData == null)
            return;



        if (enemyData.IsDead())
            return;



        float distance =
            Vector3.Distance(
                transform.position,
                player.position
            );



        if (distance <= detectionRange)
        {

            ChasePlayer(distance);

        }


    }




    // =========================================================
    // PERSEGUIR
    // =========================================================

    private void ChasePlayer(float distance)
    {


        if (distance > attackRange)
        {


            transform.position =
                Vector3.MoveTowards(

                    transform.position,

                    player.position,

                    moveSpeed * Time.deltaTime

                );


        }
        else
        {

            AttackPlayer();

        }


    }




    // =========================================================
    // ATAQUE
    // =========================================================

    private void AttackPlayer()
    {


        if (Time.time < nextAttackTime)
            return;



        nextAttackTime =
            Time.time + attackCooldown;



        int damage =
            enemyData.CalculateAttackDamage();



        if (PlayerCombatController.Instance != null)
        {

            PlayerCombatController.Instance
                .ReceiveDamage(damage);


        }



        Debug.Log(

            enemyData.Name
            +
            " atacou causando "
            +
            damage
            +
            " dano."

        );


    }




    // =========================================================
    // RECEBER DANO
    // =========================================================

    public void ReceiveDamage(int damage)
    {


        if (enemyData == null)
            return;



        enemyData.TakeDamage(damage);



        Debug.Log(

            enemyData.Name
            +
            " recebeu "
            +
            damage
            +
            " dano."

        );



        if (enemyData.IsDead())
        {

            Die();

        }


    }




    // =========================================================
    // MORTE
    // =========================================================

    private void Die()
    {


        Debug.Log(

            enemyData.Name
            +
            " morreu."

        );



        Destroy(gameObject, 1f);


    }


}