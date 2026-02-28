
Imports System.Text

Public Class LocationProfilesRuleEditor

    ' Declarations
    Private _rule As App.LocationProfileRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)
    Private Class ProfileItem
        Public Property ID As Integer
        Public Property Name As String
        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Form Events
    Public Sub New()
        InitializeComponent()

        ' Populate network names (RecentNames)
        CoBoxTargetName.Items.Clear()
        For Each rn In App.Context.Network.RecentNames
            CoBoxTargetName.Items.Add(rn)
        Next

        CoBoxProfile.Items.Clear()
        For Each p In App.Settings.Profiles
            CoBoxProfile.Items.Add(New ProfileItem With {.ID = p.ID, .Name = p.Name})
        Next
        CoBoxProfile.DisplayMember = "Name"
        CoBoxProfile.ValueMember = "ID"

    End Sub
    Private Sub LocationProfilesRuleEditor_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Skye.UI.ThemeManager.ApplyToTooltip(Tip)
        Skye.UI.ThemeManager.ApplyToTooltip(TipError)
    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        Tip.HideTooltip()
        TipError.HideTooltip()
        If CoBoxTargetName.Text.Trim() = String.Empty Then
            Dim pt = CoBoxTargetName.PointToScreen(New Point(0, CoBoxTargetName.Height))
            TipError.ShowTooltipAt(pt, "Please enter or select a network name.", My.Resources.ImageRules16)
            Exit Sub
        End If
        If CoBoxProfile.SelectedItem Is Nothing Then
            Dim pt = CoBoxProfile.PointToScreen(New Point(0, CoBoxProfile.Height))
            TipError.ShowTooltipAt(pt, "Please select a profile.", My.Resources.ImageRules16)
            Exit Sub
        End If

        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)

    End Sub

    ' Methods
    Friend Sub LoadRule(rule As App.LocationProfileRule)
        _rule = rule

        ' Target network name
        CoBoxTargetName.Text = _rule.TargetName

        ' Profile
        For Each item As ProfileItem In CoBoxProfile.Items
            If item.ID = _rule.ProfileID Then
                CoBoxProfile.SelectedItem = item
                Exit For
            End If
        Next

    End Sub
    Friend Sub SaveRule()

        ' Target network name
        _rule.TargetName = CoBoxTargetName.Text

        ' Profile
        Dim enterItem As ProfileItem = CType(CoBoxProfile.SelectedItem, ProfileItem)
        _rule.ProfileID = enterItem.ID

    End Sub

End Class
