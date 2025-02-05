﻿using Core;
using Core.Logging;
using Generic.Steps.Helpers.Interfaces;
using OpenQA.Selenium;
using TechTalk.SpecFlow;

namespace Generic.Steps.Helpers.Classes
{
    public class TextBoxStepHelper : StepHelper, ITextBoxStepHelper
    {
        private readonly ITargetForms targetForms;
        public TextBoxStepHelper(FeatureContext featureContext, ITargetForms targetForms) : base(featureContext)
        {
            this.targetForms = targetForms;
        }

        public string GetText(string textBoxName)
        {
            DebugOutput.Log($"GetText {textBoxName}");
            textBoxName = textBoxName.ToLower();
            var textBoxElement = GetTextBox(textBoxName);
            if (textBoxElement == null)
            {
                DebugOutput.Log($"Failed to get TextBox");
                return "";
            }
            string text = GetTextFromElement(textBoxElement);
            if (text == null)
            {
                DebugOutput.Log($"Failed to get ANY Text");
                return "";
            }
            if (text.Contains("Display is"))
            {
                text = text.Replace("Display is ", "");
            }
            DebugOutput.Log($"gotten {text} from textBox {textBoxName}");
            return text;
        }

        private string GetTextFromElement(IWebElement textBoxElement)
        {
            DebugOutput.Log($"GetTextFromElement {textBoxElement}");
            return SeleniumUtil.GetElementText(textBoxElement);
        }

        public bool ClearThenEnterText(string textBoxName, string text)
        {
            DebugOutput.Log($"ClearThenEnterText {textBoxName} {text}");
            textBoxName = textBoxName.ToLower();
            var textBoxElement = GetTextBox(textBoxName);
            if (textBoxElement == null) return false;
            if (!EnterTextAndKey(textBoxName, "", "clear")) return false;
            if (!EnterText(textBoxName, text)) return false;
            return true;
        }

        public bool EnterText(string textBoxName, string text, string key = "")
        {
            DebugOutput.Log($"EnterText {textBoxName} {text} {key}");
            textBoxName = textBoxName.ToLower();
            var textBoxElement = GetTextBox(textBoxName);
            if (textBoxElement == null) return false;
            if (!SeleniumUtil.EnterText(textBoxElement, text, key)) return false;
            DebugOutput.Log($"Text Entered");
            return true;
        }
        public bool EnterTextAndKey(string textBoxName, string text, string key)
        {
            if (key == null) return false;
            key = key.ToLower();
            return EnterText(textBoxName, text, key);
        }

        public bool IsDisplayed(string textBoxName)
        {
            DebugOutput.Log($"IsDisplayed {textBoxName}");
            textBoxName = textBoxName.ToLower();
            var textBoxElement = GetTextBox(textBoxName);
            if (textBoxElement == null) return false;
            return textBoxElement.Displayed;    
        }


        ///  PRIVATE

        private IWebElement GetTextBox(string textBoxName)
        {
            DebugOutput.Log($"GetTextBox {textBoxName}");
            var textBoxLocator = CurrentPage.Elements[textBoxName];
            DebugOutput.Log($"We have the LOCATOR for Accordion {textBoxName} {textBoxLocator}");
            var element = SeleniumUtil.GetElement(textBoxLocator);
            DebugOutput.Log($"TextBox Element {textBoxName} = {element}");
            if (element == null)
            {
                DebugOutput.Log($"WE HAVE FOUND NO TEXT BOX {textBoxName}");
            }
            return element;

        }

    }
}
