
Imports System.Text

Public Class ActiveAppRuleBlockEditor

    ' Declarations
    Private _rule As App.ActiveAppRule
    Friend Event RuleSaved(editor As UserControl, rule As App.IRulePreview)
    Private Class ProfileItem
        Public Property ID As Integer
        Public Property Name As String

        Public Overrides Function ToString() As String
            Return Name
        End Function
    End Class

    ' Form Events

    ' Control Events
    Private Sub BtnSave_Click(sender As Object, e As EventArgs) Handles BtnSave.Click
        SaveRule()
        RaiseEvent RuleSaved(Me, _rule)
    End Sub

    ' Methods
    ' Interface method — just a wrapper
    Friend Sub LoadRule(rule As App.ActiveAppRule)
        _rule = rule

        ' Target process
        TxtBoxTargetProcess.Text = _rule.TargetProcess

    End Sub
    Friend Sub SaveRule()

        _rule.Mode = App.ActiveAppRule.ActivationMode.ForegroundWindow
        _rule.Action = App.ActiveAppRule.Actions.BlockCapture
        _rule.TargetProcess = TxtBoxTargetProcess.Text

    End Sub

End Class
