using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001CE RID: 462
public class GUIChangeJobSmithController : MonoBehaviour
{
	// Token: 0x06000CE6 RID: 3302 RVA: 0x000809B8 File Offset: 0x0007EDB8
	private void Awake()
	{
		this.game = GameObject.Find("Game").GetComponent<Game>();
		this.commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		this.shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		this.audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		this.viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		this.changeClassTitleLabel = this.commonScreenObject.findChild(base.gameObject, "ChangeClassTitleLabel").GetComponent<UILabel>();
		this.jcList = new List<SmithJobClass>();
		this.currClassObj = null;
		this.selectedClassObj = null;
		GameObject gameObject = this.commonScreenObject.findChild(base.gameObject, "Bg_SmithStats").gameObject;
		this.smithName = this.commonScreenObject.findChild(gameObject, "SmithName").GetComponent<UILabel>();
		this.atkStat = this.commonScreenObject.findChild(gameObject, "SmithStats/Atk/AtkStat").GetComponent<UILabel>();
		this.atkStatChange = this.commonScreenObject.findChild(gameObject, "SmithStats/Atk/AtkStatChange").GetComponent<UILabel>();
		this.spdStat = this.commonScreenObject.findChild(gameObject, "SmithStats/Spd/SpdStat").GetComponent<UILabel>();
		this.spdStatChange = this.commonScreenObject.findChild(gameObject, "SmithStats/Spd/SpdStatChange").GetComponent<UILabel>();
		this.accStat = this.commonScreenObject.findChild(gameObject, "SmithStats/Acc/AccStat").GetComponent<UILabel>();
		this.accStatChange = this.commonScreenObject.findChild(gameObject, "SmithStats/Acc/AccStatChange").GetComponent<UILabel>();
		this.magStat = this.commonScreenObject.findChild(gameObject, "SmithStats/Mag/MagStat").GetComponent<UILabel>();
		this.magStatChange = this.commonScreenObject.findChild(gameObject, "SmithStats/Mag/MagStatChange").GetComponent<UILabel>();
		this.costLabel = this.commonScreenObject.findChild(gameObject, "CostLabel").GetComponent<UILabel>();
		this.cost = this.commonScreenObject.findChild(gameObject, "Cost").GetComponent<UILabel>();
		this.changeButton = this.commonScreenObject.findChild(gameObject, "ChangeButton").GetComponent<UIButton>();
		this.changeLabel = this.commonScreenObject.findChild(gameObject, "ChangeButton/ChangeLabel").GetComponent<UILabel>();
		this.designer = this.commonScreenObject.findChild(base.gameObject, "Designer").gameObject;
		this.designerLabel = this.commonScreenObject.findChild(this.designer, "DesignerLabel").GetComponent<UILabel>();
		this.designerLvl = this.commonScreenObject.findChild(this.designer, "DesignerLvl").GetComponent<UILabel>();
		this.craftsman = this.commonScreenObject.findChild(base.gameObject, "Craftsman").gameObject;
		this.craftsmanLabel = this.commonScreenObject.findChild(this.craftsman, "CraftsmanLabel").GetComponent<UILabel>();
		this.craftsmanLvl = this.commonScreenObject.findChild(this.craftsman, "CraftsmanLvl").GetComponent<UILabel>();
		this.metalworker = this.commonScreenObject.findChild(base.gameObject, "Metalworker").gameObject;
		this.metalworkerLabel = this.commonScreenObject.findChild(this.metalworker, "MetalworkerLabel").GetComponent<UILabel>();
		this.metalworkerLvl = this.commonScreenObject.findChild(this.metalworker, "MetalworkerLvl").GetComponent<UILabel>();
		this.enchanter = this.commonScreenObject.findChild(base.gameObject, "Enchanter").gameObject;
		this.enchanterLabel = this.commonScreenObject.findChild(this.enchanter, "EnchanterLabel").GetComponent<UILabel>();
		this.enchanterLvl = this.commonScreenObject.findChild(this.enchanter, "EnchanterLvl").GetComponent<UILabel>();
		this.inventor = this.commonScreenObject.findChild(base.gameObject, "Inventor").gameObject;
		this.inventorLabel = this.commonScreenObject.findChild(this.inventor, "InventorLabel").GetComponent<UILabel>();
		this.inventorLvl = this.commonScreenObject.findChild(this.inventor, "InventorLvl").GetComponent<UILabel>();
		this.artisan = this.commonScreenObject.findChild(base.gameObject, "Artisan").gameObject;
		this.artisanLabel = this.commonScreenObject.findChild(this.artisan, "ArtisanLabel").GetComponent<UILabel>();
		this.artisanLvl = this.commonScreenObject.findChild(this.artisan, "ArtisanLvl").GetComponent<UILabel>();
		this.mechanic = this.commonScreenObject.findChild(base.gameObject, "Mechanic").gameObject;
		this.mechanicLabel = this.commonScreenObject.findChild(this.mechanic, "MechanicLabel").GetComponent<UILabel>();
		this.mechanicLvl = this.commonScreenObject.findChild(this.mechanic, "MechanicLvl").GetComponent<UILabel>();
		this.maestro = this.commonScreenObject.findChild(base.gameObject, "Maestro").gameObject;
		this.maestroLabel = this.commonScreenObject.findChild(this.maestro, "MaestroLabel").GetComponent<UILabel>();
		this.maestroLvl = this.commonScreenObject.findChild(this.maestro, "MaestroLvl").GetComponent<UILabel>();
		this.alchemist = this.commonScreenObject.findChild(base.gameObject, "Alchemist").gameObject;
		this.alchemistLabel = this.commonScreenObject.findChild(this.alchemist, "AlchemistLabel").GetComponent<UILabel>();
		this.alchemistLvl = this.commonScreenObject.findChild(this.alchemist, "AlchemistLvl").GetComponent<UILabel>();
		this.virtuoso = this.commonScreenObject.findChild(base.gameObject, "Virtuoso").gameObject;
		this.virtuosoLabel = this.commonScreenObject.findChild(this.virtuoso, "VirtuosoLabel").GetComponent<UILabel>();
		this.virtuosoLvl = this.commonScreenObject.findChild(this.virtuoso, "VirtuosoLvl").GetComponent<UILabel>();
		this.mastersmith = this.commonScreenObject.findChild(base.gameObject, "MasterSmith").gameObject;
		this.mastersmithLabel = this.commonScreenObject.findChild(this.mastersmith, "MasterSmithLabel").GetComponent<UILabel>();
		this.mastersmithLvl = this.commonScreenObject.findChild(this.mastersmith, "MasterSmithLvl").GetComponent<UILabel>();
		this.statsEqualColor = new Color32(128, 71, 3, byte.MaxValue);
	}

