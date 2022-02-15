using System.Collections.Generic;
using UnityEngine;

public class GUIChangeJobSmithController : MonoBehaviour
{
	private Game game;

	private CommonScreenObject commonScreenObject;

	private ShopMenuController shopMenuController;

	private AudioController audioController;

	private ViewController viewController;

	private UILabel changeClassTitleLabel;

	private Smith smithInfo;

	private List<SmithJobClass> jcList;

	private GameObject currClassObj;

	private GameObject selectedClassObj;

	private SmithJobClass selectedJobClass;

	private UILabel smithName;

	private UILabel atkStat;

	private UILabel atkStatChange;

	private UILabel spdStat;

	private UILabel spdStatChange;

	private UILabel accStat;

	private UILabel accStatChange;

	private UILabel magStat;

	private UILabel magStatChange;

	private UILabel costLabel;

	private UILabel cost;

	private UIButton changeButton;

	private UILabel changeLabel;

	private SmithJobClass designerJobClass;

	private GameObject designer;

	private UILabel designerLabel;

	private UILabel designerLvl;

	private SmithJobClass craftsmanJobClass;

	private GameObject craftsman;

	private UILabel craftsmanLabel;

	private UILabel craftsmanLvl;

	private SmithJobClass metalworkerJobClass;

	private GameObject metalworker;

	private UILabel metalworkerLabel;

	private UILabel metalworkerLvl;

	private SmithJobClass enchanterJobClass;

	private GameObject enchanter;

	private UILabel enchanterLabel;

	private UILabel enchanterLvl;

	private SmithJobClass inventorJobClass;

	private GameObject inventor;

	private UILabel inventorLabel;

	private UILabel inventorLvl;

	private SmithJobClass artisanJobClass;

	private GameObject artisan;

	private UILabel artisanLabel;

	private UILabel artisanLvl;

	private SmithJobClass mechanicJobClass;

	private GameObject mechanic;

	private UILabel mechanicLabel;

	private UILabel mechanicLvl;

	private SmithJobClass maestroJobClass;

	private GameObject maestro;

	private UILabel maestroLabel;

	private UILabel maestroLvl;

	private SmithJobClass alchemistJobClass;

	private GameObject alchemist;

	private UILabel alchemistLabel;

	private UILabel alchemistLvl;

	private SmithJobClass virtuosoJobClass;

	private GameObject virtuoso;

	private UILabel virtuosoLabel;

	private UILabel virtuosoLvl;

	private SmithJobClass mastersmithJobClass;

	private GameObject mastersmith;

	private UILabel mastersmithLabel;

	private UILabel mastersmithLvl;

	private Color32 statsEqualColor;

