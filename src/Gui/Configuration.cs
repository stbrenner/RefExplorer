namespace RefExplorer.Gui
{
    public class Configuration
    {
        public string HideReferencesFrom { get; set; }

        public bool ShowNativeReferences { get; set; }

        public string[] EntryPoints { get; set; }

        public string HideReferencesTo { get; set; }

        public static Configuration GetDefault()
        {
            return new Configuration
                       {
                           HideReferencesTo = "mscorlib",
                           HideReferencesFrom = "Microsoft.*;System;System.*",
                           ShowNativeReferences = true,
                           EntryPoints = new string[0]
                       };
        }
    }
}
