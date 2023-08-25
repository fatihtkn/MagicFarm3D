using UnityEngine;
using TMPro;
using System;
using System.Threading;

public class BuyArea : MonoBehaviour
{
    private float price;
    public float Price { get { return price; } set { price = value; } } 
   
    private bool IsPlayerInArea;
    private int multiplier;
    public ObjectCost mainCost;
    private bool firstBuy;
    [SerializeField] private Animator costAnimator;
    private float timer;
   

    private void Start()
    {
        multiplier = 3;
        price = Convert.ToInt32(mainCost.TileCost);
        if (price <= 0) firstBuy = false;
        else if (price > 0) firstBuy = true;
        costAnimator.enabled = true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInArea= true;
        }
        
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerInArea = false;
        }
    }
    private void Update()
    {
        
        
        if (IsPlayerInArea)
        {
            if (Money.Instance.CurrentMoney > 0)
            {
                //price -= Time.deltaTime * 10f * multiplier;
               // price=Mathf.Lerp(price, 0, Time.deltaTime/**11f*multiplier*/);
                //Money.Instance.CurrentMoney -= Time.deltaTime * 10f * multiplier;
               // Money.Instance.CurrentMoney = Mathf.Lerp(Money.Instance.CurrentMoney, 0, Time.deltaTime /** 11f * multiplier*/);
               SetPriceAnimation(0.001f);
                mainCost.text.SetText(price.ToString("0"));
                multiplier += 3;
            }
        }
        if (price <= 0)
        {
           
            price = 0;
            transform.parent.GetComponent<Register>().OnBoughtObject(firstBuy);
            mainCost.GetComponent<TMP_Text>().enabled = false;
            PriortyManager.Instance.PriortyCount++;
            gameObject.SetActive(false);
            
        }
        mainCost.TileCost = price;
        
    }
    private void SetPriceAnimation(float interval)
    {
        timer += Time.deltaTime;

        if (timer >= interval)
        {
            price -= 100;
            Money.Instance.CurrentMoney -= 100;

            timer = 0;
        }
    }
   


}
