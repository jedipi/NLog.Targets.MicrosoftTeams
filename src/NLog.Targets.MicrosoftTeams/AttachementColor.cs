namespace NLog.Targets.MicrosoftTeams
{
    public static class AttachementColor
    {
        public static string GetAttachmentColor(string level)
        {
            switch (level)
            {
                case "Trace":
                    return "Light";
                case "Debug":
                    return "Light";
                case "Info":
                    return "Good";
                case "Warn":
                    return "Warning";

                case "Error":
                    return "Attention";

                case "Fatal":
                    return "Attention";
                case "Off":
                    return "Default";
                default:
                    return "Default";
            }
        }
    }
}