	// Token: 0x06000CE7 RID: 3303 RVA: 0x0008104C File Offset: 0x0007F44C
	public void processClick(string gameObjectName)
	{
		if (gameObjectName == "ChangeButton")
		{
			this.shopMenuController.doSmithJobChange(this.smithInfo, this.selectedJobClass);
			this.changeJob();
		}
		else if (gameObjectName == "CloseButton")
		{
			this.viewController.closeSmithJobChange();
		}
		else
			switch (gameObjectName)
			{
			case "Designer":
				this.fitSmithJob(this.designerJobClass);
				break;
			case "Craftsman":
				this.fitSmithJob(this.craftsmanJobClass);
				break;
			case "Metalworker":
				this.fitSmithJob(this.metalworkerJobClass);
				break;
			case "Enchanter":
				this.fitSmithJob(this.enchanterJobClass);
				break;
			case "Inventor":
				this.fitSmithJob(this.inventorJobClass);
				break;
			case "Artisan":
				this.fitSmithJob(this.artisanJobClass);
				break;
			case "Mechanic":
				this.fitSmithJob(this.mechanicJobClass);
				break;
			case "Maestro":
				this.fitSmithJob(this.maestroJobClass);
				break;
			case "Alchemist":
				this.fitSmithJob(this.alchemistJobClass);
				break;
			case "Virtuoso":
				this.fitSmithJob(this.virtuosoJobClass);
				break;
			case "MasterSmith":
				this.fitSmithJob(this.mastersmithJobClass);
				break;
			}
	}

