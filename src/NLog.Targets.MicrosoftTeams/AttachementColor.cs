namespace NLog.Targets.MicrosoftTeams
{
    public static class AttachementColor
    {
        public static string GetAttachmentColor(string level)
        {
            switch (level)
            {
                case "Trace":
                    return "ffffff";
                case "Debug":
                    return "00ff00";
                case "Info":
                    return "0094FF";
                case "Warn":
                    return "FFE97F";

                case "Error":
                    return "d9534f";

                case "Fatal":
                    return "d9534f";
                case "Off":
                    return "ffffff";
                default:
                    return "777777";
            }
        }
    }
}
