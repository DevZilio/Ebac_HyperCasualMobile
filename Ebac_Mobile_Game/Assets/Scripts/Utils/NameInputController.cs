using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NameInputController : MonoBehaviour
{
    public TMP_InputField nameInputField;
    public Button confirmButton;

    public GameObject highScoreContainer;
    

    

    private HighScoreTable highScoreTable;

    private void Awake()
    {
        highScoreTable = FindObjectOfType<HighScoreTable>();

        // Definir o limite de caracteres para 3
        nameInputField.characterLimit = 3;

        // Adicionar o evento OnEndEdit para realizar a conversão para letras maiúsculas
        nameInputField.onEndEdit.AddListener(OnEndEdit);

        
    }

    private void OnEndEdit(string text)
    {
        // Converter o texto para letras maiúsculas
        nameInputField.text = text.ToUpper();
    }

    private void OnEnable()
    {
        confirmButton.onClick.AddListener(OnConfirmButtonClick);
    }

    private void OnDisable()
    {
        confirmButton.onClick.RemoveListener(OnConfirmButtonClick);
    }

    public void OnConfirmButtonClick()
    {
        if (highScoreTable != null)
        {
            // Usar o nome já declarado no campo
            string playerName = nameInputField.text;
            int totalCoinsCollected = ItemManager.Instance.GetTotalCoins();

            // Verifique se o jogador realmente digitou um nome
            if (!string.IsNullOrEmpty(playerName))
            {
                highScoreTable.AddHighScoreEntry(totalCoinsCollected, playerName);

                // Após adicionar o novo score, atualiza a tabela imediatamente
                highScoreTable.UpdateHighScoreTable();

               
            }
        }
    }
}
