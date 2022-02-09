using UnityEngine;

public static class Constants
{
	public const float zoomSensitivity = 0.5f;

	public const float sensitivity = 0.5f;

	public const float assignMoveDistance = 400f;

	public const float WaitTime = 0.02f;

	public const float ClickHoldTime = 0.7f;

	public const float GridDistanceX = 0.73f;

	public const float GridDistanceZ = 0.74f;

	public const float MapGridDistanceX = 8.5f;

	public const float MapGridDistanceZ = 9f;

	public const float CharacterSpeed = 4f;

	public const float PathWaitSpeed = 0.05f;

	public const float AreaZ = 0f;

	public const float ObstaclesZ = 50f;

	public static float DialogueSpeed = 0.1f;

	public const float cameraXAngle = 35.264f;

	public const int cameraYAngle = 45;

	public static LanguageType LANGUAGE = CommonAPI.convertStringToLanguageType(PlayerPrefs.GetString("LANGUAGE", LanguageType.kLanguageTypeEnglish.ToString()));

	public static string PUBLISHER = "IAH";

	public const bool SANDBOX = true;

	public const string SERVER_URL = "http://54.251.97.227/WeaponStory/Development/DarkEngineConnection.php";

	public const int STEAM_APPID = 363600;

	public const int STEAM_DLCAPPID = 391860;

	public const bool DEMO = false;

	public const bool STEAM = true;

	public const bool GOG = false;

	public const string URL_DISCORD = "https://discord.me/holypotatoes";

	public const string URL_SHOP = "https://store.holypotatoesgame.com/?utm_source=hpaws-game&utm_medium=in-game-link";

	public const string URL_WEB = "https://www.day-lightstudios.com/?utm_source=hpaws-game&utm_medium=ingame-link&utm_campaign=ingame-link";

	public const string URL_FB = "https://www.facebook.com/holypotatogame/";

	public const string URL_TW = "https://twitter.com/holypotatogame";

	public const string URL_HPSPY = "https://store.steampowered.com/app/830590/Holy_Potatoes_A_Spy_Story/";

	public const string URL_HPAWS = "https://store.steampowered.com/app/363600/Holy_Potatoes_A_Weapon_Shop/";

	public const string URL_HPWIS = "https://store.steampowered.com/app/505730/Holy_Potatoes_Were_in_Space/";

	public const string URL_HPWTH = "https://store.steampowered.com/app/664320/Holy_Potatoes_What_the_Hell/";

	public const float SPD_MULT1 = 1f;

	public const float SPD_MULT2 = 1.43f;

	public const float SPD_MULT3 = 3.33f;

	public const float SPD_TIMER1 = 1f;

	public const float SPD_TIMER2 = 0.7f;

	public const float SPD_TIMER3 = 0.3f;

	public const bool SHOW_LANGUAGE = true;

	public const string VERSION = "V 1.1.6";

	public const bool SHOW_DEBUG_HUD = false;

	public const bool IS_TEXT_VER = false;

	public const bool DOWNLOAD = false;

	public const bool UNLOCK_ALL_FEATURES = false;

	public const bool TEXT_DEBUG = false;

	public const bool ENCRYPTED = true;

	public static float TIMEOUT = 15f;

	public const string DATA_PATH = "/Users/daylightLima/Documents/WSS/WSProto0/Assets";

	public const string PATH_REF_DATA = "Resources/Data/";

	public const string PATH_SAVE_DATA_NEW = "/SavedGames/";

	public const string REF_FILE_ENG = "WSREFDATA";

	public const string REF_FILE_GERMANY = "WSREFDATA_GERMANY";

	public const string REF_FILE_RUSSIA = "WSREFDATA_RUSSIA";

	public const string REF_FILE_JAP = "WSREFDATA_JAP";

	public const string REF_FILE_ITALIAN = "WSREFDATA_ITALIAN";

	public const string REF_FILE_FRENCH = "WSREFDATA_FRENCH";

	public const string REF_FILE_SPANISH = "WSREFDATA_SPANISH";

