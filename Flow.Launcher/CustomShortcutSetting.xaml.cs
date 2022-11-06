﻿using Flow.Launcher.Core.Resource;
using Flow.Launcher.ViewModel;
using System;
using System.Windows;
using System.Windows.Input;

namespace Flow.Launcher
{
    public partial class CustomShortcutSetting : Window
    {
        private SettingWindowViewModel viewModel;
        public string Key { get; set; }
        public string Value { get; set; }
        private string originalKey { get; init; } = null;
        private string originalValue { get; init; } = null;
        bool update = false;

        public CustomShortcutSetting(SettingWindowViewModel vm)
        {
            viewModel = vm;
            InitializeComponent();
        }

        public CustomShortcutSetting(string key, string value, SettingWindowViewModel vm)
        {
            viewModel = vm;
            Key = key;
            Value = value;
            originalKey = key;
            originalValue = value;
            update = true;
            InitializeComponent();
        }

        private void BtnCancel_OnClick(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrEmpty(Key) || String.IsNullOrEmpty(Value))
            {
                MessageBox.Show(InternationalizationManager.Instance.GetTranslation("emptyShortcut"));
                return;
            }
            // Check if key is modified or adding a new one
            if (((update && originalKey != Key) || !update)
                && viewModel.ShortcutExists(Key))
            {
                MessageBox.Show(InternationalizationManager.Instance.GetTranslation("duplicateShortcut"));
                return;
            }
            DialogResult = !update || originalKey != Key || originalValue != Value;
            Close();
        }

        private void cmdEsc_OnPress(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }

        private void BtnTestShortcut_OnClick(object sender, RoutedEventArgs e)
        {
            App.API.ChangeQuery(tbExpand.Text);
            Application.Current.MainWindow.Show();
            Application.Current.MainWindow.Opacity = 1;
            Application.Current.MainWindow.Focus();
        }
    }
}
