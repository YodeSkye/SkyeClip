
Imports System.Text

Public Class KeywordRuleEditor

    ' Declarations
    Private _rule As App.KeywordRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)

    ' Form Events
    Private Sub KeywordRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CoBoxAction.DataSource = [Enum].GetValues(Of App.ContentAction)()
    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)
    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.KeywordRule)
        If rule Is Nothing Then
            _rule = New KeywordRule()
        Else
            _rule = rule
        End If
        TxtBoxKeyword.Text = rule.Keyword
        CoBoxAction.SelectedItem = rule.Action
    End Sub
    Friend Sub SaveRule()
        _rule.Keyword = TxtBoxKeyword.Text
        _rule.Action = CType(CoBoxAction.SelectedItem, App.ContentAction)
    End Sub

End Class
