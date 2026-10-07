using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class CharacterSelectionManager : MonoBehaviour
{
    [Header("Interface")]

    [SerializeField]
    private Transform characterList;

    [SerializeField]
    private GameObject characterCardPrefab;

    [SerializeField]
    private TMP_Text statusText;


    private void Start()
    {
        if (statusText != null)
        {
            statusText.text = "";
        }

        if (PlayerSession.Instance == null)
        {
            SetStatus(
                "Sessão do jogador não encontrada."
            );

            Debug.LogError(
                "[CHARACTER SELECTION] PlayerSession não existe."
            );

            return;
        }

        if (!PlayerSession.Instance.authenticated)
        {
            SetStatus(
                "Usuário não autenticado."
            );

            Debug.LogWarning(
                "[CHARACTER SELECTION] Usuário não autenticado."
            );

            return;
        }

        StartCoroutine(
            LoadCharacters()
        );
    }


    // =====================================
    // CARREGAR PERSONAGENS
    // =====================================

    private IEnumerator LoadCharacters()
    {
        int userId =
            PlayerSession.Instance.userId;

        string url =
            ApiConfig.BaseURL +
            "/api/character/user/" +
            userId;


        SetStatus(
            "Carregando personagens..."
        );


        Debug.Log(
            "[CHARACTER SELECTION] Buscando personagens do UserId: " +
            userId
        );


        using UnityWebRequest request =
            UnityWebRequest.Get(url);


        yield return request.SendWebRequest();


        if (
            request.result !=
            UnityWebRequest.Result.Success
        )
        {
            string errorMessage =
                request.downloadHandler != null
                    ? request.downloadHandler.text
                    : request.error;


            Debug.LogError(
                "[CHARACTER SELECTION] Erro: " +
                errorMessage
            );


            SetStatus(
                "Não foi possível carregar os personagens."
            );


            yield break;
        }


        string json =
            request.downloadHandler.text;


        Debug.Log(
            "[CHARACTER SELECTION] Resposta:"
        );

        Debug.Log(json);


        CharacterSelectionResponse response =
            JsonUtility.FromJson<CharacterSelectionResponse>(
                json
            );


        if (response == null)
        {
            SetStatus(
                "Resposta inválida do servidor."
            );

            yield break;
        }


        if (
            response.characters == null ||
            response.characters.Length == 0
        )
        {
            SetStatus(
                "Nenhum personagem encontrado."
            );

            yield break;
        }


        CreateCharacterCards(
            response.characters
        );


        SetStatus("");
    }


    // =====================================
    // CRIAR CARDS
    // =====================================

    private void CreateCharacterCards(
        CharacterSelectionData[] characters
    )
    {
        if (characterList == null)
        {
            Debug.LogError(
                "[CHARACTER SELECTION] CharacterList não configurado."
            );

            return;
        }


        if (characterCardPrefab == null)
        {
            Debug.LogError(
                "[CHARACTER SELECTION] CharacterCardPrefab não configurado."
            );

            return;
        }


        // Remove cards anteriores
        for (
            int i = characterList.childCount - 1;
            i >= 0;
            i--
        )
        {
            Destroy(
                characterList.GetChild(i).gameObject
            );
        }


        foreach (
            CharacterSelectionData character
            in characters
        )
        {
            GameObject card =
                Instantiate(
                    characterCardPrefab,
                    characterList
                );


            CharacterCardUI cardUI =
                card.GetComponent<CharacterCardUI>();


            if (cardUI == null)
            {
                Debug.LogError(
                    "[CHARACTER SELECTION] Prefab sem CharacterCardUI."
                );

                Destroy(card);

                continue;
            }


            cardUI.Setup(
                character,
                this
            );
        }
    }


    // =====================================
    // SELECIONAR PERSONAGEM
    // =====================================

    public void SelectCharacter(
        CharacterSelectionData character
    )
    {
        if (PlayerSession.Instance == null)
        {
            SetStatus(
                "Sessão não encontrada."
            );

            return;
        }


        PlayerSession.Instance.ConnectPlayer(
            character.userId,
            character.id,
            character.name
        );


        Debug.Log(
            "[CHARACTER SELECTION] Personagem selecionado: " +
            character.name +
            " | CharacterId: " +
            character.id
        );


        SetStatus(
            "Entrando no mundo..."
        );


        SceneManager.LoadScene(
            "Artigo33"
        );
    }


    // =====================================
    // STATUS
    // =====================================

    private void SetStatus(
        string message
    )
    {
        if (statusText != null)
        {
            statusText.text = message;
        }
    }
}