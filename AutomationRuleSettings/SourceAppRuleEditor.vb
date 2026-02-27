
Imports SkyeClip.ClipRepository

Public Class SourceAppRuleEditor

    ' Declarations
    Private _rule As App.SourceAppRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)

    ' Form Events
    Public Sub New()
        InitializeComponent()

        CoBoxTargetProcess.Items.Clear()
        For Each a In App.Tray.repo.GetKnownSourceApps()
            CoBoxTargetProcess.Items.Add(a)
        Next

        Dim actions = [Enum].GetValues(Of ContentAction)().Cast(Of Object).ToArray()
        CoBoxAction.Items.AddRange(actions)

    End Sub
    Private Sub SourceAppRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
        Skye.UI.ThemeManager.ApplyToTooltip(TipError)
    End Sub

    ' Control Events
    Private Sub CoBoxTargetProcess_SelectedIndexChanged(sender As Object, e As EventArgs) Handles CoBoxTargetProcess.SelectedIndexChanged
        If TypeOf CoBoxTargetProcess.SelectedItem Is SourceAppInfo Then
            Dim item = CType(CoBoxTargetProcess.SelectedItem, SourceAppInfo)
            TxtBoxTargetProcess.Text = item.ProcessName
        End If
    End Sub
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()
        If TxtBoxTargetProcess.Text.Trim() = String.Empty Then
            Dim pt = CoBoxTargetProcess.PointToScreen(New Point(0, CoBoxTargetProcess.Height))
            TipError.ShowTooltipAt(pt, "Please enter or select a process name.", My.Resources.ImageRules16)
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
    Friend Sub LoadRule(rule As App.SourceAppRule)
        _rule = rule

        ' App name
        TxtBoxTargetProcess.Text = _rule.AppName

        ' Action
        CoBoxAction.SelectedItem = _rule.Action

    End Sub
    Friend Sub SaveRule()

        ' App name
        _rule.AppName = TxtBoxTargetProcess.Text.Trim()

        ' Action
        _rule.Action = CType(CoBoxAction.SelectedItem, App.ContentAction)

    End Sub

End Class
