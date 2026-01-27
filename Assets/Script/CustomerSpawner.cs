using UnityEngine;
using System.Collections;

public class CustomerSpawner : MonoBehaviour
{
    [Header("Prefab")]
    public GameObject customerPrefab;

    [Header("Possible Want Sets")]
    public WantSet[] possibleWantSets;

    [Header("Starting Has Items (always the same)")]
    public HasItem[] startingHas;

    private GameObject currentCustomer;
    private bool isWaitingToSpawn;

    private void Start()
    {
        SpawnCustomer();
    }

    private void Update()
    {
        if (currentCustomer == null && !isWaitingToSpawn)
        {
            StartCoroutine(SpawnAfterDelay());
        }
    }

    private IEnumerator SpawnAfterDelay()
    {
        isWaitingToSpawn = true;
        yield return new WaitForSeconds(1f);
        SpawnCustomer();
        isWaitingToSpawn = false;
    }

    private void SpawnCustomer()
    {
        currentCustomer = Instantiate(customerPrefab, transform.position, Quaternion.identity);
        CustomerControl control = currentCustomer.GetComponent<CustomerControl>();

        if (control == null)
        {
            Debug.LogError("Customer prefab missing CustomerControl!");
            return;
        }

        // Assign random want set
        WantSet chosenSet = possibleWantSets[Random.Range(0, possibleWantSets.Length)];
        control.wants = chosenSet.wants;

        // Copy Has items so each customer has its own instance
        control.has = new HasItem[startingHas.Length];
        for (int i = 0; i < startingHas.Length; i++)
        {
            control.has[i] = new HasItem
            {
                itemName = startingHas[i].itemName,
                amount = startingHas[i].amount
            };
        }

        Debug.Log("Spawned customer with want set: " + chosenSet.setName);
    }
}
