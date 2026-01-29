using UnityEngine;
using System.Collections.Generic;

public class CustomerClothing : MonoBehaviour
{
    public Transform equippedParent;

    private Dictionary<ClothingType, GameObject> equippedItems =
        new Dictionary<ClothingType, GameObject>();

    public void EquipClothing(ClothingPiece piece, Vector3 worldDropPosition)
    {
        // Remove old clothing of same type
        if (equippedItems.TryGetValue(piece.clothingType, out GameObject oldItem))
        {
            Destroy(oldItem);
        }

        // Create new visual object
        GameObject clothingObj = new GameObject(piece.clothingType.ToString());
        clothingObj.transform.SetParent(equippedParent);
        clothingObj.transform.position = worldDropPosition;

        SpriteRenderer sr = clothingObj.AddComponent<SpriteRenderer>();
        sr.sprite = piece.sr.sprite;

        // SCALE APPLIED HERE
        sr.transform.localScale = Vector3.one * 2.5f;

        sr.sortingLayerName = "Character";
        sr.sortingOrder = 5;

        equippedItems[piece.clothingType] = clothingObj;
    }

}
