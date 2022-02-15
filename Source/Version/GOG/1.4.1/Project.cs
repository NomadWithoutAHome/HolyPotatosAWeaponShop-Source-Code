using System;
using System.Collections.Generic;

[Serializable]
public class Project
{
	private string projectId;

	private string projectName;

	private string projectDesc;

	private bool isPlayerNamed;

	private Weapon projectWeapon;

	private Contract projectContract;

	private ProjectType projectType;

	private ProjectState projectState;

	private ProjectSaleState projectSaleState;

	private ProjectPhase projectPhase;

	private int timeLimit;

	private int timeElapsed;

	private long endTime;

	private int progressMax;

	private int progress;

	private int atk;

	private int spd;

	private int acc;

	private int mag;

	private int atkReq;

	private int spdReq;

	private int accReq;

	private int magReq;

	private Item enchantItem;

	private int numBoost;

	private int maxBoost;

	private List<WeaponStat> prevBoost;

	private List<Offer> offerList;

	private Hero buyer;

	private int finalPrice;

	private int finalScore;

	private Offer selectedOffer;

	private int qualifyGoldenHammer;

	private List<ProjectAchievement> projectAchievementList;

	private List<Smith> usedSmithList;

	private int forgeCost;

	public Project()
	{
		projectId = string.Empty;
		projectName = string.Empty;
		projectDesc = string.Empty;
		projectType = ProjectType.ProjectTypeNothing;
		projectState = ProjectState.ProjectStateBlank;
		projectSaleState = ProjectSaleState.ProjectSaleStateBlank;
		isPlayerNamed = false;
		projectWeapon = new Weapon();
		projectContract = new Contract();
		timeLimit = 0;
		timeElapsed = 0;
		endTime = 0L;
		progressMax = 100;
		progress = 0;
		atk = 0;
		spd = 0;
		acc = 0;
		mag = 0;
		enchantItem = new Item();
		atkReq = 0;
		spdReq = 0;
		accReq = 0;
		magReq = 0;
		numBoost = 0;
		maxBoost = 0;
		prevBoost = new List<WeaponStat>();
		projectPhase = ProjectPhase.ProjectPhaseBlank;
		offerList = new List<Offer>();
		buyer = new Hero();
		finalPrice = 0;
		finalScore = 0;
		selectedOffer = new Offer();
		qualifyGoldenHammer = -1;
		projectAchievementList = new List<ProjectAchievement>();
		usedSmithList = new List<Smith>();
		forgeCost = 0;
	}

	public Project(string aProjectId, string aProjectRefId, string aName, string aDesc, int aGold, int aLaw, int aChaos, ProjectType aProjectType, int aLimit, int aAtkReq, int aSpdReq, int aAccReq, int aMagReq, int aMaxBoost, int aMonth)
	{
		projectId = aProjectId;
		projectName = aName;
		projectDesc = aDesc;
		projectType = aProjectType;
		projectState = ProjectState.ProjectStateBlank;
		projectSaleState = ProjectSaleState.ProjectSaleStateBlank;
		isPlayerNamed = false;
		projectWeapon = new Weapon();
		projectContract = new Contract();
		timeLimit = aLimit;
		timeElapsed = 0;
		endTime = 0L;
		progressMax = 100;
		progress = 0;
		atk = 0;
		spd = 0;
		acc = 0;
		mag = 0;
		enchantItem = new Item();
		atkReq = 0;
		spdReq = 0;
		accReq = 0;
		magReq = 0;
		numBoost = 0;
		maxBoost = aMaxBoost;
		prevBoost = new List<WeaponStat>();
		projectPhase = ProjectPhase.ProjectPhaseBlank;
		offerList = new List<Offer>();
		buyer = new Hero();
		finalPrice = 0;
		finalScore = 0;
		selectedOffer = new Offer();
		qualifyGoldenHammer = -1;
		projectAchievementList = new List<ProjectAchievement>();
		usedSmithList = new List<Smith>();
		forgeCost = 0;
	}