	public const string REF_FILE_CHINESE = "WSREFDATA_CHINESE";

	public const string PATH_DYN_DATA = "WSDYNDATA.txt";

	public const string SAVEDIR = "WSDir.txt";

	public const string ENCODE_KEY_REF_DATA = "e7nc3r2e6f8k2e0y";

	public const string ENCODE_KEY_DYN_DATA = "e1n6c3dy4n9k2ey5";

	public const int DESIGN_WIDTH = 320;

	public const int DESIGN_HEIGHT = 480;

	public static float SCREEN_SCALE = 1f;

	public const float TEXT_SPEED = 0.02f;

	public static float VOLUME_INTERVAL = 0.01f;

	public const int INITIAL_GOLD = 500;

	public const int GOLD_LOAN = 2000;

	public const int SALARY_CHANCE_MAX = 3;

	public const string INTRO_DIALOGUE = "90401";

	public const int INIT_TUTORIAL_END_INDEX = 7;

	public const string NORMAL_SCENARIO_ID = "10001";

	public const float UNIT_TIME = 1f;

	public const int HALF_HOURS_PER_UNITTIME = 2;

	public const int GAME_START_TIME = 14;

	public const int SAVE_SLOT_NUM = 5;

	public const int QUEST_FINISH_EMBLEM = 1;

	public const int FULL_COMPLETE_EMBLEM = 1;

	public const int EXPLORE_STATUS_CHANCE = 3;

	public const int EXPLORE_ITEM_MAX_CHANCE = 1000;

	public const int RARE_HERO_MAX_CHANCE = 1000;

	public const int DEMO_TARGET_GOLD = 100000;

	public const long WHETSAPP_EXPIRE_TIME = 2688L;

	public const int WHETSAPP_LIST_MAX = 20;

	public const int WHETSAPP_UNREAD_MAX = 10;

	public const int PROJECT_LIST_MAX = 2;

	public const int WORK_PROGRESS_MAX = 3;

	public const int PROJECT_GROWTH_PER_BOOST = 2;

	public const int MAX_DIRECTION_POINTS = 10;

	public const int MAX_POINT_PER_DIRECTION = 5;

	public const int MIN_BOOST_NUM = 1;

	public const int MAX_BOOST_NUM = 7;

	public const float REPEAT_SMITH_PENALTY = 0.6f;

	public const int AUCTION_BID_MAX_NUM = 4;

	public const int DEFAULT_AFFINITY = 2;

	public const int UNIQUE_AFFINITY = 3;

	public const float RECOMMENDED_WEAPON_TYPE_BONUS = 1.25f;

	public const int PROJECT_DEFAULT_MAX_BOOST = 1;

	public const float REPEAT_BOOST_PENALTY = 0.6f;

	public const string LEGENDARY_WEAPON_TYPE = "901";

	public const float SCORE_AFFINITY_LOW = 0.75f;

	public const float SCORE_AFFINITY_HIGH = 1.1f;

	public const float SCORE_PRI_MATCH = 2f;

	public const float SCORE_SEC_MATCH = 1f;

	public const float TIER_LOW_CONST = 0.28f;

	public const float TIER_HIGH_CONST = 0.36f;

	public const float SCORE_CONST = 0.2f;

	public const float JCB_BASE = 4f;

	public const float JCB_POWER = 4.3f;

	public const float JCB_MULT = 28f;

	public const float JCB_OFFSET = 0f;

	public const int HERO_LEVEL_1_EXP_CONST = 250;

	public const int HERO_LEVEL_1_EXPADD_CONST = 200;

	public const float PRI_STAT_MULT = 2f;

	public const float SEC_STAT_MULT = 1f;

	public const float OVERALL_STAT_MATCH = 0.8f;

	public const int MAX_STAT_SCORE = 6;

	public const int PRI_STAT_MATCH_SCORE = 1;

	public const int SEC_STAT_MATCH_SCORE = 1;

	public const float AFFINITY_PENALTY_MULT = 1f / 30f;

