using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using UnityEditor.U2D.Aseprite;
using UnityEditor.UIElements;
using UnityEngine;


public class ClothingPiece : MonoBehaviour
{
    public int points;
    public string colour;
    public string theme;

    public BoxCollider2D bc;
    public RectTransform rt;
    private bool dragging = false;
    private Vector3 offset;
    private float initialPosX;
    private float initialPosY;
    private bool customerCollision = false;

    void Start()
    {
        initialPosX = GetComponent<RectTransform>().anchoredPosition.x;
        initialPosY = GetComponent<RectTransform>().anchoredPosition.y;
        bc = GetComponent<BoxCollider2D>();
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        if(dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Customer"))
        {
            customerCollision = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Customer"))
        {
            customerCollision = false;
        }
    }

    private void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        dragging = true;
    }

    private void OnMouseUp()
    {
        dragging = false;
        //If the clothing is dropped on top of anything but the customer, it returns to its original position
        if (customerCollision == false)
        {
            rt.anchoredPosition = new Vector2(initialPosX, initialPosY);
        }
        else
        {
            //Space to add dropped item to customer inventory, ready to check between its wants and has
        }
    }
}
