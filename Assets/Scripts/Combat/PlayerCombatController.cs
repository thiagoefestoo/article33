using UnityEngine;



public class PlayerCombatController : MonoBehaviour
{


    public static PlayerCombatController Instance;



    private PlayerData currentPlayer;



    // =========================================================
    // INICIALIZAÇÃO
    // =========================================================

    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


    }




    private void Start()
    {

        LoadPlayer();

    }




    // =========================================================
    // CARREGAR PLAYER
    // =========================================================

    private void LoadPlayer()
    {

        if (PlayerDataManager.Instance == null)
        {

            Debug.LogError(
                "[Combat] PlayerDataManager não encontrado."
            );

            return;

        }



        currentPlayer =
            PlayerDataManager.Instance.GetPlayer();



        if (currentPlayer == null)
        {

            Debug.LogError(
                "[Combat] Nenhum jogador carregado."
            );

            return;

        }



        Debug.Log(
            "[Combat] Sistema de combate iniciado."
        );


    }





    // =========================================================
    // ATAQUE
    // =========================================================

    public void AttackEnemy(EnemyData enemy)
    {


        if (enemy == null)
            return;



        if (enemy.IsDead())
            return;



        int damage =
            CalculateDamage();



        enemy.TakeDamage(damage);



        Debug.Log(

            currentPlayer.name
            +
            " causou "
            +
            damage
            +
            " dano em "
            +
            enemy.Name

        );



        if (enemy.IsDead())
        {

            EnemyDefeated(enemy);

        }


    }





    // =========================================================
    // CALCULAR DANO
    // =========================================================

    private int CalculateDamage()
    {


        if (currentPlayer == null)
            return 0;



        if (currentPlayer.stats == null)
            return 0;



        int damage =
            currentPlayer.stats.strength * 2;



        if (damage <= 0)
            damage = 1;



        return damage;


    }





    // =========================================================
    // RECEBER DANO
    // =========================================================

    public void ReceiveDamage(int damage)
    {


        if (currentPlayer == null)
            return;



        if (currentPlayer.stats == null)
            return;



        currentPlayer.stats.TakeDamage(
            damage
        );



        Debug.Log(
            "Jogador recebeu "
            +
            damage
            +
            " dano."
        );



        SavePlayer();


    }





    // =========================================================
    // INIMIGO DERROTADO
    // =========================================================

    private void EnemyDefeated(EnemyData enemy)
    {


        Debug.Log(
            "Inimigo derrotado!"
        );



        currentPlayer.experience +=
            enemy.ExperienceReward;



        currentPlayer.money +=
            enemy.MoneyReward;



        Debug.Log(

            "Recompensa: XP +"
            +
            enemy.ExperienceReward
            +
            " | Dinheiro +"
            +
            enemy.MoneyReward

        );



        SavePlayer();


    }





    // =========================================================
    // SALVAR
    // =========================================================

    private void SavePlayer()
    {


        if (UnityService.Instance == null)
        {

            Debug.LogWarning(
                "[Combat] UnityService não encontrado."
            );

            return;

        }



        UnityService.Instance
            .SavePlayer(currentPlayer);


    }




}