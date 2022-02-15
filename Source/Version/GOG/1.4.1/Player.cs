using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

[Serializable]
public class Player
{
	private string playerId;

	private string gameScenario;

	private string playerName;

	private string shopName;

	private string shopLevelRefId;

	private ShopLevel shopLevel;

	private int areaRegion;

	private int playerFame;

	private int prevRegionFame;

	private int playerTickets;

	private int playerUsedTickets;

	private int prevRegionTickets;

	private bool hasAvatar;

	private string avatarRefId;

	private string avatarName;

	private string avatarDescription;

	private string avatarImage;

	private int playerGold;

	private int playerLaw;

	private int playerChaos;

	private int usedEmblems;

	private bool tofuKnown;

	private bool randomDog;

	private bool hasDog;

	private string dogBedRefID;

	private bool playerNamedDog;

	private string dogName;

	private int dogLove;

	private float dogEnergy;

	private long dogLastFed;

	private int dogActivityCountMonth;

	private int dogActivityCountTotal;

	private bool loanUsed;

	private int salaryChancesUsed;

	private string lastPaidMonthEncode;

	private int lastPaidMonth;

	private Objective currentObjective;

	private string currentObjectiveString;

	private List<Furniture> shopFurnitureList;

	private List<Decoration> playerDecoList;

	private Dictionary<string, Decoration> currentDecoList;

	private List<Smith> playerSmithList;

	private List<Project> completedProjectList;

	private string currentProjectId;

	private Project currentProject;

	private Contract lastSelectContract;

	private Smith lastSelectSmith;

	private Area lastSelectArea;

	private QuestNEW lastSelectQuest;

	private Hero lastSelectHero;

	private WeaponType lastSelectWeaponType;

	private Weapon lastSelectWeapon;

	private List<int> lastDirectionBuild;

	private ProjectType lastSelectProjectType;

	private Project lastSelectProject;

	private QuestNEW lastDoneQuest;

	private string lastDoneQuestRefId;

	private List<Project> displayProject;

	private string displayProjectListString;

	private string showAfterQuestCutscene;

	private List<Contract> playerContractList;

	private string playerContractListString;

	private int lastContractListUpdate;

	private List<WeaponType> unlockedWeaponTypeList;

	private List<Weapon> unlockedWeaponList;

	private List<Hero> unlockedHeroList;

	private int lastDailyAction;

	private string recruitListIdString;

	private List<Smith> recruitList;

	private long playerTimeLong;

	private long lastSentSave;

	private List<TimedAction> actionList;

	private List<int> timerList;

	private Weather weather;

	private ForgeSeasonalEffect forgeEffect;

	private List<ProjectAchievement> nextGoldenHammerAwardList;

	private int nextGoldenHammerYear;

	private Dictionary<int, string> pastGoldenHammerAwardList;

	private int showAwardsFromYear;

	private bool isNamed;

	private string email;

	private int finalScore;

	private SmithStation playerStation;

	private SmithStation dogStation;

	private int designStationLv;

	private int craftStationLv;

	private int polishStationLv;

	private int enchantStationLv;

	private bool enchantLocked;

	private Weapon currentResearchWeapon;

	private Smith currentResearchSmith;

	private int researchLength;

	private int researchTimeLeft;

	private int researchCount;

	private long lastEventDate;

	private int lastAreaEvent;

	private int lastCheckRequest;

	private List<LegendaryHero> displayLegendaryList;

	private List<LegendaryHero> completedLegendaryList;

	private List<HeroRequest> displayRequestList;

	private List<HeroRequest> completedRequestList;

	private int requestCount;

	private int activityID;

	private Dictionary<string, Activity> activityList;

	private List<ShopMonthlyStarch> shopMonthlyStarchList;

	private bool skipTutorials;

	private TutorialState tutorialState;

	private int tutorialIndex;

	private int completedTutorialIndex;

	private int totalHeroExpGain;

	public Player(string aPlayerId, string aGameScenario, string aName, string aShopName, string aShopLevelRefId, int aGold, int aLaw, int aChaos, int aUsedEmblems)
	{
		playerId = aPlayerId;
		gameScenario = aGameScenario;
		playerName = aName;
		shopName = aShopName;
		shopLevelRefId = aShopLevelRefId;
		shopLevel = new ShopLevel();
		areaRegion = 1;
		playerFame = 0;
		prevRegionFame = 0;
		playerTickets = 0;
		playerUsedTickets = 0;
		prevRegionTickets = 0;
		hasAvatar = false;
		avatarRefId = string.Empty;
		playerGold = aGold;
		playerLaw = aLaw;
		playerChaos = aChaos;
		usedEmblems = aUsedEmblems;
		randomDog = true;
		hasDog = false;
		dogBedRefID = string.Empty;
		playerNamedDog = false;
		dogName = "Jagamaru";
		dogLove = 0;
		dogEnergy = 100f;
		dogLastFed = 0L;
		dogActivityCountMonth = 0;
		dogActivityCountTotal = 0;
		loanUsed = false;
		salaryChancesUsed = 0;
		lastPaidMonth = 0;
		currentObjective = new Objective();
		currentObjectiveString = string.Empty;
		shopFurnitureList = new List<Furniture>();
		playerDecoList = new List<Decoration>();
		currentDecoList = new Dictionary<string, Decoration>();
		playerSmithList = new List<Smith>();
		completedProjectList = new List<Project>();
		currentProjectId = string.Empty;
		currentProject = new Project();
		unlockedWeaponTypeList = new List<WeaponType>();
		unlockedWeaponList = new List<Weapon>();
		unlockedHeroList = new List<Hero>();
		playerContractList = new List<Contract>();
		playerContractListString = string.Empty;
		lastContractListUpdate = 0;
		lastSelectWeaponType = new WeaponType();
		lastSelectWeapon = new Weapon();
		lastSelectQuest = new QuestNEW();
		lastSelectHero = new Hero();
		lastSelectContract = new Contract();
		lastSelectSmith = new Smith();
		lastSelectArea = new Area();
		lastDirectionBuild = new List<int>();
		lastDirectionBuild.Add(1);
		lastDirectionBuild.Add(1);
		lastDirectionBuild.Add(1);
		lastDirectionBuild.Add(1);
		lastSelectProjectType = ProjectType.ProjectTypeNothing;
		lastSelectProject = new Project();
		displayProject = new List<Project>();
		displayProjectListString = string.Empty;
		lastDoneQuest = new QuestNEW();
		lastDoneQuestRefId = string.Empty;
		showAfterQuestCutscene = string.Empty;
		lastDailyAction = -1;
		recruitListIdString = string.Empty;
		recruitList = new List<Smith>();
		playerTimeLong = 14L;
		lastSentSave = 0L;
		actionList = new List<TimedAction>();
		timerList = new List<int>();
		weather = new Weather();
		forgeEffect = ForgeSeasonalEffect.ForgeSeasonalEffectNothing;
		nextGoldenHammerAwardList = new List<ProjectAchievement>();
		nextGoldenHammerYear = 0;
		pastGoldenHammerAwardList = new Dictionary<int, string>();
		showAwardsFromYear = -1;
		isNamed = false;
		tofuKnown = false;
		email = string.Empty;
		finalScore = -1;
		playerStation = SmithStation.SmithStationBlank;
		dogStation = SmithStation.SmithStationBlank;
		designStationLv = 1;
		craftStationLv = 1;
		polishStationLv = 1;
		enchantStationLv = 1;
		enchantLocked = false;
		currentResearchWeapon = null;
		currentResearchSmith = null;
		researchLength = 0;
		researchTimeLeft = 0;
		researchCount = 0;
		lastEventDate = 0L;
		lastAreaEvent = 0;
		lastCheckRequest = 0;
		displayLegendaryList = new List<LegendaryHero>();
		completedLegendaryList = new List<LegendaryHero>();
		displayRequestList = new List<HeroRequest>();
		completedRequestList = new List<HeroRequest>();
		requestCount = 0;
		activityID = 10000;
		activityList = new Dictionary<string, Activity>();
		shopMonthlyStarchList = new List<ShopMonthlyStarch>();
		skipTutorials = false;
		tutorialState = TutorialState.TutorialStateStart;
		tutorialIndex = -1;
		completedTutorialIndex = -1;
		totalHeroExpGain = 0;
	}

	public void addFame(int aFame)
	{
		playerFame += aFame;
	}

	public int getFame()
	{
		return playerFame;
	}

	public void setFame(int aFame)
	{
		playerFame = aFame;
	}

	public int tryGiveTicket()
	{
		int num = 0;
		int ticketInterval = CommonAPI.getGameData().getAreaRegionByRefID(areaRegion).getTicketInterval();
		int maxTicketsInRegion = CommonAPI.getGameData().getMaxTicketsInRegion(areaRegion);
		int num2 = (playerFame - prevRegionFame) / ticketInterval - (playerTickets - prevRegionTickets);
		for (int i = 0; i < num2; i++)
		{
			if (playerTickets - prevRegionTickets < maxTicketsInRegion)
			{
				playerTickets++;
				num++;
			}
		}
		return num;
	}

	public bool tryUseTicket(int numUse)
	{
		if (numUse <= getUnusedTickets())
		{
			playerUsedTickets += numUse;
			return true;
		}
		return false;
	}

	public int getUnusedTickets()
	{
		return playerTickets - playerUsedTickets;
	}

	public int getTickets()
	{
		return playerTickets;
	}

	public void setTickets(int aTickets)
	{
		playerTickets = aTickets;
	}

	public int getUsedTickets()
	{
		return playerUsedTickets;
	}

	public void setUsedTickets(int aUsed)
	{
		playerUsedTickets = aUsed;
	}

	public int getPrevRegionFame()
	{
		return prevRegionFame;
	}

	public void setPrevRegionFame(int aPrevFame)
	{
		prevRegionFame = aPrevFame;
	}

	public int getPrevRegionTickets()
	{
		return prevRegionTickets;
	}

	public void setPrevRegionTickets(int aPrevTickets)
	{
		prevRegionTickets = aPrevTickets;
	}

	public void setLastAreaEvent(int aLastAreaEvent)
	{
		lastAreaEvent = aLastAreaEvent;
	}

	public int getLastAreaEvent()
	{
		return lastAreaEvent;
	}

	public bool tryAreaEvent()
	{
		int playerDays = getPlayerDays();
		if (playerDays > lastAreaEvent)
		{
			lastAreaEvent = playerDays;
			return true;
		}
		return false;
	}

	public void setLastCheckRequest(int aLastRequest)
	{
		lastCheckRequest = aLastRequest;
	}

	public int getLastCheckRequest()
	{
		return lastCheckRequest;
	}

	public bool tryGiveRequest()
	{
		int playerDays = getPlayerDays();
		if (playerDays > lastCheckRequest && displayRequestList.Count < 3)
		{
			lastCheckRequest = playerDays;
			return true;
		}
		return false;
	}

	public List<HeroRequest> getDisplayRequestList()
	{
		return displayRequestList;
	}

	public void setDisplayRequestList(List<HeroRequest> aList)
	{
		displayRequestList = aList;
	}

	public void addDisplayRequest(HeroRequest aRequest)
	{
		displayRequestList.Add(aRequest);
	}

	public void removeDisplayRequest(HeroRequest aRequest)
	{
		displayRequestList.Remove(aRequest);
	}

	public List<HeroRequest> getCompletedRequestList()
	{
		return completedRequestList;
	}

	public void setCompletedRequestList(List<HeroRequest> aList)
	{
		completedRequestList = aList;
	}

	public void completeRequest(HeroRequest aRequest)
	{
		displayRequestList.Remove(aRequest);
		completedRequestList.Add(aRequest);
	}

	public int getTotalRequestCount()
	{
		return requestCount;
	}

	public void addTotalRequestCount(int aCount)
	{
		requestCount += aCount;
	}

	public void setTotalRequestCount(int aCount)
	{
		requestCount = aCount;
	}

	public string getDisplayRequestRefIDString()
	{
		string text = string.Empty;
		foreach (HeroRequest displayRequest in displayRequestList)
		{
			if (text != string.Empty)
			{
				text += '@';
			}
			text += displayRequest.getRequestId();
		}
		return text;
	}

	public string getCompletedRequestRefIDString()
	{
		string text = string.Empty;
		foreach (HeroRequest completedRequest in completedRequestList)
		{
			if (text != string.Empty)
			{
				text += '@';
			}
			text += completedRequest.getRequestId();
		}
		return text;
	}

	public bool checkDisplayLegendaryHeroList()
	{
		if (displayLegendaryList.Count < 1)
		{
			return true;
		}
		return false;
	}

	public void addLegendaryHeroRequest(LegendaryHero aLegendary)
	{
		displayLegendaryList.Add(aLegendary);
	}

	public List<LegendaryHero> getLegendaryHeroRequestList(bool acceptedOnly = false)
	{
		if (!acceptedOnly)
		{
			return displayLegendaryList;
		}
		List<LegendaryHero> list = new List<LegendaryHero>();
		foreach (LegendaryHero displayLegendary in displayLegendaryList)
		{
			if (displayLegendary.getRequestState() == RequestState.RequestStateAccepted)
			{
				list.Add(displayLegendary);
			}
		}
		return list;
	}

	public LegendaryHero getLastLegendaryHeroRequest()
	{
		return displayLegendaryList[displayLegendaryList.Count - 1];
	}

	public string getDisplayLegendaryHeroRefIDString()
	{
		string text = string.Empty;
		foreach (LegendaryHero displayLegendary in displayLegendaryList)
		{
			if (text != string.Empty)
			{
				text += '@';
			}
			text += displayLegendary.getLegendaryHeroRefId();
		}
		return text;
	}

	public void addDisplayLegendaryHero(LegendaryHero aHero)
	{
		displayLegendaryList.Add(aHero);
	}

