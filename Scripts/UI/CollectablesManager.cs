using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using System.Collections.Generic;

using System.Threading.Tasks;

public class CollectablesManager :MonoSingleton<CollectablesManager>
{
	
	[SerializeField] private TMP_Text OrbText;
    [SerializeField] private TMP_Text plantText;
    [SerializeField] private TMP_Text potionText;
    [SerializeField] private TMP_Text enabledTileText;
    [SerializeField] private TMP_Text crystalText;
    [SerializeField] private TMP_Text starText;
    [HideInInspector] public List<Transform> activeFarmList;
    [HideInInspector] public List<Transform> activeCauldronList;
    private int leafvalueRef;
    private int potionCountRef;
    private int orbCountRef;
    private int orbCount;

    public RectTransform collectableUIGroup;
    public RectTransform[] starUI;
	public int OrbCount
	{
		get { return orbCount; }
		set 
		{ 
			orbCount = value;
            DOTween.To(() => orbCountRef, x => orbCountRef = x, value, 1f).OnUpdate(() =>
            {
                SetOrbAmount(orbCountRef);
            });

            SetTopRightImagesVisbility();
        }
	}
    private int leafCount;

    public int LeafCount
    {
        get { return leafCount; }
        set
        {
            leafCount = value;
            DOTween.To(() => leafvalueRef, x => leafvalueRef = x,  value, 1f).OnUpdate(() =>
            {
                SetLeafText(leafvalueRef);
            });

            //leafvalueRef = value;
            //SetLeafText(value);
            SetTopRightImagesVisbility();
        }
    }
    private int potionCount;

    public int PotionCount
    {
        get { return potionCount; }
        set
        {
            potionCount = value;
            DOTween.To(() => potionCountRef, x => potionCountRef = x, value, 1f).OnUpdate(() =>
            {
                SetPotionAmountText(potionCountRef);
            });
            SetTopRightImagesVisbility();

        }
    }
    private int crystal;

    public int Crystal
    {
        get { return crystal; }
        set { crystal = value; SetCrystalUI(); SetTopRightImagesVisbility(); }
    }

    private int _starCount;
    public int StarCount
    {
        get => _starCount;
        set { DOTween.To(() => _starCount, x => _starCount = x, value, 1f).OnUpdate(() =>
        {
            SetStarText(_starCount);
            });

            SetTopRightImagesVisbility();   
        }

    }


    private void Awake()
    {
        SaveSystem.collectablesReegister.Add(this);
    }
    private void Start()
    {
        if (crystal <= 0)
        {
            crystal = 100;
            crystalText.SetText(crystal);
            
        }
        SetTopRightImagesVisbility();

    }



    private void SetCrystalUI()
    {
        if (crystal >= 1000000) crystalText.SetText((crystal / 1000000).ToString("0.0") + "M");
        else if (crystal >= 1000) crystalText.SetText((crystal / 1000).ToString("0.0") + "K");
        else crystalText.SetText(crystal.ToString("0"));
    }


    private void SetPotionAmountText(float fillAmount)
    {
        if (fillAmount >= 1000) potionText.SetText((fillAmount / 1000).ToString("0.0") + "K");
        else potionText.SetText(fillAmount.ToString());


    }
    private void SetOrbAmount(float fillAmount)
	{
        if (fillAmount >= 1000) OrbText.SetText((fillAmount / 1000).ToString("0.0") + "K");
        else OrbText.SetText(fillAmount.ToString());

      
    }
    private void SetLeafText(int fillAmount)
    {
        
        if (fillAmount >= 1000) plantText.SetText((fillAmount / 1000).ToString("0.0") + "K");
        else plantText.SetText(fillAmount.ToString());
        //DOTween.To(() => energyBar.fillAmount, x => energyBar.fillAmount = x, fillAmount, 10f);
       
    }
    private void SetStarText(int fillAmount)
    {

        if (fillAmount >= 1000) starText.SetText((fillAmount / 1000).ToString("0.0") + "K");
        else starText.SetText(fillAmount.ToString());
    }
    public void SetPriortyText(float priortyText)
    {
        enabledTileText.SetText(priortyText);
    }

    public void SellPotions(float multiplier)
    {
        float gain= potionCount * multiplier* Money.Instance.coinMultiplier;
        
        potionCount = 0;
        DOTween.To(() => potionCountRef, x => potionCountRef = x,potionCount, 1f).OnUpdate(() =>
        {
            SetPotionAmountText(potionCountRef);
            SetTopRightImagesVisbility();
        });
        DOTween.To(() => Money.Instance.CurrentMoney, x => Money.Instance.CurrentMoney = x, Money.Instance.CurrentMoney + gain, 1);
    }
    public void SellOrbs(float multiplier)
    {
        float gain = orbCount * multiplier*Money.Instance.coinMultiplier;

        orbCount = 0;
        SetTopRightImagesVisbility();
        DOTween.To(() => orbCountRef, x => orbCountRef = x, orbCount, 1f).OnUpdate(() =>
        {
            SetOrbAmount(orbCountRef);
        });
        DOTween.To(() => Money.Instance.CurrentMoney, x => Money.Instance.CurrentMoney = x, Money.Instance.CurrentMoney + gain, 1);
    }


    public async  void SetTopRightImagesVisbility()
    {
        int value = OrbCount + LeafCount + PotionCount + StarCount;
        
        if (value <= 0)
        {
            await Task.Delay(1500);
            collectableUIGroup.DOAnchorPosX(702, 1f);
        }
        else
        {
            
            collectableUIGroup.DOAnchorPosX(471, 0.5f);

        }
        
        
        
        
    }


}
