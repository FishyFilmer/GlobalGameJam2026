using NUnit.Framework;
using UnityEngine;

[System.Serializable]
public class Want
{
    public string itemName;
    public int points;
}

[System.Serializable]
public class HasItem
{
    public string itemName;
    public int amount;
}

public class CustomerControl : MonoBehaviour
{
    public Want[] wants = new Want[5];
    public HasItem[] has;

    [Header("Lifetime")]
    public float lifetime = 20f;

    private void Awake()
    {
        // Initialize Has amounts to 0 if not already set
        if (has != null)
        {
            for (int i = 0; i < has.Length; i++)
            {
                has[i].amount = 0;
            }
        }

        Destroy(gameObject, lifetime);
    }
}
