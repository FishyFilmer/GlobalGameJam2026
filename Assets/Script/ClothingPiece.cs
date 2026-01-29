using UnityEngine;

public class ClothingPiece : MonoBehaviour
{
    public int points;
    public string colour;
    public string theme;
    public ClothingType clothingType;
    private CustomerClothing hoveredCustomer;

    public BoxCollider2D bc;
    public RectTransform rt;
    public SpriteRenderer sr;

    private bool dragging = false;
    private Vector3 offset;
    private float initialPosX;
    private float initialPosY;

    private CustomerClothing currentCustomer;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rt = GetComponent<RectTransform>();
        sr = GetComponent<SpriteRenderer>();

        bc.isTrigger = true; // IMPORTANT

        Invoke(nameof(GetInitialPos), 0.01f);
    }

    private void GetInitialPos()
    {
        initialPosX = rt.anchoredPosition.x;
        initialPosY = rt.anchoredPosition.y;
    }

    void Update()
    {
        if (dragging)
        {
            transform.position =
                Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Customer"))
        {
            hoveredCustomer = collision.GetComponent<CustomerClothing>();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Customer"))
        {
            hoveredCustomer = null;
        }
    }


    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
        sr.transform.localScale = Vector3.one * 2.5f;
    }

    private void OnMouseUp()
    {
        dragging = false;

        if (hoveredCustomer != null)
        {
            // Use the clothing's current world position
            Vector3 dropPos = transform.position;
            dropPos.z = 0f;

            hoveredCustomer.EquipClothing(this, dropPos);
        }

        // Snap back to menu
        rt.anchoredPosition = new Vector2(initialPosX, initialPosY);
        sr.transform.localScale = Vector3.one;

        hoveredCustomer = null;
    }




}
