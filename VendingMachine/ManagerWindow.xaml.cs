using VendingMachine.BusinessLogic;
using VendingMachine.Models;
using System.Windows;
using System.Collections.Generic;
using System.Windows.Controls;

namespace VendingMachine
{
  public partial class ManagementWindow : Window
  {
    List<Can> AllCans { get; set; } = new List<Can>();
    VendingMachineLogic VendingMachineLogic { get; set; } = new VendingMachineLogic();
    public ManagementWindow()
    {
      InitializeComponent();
    }
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
      FillAvailables();

      GenerateRestockControls();
    }
    private void GenerateRestockControls()
    {
      AllCans = VendingMachineLogic.GetAllCans();
      int row = 0;
      foreach (var can in AllCans)
      {
        Label label = new Label();
        label.Content = can.Name + ":";

        TextBox textBox = new TextBox();
        textBox.Margin = new Thickness(5);
        textBox.DataContext = can;
        textBox.TextChanged += TextBox_TextChanged;
        textBox.Text = "0";

        cansGrid.Children.Add(label);
        cansGrid.Children.Add(textBox);
        Grid.SetColumn(label, 0);
        Grid.SetRow(label, row);
        Grid.SetColumn(textBox, 1);
        Grid.SetRow(textBox, row);

        row++;
      }
    }
    private void FillAvailables()
    {
      availableCashLabel.Content = VendingMachineLogic.GetAvailableCash();
      availableCreditLable.Content = VendingMachineLogic.GetAvailableCredit();
      cansDataGrid.ItemsSource = VendingMachineLogic.GetAvailableCans();
    }
    private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
    {
      TextBox textBox = (sender as TextBox);

      Can can = (textBox.DataContext as Can);

      int result;
      if (int.TryParse(textBox.Text, out result))
        can.Count = result;
    }
    private void RestockButton_Click(object sender, RoutedEventArgs e)
    {
      VendingMachineLogic.Restock(AllCans);

      FillAvailables();

      MessageBox.Show("Restock Completed Successfully.");
    }

  }
}
