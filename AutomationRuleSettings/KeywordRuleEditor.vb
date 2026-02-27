
Imports System.Text

Public Class KeywordRuleEditor

    ' Declarations
    Private _rule As App.KeywordRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)

    ' Form Events
    Private Sub KeywordRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
        Skye.UI.ThemeManager.ApplyToTooltip(TipError)
        CoBoxAction.DataSource = [Enum].GetValues(Of App.ContentAction)()
    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()
        If TxtBoxKeyword.Text.Trim() = String.Empty Then
            Dim pt = TxtBoxKeyword.PointToScreen(New Point(0, TxtBoxKeyword.Height))
            TipError.ShowTooltipAt(pt, "Please enter a keyword.", My.Resources.ImageRules16)
            Exit Sub
        End If
        If CoBoxAction.SelectedItem Is Nothing Then
            Dim pt = CoBoxAction.PointToScreen(New Point(0, CoBoxAction.Height))
            TipError.ShowTooltipAt(pt, "Please select an action.", My.Resources.ImageRules16)
            Exit Sub
        End If

        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)

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
