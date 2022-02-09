using System;
using System.Collections;
using System.Collections.Generic;

[Serializable]
public class Quest
{
	private string questId;

	private string questRefId;

	private string questName;

	private string questDesc;

	private string questJobClass;

	private QuestType questType;

	private int currentPoints;

	private int questPointTarget;

	private string requiredWeaponRefId;

	private string recommendTypeRefId;

	private int timesCompleted;

	private Subquest subquest1;

	private Subquest subquest2;

	private Subquest subquest3;

	private string questEndText;

	private Subquest choice1;

	private Subquest choice2;

	private int totalEmblems;

	public Quest()
	{
		questId = string.Empty;
		questRefId = string.Empty;
		questName = string.Empty;
		questDesc = string.Empty;
		questJobClass = string.Empty;
		questType = QuestType.QuestTypeBlank;
		currentPoints = 0;
		questPointTarget = 0;
		requiredWeaponRefId = string.Empty;
		recommendTypeRefId = string.Empty;
		timesCompleted = 0;
		subquest1 = new Subquest();
		subquest2 = new Subquest();
		subquest3 = new Subquest();
		questEndText = string.Empty;
		choice1 = new Subquest();
		choice2 = new Subquest();
		countTotalEmblems();
	}

	public Quest(string aId, string aRefId, string aName, string aDesc, string aJobClass, QuestType aQuestType, int aTarget, string aReqWpnRefId, string aRecTypeRefId, Subquest aSub1, Subquest aSub2, Subquest aSub3, string aEndText, Subquest aChoice1, Subquest aChoice2)
	{
		questId = aId;
		questRefId = aRefId;
		questName = aName;
		questDesc = aDesc;
		questJobClass = aJobClass;
		questType = aQuestType;
		currentPoints = 0;
		questPointTarget = aTarget;
		requiredWeaponRefId = aReqWpnRefId;
		recommendTypeRefId = aRecTypeRefId;
		timesCompleted = 0;
		subquest1 = aSub1;
		subquest2 = aSub2;
		subquest3 = aSub3;
		questEndText = string.Empty;
		choice1 = aChoice1;
		choice2 = aChoice2;
		countTotalEmblems();
	}

	public bool checkOneTimeQuest()
	{
		if (timesCompleted > 0 && questType == QuestType.QuestTypeInstant)
		{
			return false;
		}
		return true;
	}

	public bool checkWeapon(string aWeapon)
	{
		if (requiredWeaponRefId == string.Empty || requiredWeaponRefId == "-1" || aWeapon == requiredWeaponRefId)
		{
			return true;
		}
		return false;
	}

	public bool checkJobClass(string aJobClass)
	{
		if (aJobClass == questJobClass)
		{
			return true;
		}
		return false;
	}

	public Hashtable doQuest(int addQuestPoint)
	{
		Hashtable hashtable = new Hashtable();
		int progressPercent = getProgressPercent();
		currentPoints += addQuestPoint;
		int progressPercent2 = getProgressPercent();
		hashtable["progressBefore"] = progressPercent;
		hashtable["progressAfter"] = progressPercent2;
		if (progressPercent < 25 && progressPercent2 >= 25 && subquest1.checkSubquestExist())
		{
			hashtable["25"] = subquest1.getRefId();
		}
		if (progressPercent < 50 && progressPercent2 >= 50 && subquest2.checkSubquestExist())
		{
			hashtable["50"] = subquest2.getRefId();
		}
		if (progressPercent < 75 && progressPercent2 >= 75 && subquest3.checkSubquestExist())
		{
			hashtable["75"] = subquest3.getRefId();
		}
		int num = Math.Min(progressPercent2, 100);
		int num2 = Math.Max((num - progressPercent) / 20, 1);
		int num3 = 0;
		int num4 = 0;
		for (int i = progressPercent; i <= num; i++)
		{
			num4++;
			if (i != 25 && i != 50 && i != 75 && num4 > 20 && num2 > 0)
			{
				hashtable[i.ToString()] = "BATTLE";
				num2--;
				num3++;
				num4 -= 20;
			}
		}
		if (num2 > 0)
		{
			num3++;
			hashtable[num.ToString()] = "BATTLE";
		}
		hashtable["finalBattleCount"] = num3;
		if (currentPoints >= questPointTarget)
		{
			currentPoints = questPointTarget;
		}
		return hashtable;
	}

