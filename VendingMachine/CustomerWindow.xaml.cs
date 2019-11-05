using VendingMachine.Models;
using VendingMachine.BusinessLogic;
using System.Windows;
using System.Collections.Generic;

namespace VendingMachine
{
  public partial class CustomerWindow : Window
  {
    VendingMachineLogic VendingMachineLogic { get; set; } = new VendingMachineLogic();
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
    }
    private void ChangeSelectedCan(int selectedIndex)
    {
      if (selectedIndex < 0)
        selectedIndex = 0;

      if (AvailableCans.Count == 0)
      {
        SelectedCan = null;
        selectedCanLabel.Content = "";
      }
      else
      {
        SelectedCan = AvailableCans[selectedIndex];
        selectedCanLabel.Content = SelectedCan.Name;
      }
    }
    private void BuyButton_Click(object sender, RoutedEventArgs e)
    {
      if(SelectedCan==null)
      {
        MessageBox.Show("Choose a Drink.");
        return;
      }
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

      MessageBox.Show("Operation Completed Successfully");

    }
  }
}
