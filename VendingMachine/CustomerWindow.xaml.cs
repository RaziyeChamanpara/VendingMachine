using VendingMachine.Models;
using VendingMachine.BusinessLogic;
using System.Windows;
using System.Collections.Generic;

namespace VendingMachine
{
  public partial class CustomerWindow : Window
  {
    VendingMachineLogic VendingMachineLogic { get; set; } = VendingMachineLogic.GetInstance();
    List<Can> AvailableCans { get; set; }
    Can SelectedCan { get; set; }
    public CustomerWindow()
    {
      InitializeComponent();
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      LoadData();
    }
    private void LoadData()
    {
      AvailableCans = VendingMachineLogic.GetAvailableCans();
      cansDataGrid.ItemsSource = AvailableCans;
      cansDataGrid.SelectedIndex = 0;
    }
    private void CansDataGrid_SelectedCellsChanged(object sender, System.Windows.Controls.SelectedCellsChangedEventArgs e)
    {
      ChangeSelectedCan(cansDataGrid.SelectedIndex);

      selectedCanLabel.Content = SelectedCan.Name;
    }
    private void ChangeSelectedCan(int selectedIndex)
    {
      if (selectedIndex < 0)
        selectedIndex = 0;
      SelectedCan = AvailableCans[selectedIndex];
    }
    private void BuyButton_Click(object sender, RoutedEventArgs e)
    {
      if (creditRadioButton.IsChecked != true
        && cashRadioButton.IsChecked != true)
      {
        MessageBox.Show("Choose Your Payment Method.");
        return;
      }

      if (creditRadioButton.IsChecked == true)
        VendingMachineLogic.Sell(SelectedCan, PaymentMethod.Credit);
      else
        VendingMachineLogic.Sell(SelectedCan, PaymentMethod.Cash);

      LoadData();

    }
  }
}