	public int getForgeCost()
	{
		return forgeCost;
	}

	public void setForgeCost(int aCost)
	{
		forgeCost = aCost;
	}

	public void addForgeCost(int aCost)
	{
		forgeCost += aCost;
	}

	public List<Smith> getUsedSmithList()
	{
		return usedSmithList;
	}

	public void setUsedSmithList(List<Smith> aList)
	{
		usedSmithList = aList;
	}

	public void addUsedSmith(Smith aSmith)
	{
		usedSmithList.Add(aSmith);
		if (aSmith.checkOutsource())
		{
			addForgeCost(aSmith.getSmithHireCost());
		}
	}

	public int getTimeElapsed()
	{
		return timeElapsed;
	}

	public void setTimeElapsed(int aElapsed)
	{
		timeElapsed = aElapsed;
	}

	public int addTimeElapsed(int aElapsed)
	{
		timeElapsed += aElapsed;
		return timeElapsed;
	}

	public void autoGenerateWeaponName()
	{
		List<int> list = new List<int>();
		string empty = string.Empty;
		projectName = empty + projectWeapon.getWeaponName(isDisplay: false);
	}

	public int getProjectProgressPercent()
	{
		if (projectType == ProjectType.ProjectTypeWeapon || projectType == ProjectType.ProjectTypeUnique)
		{
			return (int)((float)progress / (float)progressMax * 100f);
		}
		return (int)((float)(atk + spd + acc + mag) / (float)(atkReq + spdReq + accReq + magReq) * 100f);
	}

	public int getNumBoost()
	{
		return numBoost;
	}

	public void setNumBoost(int aBoost)
	{
		numBoost = aBoost;
	}

	public bool checkCanBoost()
	{
		if (numBoost < maxBoost)
		{
			return true;
		}
		return false;
	}

	public int checkBoostPenalty(WeaponStat aStat)
	{
		int num = 0;
		foreach (WeaponStat item in prevBoost)
		{
			if (item == aStat)
			{
				num++;
			}
		}
		return num;
	}

	public void addBoost(WeaponStat aStat)
	{
		prevBoost.Add(aStat);
		numBoost++;
	}

	public List<WeaponStat> getPrevBoost()
	{
		return prevBoost;
	}

	public void setPrevBoost(List<WeaponStat> boostList)
	{
		prevBoost = boostList;
	}

	public int getMaxBoost()
	{
		return maxBoost;
	}

	public void setMaxBoost(int aMax)
	{
		maxBoost = aMax;
	}

	public void addMaxBoost(int aAdd)
	{
		maxBoost += aAdd;
	}

	public int getProgressMax()
	{
		return progressMax;
	}

	public void setProgressMax(int aMax)
	{
		progressMax = aMax;
	}

	public int getProgress()
	{
		return progress;
	}

	public void addProgress(int units)
	{
		progress += units;
		if (progress > progressMax)
		{
			progress = progressMax;
		}
	}

	public void setProgress(int aProgress)
	{
		progress = aProgress;
	}

	public bool checkContractTimeLeft(long currentTime)
	{
		if (endTime > currentTime)
		{
			return false;
		}
		return true;
	}

	public long getContractTimeLeft(long currentTime)
	{
		return endTime - currentTime;
	}

	public long getContractEndTime()
	{
		return endTime;
	}

	public bool checkContractFinish()
	{
		if (getAtkReqLeft() > 0 || getSpdReqLeft() > 0 || getAccReqLeft() > 0 || getMagReqLeft() > 0)
		{
			return false;
		}
		return true;
	}

	public void resetProjectStats()
	{
		atk = 0;
		spd = 0;
		acc = 0;
		mag = 0;
		timeElapsed = 0;
	}