	public const float WEAPON_QUALITY_MULT = 1f / 30f;

	public const float WEAPON_QUALITY_TIMES_MULT = -2.4f;

	public const float WEAPON_QUALITY_MAX_CHANCE = 0.95f;

	public const float WEAPON_QUALITY_MAX_VALUE = 0.675f;

	public const float WEAPON_QUALITY_POWER = -0.8f;

	public const int SMITH_MAX_LEVEL = 20;

	public const int SMITH_MAX_TAG_NUM = 4;

	public const float SMITH_SALARY_PENALTY = 5f;

	public const float SMITH_EXP_MULT_NSB = 0.5f;

	public const float SMITH_EXP_MULT_MSB = 0.5f;

	public const int EXPLORE_SALARY_MULT = 15;

	public const int MERCHANT_SALARY_MULT = 15;

	public const int MERCHANT_MAX_LEVEL = 15;

	public const int EXPLORER_MAX_LEVEL = 15;

	public const string INIT_SMITH_1 = "10001";

	public const string INIT_SMITH_2 = "10002";

	public const string INIT_SMITH_3 = "10003";

	public const string INIT_SMITH_4 = "10008";

	public const float WEAPON_AWARDS_BASE = 150f;

	public const float WEAPON_AWARDS_MULT = 2.3f;

	public const float WEAPON_AWARDS_POW = 3.5f;

	public const float WEAPON_AWARDS_ADD = 4.4f;

	public const float BEST_WPN_BENCHMARK_MULT = 1f;

	public const float BEST_WPN_NOMINATION_MULT = 0.7f;

	public const float CAT_WPN_BENCHMARK_MULT = 0.55f;

	public const float CAT_WPN_NOMINATION_MULT = 0.385f;

	public const float BRIBE_CONSTANT = 10f;

	public const float BRIBE_POW = 0.7f;

	public const float BRIBE_MULT = 60f;

	public const float BEST_WPN_BRIBE_MULT = 1f;

	public const float CAT_WPN_BRIBE_MULT = 0.3f;

	public const int CHANCEUP_PER_BRIBE = 4;

	public const float PRIZE_BASE = 5000f;

	public const float PRIZE_MULT = 5000f;

	public const float PRIZE_ADD = 0f;

	public const float PRIZE_POW = 1f;

	public const float BEST_WPN_PRIZE_MULT = 1f;

	public const float CAT_WPN_PRIZE_MULT = 0.33f;

	public const int QUEST_RANDOM_TEXT_NUM = 10;

	public const int QUEST_RANDOM_ENEMY_NUM = 10;

	public const int QUEST_RANDOM_TEXT_INTERVAL = 20;

	public const int HERO_TALK_FREQUENCY = 10;

	public const int HERO_LOYALTY_MAX_LEVEL = 5;

	public const int HERO_REQUEST_LEVEL = 3;

	public const int PREFERRED_AFFINITIY_MIN = 2;

	public const int MAX_REQUEST_NUM = 3;

	public const int MAX_LEGENDARY_REQUEST = 1;

	public const int MAX_OFFERS = 6;

	public const int EXPLORE_DURATION = 10;

	public const int BUY_DURATION = 3;

	public const int SELL_DURATION = 5;

	public const int TRAINING_DURATION = 10;

	public const int VACATION_DURATION = 24;

	public const int MAX_AREAREGION = 4;

	public const int START_DOG_LOVE = 0;

	public const int NAMING_DOG_LOVE = 100;

	public const int REDUCE_DOG_LOVE = 50;

	public const float PATATA_MULT = 1.05f;

	public const int DOG_STA_BOOST = 1;

	public const string STATE_IDLE = "101";

	public const string STATE_WORKING = "102";

	public const string STATE_GONEHOME = "103";

	public const string STATE_ONLEAVE = "104";

	public const string STATE_NAP = "105";

	public const string STATE_EXPLORE = "901";

	public const string STATE_BUY = "902";

	public const string STATE_SELL = "903";

