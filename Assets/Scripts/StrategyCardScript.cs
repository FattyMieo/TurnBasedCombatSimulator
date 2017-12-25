using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class StrategyCardScript : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	public ItemType itemType;

	public Image cardImage;
	public Text cardSymbol;
	public Text cardName;
	public Text cardTitle;

	public GameObject tooltip;
	public Text cardDesc;

	// Use this for initialization
	void Start ()
	{
		GetComponent<Button>().onClick.AddListener(OnClick);
	}

	void OnClick ()
	{
		tooltip.SetActive(false);

		if(itemType == ItemType.HealingElixir)
		{
			BattleManagerScript.Instance.playerScript.unitScript.Heal();
		}
		else if(itemType == ItemType.SmiteCard)
		{
			BattleManagerScript.Instance.engagedEnemy.unitScript.Damage();
		}
		else if(itemType != ItemType.Total)
		{
			if(BattleManagerScript.Instance.playerAttack) BattleManagerScript.Instance.attackingCard = (AttributeType)itemType;
			BattleManagerScript.Instance.playerScript.attrScript.bonusAttributeCount[(int)itemType]++;
		}

		GameManagerScript.Instance.inventory.RemoveItem(itemType, 1);
		BattleManagerScript.Instance.cardsClicked++;
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		tooltip.SetActive(true);
		cardDesc.text = Item.GetDesc(itemType);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		tooltip.SetActive(false);
	}

	public void SetCardDetails(ItemType newType)
	{
		itemType = newType;

		if(itemType == ItemType.HealingElixir)
		{
			cardTitle.text = "Item";
			cardImage.sprite = Item.GetSprite(itemType);
			cardImage.color = Color.white;
			cardSymbol.color = Color.clear;
		}
		else if(itemType == ItemType.SmiteCard)
		{
			cardTitle.text = "Item";
			cardImage.sprite = Item.GetSprite(itemType);
			cardImage.color = Color.white;
			cardSymbol.color = Color.clear;
		}
		else if(itemType != ItemType.Total)
		{
			cardTitle.text = "Strategy";
			cardImage.color = Color.clear;
			cardSymbol.color = Color.black;
			switch(itemType)
			{
				case ItemType.StrategyCard_Offense:
					cardSymbol.text = "X";
					break;
				case ItemType.StrategyCard_Defense:
					cardSymbol.text = "O";
					break;
				case ItemType.StrategyCard_Flank:
					cardSymbol.text = "&";
					break;
				case ItemType.StrategyCard_Inflitrate:
					cardSymbol.text = "%";
					break;
				case ItemType.StrategyCard_Suppress:
					cardSymbol.text = ">";
					break;
			}
		}

		cardName.text = Item.GetName(itemType);
	}
}
