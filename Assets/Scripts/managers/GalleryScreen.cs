using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryScreen : MonoBehaviour
{
    [System.Serializable]
    public class Artwork
    {
        public string name;
        public Sprite[] pieces = new Sprite[9];
        public bool[] unlockedPieces = new bool[9];
    }

    public List<Artwork> gallery = new List<Artwork>();
    public GameObject galleryUI;
    public Transform galleryContainer;
    public GameObject artworkPrefab;

    private void Start()
    {
        LoadGalleryProgress();
        DisplayGallery();
    }

    public void UnlockPiece(string artworkName, int pieceIndex)
    {
        Artwork artwork = gallery.Find(a => a.name == artworkName);
        if (artwork != null && pieceIndex >= 0 && pieceIndex < 9)
        {
            Player.Instance.UnlockPicturePiece(artworkName, pieceIndex);
            artwork.unlockedPieces[pieceIndex] = true;
            DisplayGallery();
        }
    }


 private void DisplayGallery()
{
    foreach (Transform child in galleryContainer)
    {
        Destroy(child.gameObject);
    }

    foreach (var artwork in gallery)
    {
        GameObject artworkObject = Instantiate(artworkPrefab, galleryContainer);
        artworkObject.name = artwork.name;

        for (int i = 0; i < 9; i++)
        {
            Image imageComponent = artworkObject.transform.GetChild(i).GetComponent<Image>();

            if (artwork.unlockedPieces[i])
            {
                if (artwork.pieces[i] != null)
                {
                   imageComponent.sprite = artwork.unlockedPieces[i] ? artwork.pieces[i] : null;
                    imageComponent.enabled = false;
                    imageComponent.enabled = true;
                    Debug.Log($"Displaying piece {i} of {artwork.name}");
                }
                else
                {
                    Debug.LogError($"Piece {i} of {artwork.name} is NULL!");
                }
            }
            else
            {
                imageComponent.sprite = null; 
                Debug.Log($"Piece {i} of {artwork.name} is locked.");
            }
        }
    }
}

    private void SaveGalleryProgress()
    {
        foreach (var artwork in gallery)
        {
            PlayerPrefs.SetString($"Gallery_{artwork.name}", string.Join(",", artwork.unlockedPieces));
        }
        PlayerPrefs.Save();
    }

    private void LoadGalleryProgress()
    {
        foreach (var artwork in gallery)
        {
            if (Player.Instance.PuzzleProgress.TryGetValue(artwork.name, out HashSet<int> unlockedPieces))
            {
                for (int i = 0; i < artwork.unlockedPieces.Length; i++)
                {
                    artwork.unlockedPieces[i] = unlockedPieces.Contains(i);
                }
            }
        }
    }

}