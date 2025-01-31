using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopScreen : MonoBehaviour
{
    public Player player;

    [Header("Power-ups")]
    public Product teddyBear;
    public Button teddyBearButton;
    public TextMeshProUGUI teddyBearPriceText;

    [Header("Puzzles")]
    public Product puzzlePiece;
    public Button puzzlePieceButton;
    public TextMeshProUGUI puzzlePiecePriceText;

    [Header("Player Info")]
    public TextMeshProUGUI playerPointsText;

    void Start()
    {
        // Set UI Text
        teddyBearPriceText.text = teddyBear.productName + " - " + teddyBear.price + " Points";
        // puzzlePiecePriceText.text = puzzlePiece.productName + " - " + puzzlePiece.price + " Points";

        // Add Button Listeners
        teddyBearButton.onClick.AddListener(BuyTeddyBear);
        // puzzlePieceButton.onClick.AddListener(BuyPuzzlePiece);

        UpdateUI();
    }

    void BuyTeddyBear()
    {
        if (player.CanAfford(teddyBear.price))
        {
            player.AddTeddyBear();
            player.SpendPoints(teddyBear.price);
            UpdateUI();
        }
    }

    // void BuyPuzzlePiece()
    // {
    //     if (player.CanAfford(puzzlePiece.price))
    //     {
    //         player.AddPuzzlePiece();
    //         player.SpendPoints(puzzlePiece.price);
    //         UpdateUI();
    //     }
    // }

    void UpdateUI()
    {
        playerPointsText.text = "Points: " + player.Points;
    }
}