	private void Awake()
	{
		game = GameObject.Find("Game").GetComponent<Game>();
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		shopMenuController = GameObject.Find("ShopMenuController").GetComponent<ShopMenuController>();
		audioController = GameObject.Find("AudioController").GetComponent<AudioController>();
		viewController = GameObject.Find("ViewController").GetComponent<ViewController>();
		changeClassTitleLabel = commonScreenObject.findChild(base.gameObject, "ChangeClassTitleLabel").GetComponent<UILabel>();
		jcList = new List<SmithJobClass>();
		currClassObj = null;
		selectedClassObj = null;
		GameObject aObject = commonScreenObject.findChild(base.gameObject, "Bg_SmithStats").gameObject;
		smithName = commonScreenObject.findChild(aObject, "SmithName").GetComponent<UILabel>();
		atkStat = commonScreenObject.findChild(aObject, "SmithStats/Atk/AtkStat").GetComponent<UILabel>();
		atkStatChange = commonScreenObject.findChild(aObject, "SmithStats/Atk/AtkStatChange").GetComponent<UILabel>();
		spdStat = commonScreenObject.findChild(aObject, "SmithStats/Spd/SpdStat").GetComponent<UILabel>();
		spdStatChange = commonScreenObject.findChild(aObject, "SmithStats/Spd/SpdStatChange").GetComponent<UILabel>();
		accStat = commonScreenObject.findChild(aObject, "SmithStats/Acc/AccStat").GetComponent<UILabel>();
		accStatChange = commonScreenObject.findChild(aObject, "SmithStats/Acc/AccStatChange").GetComponent<UILabel>();
		magStat = commonScreenObject.findChild(aObject, "SmithStats/Mag/MagStat").GetComponent<UILabel>();
		magStatChange = commonScreenObject.findChild(aObject, "SmithStats/Mag/MagStatChange").GetComponent<UILabel>();
		costLabel = commonScreenObject.findChild(aObject, "CostLabel").GetComponent<UILabel>();
		cost = commonScreenObject.findChild(aObject, "Cost").GetComponent<UILabel>();
		changeButton = commonScreenObject.findChild(aObject, "ChangeButton").GetComponent<UIButton>();
		changeLabel = commonScreenObject.findChild(aObject, "ChangeButton/ChangeLabel").GetComponent<UILabel>();
		designer = commonScreenObject.findChild(base.gameObject, "Designer").gameObject;
		designerLabel = commonScreenObject.findChild(designer, "DesignerLabel").GetComponent<UILabel>();
		designerLvl = commonScreenObject.findChild(designer, "DesignerLvl").GetComponent<UILabel>();
		craftsman = commonScreenObject.findChild(base.gameObject, "Craftsman").gameObject;
		craftsmanLabel = commonScreenObject.findChild(craftsman, "CraftsmanLabel").GetComponent<UILabel>();
		craftsmanLvl = commonScreenObject.findChild(craftsman, "CraftsmanLvl").GetComponent<UILabel>();
		metalworker = commonScreenObject.findChild(base.gameObject, "Metalworker").gameObject;
		metalworkerLabel = commonScreenObject.findChild(metalworker, "MetalworkerLabel").GetComponent<UILabel>();
		metalworkerLvl = commonScreenObject.findChild(metalworker, "MetalworkerLvl").GetComponent<UILabel>();
		enchanter = commonScreenObject.findChild(base.gameObject, "Enchanter").gameObject;
		enchanterLabel = commonScreenObject.findChild(enchanter, "EnchanterLabel").GetComponent<UILabel>();
		enchanterLvl = commonScreenObject.findChild(enchanter, "EnchanterLvl").GetComponent<UILabel>();
		inventor = commonScreenObject.findChild(base.gameObject, "Inventor").gameObject;
		inventorLabel = commonScreenObject.findChild(inventor, "InventorLabel").GetComponent<UILabel>();
		inventorLvl = commonScreenObject.findChild(inventor, "InventorLvl").GetComponent<UILabel>();
		artisan = commonScreenObject.findChild(base.gameObject, "Artisan").gameObject;
		artisanLabel = commonScreenObject.findChild(artisan, "ArtisanLabel").GetComponent<UILabel>();
		artisanLvl = commonScreenObject.findChild(artisan, "ArtisanLvl").GetComponent<UILabel>();
		mechanic = commonScreenObject.findChild(base.gameObject, "Mechanic").gameObject;
		mechanicLabel = commonScreenObject.findChild(mechanic, "MechanicLabel").GetComponent<UILabel>();
		mechanicLvl = commonScreenObject.findChild(mechanic, "MechanicLvl").GetComponent<UILabel>();
		maestro = commonScreenObject.findChild(base.gameObject, "Maestro").gameObject;
		maestroLabel = commonScreenObject.findChild(maestro, "MaestroLabel").GetComponent<UILabel>();
		maestroLvl = commonScreenObject.findChild(maestro, "MaestroLvl").GetComponent<UILabel>();
		alchemist = commonScreenObject.findChild(base.gameObject, "Alchemist").gameObject;
		alchemistLabel = commonScreenObject.findChild(alchemist, "AlchemistLabel").GetComponent<UILabel>();
		alchemistLvl = commonScreenObject.findChild(alchemist, "AlchemistLvl").GetComponent<UILabel>();
		virtuoso = commonScreenObject.findChild(base.gameObject, "Virtuoso").gameObject;
		virtuosoLabel = commonScreenObject.findChild(virtuoso, "VirtuosoLabel").GetComponent<UILabel>();
		virtuosoLvl = commonScreenObject.findChild(virtuoso, "VirtuosoLvl").GetComponent<UILabel>();
		mastersmith = commonScreenObject.findChild(base.gameObject, "MasterSmith").gameObject;
		mastersmithLabel = commonScreenObject.findChild(mastersmith, "MasterSmithLabel").GetComponent<UILabel>();
		mastersmithLvl = commonScreenObject.findChild(mastersmith, "MasterSmithLvl").GetComponent<UILabel>();
		statsEqualColor = new Color32(128, 71, 3, byte.MaxValue);
	}

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