	public string getCompletedLegendaryHeroRefIDString()
	{
		string text = string.Empty;
		foreach (LegendaryHero completedLegendary in completedLegendaryList)
		{
			if (text != string.Empty)
			{
				text += '@';
			}
			text += completedLegendary.getLegendaryHeroRefId();
		}
		return text;
	}

	public void addCompletedLegendaryHero(LegendaryHero aHero)
	{
		completedLegendaryList.Add(aHero);
	}

	public LegendaryHero getOngoingLegendaryHeroByWeapon(string checkWeapon)
	{
		foreach (LegendaryHero legendaryHeroRequest in getLegendaryHeroRequestList(acceptedOnly: true))
		{
			if (legendaryHeroRequest.getWeaponRefId() == checkWeapon)
			{
				return legendaryHeroRequest;
			}
		}
		return new LegendaryHero();
	}

	public void completeLegendaryHero(LegendaryHero aLegendary)
	{
		displayLegendaryList.Remove(aLegendary);
		completedLegendaryList.Add(aLegendary);
	}

	public List<LegendaryHero> getCompletedLegendaryList()
	{
		return completedLegendaryList;
	}

	public bool checkLegendaryCompleted(string aLegendaryRefId)
	{
		foreach (LegendaryHero completedLegendary in completedLegendaryList)
		{
			if (completedLegendary.getLegendaryHeroRefId() == aLegendaryRefId)
			{
				return true;
			}
		}
		return false;
	}

	public void startResearch(Weapon aWeapon, Smith aSmith, int aResearchDuration)
	{
		currentResearchWeapon = aWeapon;
		currentResearchSmith = aSmith;
		researchLength = aResearchDuration;
		researchTimeLeft = aResearchDuration;
	}

	public Weapon getCurrentResearchWeapon()
	{
		return currentResearchWeapon;
	}

	public void setCurrentResearchWeapon(Weapon aWeapon)
	{
		currentResearchWeapon = aWeapon;
	}

	public Smith getCurrentResearchSmith()
	{
		return currentResearchSmith;
	}

	public void setCurrentResearchSmith(Smith aSmith)
	{
		currentResearchSmith = aSmith;
	}

	public int getResearchLength()
	{
		return researchLength;
	}

	public void setResearchLength(int aLength)
	{
		researchLength = aLength;
	}

	public int getResearchTimeLeft()
	{
		return researchTimeLeft;
	}

	public void setResearchTimeLeft(int aTime)
	{
		researchTimeLeft = aTime;
	}

	public int getResearchCount()
	{
		return researchCount;
	}

	public void setResearchCount(int aCount)
	{
		researchCount = aCount;
	}

