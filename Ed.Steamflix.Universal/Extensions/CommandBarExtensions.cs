﻿using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Ed.Steamflix.Universal.Extensions
{
    /// <summary>
    /// http://stackoverflow.com/a/35876039/279412
    /// </summary>
    public static class CommandBarExtensions
    {
        public static readonly DependencyProperty HideMoreButtonProperty =
            DependencyProperty.RegisterAttached("HideMoreButton", typeof(bool), typeof(CommandBarExtensions),
                new PropertyMetadata(false, OnHideMoreButtonChanged));

        public static bool GetHideMoreButton(UIElement element)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            return (bool)element.GetValue(HideMoreButtonProperty);
        }

        public static void SetHideMoreButton(UIElement element, bool value)
        {
            if (element == null) throw new ArgumentNullException(nameof(element));
            element.SetValue(HideMoreButtonProperty, value);
        }

        private static void OnHideMoreButtonChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var commandBar = d as CommandBar;
            if (e == null || commandBar == null || e.NewValue == null) return;
            var morebutton = commandBar.FindChild<Button>("MoreButton");
            if (morebutton != null)
            {
                var value = GetHideMoreButton(commandBar);
                morebutton.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                commandBar.Loaded += CommandBarLoaded;
            }
        }

        private static void CommandBarLoaded(object o, object args)
        {
            var commandBar = o as CommandBar;
            var morebutton = commandBar?.FindChild<Button>("MoreButton");
            if (morebutton == null) return;
            var value = GetHideMoreButton(commandBar);
            morebutton.Visibility = value ? Visibility.Collapsed : Visibility.Visible;
            commandBar.Loaded -= CommandBarLoaded;
        }
    }
}
