
Imports System.Text

Public Class KeywordRuleEditor

    ' Declarations
    Private _rule As App.KeywordRule
    Friend Event RuleSaved(editor As UserControl)

    ' Form Events
    Private Sub KeywordRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CoBoxAction.DataSource = [Enum].GetValues(GetType(App.ContentAction))
    End Sub

    ' Control Events
    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        SaveRule()
        RaiseEvent RuleSaved(Me)
    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.KeywordRule)
        _rule = rule
        TxtBoxKeyword.Text = rule.Keyword
        CoBoxAction.SelectedItem = rule.Action
    End Sub
    Friend Sub SaveRule()
        _rule.Keyword = TxtBoxKeyword.Text
        _rule.Action = CType(CoBoxAction.SelectedItem, App.ContentAction)
    End Sub

End Class
