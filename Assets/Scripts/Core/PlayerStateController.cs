using UnityEngine;


public class PlayerStateController : MonoBehaviour
{

    public static PlayerStateController Instance;


    private void Awake()
    {

        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }


        Instance = this;


        DontDestroyOnLoad(gameObject);

    }



    // =====================================
    // RECEBER DANO
    // =====================================

    public void TakeDamage(int damage)
    {

        if (!PlayerDataManager.Instance.HasPlayer())
            return;


        PlayerStats stats =
            PlayerDataManager.Instance.GetStats();



        stats.TakeDamage(damage);



        Debug.Log(
            "Dano recebido: "
            + damage
            +
            " HP:"
            +
            stats.currentHealth
        );



        SaveState();

    }




    // =====================================
    // CURAR
    // =====================================

    public void Heal(int amount)
    {

        if (!PlayerDataManager.Instance.HasPlayer())
            return;



        PlayerStats stats =
            PlayerDataManager.Instance.GetStats();



        stats.Heal(amount);



        SaveState();

    }





    // =====================================
    // GASTAR ENERGIA
    // =====================================

    public bool UseEnergy(int amount)
    {


        PlayerStats stats =
            PlayerDataManager.Instance.GetStats();



        bool result =
            stats.ConsumeEnergy(amount);



        if (result)
            SaveState();



        return result;

    }





    // =====================================
    // SALVAR NA API
    // =====================================

    private void SaveState()
    {

        Debug.Log(
            "Estado do jogador alterado"
        );


        /*
        
        Aqui vamos chamar:

        UnityService.UpdatePlayer()

        */


    }

}