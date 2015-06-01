﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearnPolish.Desktop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new ViewModel.ViewModel();
        }

        private void TextBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Get().CheckTranslation();
            }
        }

        private void SwitchLanguages_OnClick(object sender, RoutedEventArgs e)
        {
           Get().SwitchLanguagesMethod();
        }

        private ViewModel.ViewModel Get()
        {
            return ((ViewModel.ViewModel) DataContext);
        }

        private void Translate_OnClick(object sender, RoutedEventArgs e)
        {
            Get().CheckTranslation();
        }
    }
}
