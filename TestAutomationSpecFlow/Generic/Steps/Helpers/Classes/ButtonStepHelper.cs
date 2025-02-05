﻿using Core;
using Core.Logging;
using Generic.Steps.Helpers.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Generic.Steps.Helpers.Classes
{
    public class ButtonStepHelper : StepHelper, IButtonStepHelper
    {
        private readonly ITargetForms targetForms;
        public ButtonStepHelper(FeatureContext featureContext, ITargetForms targetForms) : base(featureContext)
        {
            this.targetForms = targetForms;
        }

        public bool IsDisplayed(string buttonName)
        {
            DebugOutput.Log($"IsDisplayed {buttonName}");
            var buttonElement = GetButtonElement(buttonName);
            if (buttonElement == null) return false;
            return buttonElement.Displayed;
        }

        public bool ClickButton(string buttonName)
        {
            DebugOutput.Log($"ClickButton {buttonName}");
            var buttonElement = GetButtonElement(buttonName);
            if (buttonElement == null) return false;
            return SeleniumUtil.Click(buttonElement);
        }

        public bool DoubleClick(string buttonName)
        {
            DebugOutput.Log($"ClickButton {buttonName}");
            var buttonElement = GetButtonElement(buttonName);
            if (buttonElement == null) return false;
            return SeleniumUtil.DoubleClick(buttonElement);
        }

        public bool RightClick(string buttonName)
        {
            DebugOutput.Log($"ClickButton {buttonName}");
            var buttonElement = GetButtonElement(buttonName);
            if (buttonElement == null) return false;
            return SeleniumUtil.RightClick(buttonElement);
        }

        public bool MouseOver(string buttonName)
        {
            DebugOutput.Log($"MouseOver {buttonName}");
            var buttonElement = GetButtonElement(buttonName);
            if (buttonElement == null) return false;
            return SeleniumUtil.MoveToElement(buttonElement);
        }


        //  PRIVATE

        private IWebElement GetButtonElement(string buttonName)
        {
            DebugOutput.Log($"Getbutton {buttonName}");
            var newButtonName = GetButtonName(buttonName);
            if (CurrentPage.Elements.TryGetValue(newButtonName, out var buttonLocator))
            {
                DebugOutput.Log("Found button in dic");
                var buttonElement = SeleniumUtil.GetElement(buttonLocator);
                if (buttonElement != null) return buttonElement;
            }
            var namedButtonElement = SeleniumUtil.GetElement(By.Name(buttonName));
            if (namedButtonElement == null)
            {
                DebugOutput.Log($"Even after just name");
                return null;
            }
            return namedButtonElement;
        }

        private string GetButtonName(string buttonName)
        {
            DebugOutput.Log($"GetButtonName {buttonName}");
            buttonName = buttonName.ToLower();
            if (buttonName.Contains("button")) return buttonName;
            return buttonName + " button";
        }

    }
}
