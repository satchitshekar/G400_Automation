Imports System.IO
Imports G400_Automation.GlobalVariables
Imports G400_Automation.ExceptionLog
Public Class Form1
    Private Sub MetroButton1_Click(sender As Object, e As EventArgs) Handles MetroButton1.Click

        Dim mainCls As MainClass = New MainClass
        mainCls.Main()

    End Sub

    Private Sub MetroButton2_Click(sender As Object, e As EventArgs) Handles MetroButton2.Click

        OpenFileDialog1.Filter = "Excel File | *.xlsx"
        OpenFileDialog1.FileName = ""
        Dim result As DialogResult = OpenFileDialog1.ShowDialog()
        Dim path As String = OpenFileDialog1.FileName

        Try
            If (result = DialogResult.OK) Then
                MetroTextBox1.Text = path
                TARFilePath = path
            End If

        Catch ex As Exception
            Dim eModel As ErrorModel = New ErrorModel
            eModel.ClassName = "Form1"
            eModel.MethodName = "MetroButton2_Click"
            eModel.ExceptionMessage = ex.ToString

            Dim expLog As ExceptionLog = New ExceptionLog
            expLog.ExceptionLogTest(eModel)
        End Try

    End Sub

End Class
