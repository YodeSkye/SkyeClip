
Imports System.Text

Public Class LocationBlockRuleEditor

    ' Declarations
    Private _rule As App.LocationBlockRule
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

    End Sub

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)
    End Sub

    ' Methods
    ' Interface method — just a wrapper
    Friend Sub LoadRule(rule As App.LocationBlockRule)
        _rule = rule

        ' Network name
        CoBoxTargetName.Text = _rule.TargetName

        ' Description
        TxtBoxDescription.Text = _rule.Description

    End Sub
    Friend Sub SaveRule()

        ' Network name
        _rule.TargetName = CoBoxTargetName.Text

        ' Description
        _rule.Description = TxtBoxDescription.Text

    End Sub

End Class