	public List<int> calculateHeroWeaponStats()
	{
		List<int> list = new List<int>();
		list.Add(atk);
		list.Add(spd);
		list.Add(acc);
		list.Add(mag);
		return list;
	}

	public float getWeaponStatRatio()
	{
		int totalStat = getTotalStat();
		int num = atkReq + spdReq + accReq + magReq;
		return (float)totalStat / (float)num;
	}

	public bool checkStatsAllPass()
	{
		List<float> list = compareStat();
		foreach (float item in list)
		{
			float num = item;
			if (num < 1f)
			{
				return false;
			}
		}
		return true;
	}

	public List<float> compareStat()
	{
		List<float> list = new List<float>();
		List<int> list2 = calculateHeroWeaponStats();
		list.Add((float)list2[0] / (float)atkReq);
		list.Add((float)list2[1] / (float)spdReq);
		list.Add((float)list2[2] / (float)accReq);
		list.Add((float)list2[3] / (float)magReq);
		return list;
	}

	public List<WeaponStat> getPriSecStat()
	{
		List<WeaponStat> list = new List<WeaponStat>();
		List<int> list2 = new List<int>();
		list2.Add(atk);
		list2.Add(spd);
		list2.Add(acc);
		list2.Add(mag);
		List<int> list3 = CommonAPI.sortIndices(list2, isAscending: false);
		switch (list3[0])
		{
		case 0:
			list.Add(WeaponStat.WeaponStatAttack);
			break;
		case 1:
			list.Add(WeaponStat.WeaponStatSpeed);
			break;
		case 2:
			list.Add(WeaponStat.WeaponStatAccuracy);
			break;
		case 3:
			list.Add(WeaponStat.WeaponStatMagic);
			break;
		}
		switch (list3[1])
		{
		case 0:
			list.Add(WeaponStat.WeaponStatAttack);
			break;
		case 1:
			list.Add(WeaponStat.WeaponStatSpeed);
			break;
		case 2:
			list.Add(WeaponStat.WeaponStatAccuracy);
			break;
		case 3:
			list.Add(WeaponStat.WeaponStatMagic);
			break;
		}
		return list;
	}

	public string getProjectId()
	{
		return projectId;
	}

	public void setProjectId(string aId)
	{
		projectId = aId;
	}

	public string getProjectName(bool includePrefix)
	{
		if (includePrefix)
		{
			string text = string.Empty;
			if (CommonAPI.checkReversePrefixFormat())
			{
				if (enchantItem.getItemRefId() != string.Empty)
				{
					text = " [FF4842](" + enchantItem.getItemEffectString() + ")[-]";
				}
				return projectName + text;
			}
			if (enchantItem.getItemRefId() != string.Empty)
			{
				text = "[FF4842]" + enchantItem.getItemEffectString() + "[-] ";
			}
			return text + projectName;
		}
		return projectName;
	}

	public string getProjectPrefix()
	{
		if (enchantItem.getItemRefId() != string.Empty)
		{
			return enchantItem.getItemEffectString();
		}
		return string.Empty;
	}

	public void setProjectName(string aName)
	{
		projectName = aName;
	}

	public string getProjectDesc()
	{
		return projectDesc;
	}

	public void setProjectDesc(string aDesc)
	{
		projectDesc = aDesc;
	}

	public bool getPlayerNamed()
	{
		return isPlayerNamed;
	}

	public void setPlayerNamed(bool aValue)
	{
		isPlayerNamed = aValue;
	}

	public ProjectType getProjectType()
	{
		return projectType;
	}

	public void setProjectType(ProjectType aType)
	{
		projectType = aType;
	}

	public ProjectState getProjectState()
	{
		return projectState;
	}

	public void setProjectState(ProjectState aState)
	{
		projectState = aState;
	}

	public ProjectSaleState getProjectSaleState()
	{
		return projectSaleState;
	}

	public void setProjectSaleState(ProjectSaleState aState)
	{
		projectSaleState = aState;
	}

