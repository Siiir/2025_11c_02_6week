using TMPro;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    [SerializeField] private TMP_Text myText;
    private int coins = 0;

    void Start()
    {
        myText.text = 0.ToString();
    }

    void OnEnable()
    {
        CollectableCoinSO.OnCoinCollected += AddCoins;
    }

    void OnDisable()
    {
        CollectableCoinSO.OnCoinCollected -= AddCoins;
    }


    private void AddCoins(int value)
    {
        coins += value;
        myText.text = coins.ToString();
    }
}