using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
	private string bgm;

	private GameObject prevBGM;

	private GameObject currentBGM;

	private bool crossfade;

	private bool weatherCrossfade;

	private bool fadein;

	private bool fadeout;

	private int gemBreakNum;

	private int projNum;

	private float sfxVolume;

	private float bgmVolume;

	private CommonScreenObject commonScreenObject;

	private GameObject springBGM;

	private GameObject summerBGM;

	private GameObject autumnBGM;

	private GameObject winterBGM;

	private GameObject startMenuBGM;

	private GameObject awardsBGM;

	private GameObject creditsBGM;

	private GameObject worldMapBGM;

	private GameObject cutsceneConfrontationBGM;

	private GameObject cutsceneFinalBGM;

	private GameObject cutsceneHappyBGM;

	private GameObject cutsceneLegendaryBGM;

	private GameObject cutsceneNormalBGM;

	private GameObject cutsceneNostalgicBGM;

	private GameObject cutsceneStupidBGM;

	private GameObject buttonAudio;

	private GameObject menuOpenAudio;

	private GameObject purchaseAudio;

	private GameObject goldGainAudio;

	private GameObject fameGainAudio;

	private GameObject forgingAudio;

	private GameObject dialogueAudio;

	private GameObject slideEnterAudio;

	private GameObject slideExitAudio;

	private GameObject popupAudio;

	private GameObject numberUpAudio;

	private GameObject textTypeAudio;

	private GameObject forgeGrowthAudio;

	private GameObject forgeStartAudio;

	private GameObject forgeCompleteAudio;

	private GameObject forgeBGLoop;

	private GameObject forgeBoostIncreaseLowerAudio;

	private GameObject forgeBoostIncreaseAudio;

	private GameObject forgeBoostEnchantAudio;

	private GameObject auctionBidAudio;

	private GameObject auctionSoldAudio;

	private GameObject auctionAudienceLoop;

	private GameObject auctionScrambleLoop;

	private GameObject subquestCompleteAudio;

	private GameObject questBarLoop;

	private GameObject awardsChanceUpAudio;

	private GameObject awardsDrumRollLoop;

	private GameObject awardsPersuasionCompleteAudio;

	private GameObject awardsRiskAudio;

	private GameObject awardsCongratsAudio;

	private GameObject awardsDisqualifiedAudio;

	private GameObject awardsWaitingLoop;

	private GameObject shopPurchaseAudio;

	private GameObject smithChiAudio;

	private GameObject smithCoffeeAudio;

	private GameObject smithEnterAudio;

	private GameObject smithExitAudio;

	private GameObject smithActionAlertAudio;

	private GameObject legendAppearAudio;

	private GameObject legendRequestAudio;

	private GameObject eventAppearAudio;

	private GameObject eventFailAudio;

	private GameObject eventSuccessAudio;

	private GameObject smithDepressedAudio;

	private GameObject smithSadAudio;

	private GameObject smithNeutralAudio;

	private GameObject smithHappyAudio;

	private GameObject smithElatedAudio;

	private GameObject prevWeatherAudio;

	private GameObject currWeatherAudio;

	private GameObject beamInAudio;

	private GameObject beamOutAudio;

	private GameObject craftStationAudio;

	private GameObject designStationAudio;

	private GameObject polishStationAudio;

	private GameObject enchantStationAudio;

	private GameObject fireplaceAudio;

	private GameObject airconAudio;

	private GameObject portalAudio;

	private GameObject smithDialogueAudio;

	private GameObject smithThoughtBubbleAudio;

	private Dictionary<string, GameObject> smithFrozenAudioList = new Dictionary<string, GameObject>();

	private Dictionary<string, GameObject> smithSickAudioList = new Dictionary<string, GameObject>();

	private Dictionary<string, GameObject> smithFootStepAudioList = new Dictionary<string, GameObject>();

	private GameObject dogBarkAudio;

	private GameObject whetsappAudio;

	private GameObject objectiveCompleteAudio;

	private GameObject itemGetAudio;

	private GameObject heroHoverAudio;

	private GameObject heroSelectAudio;

	private GameObject sellStartAudio;

	private GameObject sellRatingAudio;

	private GameObject sellDialogueAudio;

	private GameObject sellHeroesSlideAudio;

	private GameObject sellCoinAudio;

	private GameObject mapConfirmAudio;

	private GameObject mapSelectItemAudio;

	private GameObject mapSlideAudio;

	private GameObject breakAudio1;

	private GameObject breakAudio2;

	private GameObject breakAudio3;

	private GameObject breakHighAudio1;

	private GameObject breakHighAudio2;

	private GameObject breakHighAudio3;

	private GameObject breakBigAudio1;

	private GameObject breakBigAudio2;

	private GameObject breakBigAudio3;

	private GameObject powerUpAudio;

	private Hashtable powerClipList;

	private GameObject enemyHitAudio1;

	private GameObject enemyHitAudio2;

	private GameObject enemyHitAudio3;

	private int hitNum;

	private GameObject taskCompleteAudio;

	private GameObject burnAudio;

	private GameObject cutAudio;

	private GameObject warningAudio;

	private GameObject spiritSpecialAudio;

	private Hashtable spiritSpecialList;

	private GameObject spiritActivateAudio;

	private GameObject bossDisableAudio;

	private GameObject bossFireAudio;

	private GameObject bossIceAudio;

	private GameObject bossLightningAudio;

	private GameObject bossVineAudio;

	private GameObject IceBreakBig;

	private GameObject IceBreakSmall;

	private GameObject UnlockBreak;

	private GameObject questClaimAudio;

	private GameObject areaUnlockAudio;

	private GameObject gemFallAudio;

	private GameObject doorOpenAudio;

	private GameObject doorCloseAudio;

	private GameObject resultProgressAudio;

	private GameObject resultStarAudio;

	private GameObject resultStageClearAudio;

	private GameObject battleStageCompleteAudio;

	private GameObject izecFlyAudio;

	private GameObject timerAudio;

	private GameObject timerBellAudio;

	private GameObject panelSwitchAudio;

	private GameObject crystalAudio;

	private GameObject resultAudio;

	private GameObject barLoop;

	private void Start()
	{
		commonScreenObject = GameObject.Find("CommonScreenObject").GetComponent<CommonScreenObject>();
		bgm = "startmenu";
		crossfade = false;
		weatherCrossfade = false;
		sfxVolume = PlayerPrefs.GetFloat("soundVol", 1f);
		bgmVolume = PlayerPrefs.GetFloat("musicVol", 1f);
		setBGM();
		playBGMAudio(string.Empty);
		gemBreakNum = 1;
		projNum = 1;
		hitNum = 1;
	}

	private void Update()
	{
		if (crossfade)
		{
			if (prevBGM != null)
			{
				if (prevBGM.GetComponent<AudioSource>().volume > 0f)
				{
					prevBGM.GetComponent<AudioSource>().volume -= Constants.VOLUME_INTERVAL;
				}
				else
				{
					prevBGM.GetComponent<AudioSource>().volume = 0f;
				}
			}
			currentBGM.GetComponent<AudioSource>().volume = bgmVolume;
			if ((prevBGM == null || prevBGM.GetComponent<AudioSource>().volume <= 0f) && currentBGM.GetComponent<AudioSource>().volume >= bgmVolume)
			{
				crossfade = false;
			}
		}
		if (fadeout)
		{
			fadein = false;
			if (currentBGM.GetComponent<AudioSource>().volume > Constants.VOLUME_INTERVAL)
			{
				currentBGM.GetComponent<AudioSource>().volume -= Constants.VOLUME_INTERVAL;
			}
			else
			{
				currentBGM.GetComponent<AudioSource>().volume = 0f;
				fadeout = false;
			}
		}
		if (fadein)
		{
			fadeout = false;
			if (currentBGM.GetComponent<AudioSource>().volume < bgmVolume)
			{
				currentBGM.GetComponent<AudioSource>().volume += Constants.VOLUME_INTERVAL;
			}
			else
			{
				currentBGM.GetComponent<AudioSource>().volume = bgmVolume;
				fadein = false;
			}
		}
		if (!weatherCrossfade)
		{
			return;
		}
		if (prevWeatherAudio != null)
		{
			if (prevWeatherAudio.GetComponent<AudioSource>().volume > 0f)
			{
				prevWeatherAudio.GetComponent<AudioSource>().volume -= Constants.VOLUME_INTERVAL;
			}
			else
			{
				prevWeatherAudio.GetComponent<AudioSource>().volume = 0f;
			}
		}
		currWeatherAudio.GetComponent<AudioSource>().volume = bgmVolume;
		if ((prevWeatherAudio == null || prevWeatherAudio.GetComponent<AudioSource>().volume <= 0f) && currWeatherAudio.GetComponent<AudioSource>().volume >= bgmVolume)
		{
			weatherCrossfade = false;
			Object.DestroyImmediate(prevWeatherAudio);
		}
	}

	public void switchBGM(string changeBGM)
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int == 1 && bgm != changeBGM)
		{
			if (changeBGM != "SILENT")
			{
				bgm = changeBGM;
				setBGM();
				playBGMAudio(string.Empty);
			}
			else
			{
				bgm = changeBGM;
				setBGM();
				stopBGMAudio();
			}
		}
	}

	public void changeBGM(string changeBGM)
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int != 1 || !(bgm != changeBGM))
		{
			return;
		}
		if (changeBGM != "SILENT")
		{
			if (crossfade && prevBGM != null)
			{
				prevBGM.GetComponent<AudioSource>().volume = 0f;
			}
			prevBGM = currentBGM;
			crossfade = true;
			bgm = changeBGM;
			setBGM();
			playBGMAudio(string.Empty);
		}
		else
		{
			if (crossfade && prevBGM != null)
			{
				prevBGM.GetComponent<AudioSource>().volume = 0f;
			}
			crossfade = false;
			bgm = changeBGM;
			fadeoutBGM();
		}
	}

	public bool checkSilent()
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int == 1)
		{
			if (!fadeout || (currentBGM != null && currentBGM.GetComponent<AudioSource>().volume <= 0f && prevBGM != null && prevBGM.GetComponent<AudioSource>().volume <= 0f))
			{
				return true;
			}
			return false;
		}
		return true;
	}

	public string getBGM()
	{
		return bgm;
	}

	public void setBGM()
	{
		CommonAPI.debug("SET BGM " + bgm);
		switch (bgm)
		{
		case "season_spring":
			springBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_SpringLoop");
			currentBGM = springBGM;
			break;
		case "season_summer":
			summerBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_SummerLoop");
			currentBGM = summerBGM;
			break;
		case "season_autumn":
			autumnBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_AutumnLoop");
			currentBGM = autumnBGM;
			break;
		case "season_winter":
			winterBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_WinterLoop");
			currentBGM = winterBGM;
			break;
		case "startmenu":
			startMenuBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_StartMenuLoop");
			currentBGM = startMenuBGM;
			break;
		case "awards":
			awardsBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_AwardsLoop");
			currentBGM = awardsBGM;
			break;
		case "credits":
			creditsBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CreditsLoop");
			currentBGM = creditsBGM;
			break;
		case "worldmap":
			worldMapBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_WorldMapLoop");
			currentBGM = worldMapBGM;
			break;
		case "cutscene_confrontation":
			cutsceneConfrontationBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneConfrontationLoop");
			currentBGM = cutsceneConfrontationBGM;
			break;
		case "cutscene_final":
			cutsceneFinalBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneFinalLoop");
			currentBGM = cutsceneFinalBGM;
			break;
		case "cutscene_happy":
			cutsceneHappyBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneHappyLoop");
			currentBGM = cutsceneHappyBGM;
			break;
		case "cutscene_legendary":
			cutsceneLegendaryBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneLegendaryLoop");
			currentBGM = cutsceneLegendaryBGM;
			break;
		case "cutscene_normal":
			cutsceneNormalBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneNormalLoop");
			currentBGM = cutsceneNormalBGM;
			break;
		case "cutscene_nostalgic":
			cutsceneNostalgicBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneNostalgicLoop");
			currentBGM = cutsceneNostalgicBGM;
			break;
		case "cutscene_stupid":
			cutsceneStupidBGM = commonScreenObject.createAudioPrefabs("BGMPrefab/audio_bgm_CutsceneStupidLoop");
			currentBGM = cutsceneStupidBGM;
			break;
		}
		destroyAllBGM();
	}

	public void playBGMAudio(string bgmToPlay = "")
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		if (@int == 1)
		{
			if ((currentBGM == null || bgm != bgmToPlay) && bgmToPlay != string.Empty)
			{
				bgm = bgmToPlay;
				setBGM();
			}
			if (currentBGM != null)
			{
				if (crossfade)
				{
					currentBGM.GetComponent<AudioSource>().volume = 0f;
				}
				else
				{
					currentBGM.GetComponent<AudioSource>().volume = bgmVolume;
				}
				currentBGM.GetComponent<AudioSource>().Play();
				fadeout = false;
			}
		}
		else
		{
			stopBGMAudio();
		}
	}

	public void stopBGMAudio()
	{
		if (currentBGM != null)
		{
			currentBGM.GetComponent<AudioSource>().Stop();
		}
	}

	public void fadeinBGM()
	{
		fadein = true;
	}

	public void fadeoutBGM()
	{
		fadeout = true;
	}

	public void stopAllLoopSE()
	{
		stopForgeBGLoopAudio();
		stopAuctionAudienceLoopAudio();
		stopResultBarLoopAudio();
		stopBurnLoopAudio();
		stopWaitingLoopAudio();
		stopDrumRollLoopAudio();
		stopQuestBarLoopAudio();
		stopAuctionScrambleLoopAudio();
		stopWeatherAudio();
	}

	public void playButtonAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			buttonAudio = commonScreenObject.createAudioPrefabs("audio_click");
			buttonAudio.GetComponent<AudioSource>().volume = sfxVolume;
			buttonAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playMenuOpenAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			menuOpenAudio = commonScreenObject.createAudioPrefabs("audio_menuOpen");
			menuOpenAudio.GetComponent<AudioSource>().volume = sfxVolume;
			menuOpenAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playPurchaseAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			purchaseAudio = commonScreenObject.createAudioPrefabs("audio_purchase");
			purchaseAudio.GetComponent<AudioSource>().volume = sfxVolume;
			purchaseAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playGoldGainAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			goldGainAudio = commonScreenObject.createAudioPrefabs("audio_goldGain");
			goldGainAudio.GetComponent<AudioSource>().volume = sfxVolume;
			goldGainAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playFameGainAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			fameGainAudio = commonScreenObject.createAudioPrefabs("audio_fameGain");
			fameGainAudio.GetComponent<AudioSource>().volume = sfxVolume;
			fameGainAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playForgingAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgingAudio = commonScreenObject.createAudioPrefabs("audio_forging");
			forgingAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgingAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playDialogueAudio(string soundName)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			dialogueAudio = commonScreenObject.createAudioPrefabs("audio_dialogue");
			AudioClip audioClip = Resources.Load("Sound/SFX/OGG/" + soundName) as AudioClip;
			if (audioClip != null)
			{
				dialogueAudio.GetComponent<AudioSource>().clip = audioClip;
				dialogueAudio.GetComponent<AudioSource>().volume = sfxVolume;
				dialogueAudio.GetComponent<AudioSource>().Play();
			}
			destroyAllEffect();
		}
	}

	public void playSlideEnterAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			slideEnterAudio = commonScreenObject.createAudioPrefabs("audio_slideEnter");
			slideEnterAudio.GetComponent<AudioSource>().volume = sfxVolume;
			slideEnterAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSlideExitAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			slideExitAudio = commonScreenObject.createAudioPrefabs("audio_slideExit");
			slideExitAudio.GetComponent<AudioSource>().volume = sfxVolume;
			slideExitAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playPopupAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			popupAudio = commonScreenObject.createAudioPrefabs("audio_popup");
			popupAudio.GetComponent<AudioSource>().volume = sfxVolume;
			popupAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playNumberUpAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			numberUpAudio = commonScreenObject.createAudioPrefabs("audio_numberUp");
			numberUpAudio.GetComponent<AudioSource>().volume = sfxVolume;
			numberUpAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopNumberUpAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && numberUpAudio != null && numberUpAudio.GetComponent<AudioSource>().isPlaying)
		{
			numberUpAudio.GetComponent<AudioSource>().Stop();
			destroyEffectImmediately(numberUpAudio);
		}
	}

	public void playTextTypeAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			textTypeAudio = commonScreenObject.createAudioPrefabs("audio_textType");
			textTypeAudio.GetComponent<AudioSource>().volume = sfxVolume;
			textTypeAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopTextTypeAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && textTypeAudio != null && textTypeAudio.GetComponent<AudioSource>().isPlaying)
		{
			textTypeAudio.GetComponent<AudioSource>().Stop();
			destroyEffectImmediately(textTypeAudio);
		}
	}

	public void playForgeGrowthAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeGrowthAudio = commonScreenObject.createAudioPrefabs("audio_forgeGrowth");
			forgeGrowthAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeGrowthAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playForgeStartAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeStartAudio = commonScreenObject.createAudioPrefabs("audio_forgeStart");
			forgeStartAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeStartAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playForgeCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeCompleteAudio = commonScreenObject.createAudioPrefabs("audio_forgeComplete");
			forgeCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playResearchCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeCompleteAudio = commonScreenObject.createAudioPrefabs("audio_forgeComplete");
			forgeCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void startForgeBGLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeBGLoop = commonScreenObject.createAudioPrefabs("audio_forgeBGLoop");
			if (!forgeBGLoop.GetComponent<AudioSource>().isPlaying)
			{
				forgeBGLoop.GetComponent<AudioSource>().volume = sfxVolume;
				forgeBGLoop.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopForgeBGLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && forgeBGLoop != null && forgeBGLoop.GetComponent<AudioSource>().isPlaying)
		{
			forgeBGLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playForgeBoostIncreaseLowerAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeBoostIncreaseLowerAudio = commonScreenObject.createAudioPrefabs("audio_forgeBoostIncreaseLower");
			forgeBoostIncreaseLowerAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeBoostIncreaseLowerAudio.GetComponent<AudioSource>().Play();
		}
		destroyAllEffect();
	}

	public void playForgeBoostIncreaseAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeBoostIncreaseAudio = commonScreenObject.createAudioPrefabs("audio_forgeBoostIncrease");
			forgeBoostIncreaseAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeBoostIncreaseAudio.GetComponent<AudioSource>().Play();
		}
		destroyAllEffect();
	}

	public void playForgeBoostEnchantAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			forgeBoostEnchantAudio = commonScreenObject.createAudioPrefabs("audio_forgeBoostEnchant");
			forgeBoostEnchantAudio.GetComponent<AudioSource>().volume = sfxVolume;
			forgeBoostEnchantAudio.GetComponent<AudioSource>().Play();
		}
		destroyAllEffect();
	}

	public void playAuctionBidAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			auctionBidAudio = commonScreenObject.createAudioPrefabs("audio_auctionBid");
			auctionBidAudio.GetComponent<AudioSource>().volume = sfxVolume;
			auctionBidAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playAuctionSoldAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			auctionSoldAudio = commonScreenObject.createAudioPrefabs("audio_auctionSold");
			auctionSoldAudio.GetComponent<AudioSource>().volume = sfxVolume;
			auctionSoldAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void startAuctionAudienceLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			auctionAudienceLoop = commonScreenObject.createAudioPrefabs("audio_auctionAudienceLoop");
			if (!auctionAudienceLoop.GetComponent<AudioSource>().isPlaying)
			{
				auctionAudienceLoop.GetComponent<AudioSource>().volume = sfxVolume;
				auctionAudienceLoop.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopAuctionAudienceLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && auctionAudienceLoop != null && auctionAudienceLoop.GetComponent<AudioSource>().isPlaying)
		{
			auctionAudienceLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void startAuctionScrambleLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			auctionScrambleLoop = commonScreenObject.createAudioPrefabs("audio_auctionScrambleLoop");
			if (!auctionScrambleLoop.GetComponent<AudioSource>().isPlaying)
			{
				auctionScrambleLoop.GetComponent<AudioSource>().volume = sfxVolume;
				auctionScrambleLoop.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopAuctionScrambleLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && auctionScrambleLoop != null && auctionScrambleLoop.GetComponent<AudioSource>().isPlaying)
		{
			auctionScrambleLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playSubquestCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			subquestCompleteAudio = commonScreenObject.createAudioPrefabs("audio_subquestComplete");
			subquestCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			subquestCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void startQuestBarLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			questBarLoop = commonScreenObject.createAudioPrefabs("audio_questBarLoop");
			if (!questBarLoop.GetComponent<AudioSource>().isPlaying)
			{
				questBarLoop.GetComponent<AudioSource>().volume = sfxVolume;
				questBarLoop.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopQuestBarLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && questBarLoop != null && questBarLoop.GetComponent<AudioSource>().isPlaying)
		{
			questBarLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playAwardsChanceUpAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsChanceUpAudio = commonScreenObject.createAudioPrefabs("audio_awardsChanceUp");
			awardsChanceUpAudio.GetComponent<AudioSource>().volume = sfxVolume;
			awardsChanceUpAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void startDrumRollLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsDrumRollLoop = commonScreenObject.createAudioPrefabs("audio_awardsDrumRoll");
			if (!awardsDrumRollLoop.GetComponent<AudioSource>().isPlaying)
			{
				awardsDrumRollLoop.GetComponent<AudioSource>().volume = sfxVolume;
				awardsDrumRollLoop.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopDrumRollLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && awardsDrumRollLoop != null && awardsDrumRollLoop.GetComponent<AudioSource>().isPlaying)
		{
			awardsDrumRollLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playAwardsPersuasionCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsPersuasionCompleteAudio = commonScreenObject.createAudioPrefabs("audio_awardsPersuasionComplete");
			awardsPersuasionCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			awardsPersuasionCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playAwardsRiskAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsRiskAudio = commonScreenObject.createAudioPrefabs("audio_awardsRisk");
			awardsRiskAudio.GetComponent<AudioSource>().volume = sfxVolume;
			awardsRiskAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playAwardsCongratsAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsCongratsAudio = commonScreenObject.createAudioPrefabs("audio_awardsCongrats");
			awardsCongratsAudio.GetComponent<AudioSource>().volume = sfxVolume;
			awardsCongratsAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playAwardsDisqualifiedAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsDisqualifiedAudio = commonScreenObject.createAudioPrefabs("audio_awardsDisqualified");
			awardsDisqualifiedAudio.GetComponent<AudioSource>().volume = sfxVolume;
			awardsDisqualifiedAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void startWaitingLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			awardsWaitingLoop = commonScreenObject.createAudioPrefabs("audio_awardsWaiting");
			if (!awardsWaitingLoop.GetComponent<AudioSource>().isPlaying)
			{
				awardsWaitingLoop.GetComponent<AudioSource>().volume = sfxVolume;
				awardsWaitingLoop.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopWaitingLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && awardsWaitingLoop != null && awardsWaitingLoop.GetComponent<AudioSource>().isPlaying)
		{
			awardsWaitingLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playShopPurchaseAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			shopPurchaseAudio = commonScreenObject.createAudioPrefabs("audio_shopPurchase");
			shopPurchaseAudio.GetComponent<AudioSource>().volume = sfxVolume;
			shopPurchaseAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithChiAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithChiAudio = commonScreenObject.createAudioPrefabs("audio_shopSmithChi");
			smithChiAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithChiAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithCoffeeAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithCoffeeAudio = commonScreenObject.createAudioPrefabs("audio_shopSmithCoffee");
			smithCoffeeAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithCoffeeAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithEnterAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithEnterAudio = commonScreenObject.createAudioPrefabs("audio_shopSmithEnter");
			smithEnterAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithEnterAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithExitAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithExitAudio = commonScreenObject.createAudioPrefabs("audio_shopSmithExit");
			smithExitAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithExitAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithActionAlertAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithActionAlertAudio = commonScreenObject.createAudioPrefabs("audio_smithActionAlert");
			smithActionAlertAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithActionAlertAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playLegendAppearAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			legendAppearAudio = commonScreenObject.createAudioPrefabs("audio_legendAppear");
			legendAppearAudio.GetComponent<AudioSource>().volume = sfxVolume;
			legendAppearAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playLegendRequestAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			legendRequestAudio = commonScreenObject.createAudioPrefabs("audio_legendRequest");
			legendRequestAudio.GetComponent<AudioSource>().volume = sfxVolume;
			legendRequestAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playEventAppearAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			eventAppearAudio = commonScreenObject.createAudioPrefabs("audio_eventAppear");
			eventAppearAudio.GetComponent<AudioSource>().volume = sfxVolume;
			eventAppearAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playEventFailAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			eventFailAudio = commonScreenObject.createAudioPrefabs("audio_eventFail");
			eventFailAudio.GetComponent<AudioSource>().volume = sfxVolume;
			eventFailAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playEventSuccessAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			eventSuccessAudio = commonScreenObject.createAudioPrefabs("audio_eventSuccess");
			eventSuccessAudio.GetComponent<AudioSource>().volume = sfxVolume;
			eventSuccessAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithMoodSfx(SmithMood aMood)
	{
		switch (aMood)
		{
		case SmithMood.SmithMoodVerySad:
			playSmithDepressedAudio();
			break;
		case SmithMood.SmithMoodSad:
			playSmithSadAudio();
			break;
		case SmithMood.SmithMoodNormal:
			playSmithNeutralAudio();
			break;
		case SmithMood.SmithMoodHappy:
			playSmithHappyAudio();
			break;
		case SmithMood.SmithMoodVeryHappy:
			playSmithElatedAudio();
			break;
		}
	}

	public void playSmithDepressedAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithDepressedAudio = commonScreenObject.createAudioPrefabs("audio_smithDepressed");
			smithDepressedAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Depressed_0" + Random.Range(1, 4)) as AudioClip;
			smithDepressedAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithDepressedAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithSadAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithSadAudio = commonScreenObject.createAudioPrefabs("audio_smithSad");
			smithSadAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Sad_0" + Random.Range(1, 4)) as AudioClip;
			smithSadAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithSadAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithNeutralAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithNeutralAudio = commonScreenObject.createAudioPrefabs("audio_smithNeutral");
			smithNeutralAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Neutral_0" + Random.Range(1, 4)) as AudioClip;
			smithNeutralAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithNeutralAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithHappyAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithHappyAudio = commonScreenObject.createAudioPrefabs("audio_smithHappy");
			smithHappyAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Happy_0" + Random.Range(1, 4)) as AudioClip;
			smithHappyAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithHappyAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithElatedAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithElatedAudio = commonScreenObject.createAudioPrefabs("audio_smithElated");
			smithElatedAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Elated_0" + Random.Range(1, 4)) as AudioClip;
			smithElatedAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithElatedAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playWeatherAudio(string weatherRefID)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			string text = string.Empty;
			switch (weatherRefID)
			{
			case "101":
				text = "HP_Weather_Breeze_01";
				break;
			case "102":
				text = "HP_Weather_Breeze_02";
				break;
			case "201":
				text = "HP_Weather_Heat_01";
				break;
			case "202":
				text = "HP_Weather_Heat_02";
				break;
			case "203":
				text = "HP_Weather_Heat_03";
				break;
			case "302":
				text = "HP_Weather_Breeze_03";
				break;
			case "303":
				text = "HP_Weather_Rain";
				break;
			case "401":
				text = "HP_Weather_Snow_01";
				break;
			case "402":
				text = "HP_Weather_Snow_02";
				break;
			case "403":
				text = "HP_Weather_Snow_03";
				break;
			}
			if (currWeatherAudio != null && text != string.Empty)
			{
				prevWeatherAudio = currWeatherAudio;
				prevWeatherAudio.name = "prev_audio_weather";
				currWeatherAudio = commonScreenObject.createAudioPrefabs("audio_weather");
				currWeatherAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/" + text) as AudioClip;
				currWeatherAudio.GetComponent<AudioSource>().volume = sfxVolume;
				currWeatherAudio.GetComponent<AudioSource>().Play();
				weatherCrossfade = true;
			}
			if (currWeatherAudio == null && text != string.Empty)
			{
				currWeatherAudio = commonScreenObject.createAudioPrefabs("audio_weather");
				currWeatherAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/" + text) as AudioClip;
				currWeatherAudio.GetComponent<AudioSource>().volume = sfxVolume;
				currWeatherAudio.GetComponent<AudioSource>().Play();
				destroyAllEffect();
			}
		}
	}

	public void stopWeatherAudio()
	{
		if (currWeatherAudio != null)
		{
			currWeatherAudio.GetComponent<AudioSource>().Stop();
		}
		if (prevWeatherAudio != null)
		{
			prevWeatherAudio.GetComponent<AudioSource>().Stop();
		}
		weatherCrossfade = false;
		destroyAllEffect();
	}

	public void playBeamInAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			beamInAudio = commonScreenObject.createAudioPrefabs("audio_beamIn");
			beamInAudio.GetComponent<AudioSource>().volume = sfxVolume;
			beamInAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBeamOutAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			beamOutAudio = commonScreenObject.createAudioPrefabs("audio_beamOut");
			beamOutAudio.GetComponent<AudioSource>().volume = sfxVolume;
			beamOutAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playCraftStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			craftStationAudio = commonScreenObject.createAudioPrefabs("audio_craftStation");
			switch (level)
			{
			case 1:
				craftStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Crafting_01") as AudioClip;
				break;
			case 2:
				craftStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Crafting_02") as AudioClip;
				break;
			case 3:
				craftStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Crafting_03") as AudioClip;
				break;
			case 4:
				craftStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Crafting_04") as AudioClip;
				break;
			}
			craftStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			craftStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopCraftStationAudio(bool destroy)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && craftStationAudio != null && craftStationAudio.GetComponent<AudioSource>().isPlaying)
		{
			craftStationAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(craftStationAudio);
			}
		}
	}

	public void resumeCraftStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && craftStationAudio != null)
		{
			craftStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			craftStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playDesignStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			designStationAudio = commonScreenObject.createAudioPrefabs("audio_designStation");
			switch (level)
			{
			case 1:
				designStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Paper_01") as AudioClip;
				break;
			case 2:
				designStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Paper_02") as AudioClip;
				break;
			case 3:
				designStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Paper_03") as AudioClip;
				break;
			case 4:
				designStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Paper_04") as AudioClip;
				break;
			}
			designStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			designStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopDesignStationAudio(bool destroy)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && designStationAudio != null && designStationAudio.GetComponent<AudioSource>().isPlaying)
		{
			designStationAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(designStationAudio);
			}
		}
	}

	public void resumeDesignStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && designStationAudio != null)
		{
			designStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			designStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playPolishStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			polishStationAudio = commonScreenObject.createAudioPrefabs("audio_polishStation");
			switch (level)
			{
			case 1:
				polishStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Grinder") as AudioClip;
				break;
			case 2:
				polishStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Grinder_02") as AudioClip;
				break;
			case 3:
				polishStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Grinder_03") as AudioClip;
				break;
			case 4:
				polishStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Grinder_04") as AudioClip;
				break;
			}
			polishStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			polishStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopPolishStationAudio(bool destroy)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && polishStationAudio != null && polishStationAudio.GetComponent<AudioSource>().isPlaying)
		{
			polishStationAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(polishStationAudio);
			}
		}
	}

	public void resumePolishStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && polishStationAudio != null)
		{
			polishStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			polishStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playEnchantStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			enchantStationAudio = commonScreenObject.createAudioPrefabs("audio_enchantStation");
			switch (level)
			{
			case 1:
				enchantStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Furnace") as AudioClip;
				break;
			case 2:
				enchantStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Furnace_02") as AudioClip;
				break;
			case 3:
				enchantStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Furnace_03") as AudioClip;
				break;
			case 4:
				enchantStationAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_BG_Furnace_04") as AudioClip;
				break;
			}
			enchantStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			enchantStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopEnchantStationAudio(bool destroy)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && enchantStationAudio != null && enchantStationAudio.GetComponent<AudioSource>().isPlaying)
		{
			enchantStationAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(enchantStationAudio);
			}
		}
	}

	public void resumeEnchantStationAudio(int level)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && enchantStationAudio != null)
		{
			enchantStationAudio.GetComponent<AudioSource>().volume = sfxVolume;
			enchantStationAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playFireplaceAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			fireplaceAudio = commonScreenObject.createAudioPrefabs("audio_fireplace");
			fireplaceAudio.GetComponent<AudioSource>().volume = sfxVolume;
			fireplaceAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopFireplaceAudio(bool destroy)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && fireplaceAudio != null && fireplaceAudio.GetComponent<AudioSource>().isPlaying)
		{
			fireplaceAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(fireplaceAudio);
			}
		}
	}

	public void resumeFireplaceAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			if (fireplaceAudio != null)
			{
				fireplaceAudio.GetComponent<AudioSource>().volume = sfxVolume;
				fireplaceAudio.GetComponent<AudioSource>().Play();
				destroyAllEffect();
			}
			else
			{
				playFireplaceAudio();
			}
		}
	}

	public void playAirconAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			airconAudio = commonScreenObject.createAudioPrefabs("audio_aircon");
			airconAudio.GetComponent<AudioSource>().volume = sfxVolume;
			airconAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopAirconAudio(bool destroy)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && airconAudio != null && airconAudio.GetComponent<AudioSource>().isPlaying)
		{
			airconAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(airconAudio);
			}
		}
	}

	public void resumeAirconAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			if (airconAudio != null)
			{
				airconAudio.GetComponent<AudioSource>().volume = sfxVolume;
				airconAudio.GetComponent<AudioSource>().Play();
				destroyAllEffect();
			}
			else
			{
				playAirconAudio();
			}
		}
	}

	public void playPortalAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			portalAudio = commonScreenObject.createAudioPrefabs("audio_portal");
			portalAudio.GetComponent<AudioSource>().volume = sfxVolume;
			portalAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithDialogueAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithDialogueAudio = commonScreenObject.createAudioPrefabs("audio_smithDialogue");
			smithDialogueAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Dialogue_0" + Random.Range(1, 9)) as AudioClip;
			smithDialogueAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithDialogueAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithThoughtBubbleAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			smithThoughtBubbleAudio = commonScreenObject.createAudioPrefabs("audio_smithThoughtBubble");
			smithThoughtBubbleAudio.GetComponent<AudioSource>().volume = sfxVolume;
			smithThoughtBubbleAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSmithFrozenAudio(string aRefID)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			if (smithFrozenAudioList.ContainsKey(aRefID))
			{
				smithFrozenAudioList[aRefID].GetComponent<AudioSource>().Play();
				return;
			}
			GameObject gameObject = commonScreenObject.createAudioPrefabs("audio_smithFrozen");
			gameObject.name = "audio_smithFrozen" + aRefID;
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
			smithFrozenAudioList.Add(aRefID, gameObject);
			destroyAllEffect();
		}
	}

	public void stopSmithFrozenAudio(string aRefID, bool destroy = true)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && smithFrozenAudioList != null && smithFrozenAudioList.ContainsKey(aRefID))
		{
			smithFrozenAudioList[aRefID].GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(smithFrozenAudioList[aRefID]);
				smithFrozenAudioList.Remove(aRefID);
			}
		}
	}

	public void playSmithSickAudio(string aRefID)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			if (smithSickAudioList.ContainsKey(aRefID))
			{
				smithSickAudioList[aRefID].GetComponent<AudioSource>().Play();
				return;
			}
			GameObject gameObject = commonScreenObject.createAudioPrefabs("audio_smithSick");
			gameObject.name = "audio_smithSick" + aRefID;
			gameObject.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Sick_0" + Random.Range(1, 4)) as AudioClip;
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
			smithSickAudioList.Add(aRefID, gameObject);
			destroyAllEffect();
		}
	}

	public void stopSmithSickAudio(string aRefID, bool destroy = true)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && smithSickAudioList != null && smithSickAudioList.ContainsKey(aRefID))
		{
			smithSickAudioList[aRefID].GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(smithSickAudioList[aRefID]);
				smithSickAudioList.Remove(aRefID);
			}
		}
	}

	public void playSmithFootstepAudio(string aRefID, int speed)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && !smithFootStepAudioList.ContainsKey(aRefID))
		{
			GameObject gameObject = commonScreenObject.createAudioPrefabs("audio_smithFootstep");
			gameObject.name = "audio_smithFootstep" + aRefID;
			gameObject.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Smith_Footstep_0" + speed) as AudioClip;
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
			smithFootStepAudioList.Add(aRefID, gameObject);
			destroyAllEffect();
		}
	}

	public void stopSmithFootstepAudio(string aRefID)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && smithFootStepAudioList != null && smithFootStepAudioList.ContainsKey(aRefID))
		{
			smithFootStepAudioList[aRefID].GetComponent<AudioSource>().Stop();
			destroyEffectImmediately(smithFootStepAudioList[aRefID]);
			smithFootStepAudioList.Remove(aRefID);
		}
	}

	public void pauseSmithFootstepAudio(string aRefID)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && smithFootStepAudioList != null && smithFootStepAudioList.ContainsKey(aRefID))
		{
			smithFootStepAudioList[aRefID].GetComponent<AudioSource>().Stop();
		}
	}

	public void resumeSmithFootstepAudio(string aRefID)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && smithFootStepAudioList != null && smithFootStepAudioList.ContainsKey(aRefID))
		{
			smithFootStepAudioList[aRefID].GetComponent<AudioSource>().Play();
		}
	}

	public void playDogBarkAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			dogBarkAudio = commonScreenObject.createAudioPrefabs("audio_dogBark");
			dogBarkAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Dog_Bark_0" + Random.Range(1, 11)) as AudioClip;
			dogBarkAudio.GetComponent<AudioSource>().volume = sfxVolume;
			dogBarkAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopDogBarkAudio(bool destroy = true)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && dogBarkAudio != null && dogBarkAudio.GetComponent<AudioSource>().isPlaying)
		{
			dogBarkAudio.GetComponent<AudioSource>().Stop();
			if (destroy)
			{
				destroyEffectImmediately(dogBarkAudio);
			}
		}
	}

	public void playWhetsappAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			whetsappAudio = commonScreenObject.createAudioPrefabs("audio_whetsapp");
			whetsappAudio.GetComponent<AudioSource>().volume = sfxVolume;
			whetsappAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playObjectiveCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			objectiveCompleteAudio = commonScreenObject.createAudioPrefabs("audio_objectiveComplete");
			objectiveCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			objectiveCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playItemGetAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			itemGetAudio = commonScreenObject.createAudioPrefabs("audio_itemGet");
			itemGetAudio.GetComponent<AudioSource>().volume = sfxVolume;
			itemGetAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playHeroHoverAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			heroHoverAudio = commonScreenObject.createAudioPrefabs("audio_heroHover");
			heroHoverAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Hero_Over_0" + Random.Range(1, 4)) as AudioClip;
			heroHoverAudio.GetComponent<AudioSource>().volume = sfxVolume;
			heroHoverAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playHeroSelectAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			heroSelectAudio = commonScreenObject.createAudioPrefabs("audio_heroSelect");
			heroSelectAudio.GetComponent<AudioSource>().volume = sfxVolume;
			heroSelectAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSellStartAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			sellStartAudio = commonScreenObject.createAudioPrefabs("audio_sellStart");
			sellStartAudio.GetComponent<AudioSource>().volume = sfxVolume;
			sellStartAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSellRatingAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			sellRatingAudio = commonScreenObject.createAudioPrefabs("audio_sellRating");
			sellRatingAudio.GetComponent<AudioSource>().volume = sfxVolume;
			sellRatingAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSellDialogueAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			sellDialogueAudio = commonScreenObject.createAudioPrefabs("audio_sellDialogue");
			sellDialogueAudio.GetComponent<AudioSource>().volume = sfxVolume;
			sellDialogueAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSellHeroesSlideAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			sellHeroesSlideAudio = commonScreenObject.createAudioPrefabs("audio_sellHeroesSlide");
			sellHeroesSlideAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Sells_result_hero_box_0" + Random.Range(1, 4)) as AudioClip;
			sellHeroesSlideAudio.GetComponent<AudioSource>().volume = sfxVolume;
			sellHeroesSlideAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSellCoinAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			sellCoinAudio = commonScreenObject.createAudioPrefabs("audio_sellCoin");
			sellCoinAudio.GetComponent<AudioSource>().clip = Resources.Load("Sound/SFX/OGG/HP_Sells_result_hero_box_0" + Random.Range(1, 4)) as AudioClip;
			sellCoinAudio.GetComponent<AudioSource>().volume = sfxVolume;
			sellCoinAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void stopSellCoinAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && sellCoinAudio != null && sellCoinAudio.GetComponent<AudioSource>().isPlaying)
		{
			sellCoinAudio.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playMapConfirmAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			mapConfirmAudio = commonScreenObject.createAudioPrefabs("audio_mapConfirm");
			mapConfirmAudio.GetComponent<AudioSource>().volume = sfxVolume;
			mapConfirmAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playMapSelectItemAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			mapSelectItemAudio = commonScreenObject.createAudioPrefabs("audio_mapSelectItem");
			mapSelectItemAudio.GetComponent<AudioSource>().volume = sfxVolume;
			mapSelectItemAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playMapSlideAudio()
	{
		CommonAPI.debug("playMapSlideAudio");
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			mapSlideAudio = commonScreenObject.createAudioPrefabs("audio_mapSlide");
			mapSlideAudio.GetComponent<AudioSource>().volume = sfxVolume;
			mapSlideAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playGemBreakAudio(int comboNum)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int != 1)
		{
			return;
		}
		float num = 1f;
		GameObject gameObject = null;
		if (comboNum > 20)
		{
			switch (gemBreakNum)
			{
			case 1:
				breakBigAudio1 = commonScreenObject.createAudioPrefabs("audio_gemBreakBig1");
				gameObject = breakBigAudio1;
				break;
			case 2:
				breakBigAudio2 = commonScreenObject.createAudioPrefabs("audio_gemBreakBig2");
				gameObject = breakBigAudio2;
				break;
			case 3:
				breakBigAudio3 = commonScreenObject.createAudioPrefabs("audio_gemBreakBig3");
				gameObject = breakBigAudio3;
				break;
			}
			num = 1f;
		}
		else if (comboNum > 10)
		{
			switch (gemBreakNum)
			{
			case 1:
				breakHighAudio1 = commonScreenObject.createAudioPrefabs("audio_gemBreakHigh1");
				gameObject = breakHighAudio1;
				break;
			case 2:
				breakHighAudio2 = commonScreenObject.createAudioPrefabs("audio_gemBreakHigh2");
				gameObject = breakHighAudio2;
				break;
			case 3:
				breakHighAudio3 = commonScreenObject.createAudioPrefabs("audio_gemBreakHigh3");
				gameObject = breakHighAudio3;
				break;
			}
			num = 1f + (float)(comboNum - 11) * 0.1f;
		}
		else
		{
			switch (gemBreakNum)
			{
			case 1:
				breakAudio1 = commonScreenObject.createAudioPrefabs("audio_gemBreak1");
				gameObject = breakAudio1;
				break;
			case 2:
				breakAudio2 = commonScreenObject.createAudioPrefabs("audio_gemBreak2");
				gameObject = breakAudio2;
				break;
			case 3:
				breakAudio3 = commonScreenObject.createAudioPrefabs("audio_gemBreak3");
				gameObject = breakAudio3;
				break;
			}
			num = 1f + (float)(comboNum - 1) * 0.1f;
		}
		if (gameObject != null)
		{
			gameObject.GetComponent<AudioSource>().pitch = num;
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
		}
		gemBreakNum++;
		if (gemBreakNum > 3)
		{
			gemBreakNum = 1;
		}
		destroyAllEffect();
	}

	public void playPowerUpAudio(string soundName)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			powerUpAudio = commonScreenObject.createAudioPrefabs("audio_powerUp");
			if (powerClipList == null)
			{
				powerClipList = new Hashtable();
			}
			AudioClip audioClip = powerClipList[soundName] as AudioClip;
			if (audioClip == null)
			{
				audioClip = Resources.Load("Sounds/SFX/" + soundName) as AudioClip;
				powerClipList.Add(soundName, audioClip);
			}
			powerUpAudio.GetComponent<AudioSource>().clip = audioClip;
			powerUpAudio.GetComponent<AudioSource>().volume = sfxVolume;
			powerUpAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playEnemyHitAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			switch (hitNum)
			{
			case 1:
				enemyHitAudio1 = commonScreenObject.createAudioPrefabsMultiple("audio_enemyHit", "audio_enemyHit1");
				enemyHitAudio1.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.3f, 0.3f);
				enemyHitAudio1.GetComponent<AudioSource>().volume = sfxVolume;
				enemyHitAudio1.GetComponent<AudioSource>().Play();
				break;
			case 2:
				enemyHitAudio2 = commonScreenObject.createAudioPrefabsMultiple("audio_enemyHit", "audio_enemyHit2");
				enemyHitAudio2.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.3f, 0.3f);
				enemyHitAudio2.GetComponent<AudioSource>().volume = sfxVolume;
				enemyHitAudio2.GetComponent<AudioSource>().Play();
				break;
			case 3:
				enemyHitAudio3 = commonScreenObject.createAudioPrefabsMultiple("audio_enemyHit", "audio_enemyHit3");
				enemyHitAudio3.GetComponent<AudioSource>().pitch = 1f + Random.Range(-0.3f, 0.3f);
				enemyHitAudio3.GetComponent<AudioSource>().volume = sfxVolume;
				enemyHitAudio3.GetComponent<AudioSource>().Play();
				break;
			}
			hitNum++;
			if (hitNum > 3)
			{
				hitNum = 1;
			}
			destroyAllEffect();
		}
	}

	public void playTaskCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			taskCompleteAudio = commonScreenObject.createAudioPrefabs("audio_taskComplete");
			taskCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			taskCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBurnLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			burnAudio = commonScreenObject.createAudioPrefabs("audio_burningLoop");
			if (!burnAudio.GetComponent<AudioSource>().isPlaying)
			{
				burnAudio.GetComponent<AudioSource>().volume = sfxVolume;
				burnAudio.GetComponent<AudioSource>().Play();
			}
		}
		destroyAllEffect();
	}

	public void stopBurnLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && burnAudio != null && burnAudio.GetComponent<AudioSource>().isPlaying)
		{
			burnAudio.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playVineCutAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			cutAudio = commonScreenObject.createAudioPrefabs("audio_vineCut");
			cutAudio.GetComponent<AudioSource>().volume = sfxVolume;
			cutAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playWarningAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			warningAudio = commonScreenObject.createAudioPrefabs("audio_warning");
			warningAudio.GetComponent<AudioSource>().volume = sfxVolume;
			warningAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playSpiritSpecialAudio(string soundName)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int != 1)
		{
			return;
		}
		bool flag = false;
		spiritSpecialAudio = commonScreenObject.createAudioPrefabs("audio_spiritSpecial");
		if (spiritSpecialList == null)
		{
			spiritSpecialList = new Hashtable();
		}
		AudioClip audioClip = spiritSpecialList[soundName] as AudioClip;
		if (audioClip == null)
		{
			audioClip = Resources.Load("Sound/" + soundName) as AudioClip;
			if (audioClip != null)
			{
				spiritSpecialList.Add(soundName, audioClip);
				flag = true;
			}
		}
		else
		{
			flag = true;
		}
		if (spiritSpecialAudio != null && flag)
		{
			spiritSpecialAudio.GetComponent<AudioSource>().clip = audioClip;
			spiritSpecialAudio.GetComponent<AudioSource>().volume = sfxVolume;
			spiritSpecialAudio.GetComponent<AudioSource>().Play();
		}
		destroyAllEffect();
	}

	public void playSpiritSpecialActivate()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			spiritActivateAudio = commonScreenObject.createAudioPrefabs("audio_spiritSpecialActivate");
			spiritActivateAudio.GetComponent<AudioSource>().volume = sfxVolume;
			spiritActivateAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBossDisable()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			bossDisableAudio = commonScreenObject.createAudioPrefabs("audio_bossDisable");
			bossDisableAudio.GetComponent<AudioSource>().volume = sfxVolume;
			bossDisableAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBossFire()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			bossFireAudio = commonScreenObject.createAudioPrefabs("audio_bossFire");
			bossFireAudio.GetComponent<AudioSource>().volume = sfxVolume;
			bossFireAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBossIce()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			bossIceAudio = commonScreenObject.createAudioPrefabs("audio_bossIce");
			bossIceAudio.GetComponent<AudioSource>().volume = sfxVolume;
			bossIceAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBossLightning()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			bossLightningAudio = commonScreenObject.createAudioPrefabs("audio_bossLightning");
			bossLightningAudio.GetComponent<AudioSource>().volume = sfxVolume;
			bossLightningAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBossVine()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			bossVineAudio = commonScreenObject.createAudioPrefabs("audio_bossVine");
			bossVineAudio.GetComponent<AudioSource>().volume = sfxVolume;
			bossVineAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playIceBreakAudio(int stage)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			GameObject gameObject;
			if (stage == 0)
			{
				IceBreakBig = commonScreenObject.createAudioPrefabs("audio_iceBreak1");
				gameObject = IceBreakBig;
			}
			else
			{
				IceBreakSmall = commonScreenObject.createAudioPrefabs("audio_iceBreak2");
				gameObject = IceBreakSmall;
			}
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playUnlockBreakAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			UnlockBreak = commonScreenObject.createAudioPrefabs("audio_unlockBreak");
			UnlockBreak.GetComponent<AudioSource>().volume = sfxVolume;
			UnlockBreak.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playQuestClaimAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			questClaimAudio = commonScreenObject.createAudioPrefabs("audio_rewardClaim");
			questClaimAudio.GetComponent<AudioSource>().volume = sfxVolume;
			questClaimAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playAreaUnlockAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			areaUnlockAudio = commonScreenObject.createAudioPrefabs("audio_areaUnlock");
			areaUnlockAudio.GetComponent<AudioSource>().volume = sfxVolume;
			areaUnlockAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playGemFallAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			gemFallAudio = commonScreenObject.createAudioPrefabs("audio_gemFall");
			gemFallAudio.GetComponent<AudioSource>().volume = sfxVolume;
			gemFallAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playDoorOpenAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			doorOpenAudio = commonScreenObject.createAudioPrefabs("audio_doorOpen");
			doorOpenAudio.GetComponent<AudioSource>().volume = sfxVolume;
			doorOpenAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playDoorCloseAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			doorCloseAudio = commonScreenObject.createAudioPrefabs("audio_doorClose");
			doorCloseAudio.GetComponent<AudioSource>().volume = sfxVolume;
			doorCloseAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playResultProgressAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			resultProgressAudio = commonScreenObject.createAudioPrefabs("audio_resultProgress");
			if (!resultProgressAudio.GetComponent<AudioSource>().isPlaying)
			{
				resultProgressAudio.GetComponent<AudioSource>().volume = sfxVolume;
				resultProgressAudio.GetComponent<AudioSource>().Play();
			}
			destroyAllEffect();
		}
	}

	public void stopProgressAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1 && resultProgressAudio != null)
		{
			resultProgressAudio.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void playResultStarAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			resultStarAudio = commonScreenObject.createAudioPrefabs("audio_resultStar");
			resultStarAudio.GetComponent<AudioSource>().volume = sfxVolume;
			resultStarAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playStageClearAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			resultStageClearAudio = commonScreenObject.createAudioPrefabs("audio_resultStageClear");
			resultStarAudio = commonScreenObject.createAudioPrefabs("audio_resultStar");
			resultStageClearAudio.GetComponent<AudioSource>().volume = sfxVolume;
			resultStageClearAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playBattleStageCompleteAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			battleStageCompleteAudio = commonScreenObject.createAudioPrefabs("audio_battleStageComplete");
			battleStageCompleteAudio.GetComponent<AudioSource>().volume = sfxVolume;
			battleStageCompleteAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playIzecFlyAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			izecFlyAudio = commonScreenObject.createAudioPrefabs("audio_izecFly");
			izecFlyAudio.GetComponent<AudioSource>().volume = sfxVolume;
			izecFlyAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playTimerSound(int timeLeft)
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			if (timeLeft > 0 && timeLeft <= 5)
			{
				timerAudio = commonScreenObject.createAudioPrefabs("audio_timeTick");
				timerAudio.GetComponent<AudioSource>().volume = sfxVolume;
				timerAudio.GetComponent<AudioSource>().Play();
			}
			else if (timeLeft == 0)
			{
				timerBellAudio = commonScreenObject.createAudioPrefabs("audio_timeBell");
				timerBellAudio.GetComponent<AudioSource>().volume = sfxVolume;
				timerBellAudio.GetComponent<AudioSource>().Play();
			}
			destroyAllEffect();
		}
	}

	public void playPanelSwitchAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			panelSwitchAudio = commonScreenObject.createAudioPrefabs("audio_panelSwitch");
			panelSwitchAudio.GetComponent<AudioSource>().volume = sfxVolume;
			panelSwitchAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playCrystalSound()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			crystalAudio = commonScreenObject.createAudioPrefabs("audio_crystalAppear");
			crystalAudio.GetComponent<AudioSource>().volume = sfxVolume;
			crystalAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public void playResultWindowAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			resultAudio = commonScreenObject.createAudioPrefabs("audio_resultEntrance");
			resultAudio.GetComponent<AudioSource>().volume = sfxVolume;
			resultAudio.GetComponent<AudioSource>().Play();
			destroyAllEffect();
		}
	}

	public GameObject playResultVictoryAudio()
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		GameObject gameObject = null;
		if (@int == 1)
		{
			gameObject = commonScreenObject.createAudioPrefabs("audio_resultVictory");
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
			gameObject.GetComponent<AudioSource>().loop = true;
			destroyAllEffect();
		}
		return gameObject;
	}

	public void stopResultVictoryAudio()
	{
		GameObject gameObject = GameObject.Find("audio_resultVictory");
		if (gameObject != null)
		{
			gameObject.GetComponent<AudioSource>().Stop();
		}
	}

	public GameObject playResultDefeatAudio()
	{
		int @int = PlayerPrefs.GetInt("musicOnOff", 1);
		GameObject gameObject = null;
		if (@int == 1)
		{
			gameObject = commonScreenObject.createAudioPrefabs("audio_resultDefeat");
			gameObject.GetComponent<AudioSource>().volume = sfxVolume;
			gameObject.GetComponent<AudioSource>().Play();
			gameObject.GetComponent<AudioSource>().loop = true;
			destroyAllEffect();
		}
		return gameObject;
	}

	public void stopResultDefeatAudio()
	{
		GameObject gameObject = GameObject.Find("audio_resultDefeat");
		if (gameObject != null)
		{
			gameObject.GetComponent<AudioSource>().Stop();
		}
	}

	public void stopResultBarLoopAudio()
	{
		int @int = PlayerPrefs.GetInt("soundOnOff", 1);
		if (@int == 1)
		{
			barLoop = commonScreenObject.createAudioPrefabs("audio_resultBarLoop");
			barLoop.GetComponent<AudioSource>().Stop();
			destroyAllEffect();
		}
	}

	public void destroyAllBGM()
	{
		destroyBGMImmediately(springBGM);
		destroyBGMImmediately(summerBGM);
		destroyBGMImmediately(autumnBGM);
		destroyBGMImmediately(winterBGM);
		destroyBGMImmediately(startMenuBGM);
		destroyBGMImmediately(awardsBGM);
		destroyBGMImmediately(creditsBGM);
		destroyBGMImmediately(worldMapBGM);
		destroyBGMImmediately(cutsceneConfrontationBGM);
		destroyBGMImmediately(cutsceneFinalBGM);
		destroyBGMImmediately(cutsceneHappyBGM);
		destroyBGMImmediately(cutsceneLegendaryBGM);
		destroyBGMImmediately(cutsceneNormalBGM);
		destroyBGMImmediately(cutsceneNostalgicBGM);
		destroyBGMImmediately(cutsceneStupidBGM);
	}

	public void destroyBGM(GameObject aObject)
	{
		if (aObject != null && currentBGM != aObject && prevBGM != aObject)
		{
			Object.Destroy(aObject);
		}
	}

	public void destroyBGMImmediately(GameObject aObject)
	{
		if (aObject != null && currentBGM != aObject && prevBGM != aObject)
		{
			Object.DestroyImmediate(aObject);
		}
	}

	public void destroyAllEffect()
	{
		destroyEffectImmediately(buttonAudio);
		destroyEffectImmediately(menuOpenAudio);
		destroyEffectImmediately(purchaseAudio);
		destroyEffectImmediately(goldGainAudio);
		destroyEffectImmediately(fameGainAudio);
		destroyEffectImmediately(forgingAudio);
		destroyEffectImmediately(forgeGrowthAudio);
		destroyEffectImmediately(forgeStartAudio);
		destroyEffectImmediately(forgeCompleteAudio);
		destroyEffectImmediately(forgeBGLoop);
		destroyEffectImmediately(forgeBoostIncreaseLowerAudio);
		destroyEffectImmediately(forgeBoostIncreaseAudio);
		destroyEffectImmediately(forgeBoostEnchantAudio);
		destroyEffectImmediately(auctionBidAudio);
		destroyEffectImmediately(auctionSoldAudio);
		destroyEffectImmediately(auctionAudienceLoop);
		destroyEffectImmediately(auctionScrambleLoop);
		destroyEffectImmediately(subquestCompleteAudio);
		destroyEffectImmediately(questBarLoop);
		destroyEffectImmediately(shopPurchaseAudio);
		destroyEffectImmediately(smithChiAudio);
		destroyEffectImmediately(smithCoffeeAudio);
		destroyEffectImmediately(smithEnterAudio);
		destroyEffectImmediately(smithExitAudio);
		destroyEffectImmediately(smithActionAlertAudio);
		destroyEffectImmediately(legendAppearAudio);
		destroyEffectImmediately(legendRequestAudio);
		destroyEffectImmediately(eventAppearAudio);
		destroyEffectImmediately(eventFailAudio);
		destroyEffectImmediately(eventSuccessAudio);
		destroyEffectImmediately(smithDepressedAudio);
		destroyEffectImmediately(smithSadAudio);
		destroyEffectImmediately(smithNeutralAudio);
		destroyEffectImmediately(smithHappyAudio);
		destroyEffectImmediately(smithElatedAudio);
		destroyEffectImmediately(beamInAudio);
		destroyEffectImmediately(beamOutAudio);
		destroyEffectImmediately(portalAudio);
		destroyEffectImmediately(smithDialogueAudio);
		destroyEffectImmediately(smithThoughtBubbleAudio);
		destroyEffectImmediately(dialogueAudio);
		destroyEffectImmediately(slideEnterAudio);
		destroyEffectImmediately(slideExitAudio);
		destroyEffectImmediately(popupAudio);
		destroyEffectImmediately(heroHoverAudio);
		destroyEffectImmediately(heroSelectAudio);
		destroyEffectImmediately(sellStartAudio);
		destroyEffectImmediately(sellRatingAudio);
		destroyEffectImmediately(sellDialogueAudio);
		destroyEffectImmediately(sellHeroesSlideAudio);
		destroyEffectImmediately(sellCoinAudio);
		destroyEffectImmediately(mapConfirmAudio);
		destroyEffectImmediately(mapSelectItemAudio);
		destroyEffectImmediately(mapSlideAudio);
		destroyEffectImmediately(awardsRiskAudio);
		destroyEffectImmediately(awardsPersuasionCompleteAudio);
		destroyEffectImmediately(awardsDrumRollLoop);
		destroyEffectImmediately(awardsChanceUpAudio);
		destroyEffectImmediately(awardsWaitingLoop);
		destroyEffectImmediately(awardsDisqualifiedAudio);
		destroyEffectImmediately(awardsCongratsAudio);
		destroyEffectImmediately(whetsappAudio);
		destroyEffectImmediately(objectiveCompleteAudio);
		destroyEffectImmediately(itemGetAudio);
	}

	public void destroyEffectImmediately(GameObject aObject)
	{
		if (aObject != null && !aObject.GetComponent<AudioSource>().isPlaying)
		{
			Object.Destroy(aObject);
		}
	}

	public void audioCleanup()
	{
		string text = "UNUSED\n";
		List<string> list = new List<string>();
		Object[] array = Resources.LoadAll("Sound");
		Object[] array2 = array;
		foreach (Object @object in array2)
		{
			list.Add(@object.name);
		}
		Object[] array3 = Resources.LoadAll("Prefab/Sound");
		Object[] array4 = array3;
		foreach (Object object2 in array4)
		{
			GameObject gameObject = object2 as GameObject;
			string item = gameObject.GetComponent<AudioSource>().clip.name;
			list.Remove(item);
		}
		foreach (string item2 in list)
		{
			text = text + item2 + "\n";
		}
		CommonAPI.debug(text);
	}

	public void setBgmVolume(float aVolume)
	{
		bgmVolume = aVolume;
		AudioSource[] componentsInChildren = base.gameObject.GetComponentsInChildren<AudioSource>();
		AudioSource[] array = componentsInChildren;
		foreach (AudioSource audioSource in array)
		{
			string[] array2 = audioSource.gameObject.name.Split('_');
			if (array2[1] == "bgm" && (prevBGM == null || (prevBGM != null && audioSource.gameObject.name != prevBGM.name)))
			{
				audioSource.GetComponent<AudioSource>().volume = bgmVolume;
			}
		}
	}

	public void setSfxVolume(float aVolume)
	{
		sfxVolume = aVolume;
		AudioSource[] componentsInChildren = base.gameObject.GetComponentsInChildren<AudioSource>();
		AudioSource[] array = componentsInChildren;
		foreach (AudioSource audioSource in array)
		{
			string[] array2 = audioSource.gameObject.name.Split('_');
			if (array2[1] != "bgm")
			{
				audioSource.GetComponent<AudioSource>().volume = sfxVolume;
			}
		}
	}

	public void setDaedalicSplashVolume(float aVolume)
	{
		GameObject gameObject = GameObject.Find("DaedalicSplashVideo");
		if (gameObject != null)
		{
			gameObject.GetComponent<AudioSource>().volume = aVolume;
		}
	}

	public void setDaylightSplashVolume(float aVolume)
	{
		GameObject gameObject = GameObject.Find("DaylightSplashVideo");
		if (gameObject != null)
		{
			gameObject.GetComponent<AudioSource>().volume = aVolume;
		}
	}
}