	public ProjectPhase getProjectPhase()
	{
		return projectPhase;
	}

	public void setProjectPhase(ProjectPhase aPhase)
	{
		projectPhase = aPhase;
	}

	public Weapon getProjectWeapon()
	{
		return projectWeapon;
	}

	public void setProjectWeapon(Weapon aWeapon)
	{
		projectWeapon = aWeapon;
	}

	public Contract getProjectContract()
	{
		return projectContract;
	}

	public void setProjectContract(Contract aContract)
	{
		projectContract = aContract;
		atkReq = projectContract.getAtkReq();
		spdReq = projectContract.getSpdReq();
		accReq = projectContract.getAccReq();
		magReq = projectContract.getMagReq();
	}

	public int getTotalReq()
	{
		return atkReq + spdReq + accReq + magReq;
	}

	public int getTotalStat()
	{
		List<int> list = calculateHeroWeaponStats();
		int num = 0;
		foreach (int item in list)
		{
			num += item;
		}
		return num;
	}

	public int getAtk()
	{
		return atk;
	}

	public void setAtk(int aStat)
	{
		atk = aStat;
		if (projectType == ProjectType.ProjectTypeContract && atk > atkReq)
		{
			atk = atkReq;
		}
		if (atk < 0)
		{
			atk = 0;
		}
	}

	public void addAtk(int aStat)
	{
		atk += aStat;
		if (projectType == ProjectType.ProjectTypeContract && atk > atkReq)
		{
			atk = atkReq;
		}
		if (atk < 0)
		{
			atk = 0;
		}
	}

	public int getSpd()
	{
		return spd;
	}

	public void setSpd(int aStat)
	{
		spd = aStat;
		if (projectType == ProjectType.ProjectTypeContract && spd > spdReq)
		{
			spd = spdReq;
		}
		if (spd < 0)
		{
			spd = 0;
		}
	}

	public void addSpd(int aStat)
	{
		spd += aStat;
		if (projectType == ProjectType.ProjectTypeContract && spd > spdReq)
		{
			spd = spdReq;
		}
		if (spd < 0)
		{
			spd = 0;
		}
	}

	public int getAcc()
	{
		return acc;
	}

	public void setAcc(int aStat)
	{
		acc = aStat;
		if (projectType == ProjectType.ProjectTypeContract && acc > accReq)
		{
			acc = accReq;
		}
		if (acc < 0)
		{
			acc = 0;
		}
	}

	public void addAcc(int aStat)
	{
		acc += aStat;
		if (projectType == ProjectType.ProjectTypeContract && acc > accReq)
		{
			acc = accReq;
		}
		if (acc < 0)
		{
			acc = 0;
		}
	}

	public int getMag()
	{
		return mag;
	}

	public void setMag(int aStat)
	{
		mag = aStat;
		if (projectType == ProjectType.ProjectTypeContract && mag > magReq)
		{
			mag = magReq;
		}
		if (mag < 0)
		{
			mag = 0;
		}
	}

	public void addMag(int aStat)
	{
		mag += aStat;
		if (projectType == ProjectType.ProjectTypeContract && mag > magReq)
		{
			mag = magReq;
		}
		if (mag < 0)
		{
			mag = 0;
		}
	}

	public bool enchantWeapon(Item item)
	{
		bool result = false;
		if (enchantItem.getItemRefId() == string.Empty && item.getItemType() == ItemType.ItemTypeEnhancement && item.tryUseItem(1, isUse: true))
		{
			enchantItem = item;
			result = true;
		}
		return result;
	}

	public Item getEnchantItem()
	{
		return enchantItem;
	}

	public void setEnchantItem(Item aItem)
	{
		enchantItem = aItem;
	}

	public int getTimeLimit()
	{
		return timeLimit;
	}

	public string getTimeLimitString()
	{
		return CommonAPI.convertHalfHoursToTimeString(timeLimit);
	}