	public bool checkCanResearch()
	{
		if (currentResearchWeapon == null || currentResearchSmith == null)
		{
			return true;
		}
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithAction().getRefId() == "904")
			{
				return false;
			}
		}
		endResearch(addResearchCount: false);
		return true;
	}

	public float getResearchPercentage()
	{
		return (float)(researchLength - researchTimeLeft) / (float)researchLength;
	}

	public bool passTimeOnResearch(int unitTime)
	{
		researchTimeLeft -= unitTime;
		if (researchTimeLeft <= 0)
		{
			return true;
		}
		return false;
	}

	public void endResearch(bool addResearchCount)
	{
		currentResearchWeapon = null;
		currentResearchSmith = null;
		researchLength = 0;
		researchTimeLeft = 0;
		if (addResearchCount)
		{
			researchCount++;
		}
	}

	public void addJobClass(string aHeroRefId, string aHeroName, string aHeroDesc, string aJobClassName, string aJobClassDesc, int aHeroTier, string aImage, float aBaseAtk, float aBaseSpd, float aBaseAcc, float aBaseMag, WeaponStat aPriStat, WeaponStat aSecStat, int aWealth, int aSellExp, int aRequestLevelMin, int aRequestLevelMax, string aRequestText, Dictionary<string, int> aWeaponTypeAffinity, string aRewardSetId, int aInitExpPoints, string aScenarioLock, int aDlc)
	{
		addToUnlockedJobClassList(new Hero(aHeroRefId, aHeroName, aHeroDesc, aJobClassName, aJobClassDesc, aHeroTier, aImage, aBaseAtk, aBaseSpd, aBaseAcc, aBaseMag, aPriStat, aSecStat, aWealth, aSellExp, aRequestLevelMin, aRequestLevelMax, aRequestText, aWeaponTypeAffinity, aRewardSetId, aInitExpPoints, aScenarioLock, aDlc));
	}

	public void addJobClassByObject(Hero jobClass)
	{
		addToUnlockedJobClassList(jobClass);
	}

	private void addToUnlockedJobClassList(Hero aJobClass)
	{
		if (unlockedHeroList.Count == 0)
		{
			unlockedHeroList.Add(aJobClass);
			return;
		}
		List<Hero> list = new List<Hero>();
		int num = Convert.ToInt32(aJobClass.getHeroRefId());
		bool flag = false;
		foreach (Hero unlockedHero in unlockedHeroList)
		{
			if (unlockedHero.getHeroRefId() != string.Empty)
			{
				if (!flag && Convert.ToInt32(unlockedHero.getHeroRefId()) > num)
				{
					list.Add(aJobClass);
					flag = true;
				}
				list.Add(unlockedHero);
			}
		}
		if (!flag)
		{
			list.Add(aJobClass);
		}
		unlockedHeroList = list;
	}

	public Hero getUnlockedJobClassByRefId(string aRefId)
	{
		foreach (Hero unlockedHero in unlockedHeroList)
		{
			if (unlockedHero.getHeroRefId() == aRefId)
			{
				return unlockedHero;
			}
		}
		return new Hero();
	}

	public void setUnlockedJobClassList(List<Hero> aList)
	{
		unlockedHeroList = aList;
	}

	public List<Hero> getJobClassListByCategory(string categoryRefID)
	{
		List<Hero> list = new List<Hero>();
		foreach (Hero unlockedHero in unlockedHeroList)
		{
			if (unlockedHero.checkUnlocked() && unlockedHero.getHeroRefId().Substring(0, 3) == categoryRefID)
			{
				list.Add(unlockedHero);
			}
		}
		return list;
	}

	public int getJobClassListByCategoryCount(string categoryRefID)
	{
		int num = 0;
		foreach (Hero unlockedHero in unlockedHeroList)
		{
			if (unlockedHero.checkUnlocked() && unlockedHero.getHeroRefId().Substring(0, 3) == categoryRefID)
			{
				num++;
			}
		}
		return num;
	}

	public void addWeaponType(string aRefId, string aName, string aSkill, string aFirstRefId, float aAtkMult, float aSpdMult, float aAccMult, float aMagMult, string aImage)
	{
		WeaponType weaponType = new WeaponType(aRefId, aName, aSkill, aFirstRefId, aAtkMult, aSpdMult, aAccMult, aMagMult, aImage);
		weaponType.doUnlock();
		addToUnlockedWeaponTypeList(weaponType);
	}

	public void unlockWeaponType(WeaponType aWeaponType)
	{
		aWeaponType.doUnlock();
		addToUnlockedWeaponTypeList(aWeaponType);
	}

	private void addToUnlockedWeaponTypeList(WeaponType aWeaponType)
	{
		if (unlockedWeaponTypeList.Count == 0)
		{
			unlockedWeaponTypeList.Add(aWeaponType);
			return;
		}
		List<WeaponType> list = new List<WeaponType>();
		int num = Convert.ToInt32(aWeaponType.getWeaponTypeRefId());
		bool flag = false;
		foreach (WeaponType unlockedWeaponType in unlockedWeaponTypeList)
		{
			if (unlockedWeaponType.getWeaponTypeRefId() != string.Empty)
			{
				if (!flag && unlockedWeaponType.getWeaponTypeRefId() == aWeaponType.getWeaponTypeRefId())
				{
					flag = true;
				}
				else if (!flag && Convert.ToInt32(unlockedWeaponType.getWeaponTypeRefId()) > num)
				{
					list.Add(aWeaponType);
					flag = true;
				}
			}
			list.Add(unlockedWeaponType);
		}
		if (!flag)
		{
			list.Add(aWeaponType);
		}
		unlockedWeaponTypeList = list;
	}

	public void setUnlockedWeaponTypeList(List<WeaponType> aList)
	{
		unlockedWeaponTypeList = aList;
	}

	public WeaponType getWeaponTypeByRefId(string refId)
	{
		foreach (WeaponType unlockedWeaponType in unlockedWeaponTypeList)
		{
			if (unlockedWeaponType.getWeaponTypeRefId() == refId)
			{
				return unlockedWeaponType;
			}
		}
		return new WeaponType();
	}

	public List<WeaponType> getResearchWeaponTypeList(bool includeCurrentResearch = true)
	{
		return new List<WeaponType>();
	}

	public void addWeapon(string aRefId, string aName, string aDesc, string aImage, WeaponType aType, float aAtkMult, float aSpdMult, float aAccMult, float aMagMult, Dictionary<string, int> aMaterialList, List<string> aRelicList, WeaponStat aResearchType, int aResearchDuration, int aResearchCost, float aResearchMood, int aDlc, string aScenarioLock)
	{
		Weapon weapon = new Weapon(aRefId, aName, aDesc, aImage, aType.getWeaponTypeRefId(), aAtkMult, aSpdMult, aAccMult, aMagMult, aMaterialList, aRelicList, aResearchType, aResearchDuration, aResearchCost, aResearchMood, aDlc, aScenarioLock);
		weapon.setWeaponType(aType);
		weapon.setWeaponUnlocked(aUnlocked: true);
		addToUnlockedWeaponList(weapon);
	}

	public void setUnlockedWeaponList(List<Weapon> aList)
	{
		unlockedWeaponList = aList;
	}

	public bool unlockWeapon(Weapon aWeapon)
	{
		bool result = false;
		if (!aWeapon.getWeaponType().checkUnlocked())
		{
			unlockWeaponType(aWeapon.getWeaponType());
			result = true;
		}
		aWeapon.setWeaponUnlocked(aUnlocked: true);
		addToUnlockedWeaponList(aWeapon);
		return result;
	}

	private void addToUnlockedWeaponList(Weapon aWeapon)
	{
		if (unlockedWeaponList.Count == 0)
		{
			unlockedWeaponList.Add(aWeapon);
			return;
		}
		List<Weapon> list = new List<Weapon>();
		int num = Convert.ToInt32(aWeapon.getWeaponRefId());
		bool flag = false;
		foreach (Weapon unlockedWeapon in unlockedWeaponList)
		{
			if (unlockedWeapon.getWeaponRefId() != string.Empty)
			{
				if (!flag && unlockedWeapon.getWeaponRefId() == aWeapon.getWeaponRefId())
				{
					flag = true;
				}
				else if (!flag && Convert.ToInt32(unlockedWeapon.getWeaponRefId()) > num)
				{
					list.Add(aWeapon);
					flag = true;
				}
			}
			list.Add(unlockedWeapon);
		}
		if (!flag)
		{
			list.Add(aWeapon);
		}
		unlockedWeaponList = list;
	}

	public Weapon getWeaponByRefId(string aRefId)
	{
		foreach (Weapon unlockedWeapon in unlockedWeaponList)
		{
			if (unlockedWeapon.getWeaponRefId() == aRefId)
			{
				return unlockedWeapon;
			}
		}
		return new Weapon();
	}

	public List<Weapon> getWeaponList()
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon unlockedWeapon in unlockedWeaponList)
		{
			if (unlockedWeapon.getWeaponUnlocked())
			{
				list.Add(unlockedWeapon);
			}
		}
		return list;
	}

	public List<Weapon> getWeaponListByType(string aWeaponTypeRefId)
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon unlockedWeapon in unlockedWeaponList)
		{
			if (unlockedWeapon.getWeaponUnlocked() && unlockedWeapon.getWeaponTypeRefId() == aWeaponTypeRefId)
			{
				list.Add(unlockedWeapon);
			}
		}
		return list;
	}

	public Weapon getResearchWeaponByType(string aWeaponTypeRefId)
	{
		int count = unlockedWeaponList.Count;
		return new Weapon();
	}

	public void giveAvatarToPlayer()
	{
		hasAvatar = true;
		if (dogStation == SmithStation.SmithStationDesign)
		{
			playerStation = SmithStation.SmithStationEnchant;
		}
		else
		{
			playerStation = SmithStation.SmithStationDesign;
		}
	}

	public bool checkHasAvatar()
	{
		return hasAvatar;
	}

	public void setHasAvatar(bool aBool)
	{
		hasAvatar = aBool;
	}

	public string getAvatarRefId()
	{
		return avatarRefId;
	}

	public string getAvatarName()
	{
		return avatarName;
	}

	public string getAvatarDescription()
	{
		return avatarDescription;
	}

	public string getAvatarImage()
	{
		return avatarImage;
	}

	public void chooseAvatar(string aRefId, string aName, string aDesc, string aImage)
	{
		avatarRefId = aRefId;
		avatarName = aName;
		avatarDescription = aDesc;
		avatarImage = aImage;
	}

	public void unlockFurniture(Furniture aFurniture)
	{
		aFurniture.setPlayerOwned(aOwned: true);
		addToShopFurnitureList(aFurniture);
	}

	private void addToShopFurnitureList(Furniture aFurniture)
	{
		if (shopFurnitureList.Count == 0)
		{
			shopFurnitureList.Add(aFurniture);
			return;
		}
		List<Furniture> list = new List<Furniture>();
		int num = Convert.ToInt32(aFurniture.getFurnitureRefId());
		bool flag = false;
		foreach (Furniture shopFurniture in shopFurnitureList)
		{
			if (shopFurniture.getFurnitureRefId() != string.Empty && !flag && Convert.ToInt32(shopFurniture.getFurnitureRefId()) > num)
			{
				list.Add(aFurniture);
				flag = true;
			}
			list.Add(shopFurniture);
		}
		if (!flag)
		{
			list.Add(aFurniture);
		}
		shopFurnitureList = list;
	}

	public void setShopFurnitureList(List<Furniture> aList)
	{
		shopFurnitureList = aList;
	}

	public List<Furniture> getCurrentShopFurnitureList()
	{
		List<Furniture> list = new List<Furniture>();
		List<string> list2 = new List<string>();
		foreach (Furniture shopFurniture in shopFurnitureList)
		{
			if (!list2.Contains(shopFurniture.getFurnitureType()))
			{
				Furniture highestPlayerFurnitureByType = getHighestPlayerFurnitureByType(shopFurniture.getFurnitureType());
				if (highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty)
				{
					list2.Add(shopFurniture.getFurnitureType());
					list.Add(highestPlayerFurnitureByType);
				}
			}
		}
		return list;
	}

	public Furniture getHighestPlayerFurnitureByType(string aType)
	{
		Furniture furniture = new Furniture();
		foreach (Furniture shopFurniture in shopFurnitureList)
		{
			if (shopFurniture.getFurnitureType() == aType && shopFurniture.getFurnitureLevel() > furniture.getFurnitureLevel())
			{
				furniture = shopFurniture;
			}
		}
		return furniture;
	}

	public bool checkOwnedFurnitureByRefID(string aRefID)
	{
		foreach (Furniture shopFurniture in shopFurnitureList)
		{
			if (shopFurniture.getFurnitureRefId() == aRefID)
			{
				return true;
			}
		}
		return false;
	}

	public List<Furniture> getCurrentShopWorkstationList()
	{
		List<Furniture> list = new List<Furniture>();
		list.Add(getHighestPlayerFurnitureByType("601"));
		list.Add(getHighestPlayerFurnitureByType("701"));
		list.Add(getHighestPlayerFurnitureByType("801"));
		list.Add(getHighestPlayerFurnitureByType("901"));
		return list;
	}

	public int countWorkstationUpgrades()
	{
		Furniture highestPlayerFurnitureByType = getHighestPlayerFurnitureByType("601");
		Furniture highestPlayerFurnitureByType2 = getHighestPlayerFurnitureByType("701");
		Furniture highestPlayerFurnitureByType3 = getHighestPlayerFurnitureByType("801");
		Furniture highestPlayerFurnitureByType4 = getHighestPlayerFurnitureByType("901");
		return highestPlayerFurnitureByType.getFurnitureLevel() + highestPlayerFurnitureByType2.getFurnitureLevel() + highestPlayerFurnitureByType3.getFurnitureLevel() + highestPlayerFurnitureByType4.getFurnitureLevel() - 4;
	}

	public List<Decoration> getOwnedDecorationList()
	{
		return playerDecoList;
	}

	public Decoration getOwnedDecorationByRefId(string aRefId)
	{
		foreach (Decoration playerDeco in playerDecoList)
		{
			if (playerDeco.getDecorationRefId() == aRefId)
			{
				return playerDeco;
			}
		}
		return new Decoration();
	}

	public List<Decoration> getOwnedDecorationListByType(string aType)
	{
		List<Decoration> list = new List<Decoration>();
		foreach (Decoration playerDeco in playerDecoList)
		{
			if (playerDeco.getDecorationType() == aType)
			{
				list.Add(playerDeco);
			}
		}
		return list;
	}

	public void setOwnedDecorationList(List<Decoration> aList)
	{
		playerDecoList = aList;
	}

	public void addOwnedDecoration(Decoration addDeco)
	{
		playerDecoList.Add(addDeco);
	}

	public Decoration getDisplayDecorationByType(string aType)
	{
		if (currentDecoList.ContainsKey(aType))
		{
			return currentDecoList[aType];
		}
		return new Decoration();
	}

	public Dictionary<string, Decoration> getDisplayDecorationList()
	{
		return currentDecoList;
	}

	public void setDisplayDecorationList(Dictionary<string, Decoration> aList)
	{
		currentDecoList = aList;
	}

	public void replaceDisplayDecoration(Decoration addDeco)
	{
		string decorationType = addDeco.getDecorationType();
		string text = string.Empty;
		if (currentDecoList.ContainsKey(decorationType))
		{
			text = currentDecoList[decorationType].getDecorationRefId();
			currentDecoList[decorationType] = addDeco;
		}
		else
		{
			currentDecoList.Add(decorationType, addDeco);
		}
		foreach (Smith playerSmith in playerSmithList)
		{
			if (text != string.Empty)
			{
				removeDecoSmithEffect(playerSmith, text);
			}
			addDecoSmithEffect(playerSmith, addDeco.getDecorationEffectList());
		}
	}

	public void removeDecoSmithEffect(Smith selectSmith, string decoRefId)
	{
		int num = 0;
		List<int> list = new List<int>();
		foreach (string smithEffectDeco in selectSmith.getSmithEffectDecoList())
		{
			if (smithEffectDeco == decoRefId)
			{
				list.Add(num);
			}
			num++;
		}
		list.Reverse();
		foreach (int item in list)
		{
			selectSmith.removeSmithEffectAt(item);
		}
	}

	public void addDecoSmithEffect(Smith selectSmith, List<DecorationEffect> effectList)
	{
		foreach (DecorationEffect effect in effectList)
		{
			string decorationBoostType = effect.getDecorationBoostType();
			float decorationBoostMult = effect.getDecorationBoostMult();
			string decorationRefId = effect.getDecorationRefId();
			switch (decorationBoostType)
			{
			case "SMITH_POW":
				selectSmith.addSmithEffect(StatEffect.StatEffectMultPower, decorationBoostMult, 0, decorationRefId, string.Empty);
				break;
			case "SMITH_INT":
				selectSmith.addSmithEffect(StatEffect.StatEffectMultIntelligence, decorationBoostMult, 0, decorationRefId, string.Empty);
				break;
			case "SMITH_TEC":
				selectSmith.addSmithEffect(StatEffect.StatEffectMultTechnique, decorationBoostMult, 0, decorationRefId, string.Empty);
				break;
			case "SMITH_LUC":
				selectSmith.addSmithEffect(StatEffect.StatEffectMultLuck, decorationBoostMult, 0, decorationRefId, string.Empty);
				break;
			case "SMITH_STA":
				selectSmith.addSmithEffect(StatEffect.StatEffectMultStamina, decorationBoostMult, 0, decorationRefId, string.Empty);
				break;
			case "SMITH_ALL":
				selectSmith.addSmithEffect(StatEffect.StatEffectMultPower, decorationBoostMult, 0, decorationRefId, string.Empty);
				selectSmith.addSmithEffect(StatEffect.StatEffectMultIntelligence, decorationBoostMult, 0, decorationRefId, string.Empty);
				selectSmith.addSmithEffect(StatEffect.StatEffectMultTechnique, decorationBoostMult, 0, decorationRefId, string.Empty);
				selectSmith.addSmithEffect(StatEffect.StatEffectMultLuck, decorationBoostMult, 0, decorationRefId, string.Empty);
				break;
			}
		}
	}

	public float checkDecoEffect(string aEffectString, string aEffectRefId)
	{
		float num = 1f;
		List<DecorationEffect> decoEffectList = getDecoEffectList();
		foreach (DecorationEffect item in decoEffectList)
		{
			if (item.getDecorationBoostType() == aEffectString && (item.getDecorationBoostRefId() == string.Empty || item.getDecorationBoostRefId() == aEffectRefId))
			{
				num *= item.getDecorationBoostMult();
			}
		}
		return num;
	}

	public List<DecorationEffect> getDecoEffectList()
	{
		List<DecorationEffect> list = new List<DecorationEffect>();
		foreach (Decoration value in currentDecoList.Values)
		{
			list.AddRange(value.getDecorationEffectList());
		}
		return list;
	}

	public void setLastDailyAction(int aLastDaily)
	{
		lastDailyAction = aLastDaily;
	}

	public int getLastDailyAction()
	{
		return lastDailyAction;
	}

	public bool tryDailyAction()
	{
		int playerDays = getPlayerDays();
		if (playerDays > lastDailyAction)
		{
			lastDailyAction = playerDays;
			return true;
		}
		return false;
	}

	public bool checkWeekend()
	{
		bool result = false;
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		if (list[3] == 6)
		{
			result = true;
		}
		return result;
	}

	public long getPlayerTimeLong()
	{
		return playerTimeLong;
	}

	public int getDayOfWeek()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		return list[3];
	}

	public int getPlayerDays()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		return (list[0] * 4 + list[1]) * 28 + list[2] * 7 + list[3];
	}

	public int getPlayerMonths()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		return list[0] * 4 + list[1];
	}

	public int getSeasonIndex()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		return list[1];
	}

	public int getTimeLeftInSeason()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		int playerDays = getPlayerDays();
		int num = (list[0] * 4 + (list[1] + 1)) * 28;
		return num - playerDays - 2;
	}

	public int getPlayerYear()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		return list[0];
	}

	public int skipToNextDay(int startHours, int halfHours)
	{
		long num = playerTimeLong;
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		list[5] = halfHours;
		list[4] = startHours;
		list[3]++;
		if (list[3] > 7)
		{
			list[3] = 1;
			list[2]++;
		}
		if (list[2] > 4)
		{
			list[2] = 1;
			list[1]++;
		}
		if (list[1] > 4)
		{
			list[1] = 1;
			list[0]++;
		}
		playerTimeLong = CommonAPI.convertIntListToHalfHours(list);
		return (int)(playerTimeLong - num);
	}

	public void setTimeLong(long halfhours)
	{
		playerTimeLong = halfhours;
	}

	public bool addTimeLong(int halfhours)
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		playerTimeLong += halfhours;
		List<int> list2 = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		if (list[3] != list2[3])
		{
			return true;
		}
		return false;
	}

	public string getPlayerTimeString()
	{
		return CommonAPI.convertHalfHoursToTimeDisplay(playerTimeLong, isMenu: true);
	}

	public List<int> getPlayerDateTimeList()
	{
		return CommonAPI.convertHalfHoursToPlayerTimeIntList(playerTimeLong);
	}

	public bool checkTimedAction(TimedAction aAction)
	{
		if (actionList.Contains(aAction))
		{
			return true;
		}
		return false;
	}

	public void addTimedAction(TimedAction action, int timer)
	{
		if (!actionList.Contains(action))
		{
			actionList.Add(action);
			timerList.Add(timer);
		}
	}

	public TimedAction passTimeOnTimedActions(int timePass)
	{
		for (int i = 0; i < actionList.Count; i++)
		{
			int num = timerList[i];
			num -= timePass;
			if (num <= 0)
			{
				TimedAction result = actionList[i];
				actionList.RemoveAt(i);
				timerList.RemoveAt(i);
				return result;
			}
			timerList[i] = num;
		}
		return TimedAction.TimedActionBlank;
	}

	public string getTimedActionListString()
	{
		string text = string.Empty;
		foreach (TimedAction action in actionList)
		{
			if (text != string.Empty)
			{
				text += "@";
			}
			text += action;
		}
		return text;
	}

	public string getTimedActionTimerListString()
	{
		string text = string.Empty;
		foreach (int timer in timerList)
		{
			if (text != string.Empty)
			{
				text += "@";
			}
			text += timer;
		}
		return text;
	}

	public void clearTimedActionLists()
	{
		actionList.Clear();
		timerList.Clear();
	}

	public void setTimedActionLists(string aActionList, string aTimerList)
	{
		clearTimedActionLists();
		if (aActionList != string.Empty)
		{
			string[] array = aActionList.Split('@');
			string[] array2 = aTimerList.Split('@');
			for (int i = 0; i < array.Length; i++)
			{
				addTimedAction(CommonAPI.convertDataStringToTimedAction(array[i]), CommonAPI.parseInt(array2[i]));
			}
		}
	}

	public Objective getCurrentObjective()
	{
		return currentObjective;
	}

	public void setCurrentObjective(Objective aObjective)
	{
		currentObjective = aObjective;
	}

	public void setDisplayProjectListString(List<Project> aList)
	{
		displayProject = aList;
	}

	public void addDisplayProject(Project aAdd)
	{
		displayProject.Add(aAdd);
	}

	public void removeDisplayProject(Project aRemove)
	{
		displayProject.Remove(aRemove);
	}

	public List<Project> getDisplayProjectList()
	{
		return displayProject;
	}

	public bool checkCanStartProject()
	{
		int num = displayProject.Count;
		if (currentProject.getProjectId() != string.Empty)
		{
			num++;
		}
		if (num < 2)
		{
			return true;
		}
		return false;
	}

	public bool checkDisplayProjectList(string aRefId)
	{
		string[] array = displayProjectListString.Split('@');
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text == aRefId)
			{
				return true;
			}
		}
		return false;
	}

	public string getDisplayProjectListIdString()
	{
		string text = string.Empty;
		foreach (Project item in displayProject)
		{
			if (text != string.Empty)
			{
				text += "@";
			}
			text += item.getProjectId();
		}
		return text;
	}

	public void setDisplayProjectListString(string aString)
	{
		displayProjectListString = aString;
	}

	public string getCurrentObjectiveString()
	{
		return currentObjectiveString;
	}

	public void setCurrentObjectiveString(string aString)
	{
		currentObjectiveString = aString;
	}

	public void setLastDoneQuest(QuestNEW quest)
	{
		lastDoneQuest = quest;
	}

	public void setLastDoneQuestRefId(string aId)
	{
		lastDoneQuestRefId = aId;
	}

	public string getLastDoneQuestRefId()
	{
		return lastDoneQuestRefId;
	}

	public QuestNEW getLastDoneQuest()
	{
		return lastDoneQuest;
	}

	public List<Contract> getPlayerContractList(List<Contract> aList)
	{
		if (playerContractList.Count < 3 || lastContractListUpdate != getPlayerMonths())
		{
			playerContractList = new List<Contract>();
			List<int> randomIntList = CommonAPI.getRandomIntList(aList.Count, 3);
			randomIntList.Sort();
			foreach (int item in randomIntList)
			{
				playerContractList.Add(aList[item]);
			}
			lastContractListUpdate = getPlayerMonths();
		}
		return playerContractList;
	}

	public string makeContractListIdString()
	{
		string text = string.Empty;
		foreach (Contract playerContract in playerContractList)
		{
			if (text != string.Empty)
			{
				text += '@';
			}
			text += playerContract.getContractRefId();
		}
		return text;
	}

	public void setContractList(List<Contract> aList)
	{
		playerContractList = aList;
	}

	public void setContractListString(string aList)
	{
		playerContractListString = aList;
	}

	public bool checkContractInStringList(string aRefId)
	{
		string[] array = playerContractListString.Split('@');
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text == aRefId)
			{
				return true;
			}
		}
		return false;
	}

	public Contract getLastSelectContract()
	{
		return lastSelectContract;
	}

	public void setLastSelectContract(Contract aContract)
	{
		lastSelectContract = aContract;
	}

	public Smith getLastSelectSmith()
	{
		return lastSelectSmith;
	}

	public void setLastSelectSmith(Smith aSmith)
	{
		lastSelectSmith = aSmith;
	}

	public void clearLastSelectSmith()
	{
		lastSelectSmith = new Smith();
	}

	public Area getLastSelectArea()
	{
		return lastSelectArea;
	}

	public void setLastSelectArea(Area aArea)
	{
		lastSelectArea = aArea;
	}

	public void clearLastSelectArea()
	{
		lastSelectArea = new Area();
	}

	public void setPlayerContractList(List<Contract> aList)
	{
		playerContractList = aList;
	}

	public void clearPlayerContractList()
	{
		playerContractList = new List<Contract>();
	}

	public void setCurrentProjectId(string aProjectId)
	{
		currentProjectId = aProjectId;
	}

	public string getCurrentProjectId()
	{
		return currentProjectId;
	}

	public Project getCurrentProject()
	{
		return currentProject;
	}

	public Weapon getCurrentProjectWeapon()
	{
		return currentProject.getProjectWeapon();
	}

	public ProjectType getCurrentProjectType()
	{
		return currentProject.getProjectType();
	}

	public void startCurrentProject(Project aProject)
	{
		currentProject = aProject;
		currentProject.setProjectState(ProjectState.ProjectStateCurrent);
		currentProject.resetProjectStats();
		currentProject.setProjectPhase(ProjectPhase.ProjectPhaseStart);
	}

	public void setCurrentProject(Project aProject)
	{
		currentProject = aProject;
	}

	public void setCurrentProjectPhase(ProjectPhase aPhase)
	{
		currentProject.setProjectPhase(aPhase);
	}

	public void setCurrentProjectState(ProjectState aState)
	{
		currentProject.setProjectState(aState);
	}

	public void endCurrentProject(ProjectState aState)
	{
		currentProject.setProjectState(aState);
		currentProject.setProjectSaleState(ProjectSaleState.ProjectSaleStateInStock);
		completedProjectList.Add(currentProject);
		currentProject = new Project();
	}

	public void destroyCurrentProject()
	{
		currentProject = new Project();
	}

	public void addCompletedProject(Project aAdd)
	{
		completedProjectList.Add(aAdd);
	}

	public List<Project> getCompletedProjectList()
	{
		return completedProjectList;
	}

	public Project getHighestSellPriceProject()
	{
		Project project = new Project();
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateSold && completedProject.getFinalPrice() > project.getFinalPrice())
			{
				project = completedProject;
			}
		}
		return project;
	}

	public List<Project> sortProjectListByStat(List<Project> unsortedList, WeaponStat sortStat, bool isAscending)
	{
		List<Project> list = new List<Project>();
		List<int> list2 = new List<int>();
		switch (sortStat)
		{
		case WeaponStat.WeaponStatAttack:
			foreach (Project unsorted in unsortedList)
			{
				list2.Add(unsorted.getAtk());
			}
			break;
		case WeaponStat.WeaponStatSpeed:
			foreach (Project unsorted2 in unsortedList)
			{
				list2.Add(unsorted2.getSpd());
			}
			break;
		case WeaponStat.WeaponStatAccuracy:
			foreach (Project unsorted3 in unsortedList)
			{
				list2.Add(unsorted3.getAcc());
			}
			break;
		case WeaponStat.WeaponStatMagic:
			foreach (Project unsorted4 in unsortedList)
			{
				list2.Add(unsorted4.getMag());
			}
			break;
		case WeaponStat.WeaponStatElement:
			foreach (Project unsorted5 in unsortedList)
			{
				list2.Add(unsorted5.getTotalStat());
			}
			break;
		}
		List<int> list3 = CommonAPI.sortIndices(list2, isAscending);
		foreach (int item in list3)
		{
			list.Add(unsortedList[item]);
		}
		return list;
	}

	public List<Project> getCompletedProjectListByType(ProjectType projType, bool includeSold, bool includeStock, bool includeSelling)
	{
		List<Project> list = new List<Project>();
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectType() == projType && ((includeSold && completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateSold) || (includeStock && completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateInStock) || (includeSelling && completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateSelling) || (includeSold && completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateDelivered)))
			{
				list.Add(completedProject);
			}
		}
		return list;
	}

	public List<Project> getCompletedProjectListByFilterType(ProjectType projType, CollectionFilterState aState, string aWeaponTypeRefId, bool sortById = true)
	{
		List<Project> list = new List<Project>();
		List<int> list2 = new List<int>();
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectType() != projType)
			{
				continue;
			}
			switch (aState)
			{
			case CollectionFilterState.CollectionFilterStateAll:
				if ((aWeaponTypeRefId == "00000" || aWeaponTypeRefId == completedProject.getProjectWeapon().getWeaponTypeRefId()) && completedProject.getProjectSaleState() != ProjectSaleState.ProjectSaleStateBlank && completedProject.getProjectSaleState() != ProjectSaleState.ProjectSaleStateRejected)
				{
					list.Add(completedProject);
					list2.Add(CommonAPI.parseInt(completedProject.getProjectId()));
				}
				break;
			case CollectionFilterState.CollectionFilterStateStock:
				if ((aWeaponTypeRefId == "00000" || aWeaponTypeRefId == completedProject.getProjectWeapon().getWeaponTypeRefId()) && completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateInStock)
				{
					list.Add(completedProject);
					list2.Add(CommonAPI.parseInt(completedProject.getProjectId()));
				}
				break;
			case CollectionFilterState.CollectionFilterStateSold:
				if ((aWeaponTypeRefId == "00000" || aWeaponTypeRefId == completedProject.getProjectWeapon().getWeaponTypeRefId()) && (completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateSold || completedProject.getProjectSaleState() == ProjectSaleState.ProjectSaleStateDelivered))
				{
					list.Add(completedProject);
					list2.Add(CommonAPI.parseInt(completedProject.getProjectId()));
				}
				break;
			}
		}
		if (sortById)
		{
			List<int> list3 = CommonAPI.sortIndices(list2, isAscending: true);
			List<Project> list4 = new List<Project>();
			{
				foreach (int item in list3)
				{
					list4.Add(list[item]);
				}
				return list4;
			}
		}
		return list;
	}

	public List<Project> getCompletedProjectListById(List<string> aIdList)
	{
		List<Project> list = new List<Project>();
		foreach (Project completedProject in completedProjectList)
		{
			if (aIdList.Contains(completedProject.getProjectId()))
			{
				list.Add(completedProject);
			}
		}
		return list;
	}

	public Project getCompletedProjectById(string aId)
	{
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectId() == aId)
			{
				return completedProject;
			}
		}
		return new Project();
	}

	public Project getProjectById(string aId)
	{
		if (currentProjectId == aId)
		{
			return currentProject;
		}
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectId() == aId)
			{
				return completedProject;
			}
		}
		return new Project();
	}

	public int getCompletedNormalWeaponCount()
	{
		int num = 0;
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectType() == ProjectType.ProjectTypeWeapon)
			{
				num++;
			}
		}
		return num;
	}

	public int getLegendaryAttemptCount()
	{
		int num = 0;
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectType() == ProjectType.ProjectTypeUnique)
			{
				num++;
			}
		}
		return num;
	}

	public List<int> getScoreWeaponStats()
	{
		List<int> list = new List<int>();
		list.Add(0);
		list.Add(0);
		list.Add(0);
		list.Add(0);
		list.Add(0);
		list.Add(0);
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectType() == ProjectType.ProjectTypeWeapon)
			{
				if (completedProject.getAtk() > list[0])
				{
					list[0] = completedProject.getAtk();
				}
				if (completedProject.getAtk() > list[1])
				{
					list[1] = completedProject.getSpd();
				}
				if (completedProject.getAtk() > list[2])
				{
					list[2] = completedProject.getAcc();
				}
				if (completedProject.getAtk() > list[3])
				{
					list[3] = completedProject.getMag();
				}
				list[4] += completedProject.getAtk();
				list[4] += completedProject.getSpd();
				list[4] += completedProject.getAcc();
				list[4] += completedProject.getMag();
				if (completedProject.getEnchantItem().getItemRefId() != string.Empty)
				{
					list[5]++;
				}
			}
		}
		return list;
	}

	public List<Project> getGoldenHammerWeaponList()
	{
		List<Project> list = new List<Project>();
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.checkQualifyGoldenHammer(nextGoldenHammerYear))
			{
				list.Add(completedProject);
			}
		}
		return list;
	}

	public int countAward(ProjectAchievement achievement)
	{
		List<Project> list = new List<Project>();
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.checkForAchievement(achievement))
			{
				list.Add(completedProject);
			}
		}
		return list.Count;
	}

	public Dictionary<ProjectAchievement, Project> getAwardedProjectList(int year)
	{
		Dictionary<ProjectAchievement, Project> dictionary = new Dictionary<ProjectAchievement, Project>();
		foreach (Project completedProject in completedProjectList)
		{
			if (!completedProject.checkQualifyGoldenHammer(year) || completedProject.getProjectAchievementList().Count <= 0)
			{
				continue;
			}
			foreach (ProjectAchievement projectAchievement in completedProject.getProjectAchievementList())
			{
				dictionary.Add(projectAchievement, completedProject);
			}
		}
		return dictionary;
	}

	public int getNextProjectId()
	{
		int num = 0;
		foreach (Project completedProject in completedProjectList)
		{
			int num2 = CommonAPI.parseInt(completedProject.getProjectId());
			if (num2 > num)
			{
				num = num2;
			}
		}
		return num + 1;
	}

	public void addCurrentProjectStats(int atk, int spd, int acc, int mag)
	{
		currentProject.addAtk(atk);
		currentProject.addSpd(spd);
		currentProject.addAcc(acc);
		currentProject.addMag(mag);
	}

	public void updateCurrentProjectStats(int atk, int spd, int acc, int mag)
	{
		currentProject.setAtk(atk);
		currentProject.setSpd(spd);
		currentProject.setAcc(acc);
		currentProject.setMag(mag);
	}

	public void buffCurrentProjectStats(float atkBuff, float spdBuff, float accBuff, float magBuff)
	{
		GameData gameData = CommonAPI.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int aStat = 0;
		if (atkBuff > 0f)
		{
			aStat = Math.Max((int)((float)currentProject.getAtk() * atkBuff), 1);
		}
		int aStat2 = 0;
		if (spdBuff > 0f)
		{
			aStat2 = Math.Max((int)((float)currentProject.getSpd() * spdBuff), 1);
		}
		int aStat3 = 0;
		if (accBuff > 0f)
		{
			aStat3 = Math.Max((int)((float)currentProject.getAcc() * accBuff), 1);
		}
		int aStat4 = 0;
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex) && magBuff > 0f)
		{
			aStat4 = Math.Max((int)((float)currentProject.getMag() * magBuff), 1);
		}
		currentProject.addAtk(aStat);
		currentProject.addSpd(aStat2);
		currentProject.addAcc(aStat3);
		currentProject.addMag(aStat4);
	}

	public void debuffCurrentProjectStats(float atkDebuff, float spdDebuff, float accDebuff, float magDebuff)
	{
		GameData gameData = CommonAPI.getGameData();
		GameScenario gameScenarioByRefId = gameData.getGameScenarioByRefId(getGameScenario());
		string gameLockSet = gameScenarioByRefId.getGameLockSet();
		int num = 0;
		if (atkDebuff > 0f)
		{
			num = Math.Max((int)((float)currentProject.getAtk() * atkDebuff), 1);
		}
		int num2 = 0;
		if (spdDebuff > 0f)
		{
			num2 = Math.Max((int)((float)currentProject.getSpd() * spdDebuff), 1);
		}
		int num3 = 0;
		if (accDebuff > 0f)
		{
			num3 = Math.Max((int)((float)currentProject.getAcc() * accDebuff), 1);
		}
		int num4 = 0;
		if (gameData.checkFeatureIsUnlocked(gameLockSet, "ENCHANT", completedTutorialIndex) && magDebuff > 0f)
		{
			num4 = Math.Max((int)((float)currentProject.getMag() * magDebuff), 1);
		}
		currentProject.addAtk(-num);
		currentProject.addSpd(-num2);
		currentProject.addAcc(-num3);
		currentProject.addMag(-num4);
	}

	public List<int> getCurrentProjectStats()
	{
		List<int> list = new List<int>();
		list.Add(currentProject.getAtk());
		list.Add(currentProject.getSpd());
		list.Add(currentProject.getAcc());
		list.Add(currentProject.getMag());
		return list;
	}

	public List<int> getCurrentProjectHeroWeaponStats()
	{
		return currentProject.calculateHeroWeaponStats();
	}

	public bool enchantWeapon(Item item)
	{
		return currentProject.enchantWeapon(item);
	}

	public List<int> getCurrentProjectStatReqs()
	{
		List<int> list = new List<int>();
		list.Add(currentProject.getAtkReq());
		list.Add(currentProject.getSpdReq());
		list.Add(currentProject.getAccReq());
		list.Add(currentProject.getMagReq());
		return list;
	}

	public List<int> getCurrentProjectStatReqsLeft()
	{
		List<int> list = new List<int>();
		list.Add(currentProject.getAtkReqLeft());
		list.Add(currentProject.getSpdReqLeft());
		list.Add(currentProject.getAccReqLeft());
		list.Add(currentProject.getMagReqLeft());
		return list;
	}

	public string getProjectName(bool includePrefix)
	{
		string projectName = currentProject.getProjectName(includePrefix);
		if (currentProject.getProjectType() == ProjectType.ProjectTypeUnique)
		{
			return projectName;
		}
		if (projectName == string.Empty || !currentProject.getPlayerNamed())
		{
			currentProject.autoGenerateWeaponName();
			return currentProject.getProjectName(includePrefix);
		}
		return projectName;
	}

	public void setProjectName(string aName)
	{
		currentProject.setProjectName(aName);
		currentProject.setPlayerNamed(aValue: true);
	}

	public int getProjectProgressPercent()
	{
		return currentProject.getProjectProgressPercent();
	}

	public int getCompletedWeaponCount()
	{
		int num = 0;
		foreach (Project completedProject in completedProjectList)
		{
			ProjectType projectType = completedProject.getProjectType();
			if (projectType == ProjectType.ProjectTypeWeapon || projectType == ProjectType.ProjectTypeUnique)
			{
				num++;
			}
		}
		return num;
	}

	public int getCompletedWeaponCountByWeaponRefId(string aRefId)
	{
		int num = 0;
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectWeapon().getWeaponRefId() == aRefId)
			{
				num++;
			}
		}
		return num;
	}

	public int getCompletedWeaponCountByWeaponTypeRefId(string aRefId)
	{
		int num = 0;
		foreach (Project completedProject in completedProjectList)
		{
			if (completedProject.getProjectWeapon().getWeaponTypeRefId() == aRefId)
			{
				num++;
			}
		}
		return num;
	}

	public void resetLastDirectionBuild()
	{
		lastDirectionBuild = new List<int>();
		lastDirectionBuild.Add(1);
		lastDirectionBuild.Add(1);
		lastDirectionBuild.Add(1);
		lastDirectionBuild.Add(1);
	}

	public List<int> getLastDirectionBuild()
	{
		return lastDirectionBuild;
	}

	public void modifyDirectionBuild(int modifyIndex, int addAmt)
	{
		int num = getRemainingBuildPoints() - addAmt;
		if (((addAmt > 0 && lastDirectionBuild[modifyIndex] < 5) || (addAmt < 0 && lastDirectionBuild[modifyIndex] > 1)) && num <= 10 && num >= 0)
		{
			lastDirectionBuild[modifyIndex] += addAmt;
		}
	}

	public bool checkDirectionBuildValid()
	{
		int num = 0;
		foreach (int item in lastDirectionBuild)
		{
			if (item < 1)
			{
				return false;
			}
			num += item;
		}
		if (num == 10)
		{
			return true;
		}
		return false;
	}

	public int getRemainingBuildPoints()
	{
		int num = 0;
		foreach (int item in lastDirectionBuild)
		{
			num += item;
		}
		return 10 - num;
	}

	public bool checkPreviousCombi(Hero jc, Weapon wpn)
	{
		return false;
	}

	public void resetSmithsWorkState(SmithAction action, int fixDuration)
	{
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.checkSmithInShop())
			{
				playerSmith.resetWorkState(action, fixDuration);
			}
		}
	}

	public void clearPlayerSmithList()
	{
		playerSmithList.Clear();
	}

	public void hireSmithByObject(Smith hired)
	{
		hired.setPlayerOwned(aOwned: true, isHire: true);
		addToSmithList(hired);
	}

	public void addSmith(Smith hired)
	{
		addToSmithList(hired);
	}

	public void fireSmithByObject(Smith fired)
	{
		fired.setAssignedRole(SmithStation.SmithStationAuto);
		fired.setPlayerOwned(aOwned: false, isHire: false);
		removeFromSmithList(fired);
	}

	public List<Smith> getSmithList()
	{
		return playerSmithList;
	}

	public List<Smith> getExploreSmithList()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getExploreState() == SmithExploreState.SmithExploreStateTravelToExplore || playerSmith.getExploreState() == SmithExploreState.SmithExploreStateExplore || playerSmith.getExploreState() == SmithExploreState.SmithExploreStateExploreTravelHome)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getBuyMaterialSmithList()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getExploreState() == SmithExploreState.SmithExploreStateTravelToBuyMaterial || playerSmith.getExploreState() == SmithExploreState.SmithExploreStateBuyMaterial || playerSmith.getExploreState() == SmithExploreState.SmithExploreStateBuyMaterialTravelHome)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getSellWeaponSmithList()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getExploreState() == SmithExploreState.SmithExploreStateTravelToSellWeapon || playerSmith.getExploreState() == SmithExploreState.SmithExploreStateSellWeapon || playerSmith.getExploreState() == SmithExploreState.SmithExploreStateSellWeaponTravelHome)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getOutOfShopSmithList()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getExploreState() != 0)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getInShopSmithList()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.checkSmithInShop())
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getInShopOrStandbySmithList()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.checkSmithInShopOrStandby())
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	private void addToSmithList(Smith aSmith)
	{
		if (playerSmithList.Count == 0)
		{
			playerSmithList.Add(aSmith);
			return;
		}
		List<Smith> list = new List<Smith>();
		int num = Convert.ToInt32(aSmith.getSmithRefId());
		bool flag = false;
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithRefId() != string.Empty && !flag && Convert.ToInt32(playerSmith.getSmithRefId()) > num)
			{
				list.Add(aSmith);
				flag = true;
			}
			list.Add(playerSmith);
		}
		if (!flag)
		{
			list.Add(aSmith);
		}
		playerSmithList = list;
	}

	private void removeFromSmithList(Smith aSmith)
	{
		playerSmithList.Remove(aSmith);
	}

	public List<Smith> getDesignSmithArray(bool includeAway)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithJob().checkDesign() && (includeAway || playerSmith.checkSmithInShopOrStandby()))
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getCraftSmithArray(bool includeAway)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithJob().checkCraft() && (includeAway || playerSmith.checkSmithInShopOrStandby()))
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getPolishSmithArray(bool includeAway)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithJob().checkPolish() && (includeAway || playerSmith.checkSmithInShopOrStandby()))
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getEnchantSmithArray(bool includeAway)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithJob().checkEnchant() && (includeAway || playerSmith.checkSmithInShopOrStandby()))
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public int getIndexOfSmith(Smith aSmith)
	{
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithId() == aSmith.getSmithId())
			{
				return playerSmithList.IndexOf(playerSmith);
			}
		}
		return -1;
	}

	public Smith getSmithByIndex(int aIndex)
	{
		if (aIndex < playerSmithList.Count)
		{
			return playerSmithList[aIndex];
		}
		return new Smith();
	}

	public Smith getSmithByRefID(string aRefID)
	{
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithRefId() == aRefID)
			{
				return playerSmith;
			}
		}
		return new Smith();
	}

	public int getSmithMaxLevel()
	{
		int num = 0;
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getSmithLevel() > num)
			{
				num = playerSmith.getSmithLevel();
			}
		}
		return num;
	}

	public int getSmithTotalLevel()
	{
		int num = 0;
		foreach (Smith playerSmith in playerSmithList)
		{
			num += playerSmith.getSmithLevel();
		}
		return num;
	}

	public int getSmithTotalStat(SmithStat stat)
	{
		int num = 0;
		foreach (Smith playerSmith in playerSmithList)
		{
			switch (stat)
			{
			case SmithStat.SmithStatAll:
				num += playerSmith.getSmithPower() - playerSmith.getSmithAddedPower();
				num += playerSmith.getSmithIntelligence() - playerSmith.getSmithAddedIntelligence();
				num += playerSmith.getSmithTechnique() - playerSmith.getSmithAddedTechnique();
				num += playerSmith.getSmithLuck() - playerSmith.getSmithAddedLuck();
				break;
			case SmithStat.SmithStatPower:
				num += playerSmith.getSmithPower() - playerSmith.getSmithAddedPower();
				break;
			case SmithStat.SmithStatIntelligence:
				num += playerSmith.getSmithIntelligence() - playerSmith.getSmithAddedIntelligence();
				break;
			case SmithStat.SmithStatTechnique:
				num += playerSmith.getSmithTechnique() - playerSmith.getSmithAddedTechnique();
				break;
			case SmithStat.SmithStatLuck:
				num += playerSmith.getSmithLuck() - playerSmith.getSmithAddedLuck();
				break;
			}
		}
		return num;
	}

	public Smith getRecruitByIndex(int aIndex)
	{
		if (aIndex < recruitList.Count)
		{
			return recruitList[aIndex];
		}
		return new Smith();
	}

	public List<Smith> getRecruitList()
	{
		return recruitList;
	}

	public string makeRecruitListIdString()
	{
		string text = string.Empty;
		foreach (Smith recruit in recruitList)
		{
			if (text != string.Empty)
			{
				text += "@";
			}
			text += recruit.getSmithRefId();
		}
		return text;
	}

	public void setRecruitListIdString(string idString)
	{
		recruitListIdString = idString;
	}

	public bool checkRecruitInStringList(string aRefId)
	{
		string[] array = recruitListIdString.Split('@');
		string[] array2 = array;
		foreach (string text in array2)
		{
			if (text == aRefId)
			{
				return true;
			}
		}
		return false;
	}

	public void setRecruitList(List<Smith> aRecruitList)
	{
		recruitList = aRecruitList;
	}

	public List<Smith> getStationSmithActiveArray(SmithStation station)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getCurrentStation() == station && playerSmith.checkSmithInShop())
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getStationSmithArray(SmithStation station)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getCurrentStation() == station)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getAssignedSmithArray(SmithStation station)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getAssignedRole() == station)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getUnstationedSmithArray()
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getCurrentStation() == SmithStation.SmithStationBlank)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public List<Smith> getAutoSmithArray(SmithStation station)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getAssignedRole() == SmithStation.SmithStationAuto && playerSmith.getCurrentStation() == station)
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public void updateSmithStations()
	{
		Project project = getCurrentProject();
		SmithStation smithStation = SmithStation.SmithStationDesign;
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.getCurrentStation() == playerSmith.getAssignedRole())
			{
				continue;
			}
			if (playerSmith.getAssignedRole() == SmithStation.SmithStationAuto && playerSmith.getCurrentStation() == SmithStation.SmithStationBlank && playerSmith.checkSmithInShop())
			{
				playerSmith.setCurrentStation(smithStation);
				switch (smithStation)
				{
				case SmithStation.SmithStationDesign:
					smithStation = SmithStation.SmithStationCraft;
					break;
				case SmithStation.SmithStationCraft:
					smithStation = SmithStation.SmithStationPolish;
					break;
				case SmithStation.SmithStationPolish:
					smithStation = SmithStation.SmithStationEnchant;
					break;
				case SmithStation.SmithStationEnchant:
					smithStation = SmithStation.SmithStationDesign;
					break;
				}
			}
			else if (playerSmith.getAssignedRole() != 0)
			{
				playerSmith.setCurrentStation(playerSmith.getAssignedRole());
			}
		}
		bool flag = false;
		bool flag2 = false;
		bool flag3 = false;
		bool flag4 = false;
		if (project.checkIsForging())
		{
			if (project.getAtkReqLeft() <= 0)
			{
				flag = true;
			}
			if (project.getSpdReqLeft() <= 0)
			{
				flag2 = true;
			}
			if (project.getAccReqLeft() <= 0)
			{
				flag3 = true;
			}
			if (project.getMagReqLeft() <= 0)
			{
				flag4 = true;
			}
		}
		int furnitureCapacity = getHighestPlayerFurnitureByType("601").getFurnitureCapacity();
		int furnitureCapacity2 = getHighestPlayerFurnitureByType("701").getFurnitureCapacity();
		int furnitureCapacity3 = getHighestPlayerFurnitureByType("801").getFurnitureCapacity();
		int furnitureCapacity4 = getHighestPlayerFurnitureByType("901").getFurnitureCapacity();
		List<Smith> assignedSmithArray = getAssignedSmithArray(SmithStation.SmithStationDesign);
		List<Smith> assignedSmithArray2 = getAssignedSmithArray(SmithStation.SmithStationCraft);
		List<Smith> assignedSmithArray3 = getAssignedSmithArray(SmithStation.SmithStationPolish);
		List<Smith> assignedSmithArray4 = getAssignedSmithArray(SmithStation.SmithStationEnchant);
		List<Smith> autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
		List<Smith> autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
		List<Smith> autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
		List<Smith> autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
		List<int> list = new List<int>();
		list.Add(assignedSmithArray.Count + autoSmithArray.Count);
		list.Add(assignedSmithArray2.Count + autoSmithArray2.Count);
		list.Add(assignedSmithArray3.Count + autoSmithArray3.Count);
		list.Add(assignedSmithArray4.Count + autoSmithArray4.Count);
		List<int> list2 = CommonAPI.sortIndices(list, isAscending: true);
		if (list[0] > furnitureCapacity)
		{
			int num = list[0] - furnitureCapacity;
			for (int i = 0; i < num; i++)
			{
				int index = UnityEngine.Random.Range(0, autoSmithArray.Count);
				switch (list2[0])
				{
				case 1:
					autoSmithArray[index].setCurrentStation(SmithStation.SmithStationCraft);
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list[1]++;
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list[0]--;
					break;
				case 2:
					autoSmithArray[index].setCurrentStation(SmithStation.SmithStationPolish);
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list[2]++;
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list[0]--;
					break;
				case 3:
					autoSmithArray[index].setCurrentStation(SmithStation.SmithStationEnchant);
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list[3]++;
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list[0]--;
					break;
				}
				list2 = CommonAPI.sortIndices(list, isAscending: true);
			}
		}
		if (list[1] > furnitureCapacity2)
		{
			int num2 = list[1] - furnitureCapacity2;
			for (int j = 0; j < num2; j++)
			{
				int index2 = UnityEngine.Random.Range(0, autoSmithArray2.Count);
				switch (list2[0])
				{
				case 0:
					autoSmithArray2[index2].setCurrentStation(SmithStation.SmithStationDesign);
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list[0]++;
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list[1]--;
					break;
				case 2:
					autoSmithArray2[index2].setCurrentStation(SmithStation.SmithStationPolish);
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list[2]++;
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list[1]--;
					break;
				case 3:
					autoSmithArray2[index2].setCurrentStation(SmithStation.SmithStationEnchant);
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list[3]++;
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list[1]--;
					break;
				}
				list2 = CommonAPI.sortIndices(list, isAscending: true);
			}
		}
		if (list[2] > furnitureCapacity3)
		{
			int num3 = list[2] - furnitureCapacity3;
			for (int k = 0; k < num3; k++)
			{
				int index3 = UnityEngine.Random.Range(0, autoSmithArray3.Count);
				switch (list2[0])
				{
				case 0:
					autoSmithArray3[index3].setCurrentStation(SmithStation.SmithStationDesign);
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list[0]++;
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list[2]--;
					break;
				case 1:
					autoSmithArray3[index3].setCurrentStation(SmithStation.SmithStationCraft);
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list[1]++;
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list[2]--;
					break;
				case 3:
					autoSmithArray3[index3].setCurrentStation(SmithStation.SmithStationEnchant);
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list[3]++;
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list[2]--;
					break;
				}
				list2 = CommonAPI.sortIndices(list, isAscending: true);
			}
		}
		if (list[3] > furnitureCapacity4)
		{
			int num4 = list[3] - furnitureCapacity4;
			for (int l = 0; l < num4; l++)
			{
				int index4 = UnityEngine.Random.Range(0, autoSmithArray4.Count);
				switch (list2[0])
				{
				case 0:
					autoSmithArray4[index4].setCurrentStation(SmithStation.SmithStationDesign);
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list[0]++;
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list[3]--;
					break;
				case 1:
					autoSmithArray4[index4].setCurrentStation(SmithStation.SmithStationCraft);
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list[1]++;
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list[3]--;
					break;
				case 2:
					autoSmithArray4[index4].setCurrentStation(SmithStation.SmithStationPolish);
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list[2]++;
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list[3]--;
					break;
				}
				list2 = CommonAPI.sortIndices(list, isAscending: true);
			}
		}
		List<int> list3 = new List<int>();
		bool flag5 = false;
		if (flag)
		{
			list3.Add(furnitureCapacity);
		}
		else
		{
			list3.Add(assignedSmithArray.Count + autoSmithArray.Count);
			flag5 = true;
		}
		if (flag2)
		{
			list3.Add(furnitureCapacity2);
		}
		else
		{
			list3.Add(assignedSmithArray2.Count + autoSmithArray2.Count);
			flag5 = true;
		}
		if (flag3)
		{
			list3.Add(furnitureCapacity3);
		}
		else
		{
			list3.Add(assignedSmithArray3.Count + autoSmithArray3.Count);
			flag5 = true;
		}
		if (flag4)
		{
			list3.Add(furnitureCapacity4);
		}
		else
		{
			list3.Add(assignedSmithArray4.Count + autoSmithArray4.Count);
			flag5 = true;
		}
		List<int> list4 = CommonAPI.sortIndices(list3, isAscending: true);
		if (flag5 && flag)
		{
			for (int m = 0; m < autoSmithArray.Count; m++)
			{
				int index5 = UnityEngine.Random.Range(0, autoSmithArray.Count);
				int num5 = list4[0];
				switch (num5)
				{
				case 1:
					if (list3[num5] < furnitureCapacity2)
					{
						autoSmithArray[index5].setCurrentStation(SmithStation.SmithStationCraft);
						autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
						list3[1]++;
						autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
						list3[0]--;
					}
					break;
				case 2:
					if (list3[num5] < furnitureCapacity3)
					{
						autoSmithArray[index5].setCurrentStation(SmithStation.SmithStationPolish);
						autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
						list3[2]++;
						autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
						list3[0]--;
					}
					break;
				case 3:
					if (list3[num5] < furnitureCapacity4)
					{
						autoSmithArray[index5].setCurrentStation(SmithStation.SmithStationEnchant);
						autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
						list3[3]++;
						autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
						list3[0]--;
					}
					break;
				}
				list4 = CommonAPI.sortIndices(list3, isAscending: true);
			}
		}
		if (flag5 && flag2)
		{
			for (int n = 0; n < autoSmithArray2.Count; n++)
			{
				int index6 = UnityEngine.Random.Range(0, autoSmithArray2.Count);
				int num6 = list4[0];
				if (list3[num6] >= furnitureCapacity2)
				{
					break;
				}
				switch (num6)
				{
				case 0:
					if (list3[num6] < furnitureCapacity)
					{
						autoSmithArray2[index6].setCurrentStation(SmithStation.SmithStationDesign);
						autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
						list3[0]++;
						autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
						list3[1]--;
					}
					break;
				case 2:
					if (list3[num6] < furnitureCapacity3)
					{
						autoSmithArray2[index6].setCurrentStation(SmithStation.SmithStationPolish);
						autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
						list3[2]++;
						autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
						list3[1]--;
					}
					break;
				case 3:
					if (list3[num6] < furnitureCapacity4)
					{
						autoSmithArray2[index6].setCurrentStation(SmithStation.SmithStationEnchant);
						autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
						list3[3]++;
						autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
						list3[1]--;
					}
					break;
				}
				list4 = CommonAPI.sortIndices(list3, isAscending: true);
			}
		}
		if (flag5 && flag3)
		{
			for (int num7 = 0; num7 < autoSmithArray3.Count; num7++)
			{
				int index7 = UnityEngine.Random.Range(0, autoSmithArray3.Count);
				int num8 = list4[0];
				switch (num8)
				{
				case 0:
					if (list3[num8] < furnitureCapacity)
					{
						autoSmithArray3[index7].setCurrentStation(SmithStation.SmithStationDesign);
						autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
						list3[0]++;
						autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
						list3[2]--;
					}
					break;
				case 1:
					if (list3[num8] < furnitureCapacity2)
					{
						autoSmithArray3[index7].setCurrentStation(SmithStation.SmithStationCraft);
						autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
						list3[1]++;
						autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
						list3[2]--;
					}
					break;
				case 3:
					if (list3[num8] < furnitureCapacity4)
					{
						autoSmithArray3[index7].setCurrentStation(SmithStation.SmithStationEnchant);
						autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
						list3[3]++;
						autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
						list3[2]--;
					}
					break;
				}
				list4 = CommonAPI.sortIndices(list3, isAscending: true);
			}
		}
		if (!flag5 || !flag4)
		{
			return;
		}
		for (int num9 = 0; num9 < autoSmithArray4.Count; num9++)
		{
			int index8 = UnityEngine.Random.Range(0, autoSmithArray4.Count);
			int num10 = list4[0];
			switch (num10)
			{
			case 0:
				if (list3[num10] < furnitureCapacity)
				{
					autoSmithArray4[index8].setCurrentStation(SmithStation.SmithStationDesign);
					autoSmithArray = getAutoSmithArray(SmithStation.SmithStationDesign);
					list3[0]++;
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list3[3]--;
				}
				break;
			case 1:
				if (list3[num10] < furnitureCapacity2)
				{
					autoSmithArray4[index8].setCurrentStation(SmithStation.SmithStationCraft);
					autoSmithArray2 = getAutoSmithArray(SmithStation.SmithStationCraft);
					list3[1]++;
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list3[3]--;
				}
				break;
			case 2:
				if (list3[num10] < furnitureCapacity3)
				{
					autoSmithArray4[index8].setCurrentStation(SmithStation.SmithStationPolish);
					autoSmithArray3 = getAutoSmithArray(SmithStation.SmithStationPolish);
					list3[2]++;
					autoSmithArray4 = getAutoSmithArray(SmithStation.SmithStationEnchant);
					list3[3]--;
				}
				break;
			}
			list4 = CommonAPI.sortIndices(list3, isAscending: true);
		}
	}

	public List<Smith> getSmithListWithTag(string tagRefId)
	{
		List<Smith> list = new List<Smith>();
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.checkHasSmithTag(tagRefId))
			{
				list.Add(playerSmith);
			}
		}
		return list;
	}

	public WeaponType getLastSelectWeaponType()
	{
		return lastSelectWeaponType;
	}

	public Weapon getLastSelectWeapon()
	{
		return lastSelectWeapon;
	}

	public Hero getLastSelectHero()
	{
		return lastSelectHero;
	}

	public QuestNEW getLastSelectQuest()
	{
		return lastSelectQuest;
	}

	public void setLastSelectWeaponType(WeaponType aWeaponType)
	{
		lastSelectWeaponType = aWeaponType;
	}

	public void setLastSelectWeapon(Weapon aWeapon)
	{
		lastSelectWeapon = aWeapon;
	}

	public void setLastSelectHero(Hero aHero)
	{
		lastSelectHero = aHero;
	}

	public void setLastSelectQuest(QuestNEW aQuest)
	{
		lastSelectQuest = aQuest;
	}

	public void setStats(string aPlayerId, string aPlayerName, string aShopName, int aPlayerGold)
	{
		playerId = aPlayerId;
		playerName = aPlayerName;
		shopName = aShopName;
		playerGold = aPlayerGold;
	}

	public void addGold(int aValue)
	{
		playerGold += aValue;
	}

	public int reduceGold(int aValue, bool allowNegative)
	{
		if (allowNegative)
		{
			playerGold -= aValue;
			return aValue;
		}
		if (playerGold > 0)
		{
			int num = Math.Min(aValue, playerGold);
			playerGold -= num;
			return num;
		}
		return 0;
	}

	public int getPlayerLaw()
	{
		return playerLaw;
	}

	public int getPlayerChaos()
	{
		return playerChaos;
	}

	public void addAlignment(int aLaw, int aChaos)
	{
		playerLaw += aLaw;
		playerChaos += aChaos;
		playerLaw = Math.Max(0, playerLaw);
		playerChaos = Math.Max(0, playerChaos);
	}

	public bool upgradeShop(ShopLevel nextShopLevel)
	{
		if (nextShopLevel.getShopRefId() != "-1")
		{
			shopLevel = nextShopLevel;
			shopLevelRefId = shopLevel.getShopRefId();
			prevRegionFame = playerFame;
			prevRegionTickets = playerTickets;
			areaRegion++;
			return true;
		}
		return false;
	}

	public bool checkDayEnd()
	{
		List<int> list = CommonAPI.convertHalfHoursToIntList(playerTimeLong);
		if (list[4] >= 23)
		{
			return true;
		}
		foreach (Smith playerSmith in playerSmithList)
		{
			if (playerSmith.checkSmithInShop())
			{
				return false;
			}
		}
		return true;
	}

	public bool checkDate(List<int> aDateList)
	{
		List<int> playerDateTimeList = getPlayerDateTimeList();
		CommonAPI.debug("aDateList " + CommonAPI.formatIntListString(aDateList));
		CommonAPI.debug("dateList " + CommonAPI.formatIntListString(playerDateTimeList));
		if (aDateList.Count < 4 || playerDateTimeList.Count < 4)
		{
			return false;
		}
		for (int i = 0; i < 4; i++)
		{
			if (playerDateTimeList[i] != aDateList[i])
			{
				return false;
			}
		}
		return true;
	}

	public void setLastEventDate(long aLastEventDate)
	{
		lastEventDate = aLastEventDate;
	}

	public long getLastEventDate()
	{
		return lastEventDate;
	}

	public SmithStation getPlayerStation()
	{
		return playerStation;
	}

	public void setPlayerStation(SmithStation aStation)
	{
		playerStation = aStation;
	}

	public SmithStation getDogStation()
	{
		return dogStation;
	}

	public void setDogStation(SmithStation aStation)
	{
		dogStation = aStation;
	}

	public SmithStation randomizeDogStation()
	{
		int max = CommonAPI.getGameData().getIntConstantByRefID("DOG_MOVE_STATION");
		if (dogStation == SmithStation.SmithStationBlank)
		{
			max = CommonAPI.getGameData().getIntConstantByRefID("DOG_MOVE_HOME");
		}
		else if (dogStation == SmithStation.SmithStationAuto)
		{
			max = 5;
		}
		switch (UnityEngine.Random.Range(0, max))
		{
		case 0:
			dogStation = SmithStation.SmithStationDesign;
			break;
		case 1:
			dogStation = SmithStation.SmithStationCraft;
			break;
		case 2:
			dogStation = SmithStation.SmithStationPolish;
			break;
		case 3:
			if (enchantLocked)
			{
				dogStation = SmithStation.SmithStationDesign;
			}
			else
			{
				dogStation = SmithStation.SmithStationEnchant;
			}
			break;
		case 4:
			if (checkHasDogBed())
			{
				dogStation = SmithStation.SmithStationDogHome;
			}
			break;
		}
		if (dogStation == playerStation)
		{
			dogStation = randomizeDogStation();
		}
		return dogStation;
	}

	public int getDogBarkChance()
	{
		return 5;
	}

	public int getDesignStationLv()
	{
		return designStationLv;
	}

	public void setDesignStationLv(int aLv)
	{
		designStationLv = aLv;
	}

	public int getCraftStationLv()
	{
		return craftStationLv;
	}

	public void setCraftStationLv(int aLv)
	{
		craftStationLv = aLv;
	}

	public int getPolishStationLv()
	{
		return polishStationLv;
	}

	public void setPolishStationLv(int aLv)
	{
		polishStationLv = aLv;
	}

	public int getEnchantStationLv()
	{
		return enchantStationLv;
	}

	public void setEnchantStationLv(int aLv)
	{
		enchantStationLv = aLv;
	}

	public bool getEnchantLocked()
	{
		return enchantLocked;
	}

	public void setEnchantLocked(bool locked)
	{
		enchantLocked = locked;
	}

	public string getPlayerId()
	{
		return playerId;
	}

	public string getGameScenario()
	{
		return gameScenario;
	}

	public void setGameScenario(string aScenario)
	{
		gameScenario = aScenario;
	}

	public string getPlayerName()
	{
		return playerName;
	}

	public void setPlayerName(string aName)
	{
		playerName = aName;
	}

	public string getShopName()
	{
		return shopName;
	}

	public string getShopLevelRefId()
	{
		return shopLevelRefId;
	}

	public void setShopLevel(ShopLevel aShopLevel)
	{
		shopLevel = aShopLevel;
		shopLevelRefId = aShopLevel.getShopRefId();
	}

	public ShopLevel getShopLevel()
	{
		return shopLevel;
	}

	public int getShopLevelInt()
	{
		return CommonAPI.parseInt(shopLevel.getShopName());
	}

	public int getAreaRegion()
	{
		return areaRegion;
	}

	public void setAreaRegion(int aAreaRegion)
	{
		areaRegion = aAreaRegion;
	}

	public int getShopMaxSmith()
	{
		int num = 0;
		List<Furniture> currentShopFurnitureList = getCurrentShopFurnitureList();
		foreach (Furniture item in currentShopFurnitureList)
		{
			num += item.getFurnitureCapacity();
		}
		return Math.Min(shopLevel.getShopMaxCapacity(), num);
	}

	public void setShopName(string aShopName)
	{
		shopName = aShopName;
	}

	public int getPlayerGold()
	{
		return playerGold;
	}

	public int getLawAlignment()
	{
		return playerLaw;
	}

	public int getChaosAlignment()
	{
		return playerChaos;
	}

	public int getTotalAlignment()
	{
		return playerLaw + playerChaos;
	}

	public float getPlayerAlignment()
	{
		if (playerLaw + playerChaos != 0)
		{
			return (float)(playerLaw - playerChaos) * 100f / (float)(playerLaw + playerChaos);
		}
		return 0f;
	}

	public string getShopNextLevelRefId()
	{
		return shopLevel.getNextShopRefId();
	}

	public int getShopUpgradeCost()
	{
		return shopLevel.getUpgradeCost();
	}

	public bool checkCanUpgradeShop()
	{
		if (shopLevel.getNextShopRefId() == "-1")
		{
			return false;
		}
		return true;
	}

	public List<WeaponType> getCollectionWeaponTypeList(CollectionFilterState aState)
	{
		List<WeaponType> list = new List<WeaponType>();
		List<Project> completedProjectListByFilterType = getCompletedProjectListByFilterType(ProjectType.ProjectTypeWeapon, aState, "00000");
		foreach (Project item in completedProjectListByFilterType)
		{
			bool flag = false;
			foreach (WeaponType item2 in list)
			{
				if (item.getProjectWeapon().getWeaponType() == item2)
				{
					flag = true;
					break;
				}
			}
			if (!flag)
			{
				list.Add(item.getProjectWeapon().getWeaponType());
			}
		}
		return list;
	}

	public List<WeaponType> getUnlockedWeaponTypeList()
	{
		return unlockedWeaponTypeList;
	}

	public List<Weapon> getUnlockedWeaponListByType(string typeRefId)
	{
		List<Weapon> list = new List<Weapon>();
		foreach (Weapon unlockedWeapon in unlockedWeaponList)
		{
			if (unlockedWeapon.getWeaponTypeRefId() == typeRefId)
			{
				list.Add(unlockedWeapon);
			}
		}
		return list;
	}

	public List<Weapon> getUnlockedWeaponList()
	{
		return unlockedWeaponList;
	}

	public List<Hero> getUnlockedJobClassList()
	{
		return unlockedHeroList;
	}

	public void setPlayerGold(int aGold)
	{
		playerGold = aGold;
	}

	public Weather getWeather()
	{
		return weather;
	}

	public void setWeather(Weather aWeather)
	{
		weather = aWeather;
	}

	public ForgeSeasonalEffect getForgeEffect()
	{
		return forgeEffect;
	}

	public void setForgeEffect(ForgeSeasonalEffect aEffect)
	{
		forgeEffect = aEffect;
	}

	public List<ProjectAchievement> getNextGoldenHammerAwardList()
	{
		return nextGoldenHammerAwardList;
	}

	public string getNextGoldenHammerAwardListString()
	{
		string text = string.Empty;
		foreach (ProjectAchievement nextGoldenHammerAward in nextGoldenHammerAwardList)
		{
			if (text != string.Empty)
			{
				text += "@";
			}
			text += nextGoldenHammerAward;
		}
		return text;
	}

	public void setNextGoldenHammerAwardList(List<ProjectAchievement> aAwardList)
	{
		nextGoldenHammerAwardList = aAwardList;
	}

	public int getNextGoldenHammerYear()
	{
		return nextGoldenHammerYear;
	}

	public void setNextGoldenHammerYear(int aYear)
	{
		nextGoldenHammerYear = aYear;
	}

	public void addNextGoldenHammerYear()
	{
		nextGoldenHammerYear++;
	}

	public void addPastGoldenHammerAwards(int year, string aAwardString)
	{
		pastGoldenHammerAwardList.Add(year, aAwardString);
	}

	public void setPastGoldenHammerAwards(Dictionary<int, string> aPastAwards)
	{
		pastGoldenHammerAwardList = aPastAwards;
	}

	public string getPastGoldenHammerAwardListString()
	{
		string text = string.Empty;
		foreach (int key in pastGoldenHammerAwardList.Keys)
		{
			if (text != string.Empty)
			{
				text += "_";
			}
			string text2 = text;
			text = text2 + key + "%" + pastGoldenHammerAwardList[key];
		}
		return text;
	}

	public List<ProjectAchievement> getPastGoldenHammerAwardsByYear(int year)
	{
		List<ProjectAchievement> list = new List<ProjectAchievement>();
		if (pastGoldenHammerAwardList.ContainsKey(year))
		{
			string text = pastGoldenHammerAwardList[year];
			string[] array = text.Split('@');
			foreach (string typeString in array)
			{
				list.Add(CommonAPI.convertStringToProjectAchievement(typeString));
			}
		}
		return list;
	}

	public void setShowAwardsFromYear(int aYear)
	{
		showAwardsFromYear = aYear;
	}

	public int getShowAwardsFromYear()
	{
		return showAwardsFromYear;
	}

	public bool checkHasProject()
	{
		if (currentProject.getProjectType() == ProjectType.ProjectTypeNothing)
		{
			return false;
		}
		return true;
	}

	public bool checkHasForgingProject()
	{
		if (currentProject.getProjectType() == ProjectType.ProjectTypeWeapon || currentProject.getProjectType() == ProjectType.ProjectTypeUnique)
		{
			return true;
		}
		return false;
	}

	public void resetDetails()
	{
		playerName = string.Empty;
		shopName = string.Empty;
	}

	public bool checkNamed()
	{
		return isNamed;
	}

	public void setNamed()
	{
		isNamed = true;
	}

	public ProjectType getLastSelectProjectType()
	{
		return lastSelectProjectType;
	}

	public void setLastSelectProjectType(ProjectType aType)
	{
		lastSelectProjectType = aType;
	}

	public Project getLastSelectProject()
	{
		return lastSelectProject;
	}

	public void setLastSelectProject(Project aProject)
	{
		lastSelectProject = aProject;
	}

	public void clearLastSelectProject()
	{
		lastSelectProject = new Project();
	}

	public void setLastPaidMonth(int aLastPaid)
	{
		lastPaidMonth = aLastPaid;
	}

	public int getLastPaidMonth()
	{
		return lastPaidMonth;
	}

	public bool checkPaySalary()
	{
		int playerMonths = getPlayerMonths();
		List<int> playerDateTimeList = getPlayerDateTimeList();
		if (playerMonths > lastPaidMonth)
		{
			lastPaidMonth = playerMonths;
			return true;
		}
		return false;
	}

	public bool getUseLoan()
	{
		return loanUsed;
	}

	public void setUseLoan(bool aUse)
	{
		loanUsed = aUse;
	}

	public bool checkPlayerLoan()
	{
		if (!loanUsed && playerGold < 0)
		{
			return true;
		}
		return false;
	}

	public void useLoan()
	{
		loanUsed = true;
	}

	public void setSalaryChancesUsed(int chancesUsed)
	{
		salaryChancesUsed = chancesUsed;
	}

	public int getSalaryChancesUsed()
	{
		return salaryChancesUsed;
	}

	public int getSalaryChancesLeft()
	{
		return 3 - salaryChancesUsed;
	}

	public int getUsedEmblems()
	{
		return usedEmblems;
	}

	public void useEmblems(int aUse)
	{
		usedEmblems += aUse;
	}

	public void setDog(bool aHasDog, string aDogName, int aDogLove, float aDogEnergy, int aCountMonth, int aCountTotal)
	{
		hasDog = aHasDog;
		dogName = aDogName;
		dogLove = aDogLove;
		dogEnergy = aDogEnergy;
		dogActivityCountMonth = aCountMonth;
		dogActivityCountTotal = aCountTotal;
	}

	public void nameDog(string aName)
	{
		if (aName != string.Empty)
		{
			dogName = aName;
			playerNamedDog = true;
		}
	}

	public bool checkPlayerNamedDog()
	{
		return playerNamedDog;
	}

	public void giveDogToPlayer()
	{
		hasDog = true;
		if (playerStation == SmithStation.SmithStationDesign)
		{
			dogStation = SmithStation.SmithStationCraft;
		}
		else
		{
			dogStation = SmithStation.SmithStationDesign;
		}
		dogLove = 0;
		dogEnergy = 100f;
		setDogLastFed(getPlayerTimeLong());
	}

	public long getDogLastFed()
	{
		return dogLastFed;
	}

	public void setDogLastFed(long aLastFed)
	{
		dogLastFed = aLastFed;
	}

	public void feedDog()
	{
		dogLastFed = getPlayerTimeLong();
	}

	public int getDogBowlState()
	{
		long num = getPlayerTimeLong() - dogLastFed;
		Furniture highestPlayerFurnitureByType = getHighestPlayerFurnitureByType("301");
		int num2 = 768;
		if (highestPlayerFurnitureByType != null && highestPlayerFurnitureByType.getFurnitureRefId() != string.Empty)
		{
			switch (highestPlayerFurnitureByType.getFurnitureLevel())
			{
			case 1:
				num2 = 960;
				break;
			case 2:
				num2 = 1152;
				break;
			case 3:
				num2 = 1344;
				break;
			case 4:
				num2 = 1536;
				break;
			}
		}
		if (num > num2)
		{
			return 0;
		}
		if (num > num2 / 2)
		{
			return 1;
		}
		return 2;
	}

	public bool checkRandomDog()
	{
		return randomDog;
	}

	public void setRandomDog(bool aBool)
	{
		randomDog = aBool;
	}

	public bool checkHasDog()
	{
		return hasDog;
	}

	public bool checkHasDogBed()
	{
		return dogBedRefID != string.Empty;
	}

	public string getDogBedRefID()
	{
		return dogBedRefID;
	}

	public void setDogBedRefID(string aRefID)
	{
		dogBedRefID = aRefID;
	}

	public string getDogName()
	{
		return dogName;
	}

	public int getDogLove()
	{
		return dogLove;
	}

	public int getDogLoveLevel()
	{
		if (dogLove > 1100)
		{
			return 5;
		}
		if (dogLove > 800)
		{
			return 4;
		}
		if (dogLove > 500)
		{
			return 3;
		}
		if (dogLove > 200)
		{
			return 2;
		}
		return 1;
	}

	public void reduceDogLove(int aValue)
	{
		dogLove -= aValue;
		if (dogLove < 0)
		{
			dogLove = 0;
		}
	}

	public void addDogLove(int aValue)
	{
		float num = checkDecoEffect("DOG_LOVE", string.Empty);
		aValue = (int)(num * (float)aValue);
		dogLove += aValue;
		if (dogLove > 1500)
		{
			dogLove = 1500;
		}
	}

	public float getDogEnergy()
	{
		return dogEnergy;
	}

	public void reduceDogEnergy(float aValue)
	{
		dogEnergy -= aValue;
		if (!checkHasDogBed() && dogEnergy < 25f)
		{
			dogEnergy = 25f;
		}
		if (dogEnergy < 0f)
		{
			dogEnergy = 0f;
		}
	}

	public int getDogMoodState()
	{
		if (dogEnergy >= 90f)
		{
			return 5;
		}
		if (dogEnergy >= 75f)
		{
			return 4;
		}
		if (dogEnergy >= 25f)
		{
			return 3;
		}
		if (dogEnergy >= 10f)
		{
			return 2;
		}
		return 1;
	}

	public void addDogEnergy(float aValue)
	{
		dogEnergy += aValue;
		if (dogEnergy > 100f)
		{
			dogEnergy = 100f;
		}
	}

	public void dogLeavesPlayer()
	{
		hasDog = false;
	}

	public string getAfterQuestCutscene()
	{
		return showAfterQuestCutscene;
	}

	public void setAfterQuestCutscene(string aSetId)
	{
		showAfterQuestCutscene = aSetId;
	}

	public int getDogActivityCountMonth()
	{
		return dogActivityCountMonth;
	}

	public int getDogActivityCountTotal()
	{
		return dogActivityCountTotal;
	}

	public bool checkMonthDogActivityCount()
	{
		bool result = false;
		if (dogActivityCountMonth > 0)
		{
			result = true;
		}
		else
		{
			reduceDogLove(50);
		}
		dogActivityCountMonth = 0;
		return result;
	}

	public void addDogActivity()
	{
		dogActivityCountMonth++;
		dogActivityCountTotal++;
	}

	public string getSaveTimeString()
	{
		string empty = string.Empty;
		string text = empty;
		return text + shopName + "#" + playerName + "#" + playerGold.ToString() + "#" + playerFame + "#" + playerTimeLong.ToString() + "#" + weather.getWeatherRefId();
	}

	public bool checkSaveLogTime()
	{
		if (playerTimeLong - lastSentSave > 48)
		{
			return true;
		}
		return false;
	}

	public void setSaveLogTime()
	{
		lastSentSave = playerTimeLong;
	}

	public string getEmail()
	{
		return email;
	}

	public bool setEmail(string aEmail)
	{
		if (checkEmailValid(aEmail))
		{
			email = aEmail;
			return true;
		}
		return false;
	}

	public bool checkEmailValid(string emailaddress)
	{
		Regex regex = new Regex("^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$");
		Match match = regex.Match(emailaddress);
		if (match.Success)
		{
			return true;
		}
		return false;
	}

	public bool getTofuKnown()
	{
		return tofuKnown;
	}

	public void setTofuKnown(bool aKnown)
	{
		tofuKnown = aKnown;
	}

	public int getFinalScore()
	{
		return finalScore;
	}

	public void setFinalScore(int aScore)
	{
		finalScore = aScore;
	}

	public int refreshSmithSalaries()
	{
		int num = 0;
		foreach (Smith playerSmith in playerSmithList)
		{
			num += playerSmith.refreshSmithSalary();
		}
		return num;
	}

	public int getSmithTotalSalary()
	{
		int num = 0;
		foreach (Smith playerSmith in playerSmithList)
		{
			num += playerSmith.getSmithSalary();
		}
		return num;
	}

	public int getTotalSmithScore()
	{
		List<Smith> list = playerSmithList;
		int num = 0;
		foreach (Smith item in list)
		{
			num += Math.Max((int)((float)item.getSmithTotalStats() * 1.2f * 0.03f), 1);
		}
		num /= list.Count;
		return num * CommonAPI.getProjectGrowthNum(list.Count);
	}

	public float calculateTotalSmithStats()
	{
		List<Smith> list = playerSmithList;
		float num = 0f;
		foreach (Smith item in list)
		{
			num += (float)item.getSmithTotalStats();
		}
		return num;
	}

	public float calculateAvgSmithStats()
	{
		List<Smith> list = playerSmithList;
		float num = 0f;
		foreach (Smith item in list)
		{
			num += (float)item.getSmithTotalStats();
		}
		return num / (float)list.Count;
	}

	public float calculateENSB(int smithListCount, int timeLimit)
	{
		float num = 0f;
		float num2 = 1.115f + 0.07f * (float)Math.Pow((float)(getShopLevelInt() - 1), 1.5);
		float num3 = 0f;
		foreach (Furniture currentShopFurniture in getCurrentShopFurnitureList())
		{
			if (currentShopFurniture.getFurnitureType() == "601" || currentShopFurniture.getFurnitureType() == "701" || currentShopFurniture.getFurnitureType() == "801" || currentShopFurniture.getFurnitureType() == "901")
			{
				num3 += (float)currentShopFurniture.getFurnitureLevel();
			}
		}
		num3 /= 4f;
		float num4 = 1f + 2f * (num3 - 1f);
		return calculateTotalSmithStats() * 0.108f * (num4 + 0.5f * (float)Math.Pow(2.0, (float)(getShopLevelInt() - 2))) * num2;
	}

	public float calculateEMSB(bool hasMilestoneBoost)
	{
		float num = 0f;
		float num2 = 1.115f + 0.07f * (float)Math.Pow((float)(getShopLevelInt() - 1), 1.5);
		float num3 = 0f;
		foreach (Furniture currentShopFurniture in getCurrentShopFurnitureList())
		{
			if (currentShopFurniture.getFurnitureType() == "601" || currentShopFurniture.getFurnitureType() == "701" || currentShopFurniture.getFurnitureType() == "801" || currentShopFurniture.getFurnitureType() == "901")
			{
				num3 += (float)currentShopFurniture.getFurnitureLevel();
			}
		}
		num3 /= 4f;
		float num4 = 1f + 2f * (num3 - 1f);
		return calculateAvgSmithStats() * 0.105f * num2 * num4;
	}

	public int calculateProjectDifficulty(ProjectType projType, int atkReq, int spdReq, int accReq, int magReq, int timeLimit)
	{
		float num = calculateENSB(playerSmithList.Count, timeLimit);
		bool hasMilestoneBoost = false;
		if (projType == ProjectType.ProjectTypeWeapon)
		{
			hasMilestoneBoost = true;
		}
		float num2 = calculateEMSB(hasMilestoneBoost);
		float num3 = (num + num2) / (float)(atkReq + spdReq + accReq + magReq);
		if (num3 < 0.1f)
		{
			return 5;
		}
		if (num3 < 0.5f)
		{
			return 4;
		}
		if (num3 < 1f)
		{
			return 3;
		}
		if (num3 < 2f)
		{
			return 2;
		}
		return 1;
	}

	public void startActivity(ActivityType aActType, string aAreaRefID, string aSmithRefID, int aTravelTime)
	{
		Activity value = new Activity(activityID.ToString(), aActType, aAreaRefID, aSmithRefID, aTravelTime);
		activityList.Add(activityID.ToString(), value);
		activityID++;
	}

	public void endActivity(string aActivityID)
	{
		activityList.Remove(aActivityID);
	}

	public void addActivityProgress(int units)
	{
		Dictionary<string, Activity>.ValueCollection values = activityList.Values;
		foreach (Activity item in values)
		{
			item.addProgress(units);
		}
	}

	public void addActivity(string activityID, Activity activity)
	{
		activityList.Add(activityID, activity);
	}

	public Dictionary<string, Activity> getActivityList()
	{
		return activityList;
	}

	public void setActivityList(Dictionary<string, Activity> aList)
	{
		activityList = aList;
	}

	public void addShopMonthlyStarch(string aShopMonthlyStarchId, int aMonth, RecordType aType, string aName, int aAmount)
	{
		shopMonthlyStarchList.Add(new ShopMonthlyStarch(aShopMonthlyStarchId, aMonth, aType, aName, aAmount));
	}

	public void clearShopMonthlyStarch()
	{
		shopMonthlyStarchList.Clear();
	}

	public List<ShopMonthlyStarch> getShopMonthlyStarchList()
	{
		return shopMonthlyStarchList;
	}

	private void checkNewMonth(int aMonth)
	{
		foreach (ShopMonthlyStarch shopMonthlyStarch in shopMonthlyStarchList)
		{
			if (shopMonthlyStarch.getMonth() == aMonth)
			{
				return;
			}
		}
		int num = aMonth * 10000;
		if (aMonth == 0)
		{
			num++;
			addShopMonthlyStarch(num.ToString(), aMonth, RecordType.RecordTypeEarningStartingCapital, string.Empty, 0);
		}
		addShopMonthlyStarch((num + 1).ToString(), aMonth, RecordType.RecordTypeEarningWeapon, string.Empty, 0);
		addShopMonthlyStarch((num + 2).ToString(), aMonth, RecordType.RecordTypeEarningContract, string.Empty, 0);
		addShopMonthlyStarch((num + 3).ToString(), aMonth, RecordType.RecordTypeEarningRequest, string.Empty, 0);
		addShopMonthlyStarch((num + 4).ToString(), aMonth, RecordType.RecordTypeEarningLegendary, string.Empty, 0);
		addShopMonthlyStarch((num + 5).ToString(), aMonth, RecordType.RecordTypeEarningBuyDiscount, string.Empty, 0);
		addShopMonthlyStarch((num + 6).ToString(), aMonth, RecordType.RecordTypeEarningMisc, string.Empty, 0);
		addShopMonthlyStarch((num + 7).ToString(), aMonth, RecordType.RecordTypeEarningLoan, string.Empty, 0);
		addShopMonthlyStarch((num + 8).ToString(), aMonth, RecordType.RecordTypeExpenseSalary, string.Empty, 0);
		addShopMonthlyStarch((num + 9).ToString(), aMonth, RecordType.RecordTypeExpenseOutsource, string.Empty, 0);
		addShopMonthlyStarch((num + 10).ToString(), aMonth, RecordType.RecordTypeExpenseBuyItem, string.Empty, 0);
		addShopMonthlyStarch((num + 11).ToString(), aMonth, RecordType.RecordTypeExpenseTraining, string.Empty, 0);
		addShopMonthlyStarch((num + 12).ToString(), aMonth, RecordType.RecordTypeExpenseVacation, string.Empty, 0);
		addShopMonthlyStarch((num + 13).ToString(), aMonth, RecordType.RecordTypeExpenseResearch, string.Empty, 0);
		addShopMonthlyStarch((num + 14).ToString(), aMonth, RecordType.RecordTypeExpenseJobChange, string.Empty, 0);
		addShopMonthlyStarch((num + 15).ToString(), aMonth, RecordType.RecordTypeExpenseRecruitHire, string.Empty, 0);
		addShopMonthlyStarch((num + 16).ToString(), aMonth, RecordType.RecordTypeExpenseShopUpgrades, string.Empty, 0);
		addShopMonthlyStarch((num + 17).ToString(), aMonth, RecordType.RecordTypeExpenseMisc, string.Empty, 0);
	}

	public void addShopMonthlyStarchByType(int aMonth, RecordType aType, string aName, int aAmount)
	{
		checkNewMonth(aMonth);
		ShopMonthlyStarch shopMonthlyStarchByMonthAndType = getShopMonthlyStarchByMonthAndType(aMonth, aType);
		if (aType == RecordType.RecordTypeSpecial || shopMonthlyStarchByMonthAndType.getShopMonthlyStarchId() == string.Empty)
		{
			addShopMonthlyStarch((shopMonthlyStarchList.Count + 1).ToString(), aMonth, aType, aName, aAmount);
		}
		else
		{
			shopMonthlyStarchByMonthAndType.addAmount(aAmount);
		}
	}

	public ShopMonthlyStarch getShopMonthlyStarchById(string aId)
	{
		foreach (ShopMonthlyStarch shopMonthlyStarch in shopMonthlyStarchList)
		{
			if (shopMonthlyStarch.getShopMonthlyStarchId() == aId)
			{
				return shopMonthlyStarch;
			}
		}
		return new ShopMonthlyStarch();
	}

	public ShopMonthlyStarch getShopMonthlyStarchByMonthAndType(int aMonth, RecordType aType)
	{
		foreach (ShopMonthlyStarch shopMonthlyStarch in shopMonthlyStarchList)
		{
			if (shopMonthlyStarch.getMonth() == aMonth && shopMonthlyStarch.getRecordType() == aType)
			{
				return shopMonthlyStarch;
			}
		}
		return new ShopMonthlyStarch();
	}

	public List<ShopMonthlyStarch> getShopMonthlyStarchByMonth(int aMonth)
	{
		List<ShopMonthlyStarch> list = new List<ShopMonthlyStarch>();
		foreach (ShopMonthlyStarch shopMonthlyStarch in shopMonthlyStarchList)
		{
			if (shopMonthlyStarch.getMonth() == aMonth)
			{
				list.Add(shopMonthlyStarch);
			}
		}
		return list;
	}

	public int getWeaponEarnings()
	{
		int num = 0;
		foreach (ShopMonthlyStarch shopMonthlyStarch in shopMonthlyStarchList)
		{
			RecordType recordType = shopMonthlyStarch.getRecordType();
			if (recordType == RecordType.RecordTypeEarningWeapon)
			{
				num += shopMonthlyStarch.getAmount();
			}
		}
		return num;
	}

	public int getTotalEarnings()
	{
		int num = 0;
		foreach (ShopMonthlyStarch shopMonthlyStarch in shopMonthlyStarchList)
		{
			RecordType recordType = shopMonthlyStarch.getRecordType();
			if (recordType == RecordType.RecordTypeEarningWeapon || recordType == RecordType.RecordTypeEarningContract || recordType == RecordType.RecordTypeEarningLegendary || recordType == RecordType.RecordTypeEarningRequest || recordType == RecordType.RecordTypeEarningMisc || (recordType == RecordType.RecordTypeSpecial && shopMonthlyStarch.getAmount() > 0))
			{
				num += shopMonthlyStarch.getAmount();
			}
		}
		return num;
	}

	public int getTotalHeroExpGain()
	{
		return totalHeroExpGain;
	}

	public void addTotalHeroExpGain(int aGain)
	{
		totalHeroExpGain += aGain;
	}

	public void setTotalHeroExpGain(int aGain)
	{
		totalHeroExpGain = aGain;
	}

	public bool checkSkipTutorials()
	{
		return skipTutorials;
	}

	public void setSkipTutorials(bool aBool)
	{
		skipTutorials = aBool;
	}

	public TutorialState getTutorialState()
	{
		return tutorialState;
	}

	public void setTutorialState(TutorialState aState)
	{
		tutorialState = aState;
	}

	public int getTutorialIndex()
	{
		return tutorialIndex;
	}

	public int getCompletedTutorialIndex()
	{
		return completedTutorialIndex;
	}

	public void setTutorialIndex(int aIndex)
	{
		tutorialIndex = aIndex;
	}

	public void setCompletedTutorialIndex(int aIndex)
	{
		completedTutorialIndex = aIndex;
	}
}