	// Token: 0x06000CE8 RID: 3304 RVA: 0x00081254 File Offset: 0x0007F654
	public void setReference(Smith aSmith)
	{
		GameData gameData = this.game.getGameData();
		this.smithInfo = aSmith;
		this.changeClassTitleLabel.text = gameData.getTextByRefId("menuSmithManagement37");
		this.smithName.text = this.smithInfo.getSmithName();
		this.atkStat.text = CommonAPI.formatNumber(this.smithInfo.getSmithPower());
		this.spdStat.text = CommonAPI.formatNumber(this.smithInfo.getSmithIntelligence());
		this.accStat.text = CommonAPI.formatNumber(this.smithInfo.getSmithTechnique());
		this.magStat.text = CommonAPI.formatNumber(this.smithInfo.getSmithLuck());
		this.costLabel.text = gameData.getTextByRefId("menuShopFurniture02");
		this.cost.text = string.Empty;
		this.changeButton.isEnabled = false;
		this.changeLabel.text = gameData.getTextByRefId("menuSmithManagement39");
		this.jcList = gameData.getJobChangeList(this.smithInfo.getExperienceList(), this.smithInfo.getSmithJob().getSmithJobRefId());
		this.designerJobClass = gameData.getSmithJobClass("10001");
		SmithExperience experienceByJobClass = this.smithInfo.getExperienceByJobClass("10001");
		this.designerLabel.text = this.designerJobClass.getSmithJobName();
		if (this.designerJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			CommonAPI.debug("designer");
			this.designer.GetComponent<UISprite>().spriteName = "current_class";
			this.designer.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.designer;
		}
		if (this.checkAbleToChange(this.designerJobClass))
		{
			int smithJobClassLevel = experienceByJobClass.getSmithJobClassLevel();
			this.designerLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel;
			if (smithJobClassLevel >= this.designerJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.designer, "DesignerMaxLeft").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.designer.GetComponent<BoxCollider>().enabled = false;
			this.designerLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.craftsmanJobClass = gameData.getSmithJobClass("10002");
		SmithExperience experienceByJobClass2 = this.smithInfo.getExperienceByJobClass("10002");
		this.craftsmanLabel.text = this.craftsmanJobClass.getSmithJobName();
		if (this.craftsmanJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.craftsman.GetComponent<UISprite>().spriteName = "current_class";
			this.craftsman.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.craftsman;
		}
		if (this.checkAbleToChange(this.craftsmanJobClass))
		{
			int smithJobClassLevel2 = experienceByJobClass2.getSmithJobClassLevel();
			this.craftsmanLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel2;
			if (smithJobClassLevel2 >= this.craftsmanJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.craftsman, "CraftsmanMaxLeft").GetComponent<UISprite>().enabled = true;
				this.commonScreenObject.findChild(this.craftsman, "CraftsmanMaxRight").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.craftsman.GetComponent<BoxCollider>().enabled = false;
			this.craftsmanLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.metalworkerJobClass = gameData.getSmithJobClass("10003");
		SmithExperience experienceByJobClass3 = this.smithInfo.getExperienceByJobClass("10003");
		this.metalworkerLabel.text = this.metalworkerJobClass.getSmithJobName();
		if (this.metalworkerJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.metalworker.GetComponent<UISprite>().spriteName = "current_class";
			this.metalworker.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.metalworker;
		}
		if (this.checkAbleToChange(this.metalworkerJobClass))
		{
			int smithJobClassLevel3 = experienceByJobClass3.getSmithJobClassLevel();
			this.metalworkerLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel3;
			if (smithJobClassLevel3 >= this.metalworkerJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.metalworker, "MetalworkerMaxLeft").GetComponent<UISprite>().enabled = true;
				this.commonScreenObject.findChild(this.metalworker, "MetalWorkerMaxRight").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.metalworker.GetComponent<BoxCollider>().enabled = false;
			this.metalworkerLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.enchanterJobClass = gameData.getSmithJobClass("10004");
		SmithExperience experienceByJobClass4 = this.smithInfo.getExperienceByJobClass("10004");
		this.enchanterLabel.text = this.enchanterJobClass.getSmithJobName();
		if (this.enchanterJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.enchanter.GetComponent<UISprite>().spriteName = "current_class";
			this.enchanter.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.enchanter;
		}
		if (this.checkAbleToChange(this.enchanterJobClass))
		{
			int smithJobClassLevel4 = experienceByJobClass4.getSmithJobClassLevel();
			this.enchanterLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel4;
			if (smithJobClassLevel4 >= this.enchanterJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.enchanter, "EnchanterMaxLeft").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.enchanter.GetComponent<BoxCollider>().enabled = false;
			this.enchanterLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.inventorJobClass = gameData.getSmithJobClass("10005");
		SmithExperience experienceByJobClass5 = this.smithInfo.getExperienceByJobClass("10005");
		this.inventorLabel.text = this.inventorJobClass.getSmithJobName();
		if (this.inventorJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.inventor.GetComponent<UISprite>().spriteName = "current_class";
			this.inventor.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.inventor;
		}
		if (this.checkAbleToChange(this.inventorJobClass))
		{
			this.commonScreenObject.findChild(this.inventor, "InventorUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel5 = experienceByJobClass5.getSmithJobClassLevel();
			this.inventorLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel5;
			if (smithJobClassLevel5 >= this.inventorJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.inventor, "InventorMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.inventor.GetComponent<BoxCollider>().enabled = false;
			this.inventorLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.artisanJobClass = gameData.getSmithJobClass("10006");
		SmithExperience experienceByJobClass6 = this.smithInfo.getExperienceByJobClass("10006");
		this.artisanLabel.text = this.artisanJobClass.getSmithJobName();
		if (this.artisanJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.artisan.GetComponent<UISprite>().spriteName = "current_class";
			this.artisan.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.artisan;
		}
		if (this.checkAbleToChange(this.artisanJobClass))
		{
			this.commonScreenObject.findChild(this.artisan, "ArtisanUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel6 = experienceByJobClass6.getSmithJobClassLevel();
			this.artisanLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel6;
			if (smithJobClassLevel6 >= this.artisanJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.artisan, "ArtisanMaxLeft").GetComponent<UISprite>().enabled = true;
				this.commonScreenObject.findChild(this.artisan, "ArtisanMaxRight").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.artisan.GetComponent<BoxCollider>().enabled = false;
			this.artisanLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.mechanicJobClass = gameData.getSmithJobClass("10007");
		SmithExperience experienceByJobClass7 = this.smithInfo.getExperienceByJobClass("10007");
		this.mechanicLabel.text = this.mechanicJobClass.getSmithJobName();
		if (this.mechanicJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.mechanic.GetComponent<UISprite>().spriteName = "current_class";
			this.mechanic.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.mechanic;
		}
		if (this.checkAbleToChange(this.mechanicJobClass))
		{
			this.commonScreenObject.findChild(this.mechanic, "MechanicUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel7 = experienceByJobClass7.getSmithJobClassLevel();
			this.mechanicLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel7;
			if (smithJobClassLevel7 >= this.mechanicJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.mechanic, "MechanicMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.mechanic.GetComponent<BoxCollider>().enabled = false;
			this.mechanicLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.maestroJobClass = gameData.getSmithJobClass("10008");
		SmithExperience experienceByJobClass8 = this.smithInfo.getExperienceByJobClass("10008");
		this.maestroLabel.text = this.maestroJobClass.getSmithJobName();
		if (this.maestroJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.maestro.GetComponent<UISprite>().spriteName = "current_class";
			this.maestro.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.maestro;
		}
		if (this.checkAbleToChange(this.maestroJobClass))
		{
			this.commonScreenObject.findChild(this.maestro, "MaestroUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel8 = experienceByJobClass8.getSmithJobClassLevel();
			this.maestroLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel8;
			if (smithJobClassLevel8 >= this.maestroJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.maestro, "MaestroMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.maestro.GetComponent<BoxCollider>().enabled = false;
			this.maestroLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.alchemistJobClass = gameData.getSmithJobClass("10009");
		SmithExperience experienceByJobClass9 = this.smithInfo.getExperienceByJobClass("10009");
		this.alchemistLabel.text = this.alchemistJobClass.getSmithJobName();
		if (this.alchemistJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.alchemist.GetComponent<UISprite>().spriteName = "current_class";
			this.alchemist.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.alchemist;
		}
		if (this.checkAbleToChange(this.alchemistJobClass))
		{
			this.commonScreenObject.findChild(this.alchemist, "AlchemistUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel9 = experienceByJobClass9.getSmithJobClassLevel();
			this.alchemistLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel9;
			if (smithJobClassLevel9 >= this.alchemistJobClass.getMaxLevel())
			{
				this.commonScreenObject.findChild(this.alchemist, "AlchemistMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			this.alchemist.GetComponent<BoxCollider>().enabled = false;
			this.alchemistLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.virtuosoJobClass = gameData.getSmithJobClass("10010");
		SmithExperience experienceByJobClass10 = this.smithInfo.getExperienceByJobClass("10010");
		this.virtuosoLabel.text = this.virtuosoJobClass.getSmithJobName();
		if (this.virtuosoJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.virtuoso.GetComponent<UISprite>().spriteName = "current_class";
			this.virtuoso.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.virtuoso;
		}
		if (this.checkAbleToChange(this.virtuosoJobClass))
		{
			this.virtuosoLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + experienceByJobClass10.getSmithJobClassLevel();
		}
		else
		{
			this.virtuoso.GetComponent<BoxCollider>().enabled = false;
			this.virtuosoLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		this.mastersmithJobClass = gameData.getSmithJobClass("10011");
		SmithExperience experienceByJobClass11 = this.smithInfo.getExperienceByJobClass("10011");
		this.mastersmithLabel.text = this.mastersmithJobClass.getSmithJobName();
		if (this.mastersmithJobClass.getSmithJobRefId() == this.smithInfo.getSmithJob().getSmithJobRefId())
		{
			this.mastersmith.GetComponent<UISprite>().spriteName = "current_class";
			this.mastersmith.GetComponent<BoxCollider>().enabled = false;
			this.currClassObj = this.mastersmith;
		}
		if (this.checkAbleToChange(this.mastersmithJobClass))
		{
			this.mastersmithLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + experienceByJobClass11.getSmithJobClassLevel();
		}
		else
		{
			this.mastersmith.GetComponent<BoxCollider>().enabled = false;
			this.mastersmithLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
	}

	// Token: 0x06000CE9 RID: 3305 RVA: 0x00082128 File Offset: 0x00080528
	private bool checkAbleToChange(SmithJobClass aJobClass)
	{
		foreach (SmithJobClass smithJobClass in this.jcList)
		{
			if (smithJobClass.getSmithJobRefId() == aJobClass.getSmithJobRefId())
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000CEA RID: 3306 RVA: 0x000821A0 File Offset: 0x000805A0
	private void fitSmithJob(SmithJobClass fitJob)
	{
		if (this.selectedClassObj != null)
		{
			this.commonScreenObject.findChild(this.selectedClassObj, "SelectedFrame").GetComponent<UISprite>().enabled = false;
		}
		string smithJobRefId = fitJob.getSmithJobRefId();
		switch (smithJobRefId)
		{
		case "10001":
			this.selectedClassObj = this.designer;
			this.selectedJobClass = this.designerJobClass;
			break;
		case "10002":
			this.selectedClassObj = this.craftsman;
			this.selectedJobClass = this.craftsmanJobClass;
			break;
		case "10003":
			this.selectedClassObj = this.metalworker;
			this.selectedJobClass = this.metalworkerJobClass;
			break;
		case "10004":
			this.selectedClassObj = this.enchanter;
			this.selectedJobClass = this.enchanterJobClass;
			break;
		case "10005":
			this.selectedClassObj = this.inventor;
			this.selectedJobClass = this.inventorJobClass;
			break;
		case "10006":
			this.selectedClassObj = this.artisan;
			this.selectedJobClass = this.artisanJobClass;
			break;
		case "10007":
			this.selectedClassObj = this.mechanic;
			this.selectedJobClass = this.mechanicJobClass;
			break;
		case "10008":
			this.selectedClassObj = this.maestro;
			this.selectedJobClass = this.maestroJobClass;
			break;
		case "10009":
			this.selectedClassObj = this.alchemist;
			this.selectedJobClass = this.alchemistJobClass;
			break;
		case "10010":
			this.selectedClassObj = this.virtuoso;
			this.selectedJobClass = this.virtuosoJobClass;
			break;
		case "10011":
			this.selectedClassObj = this.mastersmith;
			this.selectedJobClass = this.mastersmithJobClass;
			break;
		}
		this.commonScreenObject.findChild(this.selectedClassObj, "SelectedFrame").GetComponent<UISprite>().enabled = true;
		if (this.smithInfo.getSmithPower() < this.smithInfo.fitSmithPower(fitJob))
		{
			this.atkStatChange.color = Color.green;
			this.atkStatChange.text = "+" + (this.smithInfo.fitSmithPower(fitJob) - this.smithInfo.getSmithPower());
		}
		else if (this.smithInfo.getSmithPower() > this.smithInfo.fitSmithPower(fitJob))
		{
			this.atkStatChange.color = Color.red;
			this.atkStatChange.text = "-" + (this.smithInfo.getSmithPower() - this.smithInfo.fitSmithPower(fitJob));
		}
		else
		{
			this.atkStatChange.color = Color.white;
		}
		if (this.smithInfo.getSmithIntelligence() < this.smithInfo.fitSmithIntelligence(fitJob))
		{
			this.spdStatChange.color = Color.green;
			this.spdStatChange.text = "+" + (this.smithInfo.fitSmithIntelligence(fitJob) - this.smithInfo.getSmithIntelligence());
		}
		else if (this.smithInfo.getSmithIntelligence() > this.smithInfo.fitSmithIntelligence(fitJob))
		{
			this.spdStatChange.color = Color.red;
			this.spdStatChange.text = "-" + (this.smithInfo.getSmithIntelligence() - this.smithInfo.fitSmithIntelligence(fitJob));
		}
		else
		{
			this.spdStatChange.color = Color.white;
		}
		if (this.smithInfo.getSmithTechnique() < this.smithInfo.fitSmithTechnique(fitJob))
		{
			this.accStatChange.color = Color.green;
			this.accStatChange.text = "+" + (this.smithInfo.fitSmithTechnique(fitJob) - this.smithInfo.getSmithTechnique());
		}
		else if (this.smithInfo.getSmithTechnique() > this.smithInfo.fitSmithTechnique(fitJob))
		{
			this.accStatChange.color = Color.red;
			this.accStatChange.text = "-" + (this.smithInfo.getSmithTechnique() - this.smithInfo.fitSmithTechnique(fitJob));
		}
		else
		{
			this.accStatChange.color = Color.white;
		}
		if (this.smithInfo.getSmithLuck() < this.smithInfo.fitSmithLuck(fitJob))
		{
			this.magStatChange.color = Color.green;
			this.magStatChange.text = "+" + (this.smithInfo.fitSmithLuck(fitJob) - this.smithInfo.getSmithLuck());
		}
		else if (this.smithInfo.getSmithLuck() > this.smithInfo.fitSmithLuck(fitJob))
		{
			this.magStatChange.color = Color.red;
			this.magStatChange.text = "-" + (this.smithInfo.getSmithLuck() - this.smithInfo.fitSmithLuck(fitJob));
		}
		else
		{
			this.magStatChange.color = Color.white;
		}
		this.cost.text = CommonAPI.formatNumber(fitJob.getSmithJobChangeCost());
		this.changeButton.isEnabled = true;
	}

	// Token: 0x06000CEB RID: 3307 RVA: 0x000827B0 File Offset: 0x00080BB0
	private void changeJob()
	{
		this.currClassObj.GetComponent<UISprite>().spriteName = "class_detail";
		this.currClassObj.GetComponent<BoxCollider>().enabled = true;
		this.currClassObj = this.selectedClassObj;
		this.currClassObj.GetComponent<UISprite>().spriteName = "current_class";
		this.currClassObj.GetComponent<BoxCollider>().enabled = false;
		this.atkStatChange.color = Color.white;
		this.spdStatChange.color = Color.white;
		this.accStatChange.color = Color.white;
		this.magStatChange.color = Color.white;
		this.atkStat.text = CommonAPI.formatNumber(this.smithInfo.fitSmithPower(this.selectedJobClass));
		this.spdStat.text = CommonAPI.formatNumber(this.smithInfo.fitSmithIntelligence(this.selectedJobClass));
		this.accStat.text = CommonAPI.formatNumber(this.smithInfo.fitSmithTechnique(this.selectedJobClass));
		this.magStat.text = CommonAPI.formatNumber(this.smithInfo.fitSmithLuck(this.selectedJobClass));
		this.selectedClassObj = null;
		this.selectedJobClass = new SmithJobClass();
	}

	// Token: 0x04000E93 RID: 3731
	private Game game;

	// Token: 0x04000E94 RID: 3732
	private CommonScreenObject commonScreenObject;

	// Token: 0x04000E95 RID: 3733
	private ShopMenuController shopMenuController;

	// Token: 0x04000E96 RID: 3734
	private AudioController audioController;

	// Token: 0x04000E97 RID: 3735
	private ViewController viewController;

	// Token: 0x04000E98 RID: 3736
	private UILabel changeClassTitleLabel;

	// Token: 0x04000E99 RID: 3737
	private Smith smithInfo;

	// Token: 0x04000E9A RID: 3738
	private List<SmithJobClass> jcList;

	// Token: 0x04000E9B RID: 3739
	private GameObject currClassObj;

	// Token: 0x04000E9C RID: 3740
	private GameObject selectedClassObj;

	// Token: 0x04000E9D RID: 3741
	private SmithJobClass selectedJobClass;

	// Token: 0x04000E9E RID: 3742
	private UILabel smithName;

	// Token: 0x04000E9F RID: 3743
	private UILabel atkStat;

	// Token: 0x04000EA0 RID: 3744
	private UILabel atkStatChange;

	// Token: 0x04000EA1 RID: 3745
	private UILabel spdStat;

	// Token: 0x04000EA2 RID: 3746
	private UILabel spdStatChange;

	// Token: 0x04000EA3 RID: 3747
	private UILabel accStat;

	// Token: 0x04000EA4 RID: 3748
	private UILabel accStatChange;

	// Token: 0x04000EA5 RID: 3749
	private UILabel magStat;

	// Token: 0x04000EA6 RID: 3750
	private UILabel magStatChange;

	// Token: 0x04000EA7 RID: 3751
	private UILabel costLabel;

	// Token: 0x04000EA8 RID: 3752
	private UILabel cost;

	// Token: 0x04000EA9 RID: 3753
	private UIButton changeButton;

	// Token: 0x04000EAA RID: 3754
	private UILabel changeLabel;

	// Token: 0x04000EAB RID: 3755
	private SmithJobClass designerJobClass;

	// Token: 0x04000EAC RID: 3756
	private GameObject designer;

	// Token: 0x04000EAD RID: 3757
	private UILabel designerLabel;

	// Token: 0x04000EAE RID: 3758
	private UILabel designerLvl;

	// Token: 0x04000EAF RID: 3759
	private SmithJobClass craftsmanJobClass;

	// Token: 0x04000EB0 RID: 3760
	private GameObject craftsman;

	// Token: 0x04000EB1 RID: 3761
	private UILabel craftsmanLabel;

	// Token: 0x04000EB2 RID: 3762
	private UILabel craftsmanLvl;

	// Token: 0x04000EB3 RID: 3763
	private SmithJobClass metalworkerJobClass;

	// Token: 0x04000EB4 RID: 3764
	private GameObject metalworker;

	// Token: 0x04000EB5 RID: 3765
	private UILabel metalworkerLabel;

	// Token: 0x04000EB6 RID: 3766
	private UILabel metalworkerLvl;

	// Token: 0x04000EB7 RID: 3767
	private SmithJobClass enchanterJobClass;

	// Token: 0x04000EB8 RID: 3768
	private GameObject enchanter;

	// Token: 0x04000EB9 RID: 3769
	private UILabel enchanterLabel;

	// Token: 0x04000EBA RID: 3770
	private UILabel enchanterLvl;

	// Token: 0x04000EBB RID: 3771
	private SmithJobClass inventorJobClass;

	// Token: 0x04000EBC RID: 3772
	private GameObject inventor;

	// Token: 0x04000EBD RID: 3773
	private UILabel inventorLabel;

	// Token: 0x04000EBE RID: 3774
	private UILabel inventorLvl;

	// Token: 0x04000EBF RID: 3775
	private SmithJobClass artisanJobClass;

	// Token: 0x04000EC0 RID: 3776
	private GameObject artisan;

	// Token: 0x04000EC1 RID: 3777
	private UILabel artisanLabel;

	// Token: 0x04000EC2 RID: 3778
	private UILabel artisanLvl;

	// Token: 0x04000EC3 RID: 3779
	private SmithJobClass mechanicJobClass;

	// Token: 0x04000EC4 RID: 3780
	private GameObject mechanic;

	// Token: 0x04000EC5 RID: 3781
	private UILabel mechanicLabel;

	// Token: 0x04000EC6 RID: 3782
	private UILabel mechanicLvl;

	// Token: 0x04000EC7 RID: 3783
	private SmithJobClass maestroJobClass;

	// Token: 0x04000EC8 RID: 3784
	private GameObject maestro;

	// Token: 0x04000EC9 RID: 3785
	private UILabel maestroLabel;

	// Token: 0x04000ECA RID: 3786
	private UILabel maestroLvl;

	// Token: 0x04000ECB RID: 3787
	private SmithJobClass alchemistJobClass;

	// Token: 0x04000ECC RID: 3788
	private GameObject alchemist;

	// Token: 0x04000ECD RID: 3789
	private UILabel alchemistLabel;

	// Token: 0x04000ECE RID: 3790
	private UILabel alchemistLvl;

	// Token: 0x04000ECF RID: 3791
	private SmithJobClass virtuosoJobClass;

	// Token: 0x04000ED0 RID: 3792
	private GameObject virtuoso;

	// Token: 0x04000ED1 RID: 3793
	private UILabel virtuosoLabel;

	// Token: 0x04000ED2 RID: 3794
	private UILabel virtuosoLvl;

	// Token: 0x04000ED3 RID: 3795
	private SmithJobClass mastersmithJobClass;

	// Token: 0x04000ED4 RID: 3796
	private GameObject mastersmith;

	// Token: 0x04000ED5 RID: 3797
	private UILabel mastersmithLabel;

	// Token: 0x04000ED6 RID: 3798
	private UILabel mastersmithLvl;

	// Token: 0x04000ED7 RID: 3799
	private Color32 statsEqualColor;
}