	public void setEndTime(long aEndTime)
	{
		endTime = aEndTime;
	}

	public string getReqString()
	{
		string text = " | ";
		if (atkReq > 0)
		{
			string text2 = text;
			text = text2 + "Atk " + atkReq + " | ";
		}
		if (spdReq > 0)
		{
			string text2 = text;
			text = text2 + "Spd " + spdReq + " | ";
		}
		if (accReq > 0)
		{
			string text2 = text;
			text = text2 + "Acc " + accReq + " | ";
		}
		if (magReq > 0)
		{
			string text2 = text;
			text = text2 + "Mag " + magReq + " | ";
		}
		return text;
	}

	public int getAtkReq()
	{
		return atkReq;
	}

	public int getSpdReq()
	{
		return spdReq;
	}

	public int getAccReq()
	{
		return accReq;
	}

	public int getMagReq()
	{
		return magReq;
	}

	public int getAtkReqLeft()
	{
		return atkReq - atk;
	}

	public int getSpdReqLeft()
	{
		return spdReq - spd;
	}

	public int getAccReqLeft()
	{
		return accReq - acc;
	}

	public int getMagReqLeft()
	{
		return magReq - mag;
	}

	public List<Offer> getOfferList()
	{
		return offerList;
	}

	public void setOfferList(List<Offer> aOfferList)
	{
		offerList = aOfferList;
	}

	public void addOfferList(Offer aOffer)
	{
		offerList.Add(aOffer);
	}

	public Offer getSelectedOffer()
	{
		return selectedOffer;
	}

	public void setSelectedOffer(Offer aOffer)
	{
		selectedOffer = aOffer;
	}

	public Hero getBuyer()
	{
		return buyer;
	}

	public void setBuyer(Hero aHero)
	{
		buyer = aHero;
	}

	public int getFinalPrice()
	{
		return finalPrice;
	}

	public void setFinalPrice(int aPrice)
	{
		finalPrice = aPrice;
	}

	public int getFinalScore()
	{
		return finalScore;
	}

	public void setFinalScore(int aScore)
	{
		finalScore = aScore;
	}

	public int getProjectStat(WeaponStat stat)
	{
		return stat switch
		{
			WeaponStat.WeaponStatAttack => getAtk(), 
			WeaponStat.WeaponStatSpeed => getSpd(), 
			WeaponStat.WeaponStatAccuracy => getAcc(), 
			WeaponStat.WeaponStatMagic => getMag(), 
			_ => 0, 
		};
	}

	public bool checkIsForging()
	{
		if (projectType == ProjectType.ProjectTypeWeapon || projectType == ProjectType.ProjectTypeContract)
		{
			return true;
		}
		return false;
	}

	public bool checkQualifyGoldenHammer(int checkYear)
	{
		if (checkYear == qualifyGoldenHammer || checkYear == -1)
		{
			return true;
		}
		return false;
	}

	public int getQualifyGoldenHammer()
	{
		return qualifyGoldenHammer;
	}

	public void setQualifyGoldenHammer(int aQualifyYear)
	{
		qualifyGoldenHammer = aQualifyYear;
	}

	public List<ProjectAchievement> getProjectAchievementList()
	{
		return projectAchievementList;
	}

	public string getProjectAchievementListString()
	{
		string text = string.Empty;
		foreach (ProjectAchievement projectAchievement in projectAchievementList)
		{
			if (text != string.Empty)
			{
				text += "@";
			}
			text += projectAchievement;
		}
		return text;
	}

	public void setProjectAchievementList(List<ProjectAchievement> aList)
	{
		projectAchievementList = aList;
	}

	public void addProjectAchievementList(ProjectAchievement aAchievement)
	{
		projectAchievementList.Add(aAchievement);
	}

	public bool checkForAchievement(ProjectAchievement aAchievement)
	{
		if (projectAchievementList.Contains(aAchievement))
		{
			return true;
		}
		return false;
	}
}
