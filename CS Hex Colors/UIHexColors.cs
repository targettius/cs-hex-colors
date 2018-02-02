using System;
using UnityEngine;
using ColossalFramework;
using ColossalFramework.UI;


namespace CSHexColors
{
    class UIHexColors
    {
        public UIPanel сolor0Panel = null;
        public UIPanel сolor1Panel = null;
        public UIPanel сolor2Panel = null;
        public UIPanel сolor3Panel = null;

        public UIColorField сolor0Picker = null;
        public UIColorField сolor1Picker = null;
        public UIColorField сolor2Picker = null;
        public UIColorField сolor3Picker = null;

        public UITextField сolor0HexField = null;
        public UITextField сolor1HexField = null;
        public UITextField сolor2HexField = null;
        public UITextField сolor3HexField = null;


        public void InitFieldsAdder()
        {
            if (ToolsModifierControl.toolController != null)
            {
                ToolsModifierControl.toolController.eventEditPrefabChanged += new ToolController.EditPrefabChanged(FreshPrefab);
            }
        }


        private void FreshPrefab(PrefabInfo info)
        {
            if (info != null)
            {
                ToolController properties = Singleton<ToolManager>.instance.m_properties;
                var isAllowRun = false;
                if (properties != null && properties.m_editPrefabInfo != null)
                {
                    if (properties.m_editPrefabInfo.GetType() == typeof(VehicleInfo))
                    {
                        isAllowRun = true;
                    }
                    else if (properties.m_editPrefabInfo.GetType() == typeof(BuildingInfo))
                    {
                        isAllowRun = true;
                    }
                    else if (properties.m_editPrefabInfo.GetType() == typeof(PropInfo))
                    {
                        isAllowRun = true;
                    }
                    else if (properties.m_editPrefabInfo.GetType() == typeof(CitizenInfo))
                    {
                        isAllowRun = true;
                    }

                }

                if (isAllowRun)
                {
                    try { AddHexColorsFields(); }
                    catch (Exception ex) { Debug.LogException(ex); }
                }
            }
        }


        public void AddHexColorsFields()
        {
            var view = UIView.GetAView();
            var uiContainer = view.FindUIComponent("FullScreenContainer");
            var propertiosMainPanel = uiContainer.Find<UIPanel>("DecorationProperties");
            var scrollablePanel = propertiosMainPanel.Find<UIScrollablePanel>("Container");

            if (scrollablePanel.components.Count == 0)
            {
                Debug.LogWarning("scrollablePanel.components is empty - no childs");
                Debug.LogWarning("Possible RoundAbouts Editor - exit");
                return;
            }

            var materialPanel = (UIPanel)scrollablePanel.components[scrollablePanel.components.Count - 1];
            var materialToggle = materialPanel.components[0];

            if (materialPanel.components.Count!=2  &&  materialToggle.name!="")
            {
                Debug.LogWarning("Was applied workaround for Asset Prefab(+Building) AI Changer mod");
                materialPanel = (UIPanel)scrollablePanel.components[scrollablePanel.components.Count - 2];
            }
            else
            {
                Debug.Log("materialSubPanel found");
            }

            var materialSubPanel = (UIPanel)materialPanel.components[1];

            сolor0Panel = (UIPanel)materialSubPanel.components[0];
            сolor0Picker = сolor0Panel.Find<UIColorField>("Value");
            сolor0HexField = (UITextField) сolor0Panel.AddUIComponent<UITextField>();
            SetupColorField(сolor0Panel, сolor0Picker, сolor0HexField, 0);

            сolor1Panel = (UIPanel)materialSubPanel.components[1];
            сolor1Picker = сolor1Panel.Find<UIColorField>("Value");
            сolor1HexField = (UITextField)сolor1Panel.AddUIComponent<UITextField>();
            SetupColorField(сolor1Panel, сolor1Picker, сolor1HexField, 1);

            сolor2Panel = (UIPanel)materialSubPanel.components[2];
            сolor2Picker = сolor2Panel.Find<UIColorField>("Value");
            сolor2HexField = (UITextField)сolor2Panel.AddUIComponent<UITextField>();
            SetupColorField(сolor2Panel, сolor2Picker, сolor2HexField, 2);

            сolor3Panel = (UIPanel)materialSubPanel.components[3];
            сolor3Picker = сolor3Panel.Find<UIColorField>("Value");
            сolor3HexField = (UITextField)сolor3Panel.AddUIComponent<UITextField>();
            SetupColorField(сolor3Panel, сolor3Picker, сolor3HexField, 3);

            /* Event when change HEX colors */
            сolor0HexField.eventTextChanged += OnHexValueChanged;
            сolor1HexField.eventTextChanged += OnHexValueChanged;
            сolor2HexField.eventTextChanged += OnHexValueChanged;
            сolor3HexField.eventTextChanged += OnHexValueChanged;

            сolor0Picker.eventSelectedColorChanged += OnPickerValueChanged;
            сolor1Picker.eventSelectedColorChanged += OnPickerValueChanged;
            сolor2Picker.eventSelectedColorChanged += OnPickerValueChanged;
            сolor3Picker.eventSelectedColorChanged += OnPickerValueChanged;
        }


        private void SetupColorField(UIPanel colorPanel, UIColorField сolorPicker, UITextField colorHexField, uint colorIndex)
        {
            var colorLabel = colorPanel.Find<UILabel>("Name");
            colorLabel.width = 180;

            сolorPicker.position = new Vector3(200.0f, 0.0f);
            сolorPicker.name = "Color"+colorIndex+"Picker";

            colorHexField.name = "Color"+colorIndex+"Hex";
            colorHexField.builtinKeyNavigation = true;
            colorHexField.normalBgSprite = "TextFieldPanel";
            colorHexField.selectionSprite = "EmptySprite";

            colorHexField.textColor = new Color32(12, 21, 22, 255);
            colorHexField.horizontalAlignment = UIHorizontalAlignment.Center;
            colorHexField.width = 90;
            colorHexField.height = 20;
            colorHexField.position = new Vector3(255.0f, -2.0f);

            var colorValue = ColorTypeConverter.ToRGBHex( сolorPicker.selectedColor );
            colorHexField.text = colorValue;
        }


        private void OnPickerValueChanged(UIComponent comp, Color fieldValue)
        {
            if (comp == null)
                return;

            UITextField hexField = сolor0HexField;

            if (comp.name == "Color0Picker")
            {
                hexField = сolor0HexField;
            }
            else if (comp.name == "Color1Picker")
            {
                hexField = сolor1HexField;
            }
            else if (comp.name == "Color2Picker")
            {
                hexField = сolor2HexField;
            }
            else if (comp.name == "Color3Picker")
            {
                hexField = сolor3HexField;
            }

            hexField.text = ColorTypeConverter.ToRGBHex(fieldValue);
        }


        private void OnHexValueChanged(UIComponent comp, string fieldValue)
        {
            if (comp == null)
                return;

            if (fieldValue.Length != 6)
                return;

            UIColorField colorPicker = сolor0Picker;

            if (comp.name == "Color0Hex")
            {
                colorPicker = сolor0Picker;
            }
            else if (comp.name == "Color1Hex")
            {
                colorPicker = сolor1Picker;
            }
            else if (comp.name == "Color2Hex")
            {
                colorPicker = сolor2Picker;
            }
            else if (comp.name == "Color3Hex")
            {
                colorPicker = сolor3Picker;
            }

            colorPicker.selectedColor = ColorTypeConverter.HexToColor(fieldValue);
        }
    }
}
