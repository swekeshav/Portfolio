using System.Windows;
using System.Windows.Controls;

namespace Portfolio.WPF;

/// <summary>
/// Interaction logic for Calculator.xaml
/// </summary>
public partial class Calculator : Window
{
    public Calculator()
    {
        InitializeComponent();
    }

    public double LastNumber { get; set; }
    public OperatorSymbols SelectedOperator { get; set; } = OperatorSymbols.None;

    void BtnNumber_Click(object sender, RoutedEventArgs args)
    {
        var newContent = (sender as Button)?.Content;
        var currentContent = lblResult.Content;

        lblResult.Content = currentContent.Equals("0")
            ? newContent
            : $"{currentContent}{newContent}";
    }

    void BtnPoint_Click(object sender, RoutedEventArgs args)
    {
        var currentContent = lblResult.Content.ToString()!;
        if (currentContent.Contains('.'))
            return;
        lblResult.Content = $"{currentContent}.";
    }

    void BtnClear_Click(object sender, RoutedEventArgs args)
    {
        lblResult.Content = "0";
    }

    void BtnNegate_Click(object sender, RoutedEventArgs args)
    {
        var currentContent = lblResult.Content.ToString()!;
        if (currentContent.Equals("0"))
            return;
        lblResult.Content = currentContent.StartsWith('-')
            ? currentContent[1..]
            : $"-{currentContent}";
    }

    void BtnEquals_Click(object sender, RoutedEventArgs args)
    {
        OperateOnOperands();
        lblResult.Content = LastNumber.ToString();
        SelectedOperator = OperatorSymbols.None;
    }

    void BtnOperator_Click(object sender, RoutedEventArgs args)
    {
        OperateOnOperands();
        AssignOperatorSymbol(sender);
    }

    void AssignOperatorSymbol(object sender)
    {
        var operatorSymbol = (sender as Button)?.Content.ToString();
        SelectedOperator = operatorSymbol switch
        {
            "+" => OperatorSymbols.Add,
            "-" => OperatorSymbols.Subtract,
            "*" => OperatorSymbols.Multiply,
            "/" => OperatorSymbols.Divide,
            _ => OperatorSymbols.None,
        };
    }

    void OperateOnOperands()
    {
        var currentNumber = double.Parse(lblResult.Content.ToString()!);
        LastNumber = SelectedOperator switch
        {
            OperatorSymbols.Add => LastNumber + currentNumber,
            OperatorSymbols.Subtract => LastNumber - currentNumber,
            OperatorSymbols.Multiply => LastNumber * currentNumber,
            OperatorSymbols.Divide => LastNumber / currentNumber,
            _ => currentNumber,
        };
        lblResult.Content = LastNumber.ToString();
        SelectedOperator = OperatorSymbols.None;
    }
}

public enum OperatorSymbols
{
    None,
    Add,
    Subtract,
    Multiply,
    Divide,
}
