using UnityEngine;

public class MenuSwitcher : MonoBehaviour
{
    public GameObject hatsMenu;
    public GameObject clothesMenu;
    public GameObject makeUpMenu;

    public void HatsMenu()
    {
        hatsMenu.SetActive(true);
        clothesMenu.SetActive(false);
        makeUpMenu.SetActive(false);
    }

    public void ClothesMenu()
    {
        hatsMenu.SetActive(false);
        clothesMenu.SetActive(true);
        makeUpMenu.SetActive(false);
    }

    public void MakeUpMenu()
    {
        hatsMenu.SetActive(false);
        clothesMenu.SetActive(false);
        makeUpMenu.SetActive(true);
    }
}