	public const string STATE_RESEARCH = "904";

	public const string STATE_STANDBY = "905";

	public const string STATE_VACATION = "906";

	public const string STATE_TRAINING = "907";

	public const float MOOD_NSB_BASE = 0.4f;

	public const float MOOD_NSB_POWER = 1f;

	public const float MOOD_NSB_MULT = 0.2f;

	public const float MOOD_MSB_BASE = 0.4f;

	public const float MOOD_MSB_POWER = 1f;

	public const float MOOD_MSB_MULT = 0.2f;

	public const float MOOD_EXPLORE_BASE = 0.7f;

	public const float MOOD_EXPLORE_POWER = 1f;

	public const float MOOD_EXPLORE_MULT = 0.1f;

	public const float MOOD_SELL_BASE = 0.7f;

	public const float MOOD_SELL_POWER = 1f;

	public const float MOOD_SELL_MULT = 0.1f;

	public const float MOOD_BUY_BASE = 0.7f;

	public const float MOOD_BUY_POWER = 1f;

	public const float MOOD_BUY_MULT = 0.1f;

	public const float MOOD_RESEARCH_BASE = 1.6f;

	public const float MOOD_RESEARCH_POWER = 1f;

	public const float MOOD_RESEARCH_MULT = -0.2f;

	public const float MOOD_TRAIN_BASE = -0.05f;

	public const float MOOD_TRAIN_POWER = 1f;

	public const float MOOD_TRAIN_MULT = 0.05f;

	public const float MOOD_SELL_REDUCE = 3f;

	public const float MOOD_BUY_REDUCE = 2f;

	public const float MOOD_EXPLORE_REDUCE = 1f;

	public const float MOOD_TRAIN_REDUCE = 7f;

	public const int CHALLENGE_TAG_SET_MAX = 5;

	public const int CHALLENGE_STAT_MULT = 20;

	public const float CHALLENGE_STAT_POW = 1.3f;

	public const int CHALLENGE_MILESTONE_BASE = 3;

	public const float CHALLENGE_MILESTONE_GROWTH = 0.2f;

	public const int CHALLENGE_GOLD_BASE = 0;

	public const int CHALLENGE_GOLD_MULT = 300;

	public const float CHALLENGE_GOLD_POW = 0.55f;

	public const string COLOR_LIGHTRED = "E54242";

	public const string COLOR_RED = "FF4842";

	public const string COLOR_GREEN = "56AE59";

	public const string COLOR_YELLOW = "FFD84A";

	public const string COLOR_ORANGE = "FF9000";

	public const string COLOR_PURPLE = "D484F5";

	public const string COLOR_BLUE = "00AAC7";

	public const string COLOR_WHITE = "FFFFFF";

	public const string COLOR_GREY = "808080";

	public const string COLOR_LIGHTGREY = "C0C0C0";

	public const string COLOR_WHETSAPPHIGHLIGHT = "D484F5";

	public const string TEXT_LINE = "[s]                         [/s]";

	public const float PRI_STAT_COLOR_R = 0.0196f;

	public const float PRI_STAT_COLOR_G = 0.788f;

	public const float PRI_STAT_COLOR_B = 0.659f;

	public const float SEC_STAT_COLOR_R = 0.0235f;

	public const float SEC_STAT_COLOR_G = 0.522f;

	public const float SEC_STAT_COLOR_B = 0.439f;

	public const float NORM_STAT_COLOR_R = 0.0235f;

	public const float NORM_STAT_COLOR_G = 0.157f;

	public const float NORM_STAT_COLOR_B = 0.196f;

	public const float PRI_LABEL_COLOR_R = 1f;

	public const float PRI_LABEL_COLOR_G = 0.827f;

	public const float PRI_LABEL_COLOR_B = 0.478f;

	public const float SEC_LABEL_COLOR_R = 0.447f;

	public const float SEC_LABEL_COLOR_G = 1f;

	public const float SEC_LABEL_COLOR_B = 0.902f;
}
