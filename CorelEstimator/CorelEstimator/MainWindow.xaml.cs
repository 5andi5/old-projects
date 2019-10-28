using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using CorelEstimator.Managers;
using System.Windows.Media.Effects;
using CorelEstimator.Exceptions;

namespace CorelEstimator
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSelectOutput_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveDialog = new SaveFileDialog();
            saveDialog.Title = "Novērtēšanas rezultātu fails";
            saveDialog.DefaultExt = ".csv";
            saveDialog.Filter = "Comma-separated values (*.csv)|*.csv";
            if (saveDialog.ShowDialog() == true)
            {
                txtResultPath.Text = saveDialog.FileName;
                btnSelectResultFile.Effect = null;
            }
        }

        private void btnEstimate_Click(object sender, RoutedEventArgs e)
        {
            var effect = new DropShadowEffect();
            effect.BlurRadius = 10;
            effect.ShadowDepth = 0;
            if (!MainManager.HaveFilesToEstimate())
            {
                btnAdd.Effect = effect;
                MessageBox.Show("Norādiet vērtējamās datnes!", "Nav norādītas vērtējamās datnes", MessageBoxButton.OK, MessageBoxImage.Error);
                btnAdd.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtResultPath.Text))
            {
                btnSelectResultFile.Effect = effect;
                MessageBox.Show("Norādiet datni, kurā saglabāt rezultātus!", "Nav norādīta rezultātu datne", MessageBoxButton.OK, MessageBoxImage.Error);
                btnSelectResultFile.Focus();
                return;
            }
            try
            {
                MainManager.Estimate();
            }
            catch (GenericException ex)
            {
                MessageBox.Show(ex.Message, "Kļūda", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            MainManager.SaveEstimations(txtResultPath.Text);
            lbFilesToEstimate.ItemsSource = MainManager.Estimations;
            MessageBox.Show("Novērtējumi veiksmīgi saglabāti!", "", MessageBoxButton.OK, MessageBoxImage.Information);
            //Some file in list are merged, therefore re-estimation is not possible.
            DisableEstimation();
        }

        private void DisableEstimation()
        {
            btnEstimate.IsEnabled = false;
            btnAdd.IsEnabled = false;
            btnRemove.IsEnabled = false;
            btnSelectResultFile.IsEnabled = false;
        }

        private void EnableEstimation()
        {
            btnEstimate.IsEnabled = true;
            btnAdd.IsEnabled = true;
            btnRemove.IsEnabled = true;
            btnSelectResultFile.IsEnabled = true;
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Vērtējamās datnes";
            openDialog.DefaultExt = ".svg";
            openDialog.Filter = "Scalable Vector Graphics (*.svg)|*.svg";
            openDialog.Multiselect = true;
            if (openDialog.ShowDialog() == true)
            {
                MainManager.AddFilesToEstimate(openDialog.FileNames);
                btnAdd.Effect = null;
                lbFilesToEstimate.ItemsSource = MainManager.Estimations;
            }
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lbFilesToEstimate.SelectedIndex >= 0)
            {
                MainManager.RemoveFileToEstimate(lbFilesToEstimate.SelectedIndex);
                lbFilesToEstimate.ItemsSource = MainManager.Estimations;
            }
        }

        private void btnClear_Click(object sender, RoutedEventArgs e)
        {
            MainManager.RemoveAllFiles();
            lbFilesToEstimate.ItemsSource = MainManager.Estimations;
            EnableEstimation();
        }

    }
}
