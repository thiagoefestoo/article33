using UnityEngine;


public class PlayerTestController : MonoBehaviour
{

    private void Update()
    {

        // TESTE DE DANO

        if (Input.GetKeyDown(KeyCode.H))
        {

            PlayerStateController.Instance
                .TakeDamage(10);


            Debug.Log(
                "Teste: jogador recebeu 10 de dano"
            );

        }



        // TESTE DE CURA

        if (Input.GetKeyDown(KeyCode.J))
        {

            PlayerStateController.Instance
                .Heal(10);


            Debug.Log(
                "Teste: jogador curado 10 HP"
            );

        }



        // TESTE ENERGIA

        if (Input.GetKeyDown(KeyCode.K))
        {

            bool usado =
                PlayerStateController.Instance
                .UseEnergy(20);



            Debug.Log(
                "Energia usada: "
                +
                usado
            );

        }

    }

}