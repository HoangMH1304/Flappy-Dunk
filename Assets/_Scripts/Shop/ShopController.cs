using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Shop
{
    Ball,
    Wing,
    Hoop,
    Flame
}

public class ShopController : MonoBehaviour
{
    public static ShopController Instance;

    [SerializeField] public Ball[] balls;
    [SerializeField] public Wing[] wings;
    [SerializeField] public Hoop[] hoops;
    [SerializeField] public Flame[] flames;

    [SerializeField] private Transform ballContent, wingContent, hoopContent, flameContent;
    
    private int totalItems;
    private int totalUnlockItems = 0, ball = 0, wing = 0, hoop = 0, flame = 0;

    public int TotalItems { get => totalItems; set => totalItems = value; }
    public int TotalUnlockItems { get => totalUnlockItems; set => totalUnlockItems = value; }

    private void Awake()
    {
        Instance = this;
        totalItems = balls.Length + wings.Length + hoops.Length + flames.Length;

        for(int i = 0; i < balls.Length; i++) totalUnlockItems += PlayerPrefs.GetInt("Ball" + i);
        Debug.Log($"Ball: {ball}");
        for(int i = 0; i < wings.Length; i++) totalUnlockItems += PlayerPrefs.GetInt("Wing" + i);
        Debug.Log($"Wing: {wing}");

        for(int i = 0; i < hoops.Length; i++) totalUnlockItems += PlayerPrefs.GetInt("Hoop" + i);
        Debug.Log($"Hoop: {hoop}");

        for(int i = 0; i < flames.Length; i++) totalUnlockItems += PlayerPrefs.GetInt("Flame" + i);
        Debug.Log($"Flame: {flame}");


    }

    private void Start()
    {
        InitBallItems();
        InitWingItems();
        InitHoopItems();
        InitFlameItems();
        this.PostEvent(EventID.OnChangeSkin);
        //process bar of shop
        Debug.Log($"total unlock items: {totalUnlockItems}/{totalItems}");
    }

    public void DeactivateMarkIcon(Shop type, int id)
    {
        switch (type)
        {
            case Shop.Ball:
                ballContent.GetChild(id).GetChild(2).gameObject.SetActive(false);
                Debug.Log($"name: {ballContent.GetChild(id).GetChild(2).gameObject}");
                break;
            case Shop.Wing:
                wingContent.GetChild(id).GetChild(2).gameObject.SetActive(false);
                break;
            case Shop.Hoop:
                hoopContent.GetChild(id).GetChild(3).gameObject.SetActive(false);
                break;
            case Shop.Flame:
                flameContent.GetChild(id).GetChild(2).gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void InitBallItems()
    {
        GameObject templateObject = ballContent.GetChild(0).gameObject;
        foreach (var item in balls)
        {
            GameObject itemObject = Instantiate(templateObject, ballContent);
            itemObject.GetComponent<BallDisplay>().Show(item);
        }
        Destroy(templateObject);
    }

    public void InitWingItems()
    {
        GameObject templateObject = wingContent.GetChild(0).gameObject;
        foreach (var item in wings)
        {
            GameObject itemObject = Instantiate(templateObject, wingContent);
            itemObject.GetComponent<WingDisplay>().Show(item);
        }
        Destroy(templateObject);
    }

    public void InitHoopItems()
    {
        GameObject templateObject = hoopContent.GetChild(0).gameObject;
        foreach (var item in hoops)
        {
            GameObject itemObject = Instantiate(templateObject, hoopContent);
            itemObject.GetComponent<HoopDisplay>().Show(item);
        }
        Destroy(templateObject);
    }

    public void InitFlameItems()
    {
        GameObject templateObject = flameContent.GetChild(0).gameObject;
        foreach (var item in flames)
        {
            GameObject itemObject = Instantiate(templateObject, flameContent);
            itemObject.GetComponent<FlameDisplay>().Show(item);
        }
        Destroy(templateObject);
    }
}
