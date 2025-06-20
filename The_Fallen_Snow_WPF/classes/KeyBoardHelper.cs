using System;
using System.Windows.Input;
using System.Windows;


namespace The_Fallen_Snow_WPF.classes
{
    public static class KeyboardHelper
    {
        public static void BindKeyAction(UIElement element, Key key, ModifierKeys modifiers, Action action)
        {
            element.PreviewKeyDown += (sender, e) =>
            {
                if (e.Key == key && Keyboard.Modifiers == modifiers)
                {
                    action?.Invoke();
                    e.Handled = true;
                }
            };

            element.Focusable = true;
            element.Focus();
        }

        private static readonly HashSet<Key> PressedKeys = new HashSet<Key>();

        public static void BindKeyAction(UIElement element, Key key, ModifierKeys modifiers, Action onPress, Action onRelease = null)
        {
            element.PreviewKeyDown += (s, e) =>
            {
                if ((Keyboard.Modifiers & modifiers) == modifiers && e.Key == key && !PressedKeys.Contains(key))
                {
                    PressedKeys.Add(key);
                    onPress?.Invoke();
                    e.Handled = true;
                }
            };

            element.PreviewKeyUp += (s, e) =>
            {
                if (e.Key == key)
                {
                    PressedKeys.Remove(key);
                    onRelease?.Invoke();
                    e.Handled = true;
                }
            };

            element.LostKeyboardFocus += (s, e) =>
            {
                PressedKeys.Clear();
            };
        }
    }
}