Imports System.Data.OleDb
Imports G400_Automation.GlobalVariables
Public Class MainClass

    Public Sub Main()


        Dim ExceptionClass As ExceptionLog = New ExceptionLog
        Dim configClass As ReadConfig = New ReadConfig
        Dim AutoG400 As AutomateG400 = New AutomateG400

        Try
            configClass.configData()
            AutoG400.G400Automation()
        Catch ex As Exception
            Dim eModel As ErrorModel = New ErrorModel
            eModel.ClassName = "MainClass"
            eModel.MethodName = "Main"
            eModel.ExceptionMessage = ex.ToString
            ExceptionClass.ExceptionLogTest(eModel)
        End Try


    End Sub

End Class
