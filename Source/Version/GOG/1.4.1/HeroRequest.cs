using System;
using System.Collections.Generic;

[Serializable]
public class HeroRequest
{
	private string requestId;

	private string requestHero;

	private int requestDuration;

	private int requestRewardGold;

	private int requestRewardLoyalty;

	private int requestRewardFame;

	private Dictionary<string, int> rewardItemList;

	private string requestName;

	private string requestDesc;

	private string weaponTypeRefIdReq;

	private string weaponRefIdReq;

	private int atkReq;

	private int spdReq;

	private int accReq;

	private int magReq;

	private string enchantmentReq;

	private long requestStartTime;

	private RequestState requestState;

	private string deliveredProjectId;

	public HeroRequest()
	{
		requestId = string.Empty;
		requestHero = string.Empty;
		requestDuration = 0;
		requestRewardGold = 0;
		requestRewardLoyalty = 0;
		requestRewardFame = 0;
		rewardItemList = new Dictionary<string, int>();
		requestName = string.Empty;
		requestDesc = string.Empty;
		weaponTypeRefIdReq = string.Empty;
		weaponRefIdReq = string.Empty;
		atkReq = 0;
		spdReq = 0;
		accReq = 0;
		magReq = 0;
		enchantmentReq = string.Empty;
		requestStartTime = 0L;
		requestState = RequestState.RequestStateBlank;
		deliveredProjectId = string.Empty;
	}

	public HeroRequest(string aId, string aHero, int aDuration, int aGold, int aLoyalty, int aFame, Dictionary<string, int> aItemList, string aName, string aDesc, string aWeaponTypeReq, string aWeaponReq, int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, string aEnchantmentReq, long aStartTime)
	{
		requestId = aId;
		requestHero = aHero;
		requestDuration = aDuration;
		requestRewardGold = aGold;
		requestRewardLoyalty = aLoyalty;
		requestRewardFame = aFame;
		rewardItemList = aItemList;
		requestName = aName;
		requestDesc = aDesc;
		weaponTypeRefIdReq = aWeaponTypeReq;
		weaponRefIdReq = aWeaponReq;
		atkReq = aAtkReq;
		spdReq = aSpdReq;
		accReq = aAccReq;
		magReq = aMagReq;
		enchantmentReq = aEnchantmentReq;
		requestStartTime = aStartTime;
		requestState = RequestState.RequestStatePending;
		deliveredProjectId = string.Empty;
	}

	public void setRequestState(RequestState aState)
	{
		requestState = aState;
	}

	public RequestState getRequestState()
	{
		return requestState;
	}

	public int getRequestTimeLeft(long currentTime)
	{
		int num = (int)(requestStartTime + requestDuration - currentTime);
		if (num < 0)
		{
			num = 0;
		}
		return num;
	}

	public long getRequestStartTime()
	{
		return requestStartTime;
	}

	public string getRequestId()
	{
		return requestId;
	}

	public string getRequestHero()
	{
		return requestHero;
	}

	public int getRequestDuration()
	{
		return requestDuration;
	}

	public int getRequestRewardGold()
	{
		return requestRewardGold;
	}

	public int getRequestRewardLoyalty()
	{
		return requestRewardLoyalty;
	}

	public int getRequestRewardFame()
	{
		return requestRewardFame;
	}

	public Dictionary<string, int> getRequestRewardItemList()
	{
		return rewardItemList;
	}

	public string getRequestName()
	{
		return requestName;
	}

	public string getRequestDesc()
	{
		return requestDesc;
	}

	public string getRequestWeaponTypeRefIdReq()
	{
		return weaponTypeRefIdReq;
	}

	public string getRequestWeaponRefIdReq()
	{
		return weaponRefIdReq;
	}

	public bool checkHasStatReq()
	{
		if (atkReq > 0 || spdReq > 0 || accReq > 0 || magReq > 0)
		{
			return true;
		}
		return false;
	}

	public List<int> getRequestStatReq()
	{
		List<int> list = new List<int>();
		list.Add(atkReq);
		list.Add(spdReq);
		list.Add(accReq);
		list.Add(magReq);
		return list;
	}

	public int getRequestAtkReq()
	{
		return atkReq;
	}

	public int getRequestSpdReq()
	{
		return spdReq;
	}

	public int getRequestAccReq()
	{
		return accReq;
	}

	public int getRequestMagReq()
	{
		return magReq;
	}

	public string getRequestEnchantmentReq()
	{
		return enchantmentReq;
	}

	public string getDeliveredProjectId()
	{
		return deliveredProjectId;
	}

	public void setDeliveredProjectId(string aId)
	{
		deliveredProjectId = aId;
	}

	public bool checkProjectDeliverable(Project aProject)
	{
		if (getRequestWeaponTypeRefIdReq() != string.Empty && aProject.getProjectWeapon().getWeaponTypeRefId() != getRequestWeaponTypeRefIdReq())
		{
			return false;
		}
		if (getRequestWeaponRefIdReq() != string.Empty && aProject.getProjectWeapon().getWeaponRefId() != getRequestWeaponRefIdReq())
		{
			return false;
		}
		if (getRequestEnchantmentReq() != string.Empty)
		{
			GameData gameData = CommonAPI.getGameData();
			Item itemByRefId = gameData.getItemByRefId(getRequestEnchantmentReq());
			string itemEffectString = itemByRefId.getItemEffectString();
			if (aProject.getProjectPrefix() != itemEffectString)
			{
				return false;
			}
		}
		if (checkHasStatReq() && (getRequestAtkReq() > aProject.getAtk() || getRequestSpdReq() > aProject.getSpd() || getRequestAccReq() > aProject.getAcc() || getRequestMagReq() > aProject.getMag()))
		{
			return false;
		}
		return true;
	}

	public void expireRequest()
	{
		requestState = RequestState.RequestStateExpired;
	}

	public bool tryDeliverProject(Project aProject)
	{
		bool flag = checkProjectDeliverable(aProject);
		if (flag)
		{
			setDeliveredProjectId(aProject.getProjectId());
			requestState = RequestState.RequestStateCompleted;
		}
		return flag;
	}
}
