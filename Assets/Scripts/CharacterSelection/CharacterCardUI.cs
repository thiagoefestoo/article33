using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterCardUI : MonoBehaviour
{
    [Header("Textos")]
    [SerializeField] private TMP_Text nameText;
    [SerializeField] private TMP_Text classText;
    [SerializeField] private TMP_Text rankText;
    [SerializeField] private TMP_Text levelText;
    [SerializeField] private TMP_Text moneyText;

    [Header("Botão")]
    [SerializeField] private Button enterButton;

    private CharacterSelectionData characterData;
    private CharacterSelectionManager manager;

    public void Setup(
        CharacterSelectionData data,
        CharacterSelectionManager selectionManager
    )
    {
        characterData = data;
        manager = selectionManager;

        if (nameText != null)
        {
            nameText.text = data.name;
        }

        if (classText != null)
        {
            classText.text =
                "Classe: " +
                data.characterClassName;
        }

        if (rankText != null)
        {
            rankText.text =
                "Rank: " +
                data.rankName;
        }

        if (levelText != null)
        {
            levelText.text =
                "Level: " +
                data.level;
        }

        if (moneyText != null)
        {
            moneyText.text =
                "Dinheiro: R$ " +
                data.money;
        }

        if (enterButton != null)
        {
            enterButton.onClick.RemoveAllListeners();

            enterButton.onClick.AddListener(
                SelectCharacter
            );
        }
    }

    private void SelectCharacter()
    {
        if (characterData == null)
        {
            Debug.LogError(
                "[CHARACTER CARD] Dados do personagem não encontrados."
            );

            return;
        }

        if (manager == null)
        {
            Debug.LogError(
                "[CHARACTER CARD] CharacterSelectionManager não encontrado."
            );

            return;
        }

        manager.SelectCharacter(
            characterData
        );
    }
}