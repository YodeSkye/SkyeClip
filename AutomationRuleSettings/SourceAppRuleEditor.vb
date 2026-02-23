
Imports System.Text

Public Class SourceAppRuleEditor

    ' Declarations
    Private _rule As App.SourceAppRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)

    ' Form Events
    Public Sub New()
        InitializeComponent()

        ' Populate actions from enum
        Dim actions = [Enum].GetValues(Of ContentAction)().Cast(Of Object).ToArray()
        CoBoxAction.Items.AddRange(actions)

    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)
    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.SourceAppRule)
        _rule = rule

        ' App name
        TxtBoxAppName.Text = _rule.AppName

        ' Action
        CoBoxAction.SelectedItem = _rule.Action

    End Sub
    Friend Sub SaveRule()

        ' App name
        _rule.AppName = TxtBoxAppName.Text.Trim()

        ' Action
        _rule.Action = CType(CoBoxAction.SelectedItem, App.ContentAction)

    End Sub

End Class
