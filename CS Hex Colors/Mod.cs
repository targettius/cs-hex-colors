using ICities;
using UnityEngine;
using ColossalFramework.UI;
using System;
using CSHexColors.TranslationFramework;

namespace CSHexColors
{
    public class Mod : LoadingExtensionBase, IUserMod
    {
        public static Translation translation = new Translation();

        public string Name => "Hex Colors";
        public string Description => translation.GetTranslation("HEXC_DESCRIPTION");

        private UIHexColors m_HexColors = new UIHexColors();

        public override void OnCreated(ILoading loading)
        {
            base.OnCreated(loading);
            if (loading.currentMode != AppMode.AssetEditor)
                return;
        }


        public override void OnLevelLoaded(LoadMode mode)
        {
            base.OnLevelLoaded(mode);

            if (mode==LoadMode.LoadAsset  ||  mode==LoadMode.NewAsset)
            {
                try
                {
                    m_HexColors.InitFieldsAdder();
                }
                catch (Exception ex)
                {
                    Debug.LogException(ex);
                    UIView.ForwardException(new ModException(Name + " caused an error", ex));
                }
            }

        }

    }
}

/*
mkdir "c:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Files\Mods\$(SolutionName)"
del "c:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Files\Mods\$(SolutionName)\$(TargetFileName)"
xcopy /y "d:\CSL\$(SolutionName)\$(SolutionName)\obj\Debug\$(TargetFileName)" "c:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Files\Mods\$(SolutionName)"
del "c:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Files\Mods\$(SolutionName)\Locale\*.*"
xcopy /y "d:\CSL\$(SolutionName)\$(SolutionName)\Locale\*.xml" "c:\Program Files (x86)\Steam\steamapps\common\Cities_Skylines\Files\Mods\$(SolutionName)\Locale\"
*/
