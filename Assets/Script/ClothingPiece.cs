using System.Linq;
using Microsoft.Unity.VisualStudio.Editor;
using NUnit.Framework;
using NUnit.Framework.Constraints;
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
    public SpriteRenderer sr;
    private bool dragging = false;
    private Vector3 offset;
    private float initialPosX;
    private float initialPosY;
    private bool customerCollision = false;

    void Start()
    {
        bc = GetComponent<BoxCollider2D>();
        rt = GetComponent<RectTransform>();
        sr = GetComponent<SpriteRenderer>();
        //Grabs initial pos on delay to grab post grid layout positions
        Invoke("GetInitialPos", 0.01f);
    }

    private void GetInitialPos()
    {
        initialPosX = GetComponent<RectTransform>().anchoredPosition.x;
        initialPosY = GetComponent<RectTransform>().anchoredPosition.y;
    }

    void Update()
    {
        if(dragging)
        {
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
        }
    }

    //Checks if the clothing is overlapping with the customer
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
        //Changes the scale to fit the customer
        sr.transform.localScale = new Vector3(2.5f, 2.5f, 2.5f);
    }

    private void OnMouseUp()
    {
        dragging = false;
        //If the clothing is dropped on top of anything but the customer, it returns to its original position
        if (customerCollision == false)
        {
            rt.anchoredPosition = new Vector2(initialPosX, initialPosY);
            //Reverts the scale to fit the menu
            sr.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            //Space to add dropped item to customer inventory, ready to check between its wants and has
        }
    }
}