	public void setReference(Smith aSmith)
	{
		GameData gameData = game.getGameData();
		smithInfo = aSmith;
		changeClassTitleLabel.text = gameData.getTextByRefId("menuSmithManagement37");
		smithName.text = smithInfo.getSmithName();
		atkStat.text = CommonAPI.formatNumber(smithInfo.getSmithPower());
		spdStat.text = CommonAPI.formatNumber(smithInfo.getSmithIntelligence());
		accStat.text = CommonAPI.formatNumber(smithInfo.getSmithTechnique());
		magStat.text = CommonAPI.formatNumber(smithInfo.getSmithLuck());
		costLabel.text = gameData.getTextByRefId("menuShopFurniture02");
		cost.text = string.Empty;
		changeButton.isEnabled = false;
		changeLabel.text = gameData.getTextByRefId("menuSmithManagement39");
		jcList = gameData.getJobChangeList(smithInfo.getExperienceList(), smithInfo.getSmithJob().getSmithJobRefId());
		designerJobClass = gameData.getSmithJobClass("10001");
		SmithExperience experienceByJobClass = smithInfo.getExperienceByJobClass("10001");
		designerLabel.text = designerJobClass.getSmithJobName();
		if (designerJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			CommonAPI.debug("designer");
			designer.GetComponent<UISprite>().spriteName = "current_class";
			designer.GetComponent<BoxCollider>().enabled = false;
			currClassObj = designer;
		}
		if (checkAbleToChange(designerJobClass))
		{
			int smithJobClassLevel = experienceByJobClass.getSmithJobClassLevel();
			designerLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel;
			if (smithJobClassLevel >= designerJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(designer, "DesignerMaxLeft").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			designer.GetComponent<BoxCollider>().enabled = false;
			designerLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		craftsmanJobClass = gameData.getSmithJobClass("10002");
		SmithExperience experienceByJobClass2 = smithInfo.getExperienceByJobClass("10002");
		craftsmanLabel.text = craftsmanJobClass.getSmithJobName();
		if (craftsmanJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			craftsman.GetComponent<UISprite>().spriteName = "current_class";
			craftsman.GetComponent<BoxCollider>().enabled = false;
			currClassObj = craftsman;
		}
		if (checkAbleToChange(craftsmanJobClass))
		{
			int smithJobClassLevel2 = experienceByJobClass2.getSmithJobClassLevel();
			craftsmanLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel2;
			if (smithJobClassLevel2 >= craftsmanJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(craftsman, "CraftsmanMaxLeft").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(craftsman, "CraftsmanMaxRight").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			craftsman.GetComponent<BoxCollider>().enabled = false;
			craftsmanLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		metalworkerJobClass = gameData.getSmithJobClass("10003");
		SmithExperience experienceByJobClass3 = smithInfo.getExperienceByJobClass("10003");
		metalworkerLabel.text = metalworkerJobClass.getSmithJobName();
		if (metalworkerJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			metalworker.GetComponent<UISprite>().spriteName = "current_class";
			metalworker.GetComponent<BoxCollider>().enabled = false;
			currClassObj = metalworker;
		}
		if (checkAbleToChange(metalworkerJobClass))
		{
			int smithJobClassLevel3 = experienceByJobClass3.getSmithJobClassLevel();
			metalworkerLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel3;
			if (smithJobClassLevel3 >= metalworkerJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(metalworker, "MetalworkerMaxLeft").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(metalworker, "MetalWorkerMaxRight").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			metalworker.GetComponent<BoxCollider>().enabled = false;
			metalworkerLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		enchanterJobClass = gameData.getSmithJobClass("10004");
		SmithExperience experienceByJobClass4 = smithInfo.getExperienceByJobClass("10004");
		enchanterLabel.text = enchanterJobClass.getSmithJobName();
		if (enchanterJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			enchanter.GetComponent<UISprite>().spriteName = "current_class";
			enchanter.GetComponent<BoxCollider>().enabled = false;
			currClassObj = enchanter;
		}
		if (checkAbleToChange(enchanterJobClass))
		{
			int smithJobClassLevel4 = experienceByJobClass4.getSmithJobClassLevel();
			enchanterLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel4;
			if (smithJobClassLevel4 >= enchanterJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(enchanter, "EnchanterMaxLeft").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			enchanter.GetComponent<BoxCollider>().enabled = false;
			enchanterLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		inventorJobClass = gameData.getSmithJobClass("10005");
		SmithExperience experienceByJobClass5 = smithInfo.getExperienceByJobClass("10005");
		inventorLabel.text = inventorJobClass.getSmithJobName();
		if (inventorJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			inventor.GetComponent<UISprite>().spriteName = "current_class";
			inventor.GetComponent<BoxCollider>().enabled = false;
			currClassObj = inventor;
		}
		if (checkAbleToChange(inventorJobClass))
		{
			commonScreenObject.findChild(inventor, "InventorUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel5 = experienceByJobClass5.getSmithJobClassLevel();
			inventorLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel5;
			if (smithJobClassLevel5 >= inventorJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(inventor, "InventorMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			inventor.GetComponent<BoxCollider>().enabled = false;
			inventorLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		artisanJobClass = gameData.getSmithJobClass("10006");
		SmithExperience experienceByJobClass6 = smithInfo.getExperienceByJobClass("10006");
		artisanLabel.text = artisanJobClass.getSmithJobName();
		if (artisanJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			artisan.GetComponent<UISprite>().spriteName = "current_class";
			artisan.GetComponent<BoxCollider>().enabled = false;
			currClassObj = artisan;
		}
		if (checkAbleToChange(artisanJobClass))
		{
			commonScreenObject.findChild(artisan, "ArtisanUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel6 = experienceByJobClass6.getSmithJobClassLevel();
			artisanLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel6;
			if (smithJobClassLevel6 >= artisanJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(artisan, "ArtisanMaxLeft").GetComponent<UISprite>().enabled = true;
				commonScreenObject.findChild(artisan, "ArtisanMaxRight").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			artisan.GetComponent<BoxCollider>().enabled = false;
			artisanLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		mechanicJobClass = gameData.getSmithJobClass("10007");
		SmithExperience experienceByJobClass7 = smithInfo.getExperienceByJobClass("10007");
		mechanicLabel.text = mechanicJobClass.getSmithJobName();
		if (mechanicJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			mechanic.GetComponent<UISprite>().spriteName = "current_class";
			mechanic.GetComponent<BoxCollider>().enabled = false;
			currClassObj = mechanic;
		}
		if (checkAbleToChange(mechanicJobClass))
		{
			commonScreenObject.findChild(mechanic, "MechanicUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel7 = experienceByJobClass7.getSmithJobClassLevel();
			mechanicLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel7;
			if (smithJobClassLevel7 >= mechanicJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(mechanic, "MechanicMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			mechanic.GetComponent<BoxCollider>().enabled = false;
			mechanicLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		maestroJobClass = gameData.getSmithJobClass("10008");
		SmithExperience experienceByJobClass8 = smithInfo.getExperienceByJobClass("10008");
		maestroLabel.text = maestroJobClass.getSmithJobName();
		if (maestroJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			maestro.GetComponent<UISprite>().spriteName = "current_class";
			maestro.GetComponent<BoxCollider>().enabled = false;
			currClassObj = maestro;
		}
		if (checkAbleToChange(maestroJobClass))
		{
			commonScreenObject.findChild(maestro, "MaestroUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel8 = experienceByJobClass8.getSmithJobClassLevel();
			maestroLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel8;
			if (smithJobClassLevel8 >= maestroJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(maestro, "MaestroMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			maestro.GetComponent<BoxCollider>().enabled = false;
			maestroLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		alchemistJobClass = gameData.getSmithJobClass("10009");
		SmithExperience experienceByJobClass9 = smithInfo.getExperienceByJobClass("10009");
		alchemistLabel.text = alchemistJobClass.getSmithJobName();
		if (alchemistJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			alchemist.GetComponent<UISprite>().spriteName = "current_class";
			alchemist.GetComponent<BoxCollider>().enabled = false;
			currClassObj = alchemist;
		}
		if (checkAbleToChange(alchemistJobClass))
		{
			commonScreenObject.findChild(alchemist, "AlchemistUnlock").GetComponent<UISprite>().enabled = true;
			int smithJobClassLevel9 = experienceByJobClass9.getSmithJobClassLevel();
			alchemistLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + smithJobClassLevel9;
			if (smithJobClassLevel9 >= alchemistJobClass.getMaxLevel())
			{
				commonScreenObject.findChild(alchemist, "AlchemistMax").GetComponent<UISprite>().enabled = true;
			}
		}
		else
		{
			alchemist.GetComponent<BoxCollider>().enabled = false;
			alchemistLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		virtuosoJobClass = gameData.getSmithJobClass("10010");
		SmithExperience experienceByJobClass10 = smithInfo.getExperienceByJobClass("10010");
		virtuosoLabel.text = virtuosoJobClass.getSmithJobName();
		if (virtuosoJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			virtuoso.GetComponent<UISprite>().spriteName = "current_class";
			virtuoso.GetComponent<BoxCollider>().enabled = false;
			currClassObj = virtuoso;
		}
		if (checkAbleToChange(virtuosoJobClass))
		{
			virtuosoLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + experienceByJobClass10.getSmithJobClassLevel();
		}
		else
		{
			virtuoso.GetComponent<BoxCollider>().enabled = false;
			virtuosoLvl.text = gameData.getTextByRefId("menuChangeJob01");
		}
		mastersmithJobClass = gameData.getSmithJobClass("10011");
		SmithExperience experienceByJobClass11 = smithInfo.getExperienceByJobClass("10011");
		mastersmithLabel.text = mastersmithJobClass.getSmithJobName();
		if (mastersmithJobClass.getSmithJobRefId() == smithInfo.getSmithJob().getSmithJobRefId())
		{
			mastersmith.GetComponent<UISprite>().spriteName = "current_class";
			mastersmith.GetComponent<BoxCollider>().enabled = false;
			currClassObj = mastersmith;
		}
		if (checkAbleToChange(mastersmithJobClass))
		{
			mastersmithLvl.text = gameData.getTextByRefId("smithStatsShort01") + " " + experienceByJobClass11.getSmithJobClassLevel();
			return;
		}
		mastersmith.GetComponent<BoxCollider>().enabled = false;
		mastersmithLvl.text = gameData.getTextByRefId("menuChangeJob01");
	}

	private bool checkAbleToChange(SmithJobClass aJobClass)
	{
		foreach (SmithJobClass jc in jcList)
		{
			if (jc.getSmithJobRefId() == aJobClass.getSmithJobRefId())
			{
				return true;
			}
		}
		return false;
	}

	private void fitSmithJob(SmithJobClass fitJob)
	{
		if (selectedClassObj != null)
		{
			commonScreenObject.findChild(selectedClassObj, "SelectedFrame").GetComponent<UISprite>().enabled = false;
		}
		switch (fitJob.getSmithJobRefId())
		{
		case "10001":
			selectedClassObj = designer;
			selectedJobClass = designerJobClass;
			break;
		case "10002":
			selectedClassObj = craftsman;
			selectedJobClass = craftsmanJobClass;
			break;
		case "10003":
			selectedClassObj = metalworker;
			selectedJobClass = metalworkerJobClass;
			break;
		case "10004":
			selectedClassObj = enchanter;
			selectedJobClass = enchanterJobClass;
			break;
		case "10005":
			selectedClassObj = inventor;
			selectedJobClass = inventorJobClass;
			break;
		case "10006":
			selectedClassObj = artisan;
			selectedJobClass = artisanJobClass;
			break;
		case "10007":
			selectedClassObj = mechanic;
			selectedJobClass = mechanicJobClass;
			break;
		case "10008":
			selectedClassObj = maestro;
			selectedJobClass = maestroJobClass;
			break;
		case "10009":
			selectedClassObj = alchemist;
			selectedJobClass = alchemistJobClass;
			break;
		case "10010":
			selectedClassObj = virtuoso;
			selectedJobClass = virtuosoJobClass;
			break;
		case "10011":
			selectedClassObj = mastersmith;
			selectedJobClass = mastersmithJobClass;
			break;
		}
		commonScreenObject.findChild(selectedClassObj, "SelectedFrame").GetComponent<UISprite>().enabled = true;
		if (smithInfo.getSmithPower() < smithInfo.fitSmithPower(fitJob))
		{
			atkStatChange.color = Color.green;
			atkStatChange.text = "+" + (smithInfo.fitSmithPower(fitJob) - smithInfo.getSmithPower());
		}
		else if (smithInfo.getSmithPower() > smithInfo.fitSmithPower(fitJob))
		{
			atkStatChange.color = Color.red;
			atkStatChange.text = "-" + (smithInfo.getSmithPower() - smithInfo.fitSmithPower(fitJob));
		}
		else
		{
			atkStatChange.color = Color.white;
		}
		if (smithInfo.getSmithIntelligence() < smithInfo.fitSmithIntelligence(fitJob))
		{
			spdStatChange.color = Color.green;
			spdStatChange.text = "+" + (smithInfo.fitSmithIntelligence(fitJob) - smithInfo.getSmithIntelligence());
		}
		else if (smithInfo.getSmithIntelligence() > smithInfo.fitSmithIntelligence(fitJob))
		{
			spdStatChange.color = Color.red;
			spdStatChange.text = "-" + (smithInfo.getSmithIntelligence() - smithInfo.fitSmithIntelligence(fitJob));
		}
		else
		{
			spdStatChange.color = Color.white;
		}
		if (smithInfo.getSmithTechnique() < smithInfo.fitSmithTechnique(fitJob))
		{
			accStatChange.color = Color.green;
			accStatChange.text = "+" + (smithInfo.fitSmithTechnique(fitJob) - smithInfo.getSmithTechnique());
		}
		else if (smithInfo.getSmithTechnique() > smithInfo.fitSmithTechnique(fitJob))
		{
			accStatChange.color = Color.red;
			accStatChange.text = "-" + (smithInfo.getSmithTechnique() - smithInfo.fitSmithTechnique(fitJob));
		}
		else
		{
			accStatChange.color = Color.white;
		}
		if (smithInfo.getSmithLuck() < smithInfo.fitSmithLuck(fitJob))
		{
			magStatChange.color = Color.green;
			magStatChange.text = "+" + (smithInfo.fitSmithLuck(fitJob) - smithInfo.getSmithLuck());
		}
		else if (smithInfo.getSmithLuck() > smithInfo.fitSmithLuck(fitJob))
		{
			magStatChange.color = Color.red;
			magStatChange.text = "-" + (smithInfo.getSmithLuck() - smithInfo.fitSmithLuck(fitJob));
		}
		else
		{
			magStatChange.color = Color.white;
		}
		cost.text = CommonAPI.formatNumber(fitJob.getSmithJobChangeCost());
		changeButton.isEnabled = true;
	}

	private void changeJob()
	{
		currClassObj.GetComponent<UISprite>().spriteName = "class_detail";
		currClassObj.GetComponent<BoxCollider>().enabled = true;
		currClassObj = selectedClassObj;
		currClassObj.GetComponent<UISprite>().spriteName = "current_class";
		currClassObj.GetComponent<BoxCollider>().enabled = false;
		atkStatChange.color = Color.white;
		spdStatChange.color = Color.white;
		accStatChange.color = Color.white;
		magStatChange.color = Color.white;
		atkStat.text = CommonAPI.formatNumber(smithInfo.fitSmithPower(selectedJobClass));
		spdStat.text = CommonAPI.formatNumber(smithInfo.fitSmithIntelligence(selectedJobClass));
		accStat.text = CommonAPI.formatNumber(smithInfo.fitSmithTechnique(selectedJobClass));
		magStat.text = CommonAPI.formatNumber(smithInfo.fitSmithLuck(selectedJobClass));
		selectedClassObj = null;
		selectedJobClass = new SmithJobClass();
	}
}
