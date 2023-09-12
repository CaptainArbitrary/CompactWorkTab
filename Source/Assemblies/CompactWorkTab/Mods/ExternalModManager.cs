namespace CompactWorkTab.Mods
{
    public static class ExternalModManager
    {
        public static int MinPriority => Constants.MinPriority;
        public static int DefPriority => PriorityMaster.DefaultPriority ?? Constants.DefPriority;
        public static int MaxPriority => PriorityMaster.MaxPriority ?? Constants.MaxPriority;

        public static int RectYOffset => WorkManager.RectYOffset;
    }
}
