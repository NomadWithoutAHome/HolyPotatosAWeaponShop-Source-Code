using System;

[Serializable]
public class LegendaryHero
{
	private string legendaryHeroRefId;

	private string legendaryHeroName;

	private string legendaryHeroDescription;

	private string legendaryQuestName;

	private string legendaryQuestDescription;

	private string image;

	private string weaponRefId;

	private int reqAtk;

	private int reqSpd;

	private int reqAcc;

	private int reqMag;

	private string rewardItemType;

	private string rewardItemRefId;

	private int rewardItemQty;

	private int rewardGold;

	private int rewardFame;

	private UnlockCondition unlockCondition;

	private int unlockConditionValue;

	private string checkString;

	private int checkNum;

	private string successComment;

	private string failComment;

	private string heroVisitDialogueSetId;

	private string forgeFailDialogueSetId;

	private string forgeSuccessDialogueRefId;

	private string scenarioLock;

	private int dlc;

	private RequestState requestState;

	public LegendaryHero()
	{
		legendaryHeroRefId = string.Empty;
		legendaryHeroName = string.Empty;
		legendaryHeroDescription = string.Empty;
		legendaryQuestName = string.Empty;
		legendaryQuestDescription = string.Empty;
		image = string.Empty;
		weaponRefId = string.Empty;
		reqAtk = 0;
		reqSpd = 0;
		reqAcc = 0;
		reqMag = 0;
		rewardItemType = string.Empty;
		rewardItemRefId = string.Empty;
		rewardItemQty = 0;
		rewardGold = 0;
		rewardFame = 0;
		unlockCondition = UnlockCondition.UnlockConditionNone;
		unlockConditionValue = 0;
		checkString = string.Empty;
		checkNum = 0;
		successComment = string.Empty;
		failComment = string.Empty;
		heroVisitDialogueSetId = string.Empty;
		forgeFailDialogueSetId = string.Empty;
		forgeSuccessDialogueRefId = string.Empty;
		scenarioLock = string.Empty;
		dlc = 0;
		requestState = RequestState.RequestStateBlank;
	}

	public LegendaryHero(string aRefId, string aName, string aDesc, string aQuestName, string aQuestDesc, string aImage, string aWeaponRefId, int aAtk, int aSpd, int aAcc, int aMag, string aRewardItemType, string aRewardItemRefId, int aRewardItemQty, int aGold, int aFame, UnlockCondition aCondition, int aConditionValue, string aCheckString, int aCheckNum, string aSuccessComment, string aFailComment, string aVisitDialogue, string aFailDialogue, string aSuccessDialogue, string aScenarioLock, int aDlc)
	{
		legendaryHeroRefId = aRefId;
		legendaryHeroName = aName;
		legendaryHeroDescription = aDesc;
		legendaryQuestName = aQuestName;
		legendaryQuestDescription = aQuestDesc;
		image = aImage;
		weaponRefId = aWeaponRefId;
		reqAtk = aAtk;
		reqSpd = aSpd;
		reqAcc = aAcc;
		reqMag = aMag;
		rewardItemType = aRewardItemType;
		rewardItemRefId = aRewardItemRefId;
		rewardItemQty = aRewardItemQty;
		rewardGold = aGold;
		rewardFame = aFame;
		unlockCondition = aCondition;
		unlockConditionValue = aConditionValue;
		checkString = aCheckString;
		checkNum = aCheckNum;
		successComment = aSuccessComment;
		failComment = aFailComment;
		heroVisitDialogueSetId = aVisitDialogue;
		forgeFailDialogueSetId = aFailDialogue;
		forgeSuccessDialogueRefId = aSuccessDialogue;
		scenarioLock = aScenarioLock;
		dlc = aDlc;
		requestState = RequestState.RequestStateBlank;
	}

	public string getLegendaryHeroRefId()
	{
		return legendaryHeroRefId;
	}

	public string getLegendaryHeroName()
	{
		return legendaryHeroName;
	}

	public string getLegendaryHeroDescription()
	{
		return legendaryHeroDescription;
	}

	public string getLegendaryQuestName()
	{
		return legendaryQuestName;
	}

	public string getLegendaryQuestDescription()
	{
		return legendaryQuestDescription;
	}

	public string getImage()
	{
		return image;
	}

	public string getWeaponRefId()
	{
		return weaponRefId;
	}

	public int getReqAtk()
	{
		return reqAtk;
	}

	public int getReqSpd()
	{
		return reqSpd;
	}

	public int getReqAcc()
	{
		return reqAcc;
	}

	public int getReqMag()
	{
		return reqMag;
	}

	public string getRewardItemType()
	{
		return rewardItemType;
	}

	public string getRewardItemRefId()
	{
		return rewardItemRefId;
	}

	public int getRewardItemQty()
	{
		return rewardItemQty;
	}

	public int getRewardGold()
	{
		return rewardGold;
	}

	public int getRewardFame()
	{
		return rewardFame;
	}

	public UnlockCondition getUnlockCondition()
	{
		return unlockCondition;
	}

	public int getUnlockConditionValue()
	{
		return unlockConditionValue;
	}

	public string getCheckString()
	{
		return checkString;
	}

	public int getCheckNum()
	{
		return checkNum;
	}

	public string getSuccessComment()
	{
		return successComment;
	}

	public string getFailComment()
	{
		return failComment;
	}

	public string getHeroVisitDialogueSetId()
	{
		return heroVisitDialogueSetId;
	}

	public string getForgeFailDialogueSetId()
	{
		return forgeFailDialogueSetId;
	}

	public string getForgeSuccessDialogueRefId()
	{
		return forgeSuccessDialogueRefId;
	}

	public void setRequestState(RequestState aState)
	{
		requestState = aState;
	}

	public int getDlc()
	{
		return dlc;
	}

	public bool checkScenarioAllow(string aScenario)
	{
		if (scenarioLock.Length > 0)
		{
			string[] array = scenarioLock.Split('@');
			string[] array2 = array;
			foreach (string text in array2)
			{
				if (text == aScenario)
				{
					return false;
				}
			}
		}
		return true;
	}

	public RequestState getRequestState()
	{
		return requestState;
	}

	public bool trySubmitWeapon(Project aProject)
	{
		if (aProject.getProjectWeapon().getWeaponRefId() == weaponRefId && aProject.getAtk() >= reqAtk && aProject.getSpd() >= reqSpd && aProject.getAcc() >= reqAcc && aProject.getMag() >= reqMag)
		{
			return true;
		}
		return false;
	}
}
