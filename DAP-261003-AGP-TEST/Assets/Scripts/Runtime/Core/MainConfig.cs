namespace DAP.Runtime.Core
{
    public static class MainConfig
    {
        public struct SceneName
        {
            public const string SCENE_MAIN_MENU = "SC1_MainMenu";
            public const string SCENE_GAMEPLAY = "SC2_Game";
        }

        public struct SaveProvider
        {
            public const string SAVE_JSON_NAME = "save.json";
        }

        public struct GeneralNaming
        {
            public const string SPACER = "_spacer_", SCORE = "Score", MISSES = "Misses", STARS_EARNED = "Stars Earned";
        }
    }
}