	public List<Subquest> doQuestOld(int addQuestPoint)
	{
		List<Subquest> list = new List<Subquest>();
		int progressPercent = getProgressPercent();
		currentPoints += addQuestPoint;
		int progressPercent2 = getProgressPercent();
		if (progressPercent < 25 && progressPercent2 >= 25 && subquest1.checkSubquestExist())
		{
			list.Add(subquest1);
		}
		if (progressPercent < 50 && progressPercent2 >= 50 && subquest2.checkSubquestExist())
		{
			list.Add(subquest2);
		}
		if (progressPercent < 75 && progressPercent2 >= 75 && subquest3.checkSubquestExist())
		{
			list.Add(subquest3);
		}
		if (currentPoints >= questPointTarget)
		{
			currentPoints = questPointTarget;
		}
		return list;
	}

	public bool checkQuestComplete()
	{
		if (currentPoints >= questPointTarget)
		{
			return true;
		}
		return false;
	}

	public List<Subquest> getChoiceList()
	{
		List<Subquest> list = new List<Subquest>();
		list.Add(choice1);
		if (choice2.getRefId() != string.Empty)
		{
			list.Add(choice2);
		}
		return list;
	}

	public void finishQuest()
	{
		currentPoints = 0;
	}

	public string getQuestId()
	{
		return questId;
	}

	public string getQuestRefId()
	{
		return questRefId;
	}

	public string getQuestName()
	{
		return questName;
	}

	public string getQuestDesc()
	{
		return questDesc;
	}

	public string getQuestEndText()
	{
		return questEndText;
	}

	public string getQuestJobClass()
	{
		return questJobClass;
	}

	public string getRecommendedWeaponTypeRefId()
	{
		return recommendTypeRefId;
	}

	public string getRequiredWeaponRefId()
	{
		return requiredWeaponRefId;
	}

	public int getCurrentPoints()
	{
		return currentPoints;
	}

	public void setCurrentPoints(int aPoints)
	{
		currentPoints = aPoints;
	}

	public int getProgressPercent()
	{
		return currentPoints * 100 / questPointTarget;
	}

	public void restartQuest()
	{
		currentPoints = 0;
	}

	public int getMaxTargetPoints()
	{
		return questPointTarget;
	}

	public void addTimesCompleted()
	{
		timesCompleted++;
	}

	public int getTimesCompleted()
	{
		return timesCompleted;
	}

	public void setTimesCompleted(int aCompleted)
	{
		timesCompleted = aCompleted;
	}

	public QuestType getQuestType()
	{
		return questType;
	}

	public Subquest getSubquestByRefId(string aRefId)
	{
		if (subquest1.getRefId() == aRefId)
		{
			return subquest1;
		}
		if (subquest2.getRefId() == aRefId)
		{
			return subquest2;
		}
		if (subquest3.getRefId() == aRefId)
		{
			return subquest3;
		}
		if (choice1.getRefId() == aRefId)
		{
			return choice1;
		}
		if (choice2.getRefId() == aRefId)
		{
			return choice2;
		}
		return new Subquest();
	}

	public List<Subquest> getSubquestList()
	{
		List<Subquest> list = new List<Subquest>();
		if (subquest1 != null && subquest1.checkSubquestExist())
		{
			list.Add(subquest1);
		}
		if (subquest2 != null && subquest2.checkSubquestExist())
		{
			list.Add(subquest2);
		}
		if (subquest3 != null && subquest3.checkSubquestExist())
		{
			list.Add(subquest3);
		}
		return list;
	}

	public Subquest getSubquestByIndex(int subquestNum)
	{
		return subquestNum switch
		{
			1 => subquest1, 
			2 => subquest2, 
			_ => subquest3, 
		};
	}

	private void countTotalEmblems()
	{
		totalEmblems = 2;
		if (subquest1.checkSubquestExist())
		{
			totalEmblems++;
		}
		if (subquest2.checkSubquestExist())
		{
			totalEmblems++;
		}
		if (subquest3.checkSubquestExist())
		{
			totalEmblems++;
		}
	}

	public int getTotalEmblems()
	{
		return totalEmblems;
	}

	public int getPlayerEmblems()
	{
		int num = 0;
		bool flag = true;
		if (subquest1.checkSubquestUnlocked())
		{
			num++;
		}
		else
		{
			flag = false;
		}
		if (subquest2.checkSubquestUnlocked())
		{
			num++;
		}
		else
		{
			flag = false;
		}
		if (subquest3.checkSubquestUnlocked())
		{
			num++;
		}
		else
		{
			flag = false;
		}
		if (timesCompleted > 0)
		{
			num++;
		}
		else
		{
			flag = false;
		}
		if (flag)
		{
			num++;
		}
		return num;
	}
}
